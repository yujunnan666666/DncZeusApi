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
using DncZeus.Api.ViewModels.Weixin.Know;

using DncZeus.Api.Entities.Weixin;
using DncZeus.Api.RequestPayload.Weixin.Know;
using DncZeus.Api.ViewModels.Weixin.Cases;

namespace DncZeus.Api.Controllers.Api.Weixin
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [ApiController]
    [ApiVersion("7.0")] //V6
    [Route("api/v7/Weixin/[controller]/[action]")]
    public class KnowController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        

        public KnowController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            
        }
        [HttpPost]
        public IActionResult List(KnowRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = _dbContext.WeiXinKnowledge.AsQueryable();

                if (payload.catalog!="") {
                    query = query.Where(x => x.Catalog == payload.catalog);
                }
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x =>
                    (
                    
                    x.CaseName.Contains(payload.Kw.Trim())
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
        /// 创建
        /// </summary>
        /// <param name="model">类别视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(KnowCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
          
            using (_dbContext)
            {
                var entity = _mapper.Map<KnowCreateViewModel,WeiXinKnowledge>(model);
                entity.OrgID = AuthContextService.CurrentUser.OrgID;
                entity.Guid = Guid.NewGuid();

                entity.moddate = entity.credate = DateTime.Now;
                entity.moduser = entity.creuser = AuthContextService.CurrentUser.DisplayName;

                _dbContext.WeiXinKnowledge.Add(entity);
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
                var entity = _dbContext.WeiXinKnowledge.FirstOrDefault(x => x.Guid == guid);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<WeiXinKnowledge, KnowCreateViewModel>(entity));
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
        public IActionResult Edit(KnowCreateViewModel model)
        {
           
           
            using (_dbContext)
            {

                
                var entity = _dbContext.WeiXinKnowledge.FirstOrDefault(x => x.Guid == model.Guid);

                entity.CaseName = model.CaseName;
                entity.CaseSketch = model.CaseSketch;
                entity.Catalog = model.Catalog;
                entity.pathUrl = model.pathUrl;
                entity.thumbUrl = model.thumbUrl;
                entity.status = model.status;
                entity.HtmlContent = model.HtmlContent;
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
                    var sql = string.Format("DELETE FROM WeiXinKnowledge WHERE Guid IN ({0})", parameterNames);
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
                        sql = "UPDATE WeiXinKnowledge SET status=0, cfmuser='{1}', cfmdate=getdate() WHERE  Guid IN ({0})";
                        sql = string.Format(sql, parameterNames, AuthContextService.CurrentUser.DisplayName);
                    }
                    else
                    {
                        sql = "UPDATE WeiXinKnowledge SET status=1, cfmuser='{1}',cfmdate=getdate() WHERE  Guid IN ({0})";
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
                var sql = string.Format("UPDATE WeiXinKnowledge SET enabled=@enabled WHERE Guid IN ({0})", parameterNames);

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

        [HttpPost]
        public IActionResult ListByAuth(KnowRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var strSql = @"
                            select * from WeiXinKnowledge where Guid in(
	                        SELECT 
		                        know.Guid
		                        FROM [dbo].[WeiXinKnowledgePower] AS knowprg
		                        INNER JOIN Secuser AS u ON u.userno= knowprg.userno
		                        inner join WeiXinKnowledge AS know ON know.Guid =knowprg.knowledgeGuid
		                        where knowprg.usetype=1 and knowprg.userno={0} and knowprg.candown=1
								UNION
								(
									SELECT 
									know.Guid
									FROM [dbo].[WeiXinKnowledgePower] AS knowprg
									inner JOIN Secgroup AS g ON g.groupno= knowprg.userno
									left join Secguser as gu ON gu.groupid=g.Guid
									inner join secuser as u ON u.Guid=gu.userid
									inner join WeiXinKnowledge AS know ON know.Guid =knowprg.knowledgeGuid
									where knowprg.usetype=2 and  u.userno={0} and knowprg.candown=1
								)
                                )";
                IQueryable<WeiXinKnowledge> query; 
                if (payload.userno == "admin")
                {
                    query = _dbContext.WeiXinKnowledge.AsQueryable();
                }
                else {
                    query= _dbContext.WeiXinKnowledge.FromSql(strSql, payload.userno);
                }

                if (payload.catalog != "")
                {
                    query = query.Where(x => x.Catalog == payload.catalog);
                }
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.CaseName.Contains(payload.Kw.Trim()));


                }
                
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();


                var totalCount = query.Count();

                response.SetData(list, totalCount);
                return Ok(response);

            }

        }

    }
}