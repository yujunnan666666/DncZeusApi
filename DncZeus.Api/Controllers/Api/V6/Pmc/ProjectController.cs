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
using DncZeus.Api.RequestPayload.Sec.Button;
using DncZeus.Api.ViewModels.Pmc.Project;
using DncZeus.Api.Entities.Pmc;
using DncZeus.Api.RequestPayload.Pmc.Projects;
using DncZeus.Api.RequestPayload.Pmc.Worknote;

namespace DncZeus.Api.Controllers.Api.Pmc

{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [ApiController]
    [ApiVersion("6.0")] //V6
    [Route("api/v6/Pmc/[controller]/[action]")]
    public class ProjectController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        

        public ProjectController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            
        }
        [HttpPost]
        public IActionResult List(ProjectRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = _dbContext.PmcProjects.AsQueryable();
              
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x =>
                    (
                    x.Code.Contains(payload.Kw.Trim()) ||
                    x.Name.Contains(payload.Kw.Trim())
                    )
                    );
                }
                if (payload.status != 99) {
                    query = query.Where(x => x.status == payload.status );
                }
                if (payload.diffStatus != 99)
                {
                    query = query.Where(x =>x.diffStatus == payload.diffStatus);
                }
                if (payload.focusStatus != 99)
                {
                    query = query.Where(x => x.focusStatus == payload.focusStatus);
                }
               // query = query.OrderBy("trseq", false);
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
               
                
                var totalCount = query.Count();
                //var data = list.Select(_mapper.Map<PmcProjects, CategoryJsonModel>);

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
        public IActionResult Create(ProjectCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            /*if (model.Code.Trim().Length <= 0)
            {
                response.SetFailed("请输入图标名称");
                return Ok(response);
            }*/
            using (_dbContext)
            {
               
                var entity = _mapper.Map<ProjectCreateViewModel, PmcProjects>(model);
                
                if (_dbContext.PmcProjects.Count(x => x.Code == model.Code) > 0)
                {
                    var str = model.Code;
                    var proNo = int.Parse(str.Substring(1, str.Length - 1)) + 1;
                    entity.Code = "P" + proNo;

                }
                entity.moddate=entity.credate = DateTime.Now;
                entity.moduser=entity.creuser = AuthContextService.CurrentUser.DisplayName;
                
                _dbContext.PmcProjects.Add(entity);
                _dbContext.SaveChanges();
                response.SetData(entity.Guid);
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
                var entity = _dbContext.PmcProjects.FirstOrDefault(x => x.Guid == guid);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<PmcProjects, ProjectCreateViewModel>(entity));
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
        public IActionResult Edit(ProjectCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (model.Code.Trim().Length <= 0)
            {
                response.SetFailed("请输入名称");
                return Ok(response);
            }
            using (_dbContext)
            {
               
                var entity = _dbContext.PmcProjects.FirstOrDefault(x => x.Guid == model.Guid);
                entity.Name = model.Name;
                entity.custcode = model.custcode;
                entity.custname = model.custname;
                entity.qty = model.qty;
                entity.totalmoney = model.totalmoney;
                entity.procreDate = model.procreDate;
                entity.isbatch = model.isbatch;
                entity.custDate = model.custDate;
                entity.factoryDate1 = model.factoryDate1;
                entity.factoryDate2 = model.factoryDate2;
                entity.followgrade = model.followgrade;
                entity.status = model.status;
                entity.diffStatus = model.diffStatus;
                entity.focusStatus = model.focusStatus;
                entity.remark = model.remark;
                entity.area = model.area;
                entity.address = model.address;
                entity.phone = model.phone;
                entity.contacter = model.contacter;
                entity.isdiff = model.isdiff;
                entity.isfocus = model.isfocus;

                entity.credate = model.credate;
                entity.creuser = model.creuser;
                entity.moddate = DateTime.Now;
                entity.moduser = entity.creuser = AuthContextService.CurrentUser.DisplayName;

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
                    var sql = string.Format("DELETE FROM PmcProjects WHERE Guid IN ({0})", parameterNames);
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
                case "projectCfm": //项目审核
                     response = UpdateStatus(2, ids,"cfm");
                     break;
                case "projectCfm2": //项目评核
                    response = UpdateStatus(3, ids, "cfm");
                    break;
                case "imgCfm": //草图审核
                    response = UpdateStatus(4, ids, "cfm");
                    break;
                case "clientCfm": //客户确图
                    response = UpdateStatus(5, ids, "cfm");
                    break;
                case "orderCfm": //订单评审
                    response = UpdateStatus(7, ids, "cfm");
                    break;
                case "itemCfm": //重点物料审图
                    response = UpdateStatus(8, ids, "cfm"); 
                    break;

                case "projectOpen": //项目审核撤回
                    response = UpdateStatus(1, ids, "open");
                     break;
                case "projectOpen2": //项目评审撤回
                    response = UpdateStatus(2, ids, "open");
                    break;
                case "imgOpen": //草图审核撤回
                    response = UpdateStatus(3, ids, "open");
                    break;
                case "clientOpen": //客户确图撤回
                    response = UpdateStatus(4, ids, "open");
                    break;
                case "orderOpen": //订单评审撤回
                    response = UpdateStatus(6, ids, "open");
                    break;
                case "itemOpen": //重点物料审图撤回
                    response = UpdateStatus(7, ids, "open");
                    break;
                /* case "Invalid": //失效
                     response = UpdateIsValid(isValid.Invalid, ids);
                     break;
                 case "Valid": //有效
                     response = UpdateIsValid(isValid.Valid, ids);
                     break;*/
                default:
                    break;
            }
            return Ok(response);
        }

        /// <summary>
        /// 获取当天项目流水号
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200)]
        public int GetTodayCount(String today)
        {
            using (_dbContext)
            {
                var count = 0;
                var strSql = @"select * from Pmcprojects where DateDiff(dd,credate,getdate())=0";
                count = _dbContext.PmcProjects.FromSql(strSql).Count();
                return count;
            }
        }

        /// <summary>
        /// 获取客户数据
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetClientList(int curPage,string kw)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var strSql = @"select fCCode,fCNName,fCName,fConditionFlag from  SF.DCF19_MY.dbo.t_CRMM_CstMst t1 where t1.fConditionFlag!='4'";
                var query = _dbContext.SfClient.FromSql(strSql);
                if (!string.IsNullOrEmpty(kw))
                {
                    query = query.Where(x =>
                    (
                    x.fCNName.Contains(kw.Trim()) ||
                    x.fCName.Contains(kw.Trim())
                    )
                    );
                }
                var totalCount = query.Count();
                var list = query.Paged(curPage, 15).ToList();
                response.SetData(list,totalCount);
                return Ok(response);
               
            }
        }

        /// <summary>
        /// 任务列表
        /// </summary>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult GetDutyList(ProjectPlanViewModel payload)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                //var query = _dbContext.PmcDuties.AsQueryable().Where(x=>x.status==2);
                var strSql = @"SELECT duty.Guid,duty.OrgID,duty.Code,duty.Name,duty.department,duty.iskeep,duty.dutiesGuid,duty.itype,duty.dotype,duty.projectStatus,duty.status,duty.remark,duty.creuser,duty.credate,duty.moduser,duty.moddate,duty.cfmuser,duty.cfmdate,
