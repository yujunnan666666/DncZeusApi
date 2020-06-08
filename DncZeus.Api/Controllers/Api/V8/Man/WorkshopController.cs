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

using DncZeus.Api.RequestPayload.Man.OutSplit;
using DncZeus.Api.ViewModels.Man.OutSplit;
using DncZeus.Api.RequestPayload.Man.Workshop;
using DncZeus.Api.ViewModels.Man.Workshop;

namespace DncZeus.Api.Controllers.Api.Weixin
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [ApiController]
    [ApiVersion("8.0")] //V6
    [Route("api/v8/Man/[controller]/[action]")]
    public class WorkshopController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        

        public WorkshopController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            
        }
        [HttpPost]
        public IActionResult List(WorkshopRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {

               
                var strSql = @"select b.Guid,b.factoryGuid,b.countType,b.ordCode,b.itemCode,b.itemName,b.factoryPrice,b.ordQty,b.creuser,b.credate,line.remark,line.workDate,isnull(line.qty,0) as qty,line.Guid as lineGuid,
                                (select isnull(sum(bb.qty),0) as sumqty from ManWorkshopOut as aa
                                    inner join ManWorkshopOutLine as bb on(bb.workshopOutGuid=aa.Guid)
                                where aa.factoryGuid={0} and aa.workProcessGuid={1} and bb.workDate<{2} and aa.ordCode=b.ordCode and aa.itemCode=b.itemCode) as sumqty,isnull(d.rate,0) as 'rate'
                                from ManWorkshopOut as b
                                inner join (select * from ManWorkshopOutLine where  credate in (select max(credate) from [dbo].[ManWorkshopOutLine] as a where a.workDate={2} group by a.workshopOutGuid)) AS line ON (line.workshopOutGuid = b.Guid)
                                left join ManOutSplit as c on(c.factoryGuid=b.factoryGuid and c.ordCode=b.ordCode and c.itemCode=b.itemCode)
left join ManOutSplitLine as d on(d.workProcessGuid=c.Guid and d.ProcessGuid={1})
where  b.workProcessGuid={1} and b.countType={3}";

                var query = _dbContext.WorkshopList.FromSql(strSql, payload.factoryGuid,payload.workProcessGuid,payload.workDate,payload.countType);

                //query = query.Where(x => x.workDate == payload.workDate);
                
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x =>
                    (
                    
                    x.ordCode.Contains(payload.Kw.Trim()) ||
                    x.itemCode.Contains(payload.Kw.Trim()) ||
                    x.itemName.Contains(payload.Kw.Trim())
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
        public IActionResult Create(WorkshopCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
          
            using (_dbContext)
            {
                //var enti = _dbContext.ManWorkshopOut.FirstOrDefault(x => x.factoryGuid == model.factoryGuid && x.workProcessGuid == model.workProcessGuid);
                Guid workshopGuid;
               
                    //新增
                    var factory = _dbContext.ManFactory.FirstOrDefault(x => x.Guid == model.factoryGuid);
                    var countType = factory.countType;
                    var ent = new ManWorkshopOut();
                    workshopGuid = Guid.NewGuid();
                    ent.Guid = workshopGuid;
                    ent.OrgID = AuthContextService.CurrentUser.OrgID;
                    ent.factoryGuid = model.factoryGuid;
                    ent.workProcessGuid = model.workProcessGuid;
                    ent.countType = countType;
                    ent.ordCode = model.ordCode;
                    ent.itemCode = model.itemCode;
                    ent.itemName = model.itemName;
                    ent.factoryPrice = model.factoryPrice;
                    ent.ordQty = model.ordQty;
                    ent.sumQty = model.sumQty;
                    ent.credate = DateTime.Now;
                    ent.creuser = AuthContextService.CurrentUser.DisplayName;
                    _dbContext.ManWorkshopOut.Add(ent);
               
                
                    //插入关系到子表
                    var ent2 = new ManWorkshopOutLine();
                    ent2.Guid = Guid.NewGuid();
                    ent2.workshopOutGuid = workshopGuid;
                    ent2.workDate = model.workDate;
                    ent2.qty = model.qty;
                    ent2.output = model.output;
                    ent2.remark = model.remark;
                    ent2.credate = DateTime.Now;
                    ent2.creuser = AuthContextService.CurrentUser.DisplayName;
                _dbContext.ManWorkshopOutLine.Add(ent2);

                var entity3 = _dbContext.ManOutSplit.FirstOrDefault(x => x.ordCode == model.ordCode && x.itemCode==model.itemCode);
                if (entity3 == null) {
                    //插入到订单产值分配表
                    var ent3 = new ManOutSplit();
                    ent3.Guid = Guid.NewGuid();
                    ent3.OrgID = AuthContextService.CurrentUser.OrgID;
                    ent3.factoryGuid = model.factoryGuid;
                    ent3.countType = countType;
                    ent3.ordCode = model.ordCode;
                    ent3.itemCode = model.itemCode;
                    ent3.itemName = model.itemName;
                    ent3.status = 0;
                    ent3.moddate = ent.credate = DateTime.Now;
                    ent3.moduser = ent.creuser = AuthContextService.CurrentUser.DisplayName;
                    _dbContext.ManOutSplit.Add(ent3);
                }
               
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
               // var entity = _dbContext.ManFactory.FirstOrDefault(x => x.Guid == guid);
                var response = ResponseModelFactory.CreateInstance;
                //response.SetData(_mapper.Map<ManFactory, WorkshopCreateViewModel>(entity));
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
        public IActionResult Edit(WorkshopCreateViewModel model)
        {
           
           
            using (_dbContext)
            {
                var entity = _dbContext.ManWorkshopOut.FirstOrDefault(x => x.Guid == model.Guid );
                entity.countType = model.countType;
                entity.ordCode = model.ordCode;
                entity.itemCode = model.itemCode;
                entity.itemName = model.itemName;
                entity.factoryPrice = model.factoryPrice;
                entity.ordQty = model.ordQty;
                entity.sumQty = model.sumQty;


                //修改子表
                var ent = _dbContext.ManWorkshopOutLine.FirstOrDefault(x => x.workDate == model.workDate && x.workshopOutGuid==entity.Guid);
                if (ent != null)
                {
                    //修改
                    ent.qty = model.qty;
                    ent.output = model.output;
                    ent.workDate = model.workDate;
                    ent.remark = model.remark;
                    ent.moddate = DateTime.Now;
                    ent.moduser = AuthContextService.CurrentUser.DisplayName;
                }
                else {
                    //新增
                    //插入关系到子表
                    var ent2 = new ManWorkshopOutLine();
                    ent2.Guid = Guid.NewGuid();
                    ent2.workshopOutGuid = entity.Guid;
                    ent2.workDate = model.workDate;
                    ent2.qty = model.qty;
                    ent2.output = model.output;
                    ent2.remark = model.remark;
                    ent2.credate = DateTime.Now;
                    ent2.creuser = AuthContextService.CurrentUser.DisplayName;
                    _dbContext.ManWorkshopOutLine.Add(ent2);
                }
                
                
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
                    var sql = string.Format("DELETE FROM ManWorkshopOut WHERE Guid IN ({0})", parameterNames);
                    int li_ret = _dbContext.Database.ExecuteSqlCommand(sql, parameters);

                    var sql2 = string.Format("DELETE FROM ManWorkshopOutLine WHERE workshopOutGuid IN ({0})", parameterNames);
                    int li_ret2 = _dbContext.Database.ExecuteSqlCommand(sql2, parameters);


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
                        sql = "UPDATE ManOutSplit SET status=0, cfmuser='{1}', cfmdate=getdate() WHERE  Guid IN ({0})";
                        sql = string.Format(sql, parameterNames, AuthContextService.CurrentUser.DisplayName);
                    }
                    else
                    {
                        sql = "UPDATE ManOutSplit SET status=1, cfmuser='{1}',cfmdate=getdate() WHERE  Guid IN ({0})";
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
                var sql = string.Format("UPDATE ManOutSplit SET enabled=@enabled WHERE Guid IN ({0})", parameterNames);

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
        

        //查询订单号
        [HttpPost]
        public IActionResult QueryGoodsByOrder(QuerySfModel payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var strSql = @"select t1.fOrdNo as 'fNo',t4.fGoodsCode,t4.fGoodsName,t3.fOrdQty as 'fQty',t3.fUnitCode
,case when isnull(t3._x_f002,0) = 0 then t3.fUP else t3._x_f002 end * case when charindex('PMC',t1.fordno) = 1 then 1 else 0.97 end   as fUP
from SF.DCF19_MY.dbo.t_copd_ordmst t1
inner join SF.DCF19_MY.dbo.t_COPD_OrdItem t3 on t1.fOrdNo = t3.fOrdNo
inner join SF.DCF19_MY.dbo.t_BOMM_GoodsMst t4 on t3.fGoodsID = t4.fGoodsID
where t1.fOrdNo ={0} ";

                var query = _dbContext.SfGoods.FromSql(strSql, payload.no);
                var list = query.ToList();
                var totalCount = query.Count();
                response.SetData(list, totalCount);
                return Ok(response);

            }
        }

        //查询制令号
        [HttpPost]
        public IActionResult QueryGoodsByFmono(QuerySfModel payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var strSql = @"select t1.fMONo as 'fNo',t4.fGoodsCode,t4.fGoodsName,t2.fMoQty as 'fQty',t3.fUnitCode
,case when isnull(t3._x_f002,0) = 0 then t3.fUP else t3._x_f002 end * case when charindex('W',t1.fMONo) = 1 or charindex('PMC',t1.fordno) = 1 then 1 else 0.97 end   as fUP
from SF.DCF19_MY.dbo.t_MPSM_MOMst t1
inner join SF.DCF19_MY.dbo.t_MPSM_MOItem t2 on t1.fMONo = t2.fMONo
inner join SF.DCF19_MY.dbo.t_COPD_OrdItem t3 on t1.fOrdNo = t3.fOrdNo and t2.fGoodsID = t3.fGoodsID
inner join SF.DCF19_MY.dbo.t_BOMM_GoodsMst t4 on t2.fGoodsID = t4.fGoodsID
where t1.fMONo = {0} ";

                var query = _dbContext.SfGoods.FromSql(strSql, payload.no);
                var list = query.ToList();
                var totalCount = query.Count();
                response.SetData(list, totalCount);
                return Ok(response);

            }
        }

        /// <summary>
        /// 修改折算数
        /// </summary>
        /// <param name="model">图标视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult EditQty(QueryWorkshopout model)
        {
            using (_dbContext)
            {
                var outEnt = _dbContext.ManWorkshopOut.FirstOrDefault(x => x.Guid == model.workshopOutGuid);
               
                //修改子表
                var ent = _dbContext.ManWorkshopOutLine.FirstOrDefault(x => x.workDate == model.workDate && x.workshopOutGuid == model.workshopOutGuid);
                if (ent != null)
                {
                    //修改
                    ent.qty = model.qty;
                    ent.output = model.output;
                }
                else
                {
                    //新增
                    //插入关系到子表
                    var ent2 = new ManWorkshopOutLine();
                    ent2.Guid = Guid.NewGuid();
                    ent2.workshopOutGuid = model.workshopOutGuid;
                    ent2.workDate = model.workDate;
                    ent2.qty = model.qty;
                    ent2.output = model.output;
                    ent2.remark = "";
                    ent2.credate = DateTime.Now;
                    ent2.creuser = AuthContextService.CurrentUser.DisplayName;
                    _dbContext.ManWorkshopOutLine.Add(ent2);
                }


                var response = ResponseModelFactory.CreateInstance;

                _dbContext.SaveChanges();
                response.SetSuccess();
                return Ok(response);
            }
        }

        //获取已填写日报的车间
        [HttpPost]
        public IActionResult GetWorkprocessByDone(WorkshopRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                //var query = _dbContext.ManWorkshopOutLine.AsQueryable().Where(x => x.qty != 0);
                var query = _dbContext.ManWorkshopOutLine.AsQueryable().Where(x=>x.workDate==payload.workDate).GroupJoin(_dbContext.ManWorkshopOut, a => a.workshopOutGuid, b => b.Guid, (a, b) => new
                {
                    WorkshopOutLine = a,
                    WorkshopOut = b
                }).SelectMany(x => x.WorkshopOut, (a, b) => new
                { 
                    OutLine = a.WorkshopOutLine,
                    Out=b,
                    processGuid= b.workProcessGuid
                }).Where(x=>x.Out.factoryGuid==payload.factoryGuid).GroupJoin(_dbContext.ManWorkProcess, a => a.processGuid, b => b.Guid, (a, b) => new
                {
                    a.Out,
                    a.OutLine,
                    Process = b
                }).SelectMany(x => x.Process, (a, b) => new
                {
                   b.workName,
                   a.OutLine.qty
                });
                var list = query.Where(x => x.qty == 0).ToList();
                var query2 = _dbContext.ManWorkshopOutLine.AsQueryable().Where(x => x.workDate == payload.workDate).GroupJoin(_dbContext.ManWorkshopOut, a => a.workshopOutGuid, b => b.Guid, (a, b) => new
                {
                    WorkshopOutLine = a,
                    WorkshopOut = b
                }).SelectMany(x => x.WorkshopOut, (a, b) => new
                {
                    OutLine = a.WorkshopOutLine,
                    Out = b,
                    processGuid = b.workProcessGuid
                }).Where(x => x.Out.factoryGuid == payload.factoryGuid).GroupJoin(_dbContext.ManWorkProcess, a => a.processGuid, b => b.Guid, (a, b) => new
                {
                    a.Out,
                    a.OutLine,
                    Process = b
                }).SelectMany(x => x.Process, (a, b) => new
                {
                    b.workName
                });

                foreach (var item in list) {
                    query2=query2.Where(x => x.workName != item.workName);
                }
                var list2 = query2.Distinct().ToList();
                var totalCount = query2.Count();
                response.SetData(list2, totalCount);
                return Ok(response);

            }

        }

        //读取历史数据
        [HttpPost]
        public IActionResult GetHistoryList(WorkshopRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {


                var strSql = @"select b.ordCode,b.itemCode,b.itemName,b.factoryPrice,b.ordQty
                                from ManWorkshopOut as b
                                inner join (select * from ManWorkshopOutLine where  credate in (select max(credate) from [dbo].[ManWorkshopOutLine] as a where datediff(week,a.workDate,getdate())=0 group by a.workshopOutGuid)) AS line ON (line.workshopOutGuid = b.Guid)
                                left join ManOutSplit as c on(c.factoryGuid=b.factoryGuid and c.ordCode=b.ordCode and c.itemCode=b.itemCode)
left join ManOutSplitLine as d on(d.workProcessGuid=c.Guid )
where b.factoryGuid={0} and b.countType={2} 
Group by b.ordCode,b.itemCode,b.itemName,b.factoryPrice,b.ordQty";

                var query = _dbContext.HistoryWorkshopList.FromSql(strSql, payload.factoryGuid, payload.workDate,payload.countType);
                var totalCount = query.Count();
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();


                

                response.SetData(list, totalCount);
                return Ok(response);

            }

        }

    }
}