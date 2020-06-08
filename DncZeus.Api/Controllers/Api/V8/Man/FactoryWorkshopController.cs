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
using DncZeus.Api.RequestPayload.Man.Factory;
using DncZeus.Api.ViewModels.Man.Factory;
using DncZeus.Api.ViewModels.Man.FactoryWorkshop;
using DncZeus.Api.RequestPayload.Man.FactoryWorkshop;

namespace DncZeus.Api.Controllers.Api.Weixin
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [ApiController]
    [ApiVersion("8.0")] //V6
    [Route("api/v8/Man/[controller]/[action]")]
    public class FactoryWorkshopController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        

        public FactoryWorkshopController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            
        }

        [HttpPost]
        public IActionResult GetFactoryWorkshopInfo(FactoryWorkshopRequestPayload payload)
        {

            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {

                var strSql = @"
select a.factoryGuid,a.yearPlanOutQty,isnull(b.monPlanOutQty,0) as 'monPlanOutQty',isnull(c.monAddDoneOutQty,0) as 'monAddDoneOutQty',isnull(d.sumPeople,0) as 'sumPeople',isnull(e.yearAddPlanOutQty,0) as 'yearAddPlanOutQty',isnull(f.yearAddDoneOutQty,0) as 'yearAddDoneOutQty',isnull(g.todayDoneOutQty,0) as 'todayDoneOutQty',isnull(h.targetOut ,0) as 'targetOut',isnull(h.realityOut,0) as 'realityOut',isnull(h.sumTime,0) as 'sumTime',isnull(h.status,0) as 'status'
from(select sum(outqty) as 'yearPlanOutQty',factoryGuid from ManFactoryMonPlan  where factoryGuid in({0}) and YY={1} GROUP BY factoryGuid) as a
left join (
	select outqty as 'monPlanOutQty',factoryGuid from ManFactoryMonPlan where factoryGuid in({0}) and YY={1} and MM={2}
	) as b on (b.factoryGuid=a.factoryGuid)
left join (
	select sum(outline.output) as 'monAddDoneOutQty',out.factoryGuid from ManWorkshopOut as out 
	inner join ManWorkshopOutLine as outline on (outline.workshopOutGuid=out.Guid)
	where out.factoryGuid in({0}) and outline.workDate BETWEEN (SELECT   DATEADD(mm, DATEDIFF(mm, 0, GETDATE()), 0)) and (SELECT   DATEADD(d, - 1, DATEADD(mm, DATEDIFF(mm, 0, GETDATE())+1, 0)))
	group by out.factoryGuid
) as c on (c.factoryGuid=a.factoryGuid)
left join (
	select cadre+logistic+worker as 'sumPeople',Guid as 'factoryGuid' from ManFactory where Guid in({0})
) as d on(d.factoryGuid=a.factoryGuid)
left join (
	select sum(outqty) as 'yearAddPlanOutQty',factoryGuid from ManFactoryMonPlan where factoryGuid in({0}) and YY={1} and CONVERT(INT,MM)<=CONVERT(INT,{2}) GROUP BY factoryGuid
) as e on(e.factoryGuid=a.factoryGuid)
left join (
	select sum(outline.output) as 'yearAddDoneOutQty',factoryGuid from ManWorkshopOut as out 
inner join ManWorkshopOutLine as outline on (outline.workshopOutGuid=out.Guid)
where out.factoryGuid in({0}) and outline.workDate <=(SELECT   DATEADD(d, - 1, DATEADD(mm, DATEDIFF(mm, 0, GETDATE())+1, 0))) GROUP BY factoryGuid
) as f on(f.factoryGuid=a.factoryGuid)
left join (
select sum(outline.output) as 'todayDoneOutQty',out.factoryGuid from ManWorkshopOut as out 
inner join ManWorkshopOutLine as outline on (outline.workshopOutGuid=out.Guid)
where out.factoryGuid in({0}) and  DATEDIFF(day,outline.workDate,{3})=0 GROUP BY out.factoryGuid
) as g on(g.factoryGuid=a.factoryGuid)
left join (
	select aa.targetOut,aa.realityOut,aa.sumTime,aa.factoryGuid,aa.status from ManFactoryWorkout aa where wrokDate={3} and factoryGuid={0}
) as h on(h.factoryGuid=a.factoryGuid)";

                var query = _dbContext.FactoryWorkshopInfo.FromSql(strSql, payload.factoryGuid, payload.yy, payload.mm,payload.workDate);
                var list = query.ToList();
                if (list.Count > 0)
                {
                    response.SetData(list[0]);
                }
                else {
                    response.SetData(null);
                }
                
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
        public IActionResult Edit(FactoryWorkshopCreateViewModel model)
        {
           
           
            using (_dbContext)
            {
                //工厂日产
                var entWorkout = _dbContext.ManFactoryWorkout.FirstOrDefault(x => x.factoryGuid == model.factoryGuid && x.wrokDate == model.workdate);
                if (entWorkout != null)
                {
                    entWorkout.targetOut = model.output.targetOut;
                    entWorkout.realityOut = model.output.realityOut;
                    entWorkout.sumTime = model.output.sumTime;
                    entWorkout.status = Convert.ToInt32(model.status);

                }
                else {
                    var entout = new ManFactoryWorkout();
                    entout.Guid = new Guid();
                    entout.factoryGuid = model.factoryGuid;
                    entout.wrokDate = model.workdate;
                    entout.OrgID = AuthContextService.CurrentUser.OrgID;
                    entout.targetOut = model.output.targetOut;
                    entout.realityOut = model.output.realityOut;
                    entout.sumTime = model.output.sumTime;
                    entout.status = Convert.ToInt32(model.status);
                    entout.credate = DateTime.Now;
                    entout.creuser = AuthContextService.CurrentUser.DisplayName;

                    _dbContext.ManFactoryWorkout.Add(entout);

                }

                    //出勤
                    var entWorkdate = _dbContext.ManFactoryWorkdate.FirstOrDefault(x => x.factoryGuid == model.factoryGuid && x.workDate==model.workdate);
                if (entWorkdate != null)
                {
                    entWorkdate.cadre = model.factoryWorkDate.cadre;
                    entWorkdate.logistic = model.factoryWorkDate.logistic;
                    entWorkdate.worker = model.factoryWorkDate.worker;
                    entWorkdate.cadreLeave = model.factoryWorkDate.cadreLeave;
                    entWorkdate.logisticLeave = model.factoryWorkDate.logisticLeave;
                    entWorkdate.workerLeave = model.factoryWorkDate.workerLeave;
                    entWorkdate.cadreRest = model.factoryWorkDate.cadreRest;
                    entWorkdate.logisticRest = model.factoryWorkDate.logisticRest;
                    entWorkdate.workerRest = model.factoryWorkDate.workerRest;
                    entWorkdate.moddate = DateTime.Now;
                    entWorkdate.moduser = AuthContextService.CurrentUser.DisplayName;
                }
                else {
                    var ent = new ManFactoryWorkdate();
                    ent.Guid = new Guid();
                    ent.factoryGuid = model.factoryGuid;
                    ent.workDate = model.workdate;
                    ent.OrgID = AuthContextService.CurrentUser.OrgID;
                    ent.cadre = model.factoryWorkDate.cadre;
                    ent.logistic = model.factoryWorkDate.logistic;
                    ent.worker = model.factoryWorkDate.worker;
                    ent.cadreLeave = model.factoryWorkDate.cadreLeave;
                    ent.logisticLeave = model.factoryWorkDate.logisticLeave;
                    ent.workerLeave = model.factoryWorkDate.workerLeave;
                    ent.cadreRest = model.factoryWorkDate.cadreRest;
                    ent.logisticRest = model.factoryWorkDate.logisticRest;
                    ent.workerRest = model.factoryWorkDate.workerRest;
                    ent.moddate = ent.credate = DateTime.Now;
                    ent.moduser = ent.creuser = AuthContextService.CurrentUser.DisplayName;

                    _dbContext.ManFactoryWorkdate.Add(ent);
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
                    var sql = string.Format("DELETE FROM ManFactory WHERE Guid IN ({0})", parameterNames);
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
                        sql = "UPDATE ManFactory SET status=0, cfmuser='{1}', cfmdate=getdate() WHERE  Guid IN ({0})";
                        sql = string.Format(sql, parameterNames, AuthContextService.CurrentUser.DisplayName);
                    }
                    else
                    {
                        sql = "UPDATE ManFactory SET status=1, cfmuser='{1}',cfmdate=getdate() WHERE  Guid IN ({0})";
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
                var sql = string.Format("UPDATE ManFactory SET enabled=@enabled WHERE Guid IN ({0})", parameterNames);

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
        /// 出勤
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult GetFactoryWorkDate(FactoryWorkshopRequestPayload payload)
        {
            using (_dbContext)
            {
                var entity = _dbContext.ManFactoryWorkdate.FirstOrDefault(x => x.factoryGuid == payload.factoryGuid && x.workDate==payload.workDate);

                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<ManFactoryWorkdate, FactoryWorkdate>(entity));
                return Ok(response);
            }
        }
        /// <summary>
        /// 工厂日产
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult GetFactoryWorkOut(FactoryWorkshopRequestPayload payload)
        {
            using (_dbContext)
            {
                var entity = _dbContext.ManFactoryWorkout.FirstOrDefault(x => x.factoryGuid == payload.factoryGuid && x.wrokDate == payload.workDate);

                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<ManFactoryWorkout, FactoryWorkout>(entity));
                return Ok(response);
            }
        }
        /// <summary>
        /// 月度计划
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult GetMonthPlan(PlanRequestPayload payload)
        {
            using (_dbContext)
            {
                var query = _dbContext.ManFactoryMonPlan.AsQueryable().Where(x => x.factoryGuid == payload.factoryGuid && x.YY == payload.yy);

                var response = ResponseModelFactory.CreateInstance;
                response.SetData(query.ToList());
                return Ok(response);
            }
        }

        /// <summary>
        /// 月度计划_编辑
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult EditMonthPlan(MonthPlanCreateModel model)
        {
            using (_dbContext)
            {
                foreach (MonthPlan plan in model.list) {
                    var entity = _dbContext.ManFactoryMonPlan.FirstOrDefault(x => x.factoryGuid == model.factoryGuid && x.YY == model.YY && x.MM==plan.MM);
                    if (entity != null)
                    {
                        entity.outqty = plan.outqty;
                        entity.moddate = DateTime.Now;
                        entity.moduser = AuthContextService.CurrentUser.DisplayName;
                    }
                    else
                    {
                        var ent = new ManFactoryMonPlan();
                        ent.Guid = new Guid();
                        ent.factoryGuid = model.factoryGuid;
                        ent.YY = model.YY;
                        ent.MM = plan.MM;
                        ent.outqty = plan.outqty;
                        ent.OrgID = AuthContextService.CurrentUser.OrgID;

                        ent.moddate = ent.credate = DateTime.Now;
                        ent.moduser = ent.creuser = AuthContextService.CurrentUser.DisplayName;

                        _dbContext.ManFactoryMonPlan.Add(ent);
                    }
                }
                
                var response = ResponseModelFactory.CreateInstance;

                _dbContext.SaveChanges();
                response.SetSuccess();
                return Ok(response);
            }
        }

        /// <summary>
        /// 撤回
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult RollbackFactoryWorkshop(FactoryWorkshopCreateViewModel model)
        {
            using (_dbContext)
            {
               
                    var entity = _dbContext.ManFactoryWorkout.FirstOrDefault(x => x.factoryGuid == model.factoryGuid && x.wrokDate == model.workdate);
                    if (entity != null)
                    {
                        entity.status = Convert.ToInt32( model.status);
                       
                    }
                  
               

                var response = ResponseModelFactory.CreateInstance;

                _dbContext.SaveChanges();
                response.SetSuccess();
                return Ok(response);
            }
        }


    }
}