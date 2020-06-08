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
using DncZeus.Api.RequestPayload.Pmc.Projectitem;
using DncZeus.Api.ViewModels.Pmc.Projectitem;
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
    public class ProjectitemController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        

        public ProjectitemController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            
        }
        [HttpPost]
        public IActionResult List(ProjectitemRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                //var query = _dbContext.PmcProjectitem.AsQueryable().Where(x=>x.itemType==payload.itemType && x.projectlineGuid IN (payload.lineGuid));
                 var strSql = @"SELECT
                            item.Guid,
                            item.projectlineGuid,
                            item.itemType,
                            item.unitCode,
                            item.itemcode,
                            item.itemname,
                            item.erpname,
                            item.qty,
                            item.reqDate,
                            item.reqqty,
                            item.purqty,
                            item.inqty,
                            item.status,
                            item.remark,
                            item.creuser,
                            item.credate,
                            item.moduser,
                            item.moddate
                            
                            FROM [dbo].[PmcProjectitem] AS item 
                            INNER JOIN [dbo].[PmcProjectline] AS line ON (line.Guid = item.projectlineGuid) 
                            INNER JOIN [dbo].[PmcProjects] AS pro ON (pro.Guid = line.projectsGuid)
                            WHERE pro.Guid IN ({0}) and item.itemType={1}";

                
                var query = _dbContext.PmcProjectitem.FromSql(strSql, payload.projectGuid,payload.itemType);
                query = query.Where(x => x.itemType == payload.itemType);
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                   /* query = query.Where(x =>
                    (
                    x.Code.Contains(payload.Kw.Trim()) ||
                    x.Name.Contains(payload.Kw.Trim())
                    )
                    );*/
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
        public IActionResult Create(ProjectitemCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
          
            using (_dbContext)
            {
               
                var entity = _mapper.Map<ProjectitemCreateViewModel, PmcProjectitem>(model);

                /*if (_dbContext.PmcProjectitem.Count(x => x.Code == model.Code) > 0)
                {
                    response.SetFailed("编号已存在");
                    return Ok(response);
                }*/
                entity.Guid = new Guid();
                entity.moddate = entity.credate = DateTime.Now;
                entity.moduser = entity.creuser = AuthContextService.CurrentUser.DisplayName;

                _dbContext.PmcProjectitem.Add(entity);
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
                var entity = _dbContext.PmcProjectitem.FirstOrDefault(x => x.Guid == guid);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<PmcProjectitem, ProjectitemCreateViewModel>(entity));
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
        public IActionResult Edit(ProjectitemCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
         
            using (_dbContext)
            {
                
                var entity = _dbContext.PmcProjectitem.FirstOrDefault(x => x.Guid == model.Guid);
            
                entity.projectlineGuid = model.projectlineGuid;
                entity.itemcode = model.itemcode;
                entity.itemname = model.itemname; 
                entity.erpname = model.erpname; 
                entity.qty = model.qty;
                entity.unitcode = model.unitcode;
                entity.reqDate = model.reqDate; 
                entity.reqqty = model.reqqty; 
                entity.purqty = model.purqty; 
                entity.inqty = model.inqty; 
                entity.status = model.status; 
                entity.remark = model.remark; 
               

                entity.moddate  = DateTime.Now;
                entity.moduser  = AuthContextService.CurrentUser.DisplayName;

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
                    var sql = string.Format("DELETE FROM PmcProjectitem WHERE Guid IN ({0})", parameterNames);
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
                        sql = "UPDATE PmcProjectitem SET status=2, cfmuser='{1}', cfmdate=getdate() WHERE  Guid IN ({0})";
                        sql = string.Format(sql, parameterNames, AuthContextService.CurrentUser.DisplayName);
                    }
                    else
                    {
                        sql = "UPDATE PmcProjectitem SET status=3, cfmuser='{1}',cfmdate=getdate() WHERE  Guid IN ({0})";
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
                var sql = string.Format("UPDATE PmcProjectitem SET enabled=@enabled WHERE Guid IN ({0})", parameterNames);

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

        /// <summary>
        /// 获取ERP物料列表
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetErpItemList(int curPage, string kw)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var strSql = @"select fGoodsCode,fGoodsName,fSizeDesc,fFgType from  SF.DCF19_MY.dbo.t_BOMM_GoodsMst t1";
                var query = _dbContext.SfItem.FromSql(strSql);
                if (!string.IsNullOrEmpty(kw))
                {
                    query = query.Where(x =>
                    (
                    x.fGoodsName.Contains(kw.Trim())
                    
                    )
                    );
                }
                var totalCount = query.Count();
                var list = query.Paged(curPage, 15).ToList();
                response.SetData(list, totalCount);
                return Ok(response);

            }
        }

        /// <summary>
        /// 获取数夫重点物料列表
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetProjectFocusItemList(int curPage, int pageSize, string proNo)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var strSql = @"select top 100 percent t1.fPrayNo,t2.fOrdNo,t2.fMoNo,t3.fGoodsCode,t3.fGoodsName,fPrayQty,t1.fcflag 
                                from SF.DCF19_MY.dbo.t_PURD_PrayMst t1
                                inner join SF.DCF19_MY.dbo.t_PURD_PrayItem t2 on t1.fPrayNo = t2.fPrayNo
                                inner join SF.DCF19_MY.dbo.t_BOMM_GoodsMst t3 on t2.fGoodsID = t3.fGoodsID
                                where t1._x_xmbh = 'P200331001'
                                order by t2.fSNo";
                var query = _dbContext.SfFocusItem.FromSql(strSql, proNo);
               
                var totalCount = query.Count();
                var list = query.Paged(curPage, pageSize).ToList();

                response.SetData(list, totalCount);
                return Ok(response);

            }
        }


    }
}