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
using DncZeus.Api.RequestPayload.Sec.User;
using DncZeus.Api.ViewModels.Sec.User;
using System.Collections.Generic;
using DncZeus.Api.ViewModels.Sec.Group;
using File = DncZeus.Api.Entities.File;

namespace DncZeus.Api.Controllers.Api.Sec
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [ApiController]
    [ApiVersion("5.0")] //V5
    [Route("api/v5/Sec/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        private IHostingEnvironment _host;

        public UserController(DncZeusDbContext dbContext, IMapper mapper, IHostingEnvironment host)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _host = host;
        }
        [HttpPost]
        public IActionResult List(UserRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = _dbContext.Secuser.AsQueryable();
                /*if (!string.IsNullOrEmpty(payload.Code))
                {
                    query = query.Where(x => x.Code.Contains(payload.Code.Trim()));
                }
                if (!string.IsNullOrEmpty(payload.Name))
                {
                    query = query.Where(x => x.Name.Contains(payload.Name.Trim()));
                }*/

                //query = query.Where(x => (x.enabled == payload.enabled));

                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x =>
                    (
                    x.userno.Contains(payload.Kw.Trim()) ||
                    x.username.Contains(payload.Kw.Trim())
                    )
                    );
                }
               // query=query.OrderBy("userorder", false);
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
        public IActionResult Create(UserCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
          
            using (_dbContext)
            {
               
                var entity = _mapper.Map<UserCreateViewModel, Secuser>(model);

                if (_dbContext.Secuser.Count(x => x.userno == model.userno) > 0)
                {
                    response.SetFailed("账户编号已存在");
                    return Ok(response);
                }
                if (model.wxAccount.Trim() != "") {
                    if (_dbContext.Secuser.Count(x => x.wxAccount == model.wxAccount) > 0)
                    {
                        response.SetFailed("微信账号已存在");
                        return Ok(response);
                    }
                }
               

                entity.Guid = new Guid();
                entity.moddate = entity.credate = DateTime.Now;
                entity.moduser = entity.creuser = AuthContextService.CurrentUser.DisplayName;

                _dbContext.Secuser.Add(entity);
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
                var entity = _dbContext.Secuser.FirstOrDefault(x => x.Guid == guid);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<Secuser, UserCreateViewModel>(entity));
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
        public IActionResult Edit(UserCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
           /* if (model.Code.Trim().Length <= 0)
            {
                response.SetFailed("请输入名称");
                return Ok(response);
            }*/
            using (_dbContext)
            {
                
                var entity = _dbContext.Secuser.FirstOrDefault(x => x.Guid == model.Guid);
                string old_userno = entity.userno;

                entity.code = model.code;
                entity.userno = model.userno;
                entity.username = model.username;
                entity.utype = model.utype;
                entity.enabled = model.enabled; 
                entity.pwd = model.pwd; 
                entity.telno = model.telno; 
                entity.email = model.email; 
                entity.EmpNo = model.EmpNo; 
                entity.EmpName = model.EmpName; 
                entity.depno = model.depno; 
                entity.depname = model.depname; 
                entity.jobno = model.jobno; 
                entity.jobname = model.jobname; 
                entity.userLogo = model.userLogo; 

                entity.wxId = model.wxId; 
                entity.wxAccount = model.wxAccount; 
                entity.wxName = model.wxName; 
                entity.wxPwd = model.wxPwd; 
                entity.wxPhone = model.wxPhone; 
                entity.wxMail = model.wxMail;
                entity.moduser = AuthContextService.CurrentUser.DisplayName;
                entity.moddate = DateTime.Now;

                var query = _dbContext.Secuser.AsQueryable().Where(x => x.Guid != model.Guid &&x.wxAccount == model.wxAccount);
                if (query.Count() > 0)
                {
                    response.SetFailed("微信账号已存在");
                    return Ok(response);
                }
                else {
                    _dbContext.SaveChanges(); 
                    response.SetSuccess();
                    return Ok(response);
                }

                
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
                    /*var idsArr = ids.Split(",");*/
                    var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                    var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                    var sql = string.Format("DELETE FROM Secuser WHERE Guid IN ({0})", parameterNames);
                    int li_ret = _dbContext.Database.ExecuteSqlCommand(sql, parameters);
                   
                    var sql1= string.Format("DELETE FROM Secuserorg WHERE  userid IN ({0})", parameterNames);
                    var sql2= string.Format("DELETE FROM Secuprg WHERE  userid IN ({0})", parameterNames);
                    var sql3= string.Format("DELETE FROM Secguser WHERE  userid IN ({0})", parameterNames);
                    //关联删除
                    _dbContext.Database.ExecuteSqlCommand(sql1, parameters);
                    _dbContext.Database.ExecuteSqlCommand(sql2, parameters);
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
                var sql = string.Format("UPDATE Secuser SET enabled=@enabled WHERE Guid IN ({0})", parameterNames);

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

        //根据用户编号获取对应绑定组织编号
        [HttpGet]
        public IActionResult GetOrgByUser(Guid userid)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var entity = _dbContext.Secuserorg.FirstOrDefault(x => x.userid == userid);
                var list = new List<MisOrganization>();
                var totalCount = 0;
                if (entity != null) {
                    var orgQuery = _dbContext.MisOrganization.AsQueryable();

                    orgQuery = orgQuery.Where(x => (x.ID == entity.orgid));


                    list = orgQuery.Paged(1, 20).ToList();
                    totalCount = orgQuery.Count();

                }

                response.SetData(list,totalCount);
                return Ok(response);
            }
        }

        //用户绑定组织操作
        [HttpPost]
        public IActionResult UserBindOrg(Secuserorg paramObj)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {   
                var query = _dbContext.Secuserorg.AsQueryable();
                var UserOrgEntity = _dbContext.Secuserorg.FirstOrDefault(x => x.userid == paramObj.userid);
                if (UserOrgEntity != null)
                {
                    //已绑定
                    response.SetFailed("已绑定组织");
                    return Ok(response);
                }
                else {
                    //未绑定
                    var entity = new Secuserorg();
                    entity.Guid = new Guid();
                    entity.userid = paramObj.userid;
                    entity.orgid = paramObj.orgid;

                    _dbContext.Secuserorg.Add(entity);
                    _dbContext.SaveChanges();

                    response.SetSuccess();
                    return Ok(response);
                }
                
               
            }
        }

        //用户解绑组织操作
        [HttpPost]
        public IActionResult UserUnBindOrg(Secuserorg paramObj)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = _dbContext.Secuserorg.AsQueryable();
                var UserOrgEntity = _dbContext.Secuserorg.FirstOrDefault(x => x.userid == paramObj.userid);
                if (UserOrgEntity != null)
                {
                    _dbContext.Secuserorg.Remove(UserOrgEntity);
                    _dbContext.SaveChanges();
                    response.SetSuccess("操作成功");
                    return Ok(response);
                }
                else
                {
                    //未绑定
                    response.SetFailed("绑定不存在");
                    return Ok(response);
                }


            }
        }

        //根据用户编号获取对应绑定群组编号
        [HttpGet]
        public IActionResult GetGroupByUser(Guid userid)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var entitys = _dbContext.Secguser.AsQueryable().Where(x => x.userid == userid).ToList();
                var list = new List<Secgroup>();
                var totalCount = 0;
                foreach (Secguser obj in entitys) {
                    
                    var groupQuery = _dbContext.Secgroup.FirstOrDefault(x => x.Guid == obj.groupid);
                    list.Add(groupQuery);
                } 

                response.SetData(list);
                return Ok(response);
            }
        }

        //用户下未绑定角色用户列表
        [HttpGet]
        public IActionResult GetUnbindInUserList(Guid userid)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = _dbContext.Secguser.AsQueryable();
                var list = query.Where(x => x.Guid == userid).ToList();

                var groupList = _dbContext.Secgroup.AsQueryable();
                foreach (Secguser obj in list)
                {
                    var bindgroupid = obj.groupid;
                    groupList = groupList.Where(x => x.Guid != bindgroupid);
                }

                response.SetData(groupList.ToList());
                return Ok(response);
            }
        }

        
        /// <summary>
        /// 创建用户角色
        /// </summary>
        /// <param name="model">类别视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult CreateUserGroup(UserGroupCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {
                var entity = new Secguser();
                entity.userid = model.user.Guid;
                for (int i = 0; i < model.groups.Length; i++)
                {

                    entity.Guid = new Guid();
                    entity.groupid = model.groups[i].Guid;

                    _dbContext.Secguser.Add(entity);
                    _dbContext.SaveChanges();
                }
                response.SetSuccess();
                return Ok(response);
            }
        }

        /// <summary>
        /// 删除用户群组
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult UserUnBindGroup(GroupUserCreateViewModel model)
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

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult UploadImg()
        {
            var response = ResponseModelFactory.CreateInstance;

            var oFile = Request.Form.Files["file"];

                 var fileName = DateTime.Now.ToString("yyyymmddhhMMssss") + ".png";
                var path = Path.Combine(_host.ContentRootPath + "\\Upload\\img");
                 var allPath = Path.Combine(path, fileName);
                 using (var stream = System.IO.File.Create(allPath))
                 {
                      oFile.CopyTo(stream);
                     stream.Close();
                     stream.Dispose();
                 }
            response.SetData("/Upload/img/" + fileName);

            return Ok(response);
        }

        public class uploadResult
        {
            public string fileName { get; set; }
            public string error { get; set; }
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult UpdatePassword(PasswordViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                
                var entity = _dbContext.Secuser.FirstOrDefault(x => x.Guid == model.userId && x.pwd==model.oldpwd);
                if (entity!=null) {
                    entity.pwd = model.newpwd;
                    _dbContext.SaveChanges();
                    response.SetSuccess();
                }
                else {
                    response.SetFailed("原密码错误");
                }

                return Ok(response);
            }
        }

    }
}