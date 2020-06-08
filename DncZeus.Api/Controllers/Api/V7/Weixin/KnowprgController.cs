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


using DncZeus.Api.Entities.Weixin;

using DncZeus.Api.ViewModels.Weixin.Cases;
using DncZeus.Api.ViewModels.Weixin.Know;
using DncZeus.Api.RequestPayload.Weixin.Know;

namespace DncZeus.Api.Controllers.Api.Weixin
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [ApiController]
    [ApiVersion("7.0")] //V6
    [Route("api/v7/Weixin/[controller]/[action]")]
    public class KnowprgController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        

        public KnowprgController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            
        }
        [HttpPost]
        public IActionResult List(KnowprgRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {

                //用户
                var query1 = _dbContext.WeiXinKnowledgePower.AsQueryable().Where(x=>x.usetype=="1").GroupJoin(_dbContext.WeiXinKnowledge, a => a.knowledgeGuid, b => b.Guid, (a, b) => new
                {
                    Power = a,
                    Know = b
                }).SelectMany(x => x.Know, (a, b) => new
                {
                    a.Power,
                    b.Catalog,
                    b.CaseName
                }).GroupJoin(_dbContext.Secuser, a => a.Power.userno, b => b.userno, (a, b) => new
                {
                    a.Power,
                    a.Catalog,
                    a.CaseName,
                    User=b
                }).SelectMany(x => x.User, (a, b) => new
                {
                    a.CaseName,
                    a.Catalog,
                    a.Power.userno,
                    a.Power.usetype,
                    a.Power.Guid,
                    a.Power.candown,
                    a.Power.credate,
                    a.Power.creuser,
                    a.Power.knowledgeGuid,
                    b.username
                });
                //角色
                var query2 = _dbContext.WeiXinKnowledgePower.AsQueryable().Where(x => x.usetype == "2").GroupJoin(_dbContext.WeiXinKnowledge, a => a.knowledgeGuid, b => b.Guid, (a, b) => new
                {
                    Power = a,
                    Know = b
                }).SelectMany(x => x.Know, (a, b) => new
                {
                    a.Power,
                    b.Catalog,
                    b.CaseName
                }).GroupJoin(_dbContext.Secgroup, a => a.Power.userno, b => b.groupno, (a, b) => new
                {
                    a.Power,
                    a.Catalog,
                    a.CaseName,
                    User = b
                }).SelectMany(x => x.User, (a, b) => new
                {
                    a.CaseName,
                    a.Catalog,
                    a.Power.userno,
                    a.Power.usetype,
                    a.Power.Guid,
                    a.Power.candown,
                    a.Power.credate,
                    a.Power.creuser,
                    a.Power.knowledgeGuid,
                    username =b.groupname 
                });
                query1 = query1.Union(query2);

                var list = query1.Paged(payload.CurrentPage, payload.PageSize).ToList();
               
                
                var totalCount = query1.Count();

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
        public IActionResult Create(KnowprgCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
          
            using (_dbContext)
            {
                var entity = _mapper.Map<KnowprgCreateViewModel, WeiXinKnowledgePower>(model);
                
                entity.Guid = Guid.NewGuid();
                entity.credate = DateTime.Now;
                entity.creuser = AuthContextService.CurrentUser.DisplayName;


                _dbContext.WeiXinKnowledgePower.Add(entity);
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
                var entity = _dbContext.WeiXinKnowledgePower.FirstOrDefault(x => x.Guid == guid);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<WeiXinKnowledgePower, KnowprgCreateViewModel>(entity));
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
        public IActionResult Edit(KnowprgCreateViewModel model)
        {
           
           
            using (_dbContext)
            {

                
                var entity = _dbContext.WeiXinKnowledgePower.FirstOrDefault(x => x.Guid == model.Guid);

                entity.knowledgeGuid = model.knowledgeGuid;
                entity.usetype = model.usetype;
                entity.userno = model.userno;
                entity.candown = model.candown;
                
                var response = ResponseModelFactory.CreateInstance;

                _dbContext.SaveChanges();
                response.SetSuccess();
                return Ok(response);
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
               /* case "Cfm": //审核
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
                    break;*/
                default:
                    break;
            }
            return Ok(response);
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
                    var sql = string.Format("DELETE FROM WeiXinKnowledgePower WHERE Guid IN ({0})", parameterNames);
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
        /// 监测是否可下载
        /// </summary>
        /// 
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult CanDownload(string userno,Guid? knowledgeGuid)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {
                var strSql = @"
                            SELECT 
		                    knowprg.candown as val
		                    FROM [dbo].[WeiXinKnowledgePower] AS knowprg
		                    INNER JOIN Secuser AS u ON u.userno= knowprg.userno
		                    where knowprg.usetype=1 and knowprg.userno={1} and knowprg.knowledgeGuid in({0})
	                        UNION
	                        (
		                        SELECT 
		                        knowprg.candown as val
		                        FROM [dbo].[WeiXinKnowledgePower] AS knowprg
		                        inner JOIN Secgroup AS g ON g.groupno= knowprg.userno
		                        left join Secguser as gu ON gu.groupid=g.Guid
		                        inner join secuser as u ON u.Guid=gu.userid
		                        where knowprg.usetype=2 and  u.userno={1} and knowprg.knowledgeGuid in({0}) 
	                        )";


                var query = _dbContext.retValue.FromSql(strSql,knowledgeGuid, userno);
                var list = query.ToList();
                //可下载
                if (list.Count > 1)
                {
                    response.SetData("1");
                }
                else if (list.Count == 1) {
                    if (list[0].val == "0")
                    {
                        response.SetData("0");
                    }else
                    {
                        response.SetData("1");
                    }
                }
                else
                {
                    response.SetData("1");
                }
                
                response.SetSuccess();
                return Ok(response);
            }
        }


    }
}