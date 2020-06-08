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
using DncZeus.Api.Entities.Man;
using DncZeus.Api.RequestPayload.Man.Workprocess;
using DncZeus.Api.ViewModels.Man.Workprocess;

namespace DncZeus.Api.Controllers.Api.Weixin
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [ApiController]
    [ApiVersion("8.0")] //V6
    [Route("api/v8/Man/[controller]/[action]")]
    public class WorkprocessController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        

        public WorkprocessController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            
        }
        [HttpPost]
        public IActionResult List(WorkprocessRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
               
                var query = _dbContext.ManWorkProcess.AsQueryable().GroupJoin(_dbContext.ManFactory, a => a.factoryGuid, b => b.Guid, (a, b) => new
                {
                    Workprocess = a,
                    Factory = b
                }).SelectMany(x => x.Factory, (a, b) => new
                {
                    a.Workprocess.Guid,
                    a.Workprocess.OrgID,
                    a.Workprocess.factoryGuid,
                    a.Workprocess.workCode,
                    a.Workprocess.workName,
                    a.Workprocess.rate,
                    a.Workprocess.isvalid,
                    a.Workprocess.credate,
                    a.Workprocess.creuser,
                    a.Workprocess.moddate,
                    a.Workprocess.moduser,
                    factoryName = b.name
                });
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x =>
                    (

                    x.workName.Contains(payload.Kw.Trim()) ||
                    x.workCode.Contains(payload.Kw.Trim())
                    )
                    );
                }
                if (payload.factoryGuid != null)
                {
                    query = query.Where(x => x.factoryGuid == payload.factoryGuid);
                }
                query = query.OrderBy("workCode", false);
                query=query.OrderBy("factoryGuid", false);
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
               
                
                var totalCount = query.Count();

                response.SetData(list, totalCount);
                return Ok(response);

            }

        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="model">类别视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(WorkprocessCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
          
            using (_dbContext)
            {
                /*var entity = _dbContext.ManWorkProcess.FirstOrDefault(x => x.code == model.code);
                if (entity != null)
                {
                    response.SetFailed("编号已存在");
                }
                else {*/
                    var ent = _mapper.Map<WorkprocessCreateViewModel, ManWorkProcess>(model);
                    ent.OrgID = AuthContextService.CurrentUser.OrgID;
                    ent.Guid = Guid.NewGuid();
                    ent.moddate = ent.credate = DateTime.Now;
                    ent.moduser = ent.creuser = AuthContextService.CurrentUser.DisplayName;

                    _dbContext.ManWorkProcess.Add(ent);
                    _dbContext.SaveChanges();
                    response.SetSuccess();
                /*}*/
                
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
                var entity = _dbContext.ManWorkProcess.FirstOrDefault(x => x.Guid == guid);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<ManWorkProcess, WorkprocessCreateViewModel>(entity));
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
        public IActionResult Edit(WorkprocessCreateViewModel model)
        {
           
           
            using (_dbContext)
            {

                
                var entity = _dbContext.ManWorkProcess.FirstOrDefault(x => x.Guid == model.Guid);

                entity.factoryGuid = model.factoryGuid;
                entity.workCode = model.workCode;
                entity.workName = model.workName;
                entity.rate = model.rate;
                entity.isvalid = model.isvalid;
                entity.moddate = DateTime.Now;
                entity.moduser = AuthContextService.CurrentUser.DisplayName;
                var response = ResponseModelFactory.CreateInstance;

                _dbContext.SaveChanges();
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
                    var sql = string.Format("DELETE FROM ManWorkProcess WHERE Guid IN ({0})", parameterNames);
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
                        sql = "UPDATE ManWorkProcess SET status=0, cfmuser='{1}', cfmdate=getdate() WHERE  Guid IN ({0})";
                        sql = string.Format(sql, parameterNames, AuthContextService.CurrentUser.DisplayName);
                    }
                    else
                    {
                        sql = "UPDATE ManWorkProcess SET status=1, cfmuser='{1}',cfmdate=getdate() WHERE  Guid IN ({0})";
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
                    response = UpdateIsValid(0, ids);
                    break;
                case "Valid": //有效
                    response = UpdateIsValid(1, ids);
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
        private ResponseModel UpdateIsValid(int isvalid, string ids)
        {
            using (_dbContext)
            {
                var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                var sql = string.Format("UPDATE ManWorkProcess SET isvalid=@isvalid WHERE Guid IN ({0})", parameterNames);

                parameters.Add(new SqlParameter("@isvalid", isvalid));
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
        
        

    }
}