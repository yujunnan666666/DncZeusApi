using AutoMapper;
using DncZeus.Api.Entities;
using DncZeus.Api.Entities.SF;
using DncZeus.Api.Entities.Enums;
using DncZeus.Api.Extensions;
using DncZeus.Api.Extensions.AuthContext;
using DncZeus.Api.Extensions.CustomException;
using DncZeus.Api.Extensions.DataAccess;
using DncZeus.Api.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using OfficeOpenXml;
using DncZeus.Api.Entities.QueryModels;
using DncZeus.Api.Entities.QueryModels.SF;
using DncZeus.Api.Entities.QueryModels.Tmp;
using DncZeus.Api.RequestPayload.Sec.Org;
using DncZeus.Api.ViewModels.Sec.Org;
using DncZeus.Api.Entities.Sec;
using static DncZeus.Api.Entities.Tmp.tmpEnum;
using DncZeus.Api.RequestPayload.Sec.Prg;
using DncZeus.Api.ViewModels.Sec.Prg;
using Newtonsoft.Json.Linq;
using DncZeus.Api.RequestPayload.Sec.Group;
using System.Collections.Generic;
using DncZeus.Api.ViewModels.Sec.Group;

namespace DncZeus.Api.Controllers.Api.Sec
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [ApiController]
    [ApiVersion("5.0")] //V5
    [Route("api/v5/Sec/[controller]/[action]")]
    public class GroupController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        

        public GroupController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            
        }
        [HttpPost]
        public IActionResult List(GroupRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = _dbContext.Secgroup.AsQueryable();
               

                //query = query.Where(x => (x.enabled == payload.enabled));

                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x =>
                    (
                    x.groupno.Contains(payload.Kw.Trim()) ||
                    x.groupname.Contains(payload.Kw.Trim())
                    )
                    );
                }
                //query=query.OrderBy("prgorder", false);
                // query = query.OrderBy("trseq", false);
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
               
                
                var totalCount = query.Count();
                //var data = list.Select(_mapper.Map<MisCatalog, CategoryJsonModel>);

                response.SetData(list, totalCount);
                return Ok(response);

            }
        }

        /// <summary>
        /// 创建类别
        /// </summary>
        /// <param name="model">类别视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(GroupCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
          
            using (_dbContext)
            {
               
                var entity = _mapper.Map<GroupCreateViewModel, Secgroup>(model);
                if (_dbContext.Secgroup.Count(x => x.groupno == model.groupno) > 0)
                {
                    response.SetFailed("群组编号已存在");
                    return Ok(response);
                }

                entity.Guid = new Guid();
                
                _dbContext.Secgroup.Add(entity);
                _dbContext.SaveChanges();

                response.SetSuccess();
                return Ok(response);
            }
        }

        /// <summary>
        /// 类别详情
        /// </summary>
        /// <param name="id">类别ID</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Detail(Guid guid)
        {
            using (_dbContext)
            {
                var entity = _dbContext.Secgroup.FirstOrDefault(x => x.Guid == guid);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<Secgroup, GroupCreateViewModel>(entity));
                return Ok(response);
            }
        }

        /// <summary>
        /// 修改类别
        /// </summary>
        /// <param name="model">图标视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(GroupCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
           
            using (_dbContext)
            {
               
                var entity = _dbContext.Secgroup.FirstOrDefault(x => x.Guid == model.Guid);
                string old_groupno = entity.groupno;

                entity.groupno = model.groupno;
                entity.gtype = model.gtype;
                entity.groupname = model.groupname;
                entity.remark = model.remark;
                entity.enabled = model.enabled;



                _dbContext.SaveChanges();
                response.SetSuccess();

                /*_dbContext.Database.ExecuteSqlCommand("UPDATE Secguser set groupno={0} WHERE groupno={1}", model.groupno, old_groupno);
                _dbContext.Database.ExecuteSqlCommand("UPDATE Secgprg set groupno={0} WHERE groupno={1}", model.groupno, old_groupno);*/

                return Ok(response);
            }
        }

        /// <summary>
        /// 删除(开立状态)
        /// </summary>
        /// <param name="ids">ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel Delete(string ids)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateInstance;
                try {
                    var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                    var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                    var sql = string.Format("DELETE FROM Secgroup WHERE Guid IN ({0})", parameterNames);
                    int li_ret = _dbContext.Database.ExecuteSqlCommand(sql, parameters);

                    var sql1 = string.Format("DELETE FROM Secgprg WHERE  groupid IN ({0})", parameterNames);
                    var sql3 = string.Format("DELETE FROM Secguser WHERE  userid IN ({0})", parameterNames);
                    //关联删除
                    _dbContext.Database.ExecuteSqlCommand(sql1, parameters);
                    _dbContext.Database.ExecuteSqlCommand(sql3, parameters);

                    response.SetData(new
                    {
                        affectCount = li_ret
                    });
                    
                }
                catch (Exception e)
                {
                    
                    response.SetFailed("删除失败:当前选项已被使用，无法删除！");
                    
                }
                return response;


            }
        }

        /// <summary>
        /// 批量操作
        /// </summary>
        /// <param name="command"></param>
        /// <param name="ids">用户ID,多个以逗号分隔</param>
        /// <param name="printType">打印时传入参数：打印类型</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Batch(string command, string ids)
        {
            var response = ResponseModelFactory.CreateInstance;
            switch (command)
            {
                case "delete": //删除
                    response = Delete(ids);
                    break;
                /*case "Cfm": //审核
                    response = UpdateStatus(Status.Cfm, ids);
                    break;
                case "Open": //弃审
                    response = UpdateStatus(Status.Open, ids);
                    break;*/
                case "Invalid": //失效
                    response = UpdateIsValid("N", ids);
                    break;
                case "Valid": //有效
                    response = UpdateIsValid("Y", ids);
                    break;
                default:
                    break;
            }
            return Ok(response);
        }

        /// <summary>
        /// 有效失效
        /// </summary>
        /// <param name="isValid"></param>
        /// <param name="ids">条码ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel UpdateIsValid(string enabled, string ids)
        {
            using (_dbContext)
            {
                var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                var sql = string.Format("UPDATE Secgroup SET enabled=@enabled WHERE Guid IN ({0})", parameterNames);

                parameters.Add(new SqlParameter("@enabled", enabled));
                //parameters.Add(new SqlParameter("@valuser", AuthContextService.CurrentUser.DisplayName));
                int li_ret = _dbContext.Database.ExecuteSqlCommand(sql, parameters);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(new
                {
                    affectCount = li_ret
                });
                return response;
            }
        }

        //群组编号查找列表
        [HttpGet]
        public IActionResult GetUserListByRole(Guid groupid)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var groupQuery = _dbContext.Secguser.AsQueryable();
                var userQuery = _dbContext.Secuser.AsQueryable();

                var groutList = groupQuery.Where(x => (x.groupid == groupid)).ToList();
                var userList = new List<Secuser>();
                foreach (Secguser obj in groutList) {
                    var entity=_dbContext.Secuser.FirstOrDefault(x => x.Guid == obj.userid);
                    userList.Add(entity);
                }
                

                response.SetData(userList);
                return Ok(response);
            }
        }

        //群组下未绑定角色用户列表
        [HttpGet]
        public IActionResult GetUnbindRoleList(Guid groupid)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = _dbContext.Secguser.AsQueryable();
                var list=query.Where(x => x.groupid == groupid).ToList();

                var userList= _dbContext.Secuser.AsQueryable();
                foreach (Secguser obj in list) {
                    var binduserid = obj.userid;
                   userList=userList.Where(x =>x.Guid != binduserid);
                }

                response.SetData(userList.ToList());
                return Ok(response);
            }
        }

        /// <summary>
        /// 创建程式按钮
        /// </summary>
        /// <param name="model">类别视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult CreateRoleUser(GroupUserCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {
                var entity = new Secguser();
                entity.groupid = model.groupid;
                for (int i = 0; i < model.users.Length; i++)
                {
                    entity.Guid = new Guid();
                    entity.userid = model.users[i].Guid;

                    _dbContext.Secguser.Add(entity);
                    _dbContext.SaveChanges();
                }
                response.SetSuccess();
                return Ok(response);
            }
        }
        /// <summary>
        /// 删除群组用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult RemoveUserInGroup(GroupUserCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {
                var entity = _dbContext.Secguser.FirstOrDefault(x => x.userid == model.userid && x.groupid == model.groupid);
                _dbContext.Secguser.Remove(entity);
                _dbContext.SaveChanges();
                response.SetSuccess();
                return Ok(response);
            }
        }


    }
}