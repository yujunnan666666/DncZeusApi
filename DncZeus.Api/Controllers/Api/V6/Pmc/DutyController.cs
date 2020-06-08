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
using DncZeus.Api.RequestPayload.Pmc.Duty;
using DncZeus.Api.ViewModels.Pmc.Duty;
using DncZeus.Api.Entities.Pmc;
using System.Collections.Generic;

namespace DncZeus.Api.Controllers.Api.Pmc
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [ApiController]
    [ApiVersion("6.0")] //V6
    [Route("api/v6/Pmc/[controller]/[action]")]
    public class DutyController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        

        public DutyController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            
        }
        [HttpPost]
        public IActionResult List(DutyRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = _dbContext.PmcDuties.AsQueryable();
              

                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x =>
                    (
                    x.Code.Contains(payload.Kw.Trim()) ||
                    x.Name.Contains(payload.Kw.Trim())
                    )
                    );
                }
                //query=query.OrderBy("funorder", false);
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
               
                
                var totalCount = query.Count();

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
        public IActionResult Create(DutyCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
          
            using (_dbContext)
            {
                Guid guid;
                var entity = new PmcDuties();

                if (_dbContext.PmcDuties.Count(x => x.Code == model.Code) > 0)
                {
                    response.SetFailed("编号已存在");
                    return Ok(response);
                }
                guid = Guid.NewGuid();
                entity.Guid = guid;
                entity.OrgID = model.OrgID;
                entity.Code = model.Code;
                entity.Name = model.Name;
                entity.department = model.department;
                entity.iskeep = model.iskeep;
                entity.keepdate = model.keepdate;
                entity.dutiesGuid = model.dutiesGuid;
                entity.dodate = model.dodate;
                entity.itype = model.itype;
                entity.projectStatus = model.projectStatus;
                entity.status = model.status;
                entity.remark = model.remark;
                entity.projectStatus = model.projectStatus;
                entity.moddate = entity.credate = DateTime.Now;
                entity.moduser = entity.creuser = AuthContextService.CurrentUser.DisplayName;

                _dbContext.PmcDuties.Add(entity);
                _dbContext.SaveChanges();

                //插入次任务
                foreach (PmcDuties duty in model.secondDutys) {
                    var ent = new PmcDutiesecond();
                    ent.Guid = new Guid();
                    ent.dutiesGuid = guid;
                    ent.secondGuid = duty.Guid;
                    ent.credate = DateTime.Now;
                    ent.creuser = AuthContextService.CurrentUser.DisplayName;
                    _dbContext.PmcDutiesecond.Add(ent);
                    _dbContext.SaveChanges();
                }
               

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
                var entity = _dbContext.PmcDuties.FirstOrDefault(x => x.Guid == guid);
                var secondList= _dbContext.PmcDutiesecond.AsQueryable().Where(x => x.dutiesGuid==guid).ToList();
                List<PmcDuties> dutyList = new List<PmcDuties>();
                foreach (PmcDutiesecond duty in secondList) {
                    var dutyEnt = _dbContext.PmcDuties.FirstOrDefault(x => x.Guid == duty.secondGuid);
                    dutyList.Add(dutyEnt);
                } 
                var ent = new DutyCreateViewModel();
                ent.Guid = entity.Guid;
                ent.OrgID = entity.OrgID;
                ent.Code = entity.Code;
                ent.Name = entity.Name;
                ent.department = entity.department;
                ent.iskeep = entity.iskeep;
                ent.keepdate = entity.keepdate;
                ent.dutiesGuid = entity.dutiesGuid;
                ent.dodate = entity.dodate;
                ent.dotype = entity.dotype;
                ent.itype = entity.itype;
                ent.projectStatus = entity.projectStatus;
                ent.status = entity.status;
                ent.remark = entity.remark;
                ent.projectStatus = entity.projectStatus;
                ent.secondDutys = dutyList;
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(ent);
                return Ok(response);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">图标视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(DutyCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
           
            using (_dbContext)
            {
                
                var entity = _dbContext.PmcDuties.FirstOrDefault(x => x.Guid == model.Guid);
            
                entity.Code = model.Code;
                entity.Name = model.Name;
                entity.department = model.department;
                entity.iskeep = model.iskeep; 
                entity.keepdate = model.keepdate; 
                entity.dutiesGuid = model.dutiesGuid;
                entity.itype = model.itype;
                entity.dotype = model.dotype;
                entity.dodate = model.dodate; 
                entity.status = model.status; 
                entity.remark = model.remark;
                entity.projectStatus = model.projectStatus;
                entity.moddate  = DateTime.Now;
                entity.moduser  = AuthContextService.CurrentUser.DisplayName;

                //删除原来的次任务
                var oldSecondQuery= _dbContext.PmcDutiesecond.AsQueryable().Where(x => x.dutiesGuid == model.Guid);
                _dbContext.PmcDutiesecond.RemoveRange(oldSecondQuery);
                _dbContext.SaveChanges();

                //插入次任务
                foreach (PmcDuties duty in model.secondDutys)
                {
                    var ent = new PmcDutiesecond();
                    ent.Guid = new Guid();
                    ent.dutiesGuid = model.Guid;
                    ent.secondGuid = duty.Guid;
                    ent.credate = DateTime.Now;
                    ent.creuser = AuthContextService.CurrentUser.DisplayName;
                    _dbContext.PmcDutiesecond.Add(ent);
                    _dbContext.SaveChanges();
                }

                response.SetSuccess();
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
                    var sql = string.Format("DELETE FROM PmcDuties WHERE Guid IN ({0})", parameterNames);
                    int li_ret = _dbContext.Database.ExecuteSqlCommand(sql, parameters);

                    
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
        /// 审核/弃审
        /// </summary>
        /// <param name="status"></param>
        /// <param name="ids">ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel UpdateStatus(Status status, string ids)
        {
            using (_dbContext)
            {
                var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                var sql = "";
               
                    if (status == Status.Cfm)
                    {
                        sql = "UPDATE PmcDuties SET status=2, cfmuser='{1}', cfmdate=getdate() WHERE  Guid IN ({0})";
                        sql = string.Format(sql, parameterNames, AuthContextService.CurrentUser.DisplayName);
                    }
                    else
                    {
                        sql = "UPDATE PmcDuties SET status=3, cfmuser='{1}',cfmdate=getdate() WHERE  Guid IN ({0})";
                        sql = string.Format(sql, parameterNames, AuthContextService.CurrentUser.DisplayName);
                    }
                

                int li_ret = _dbContext.Database.ExecuteSqlCommand(sql, parameters);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(new
                {
                    affectCount = li_ret
                });
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
                case "Cfm": //审核
                    response = UpdateStatus(Status.Cfm, ids);
                    break;
                case "Open": //弃审
                    response = UpdateStatus(Status.Open, ids);
                    break;
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
                var sql = string.Format("UPDATE PmcDuties SET enabled=@enabled WHERE Guid IN ({0})", parameterNames);

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
        //获取父级任务
        [HttpGet]
        public IActionResult GetParentDuty(Guid? guid)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var list = new List<PmcDuties>();
                if (guid.ToString() != "")
                {
                    list = _dbContext.PmcDuties.AsQueryable().Where(x => x.Guid != guid).ToList();
                    response.SetData(list);
                }
                else {
                    list = _dbContext.PmcDuties.AsQueryable().ToList();
                    response.SetData(list);
                }
                
                return Ok(response);

            }

        }
        //获取次级任务
        [HttpGet]
        public IActionResult GetSecondDuty(Guid? guid)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var list = new List<PmcDutiesecond>();
                if (guid.ToString() != "")
                {
                    list = _dbContext.PmcDutiesecond.AsQueryable().Where(x => x.dutiesGuid == guid).ToList();
                    response.SetData(list);
                }
                else
                {
                    list = _dbContext.PmcDutiesecond.AsQueryable().ToList();
                    response.SetData(list);
                }

                return Ok(response);

            }

        }

    }
}