isnull(planday.keepdate,duty.keepdate) as keepdate,isnull(planday.dodate,duty.dodate) as dodate                            
FROM [dbo].[PmcDuties] AS duty  
LEFT JOIN [dbo].[PmcProjectplanday] AS planday ON (planday.dutiesGuid=duty.Guid and planday.projectsGuid={0})
                                WHERE   duty.status=2";



                var query = _dbContext.PmcDuties.FromSql(strSql, payload.projectGuid);

                //没有难点物料
                if (payload.diffItem == 0 && payload.focusitem == 1)
                {
                    query = query.Where(x => x.dotype == 1 || x.dotype == 3);
                }

                //没有重点物料
                if (payload.diffItem == 1 && payload.focusitem == 0)
                {
                    query = query.Where(x => x.dotype == 1 || x.dotype == 2);
                }

                //没有重点物料和难点物料
                if (payload.diffItem == 0 && payload.focusitem == 0)
                {
                    query = query.Where(x => x.dotype == 1);
                }


                /* var strSql = @"select *  SF.DCF19_TEST.dbo.t_CRMM_CstMst t1 ";
                 var query = _dbContext.SfClient.FromSql(strSql);*/


                var list = query.ToList();
                var totalCount = list.Count();

                response.SetData(list,totalCount);
                return Ok(response);

            }
        }

        //计划排程列表
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult GetPlanList(ProjectPlanViewModel payload)
        {
         
            var response = ResponseModelFactory.CreateResultInstance;
            try
            {
                using (_dbContext)
                {
                    //Guid projectGuid = new Guid(payload.projectId);
                    var strSql = @"SELECT 
                                proplan.pbegindate as startDate,proplan.penddate as endDate,proplan.Guid as projectplanGuid,proplan.totalqty,proplan.status,proplan.restDesc,isnull(proplan.restCount,0) as restDay,proplan.abegindate,proplan.aenddate,
                                duty.Guid as dutiesGuid,duty.department,isnull(duty.projectStatus,0) as dutyStatus,isnull(duty.iskeep,0) as iskeep,duty.Name,duty.itype,duty.dotype,
isnull(planday.keepdate,duty.keepdate) as keepdate,isnull(planday.dodate,duty.dodate) as dodate                                  
FROM [dbo].[PmcProjectplan] AS proplan  
                                INNER JOIN [dbo].[PmcDuties] AS duty ON (duty.Guid = proplan.dutiesGuid)
LEFT JOIN [dbo].[PmcProjectplanday] AS planday ON (planday.dutiesGuid=duty.Guid and planday.projectsGuid={0})
                                WHERE proplan.projectsGuid IN ({0}) and proplan.isdel=0";



                    var query = _dbContext.PmcDutyPlanList.FromSql(strSql, payload.projectGuid);
                    //没有难点物料
                    if (payload.diffItem == 0 && payload.focusitem == 1)
                    {
                        query = query.Where(x => x.dotype == 1 || x.dotype == 3);
                    }

                    //没有重点物料
                    if (payload.diffItem == 1 && payload.focusitem == 0)
                    {
                        query = query.Where(x => x.dotype == 1 || x.dotype == 2 );
                    }

                    //没有重点物料和难点物料
                    if (payload.diffItem == 0 && payload.focusitem == 0)
                    {
                        query = query.Where(x => x.dotype == 1);
                    }
                    var totalCount = query.Count();
                    var list = query.ToList();

                    response.SetData(list, totalCount);
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                response.SetFailed("查询失败:" + e.Message);
                return Ok(response);
            }

        }

        /// <summary>
        /// 创建计划
        /// </summary>
        /// <param name="model">类别视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult CreatePlan(PlanCreateArr model)
        {
            var response = ResponseModelFactory.CreateInstance;
          
            using (_dbContext)
            {
                var projectsGuid = model.projectsGuid;
                if (_dbContext.PmcProjectplan.Count(x => x.projectsGuid == projectsGuid) > 0)
                {
                    //编辑
                    var ents = _dbContext.PmcProjectplan.AsQueryable().Where(x => x.projectsGuid == projectsGuid && x.isdel==0);
                    foreach (PmcProjectplan plan in ents)
                    {
                        var planFollow = _dbContext.WjgcPlanfllow.AsQueryable().Where(x => x.projectplanGuid == plan.Guid);
                        _dbContext.WjgcPlanfllow.RemoveRange(planFollow);

                        plan.isdel = 1;

                    }
                    _dbContext.SaveChanges();
                    //_dbContext.PmcProjectplan.RemoveRange(ents);

                    foreach (PlanCreateViewModel item in model.planList)
                    {
                        var entity = new PmcProjectplan();
                        entity.Guid = new Guid();
                        entity.projectsGuid = projectsGuid;
                        entity.ptype = item.itype;
                        entity.dutiesGuid = item.Code!=null? item.Guid:item.dutiesGuid;
                        entity.iskeep = item.iskeep;
                        entity.dodate = item.dodate;
                        entity.pbegindate = item.startDate;
                        entity.penddate = item.endDate;
                        entity.restDesc = item.restDesc;
                        entity.restCount = item.restCount;

                        entity.curProgress = "";
                        _dbContext.PmcProjectplan.Add(entity);

                    }
                }
                else {
                    foreach (PlanCreateViewModel item in model.planList)
                    {
                        var entity = new PmcProjectplan();
                        entity.Guid = new Guid();
                        entity.projectsGuid = projectsGuid;
                        entity.ptype = item.itype;
                        entity.dutiesGuid = item.Guid;
                        entity.iskeep = item.iskeep;
                        entity.dodate = item.dodate;
                        entity.pbegindate = item.startDate;
                        entity.penddate = item.endDate;
                        entity.restDesc = item.restDesc;
                        entity.restCount = item.restCount;

                        entity.curProgress = "";
                        _dbContext.PmcProjectplan.Add(entity);

                    }
                }
                
              
               
                
                
                _dbContext.SaveChanges();


                response.SetSuccess();
                return Ok(response);
            }
        }

        /// <summary>
        /// 审核/弃审
        /// </summary>
        /// <param name="status"></param>
        /// <param name="ids">ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel UpdateStatus(int status, string ids,string type)
        {
            using (_dbContext)
            {
                var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                var sql = "";

               
                    sql = "UPDATE PmcProjects SET status={2}, cfmuser='{1}', cfmdate=getdate() WHERE  Guid IN ({0})";
                    sql = string.Format(sql, parameterNames, AuthContextService.CurrentUser.DisplayName,status);
                

                int li_ret = _dbContext.Database.ExecuteSqlCommand(sql, parameters);
                var response = ResponseModelFactory.CreateInstance;

                response.SetData(new
                {
                    affectCount = li_ret
                });
                if (type == "cfm") {
                    UpdateProjectplanInfo(_dbContext, ids, status);
                }
                

                return response;
            }
        }

        /// <summary>
        /// 添加操作记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult CreateWorknote(WorknoteCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {
                PmcWorknote worknote = new PmcWorknote();
                worknote.Guid = new Guid();
                worknote.projectsGuid = model.projectsGuid;
                worknote.wtype = model.wtype;
                worknote.followRemark = model.followRemark;
                worknote.creuser = AuthContextService.CurrentUser.DisplayName;
                worknote.credate = DateTime.Now;

                _dbContext.PmcWorknote.Add(worknote);
                _dbContext.SaveChanges();

                response.SetSuccess();
                return Ok(response);
            }
        }

        [HttpPost]
        public IActionResult GetWorknoteList(WorknoteRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = _dbContext.PmcWorknote.AsQueryable().Where(x=>x.projectsGuid== payload.projectsGuid);

                query = query.OrderBy("credate", true);
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();

                var totalCount = query.Count();
                

                response.SetData(list, totalCount);
                return Ok(response);

            }


        }

        /// <summary>
        /// 修改计划排程天数
        /// </summary>
        /// <param name="id">类别ID</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult UpdatePlanDay(ProjectPlanDayCreateModel model)
        {
            using (_dbContext)
            {
                var entity = _dbContext.PmcProjectplanday.FirstOrDefault(x => x.projectsGuid == model.row.projectsGuid && x.dutiesGuid==model.row.dutiesGuid);
                if (entity != null) {
                    //编辑
                    /*entity.prkeepdate = model.row.prkeepdate;
                    entity.prdodate = model.row.prdodate;
                    entity.keepdate = model.row.keepdate;
                    entity.dodate = model.row.dodate;*/
                    if (model.col == "keepdate") {
                        entity.keepdate = model.row.keepdate;
                    }
                    if (model.col == "dodate") {
                        entity.dodate = model.row.dodate;
                    }
                }
                else{
                    //新增
                    PmcProjectplanday ent = new PmcProjectplanday();
                    ent.Guid = new Guid();
                    ent.projectsGuid = model.row.projectsGuid;
                    ent.dutiesGuid = model.row.dutiesGuid;
                    ent.prkeepdate = model.row.prkeepdate;
                    ent.prdodate = model.row.prdodate;
                    ent.keepdate = model.row.keepdate;
                    ent.dodate = model.row.dodate;

                    _dbContext.PmcProjectplanday.Add(ent);

                }
                _dbContext.SaveChanges();

                var response = ResponseModelFactory.CreateInstance;
                response.SetSuccess();
                //response.SetData(_mapper.Map<PmcProjectplanday, ProjectPlanDayCreateModel>(entity));
                return Ok(response);
            }
        }

        //更新计划排程信息
        private void UpdateProjectplanInfo(DncZeusDbContext _dbContext,string ids,int status)
        {
            var response = ResponseModelFactory.CreateInstance;
            try
            {
                string dutyName = "";
                switch (status)
                {
                    case 2: //项目审核
                        dutyName = "项目立项";
                        break;
                    case 3: //项目评审
                        dutyName = "立项评审";
                        break;
                    case 4: //草图审核
                        dutyName = "出草图";
                        break;
                    case 5: //客户确图
                        dutyName = "客人确认图纸";
                        break;
                    case 7: //订单评审
                        dutyName = "订单评审";
                        break;
                    case 8: //出重点物料图
                        dutyName = "出重点物料图";
                        break;
                    
                    default:
                        break;
                }
                //Guid projectGuid = new Guid(payload.projectId);
                var strSql = @"SELECT
		            proplan.Guid,
proplan.projectsGuid,
proplan.ptype,
proplan.dutiesGuid,
proplan.curProgress,
proplan.iskeep,
proplan.dodate,
proplan.pbegindate,
proplan.penddate,
proplan.abegindate,
proplan.aenddate,
proplan.totalqty,
proplan.status,
proplan.remark,
proplan.isdel,
proplan.restDesc,
proplan.restCount
		            FROM [dbo].[PmcProjects] AS pro 
		            INNER JOIN [dbo].[PmcProjectplan] AS proplan ON (proplan.projectsGuid = pro.Guid) 
		            INNER JOIN [dbo].[PmcDuties] AS duty ON (duty.Guid = proplan.dutiesGuid) 
		            where pro.Guid in({0}) and duty.Name={1} and proplan.isdel=0";

                    var idsArr = ids.Split(",");
                    foreach (string str in idsArr)
                    {
                        Guid projectsGuid = new Guid(str);
                        var planList = _dbContext.PmcProjectplan.FromSql(strSql, projectsGuid, dutyName).ToList();
                        //新增
                        PmcPlannote ent = new PmcPlannote();
                        ent.Guid = new Guid();
                        ent.projectplanGuid = planList[0].Guid;
                        ent.projectlineGuid = null;
                        ent.projectitemGuid = null;
                        ent.remark = "审核通过";
                        ent.workdate = DateTime.Now;
                        ent.workstatus = 4;
                        ent.workqty = 0;
                        ent.creuser = AuthContextService.CurrentUser.DisplayName;
                        ent.credate = DateTime.Now;

                        _dbContext.PmcPlannote.Add(ent);

                        //修改plan实际完成日期和状态
                        planList[0].status = 3;
                        planList[0].abegindate = DateTime.Now;
                        planList[0].aenddate = DateTime.Now;



                }
                _dbContext.SaveChanges();



                //return Ok(response);

            }
            catch (Exception e)
            {
                response.SetFailed("查询失败:" + e.Message);
               // return Ok(response);
            }

        }


    }
}