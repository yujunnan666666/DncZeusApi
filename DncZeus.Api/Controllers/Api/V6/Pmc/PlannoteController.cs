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
using DncZeus.Api.ViewModels.Pmc.Plannote;
using DncZeus.Api.RequestPayload.Pmc.Plannote;
using Microsoft.EntityFrameworkCore.Internal;
using DncZeus.Api.Entities.Sec;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
//using DncZeus.Api.Controllers.Api.Mis;

namespace DncZeus.Api.Controllers.Api.Pmc
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [ApiController]
    [ApiVersion("6.0")] //V6
    [Route("api/v6/Pmc/[controller]/[action]")]
    public class PlannoteController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        

        public PlannoteController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            
        }
        [HttpPost]
        public IActionResult List(PlannoteRequestPayload payload)
        {
           /* var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = _dbContext.PmcDuties.AsQueryable();
                var totalCount = query.Count();
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();

                response.SetData(list, totalCount);
                return Ok(response);
            }*/
                var response = ResponseModelFactory.CreateResultInstance;
            try
            {
                using (_dbContext)
                {

                    /* var strSql = @"SELECT
                                         pro.Name,pro.custname,pro.custDate,isnull(pro.status,0) as proStatus,
                                         proplan.pbegindate,proplan.penddate,proplan.Guid as projectplanGuid,proplan.abegindate,proplan.aenddate,proplan.totalqty,proplan.status,
                                         line.qty,line.custitemname,line.Guid as lineGuid,
                                         note.workdate,isnull(note.workstatus,0) as workstatus,isnull(note.workqty,0) as workqty,note.remark,note.Guid as noteGuid,
                                         duty.department,isnull(duty.projectStatus,0) as dutyStatus,isnull(duty.iskeep,0) as iskeep,duty.Name as dutyName,duty.itype,
                                         follow.nodes,
                                         isnull(note1.workstatus,0) as prev1,
                                         isnull(note2.workstatus,0) as prev2,
                                         isnull(note3.workstatus,0) as prev3

                                         FROM [dbo].[PmcProjects] AS pro 
                                         INNER JOIN [dbo].[PmcProjectplan] AS proplan ON (proplan.projectsGuid = pro.Guid) 
                                         INNER JOIN [dbo].[PmcProjectline] AS line ON (line.projectsGuid = pro.Guid) 
                                         LEFT JOIN (select * from [dbo].[PmcPlannote] where credate in (select max(credate) from [dbo].[PmcPlannote] as a group by a.projectplanGuid,a.projectlineGuid)) AS note ON (note.projectplanGuid = proplan.Guid and note.projectlineGuid=line.Guid)
                                         LEFT JOIN [dbo].[PmcDuties] AS duty ON (duty.Guid = proplan.dutiesGuid)
                                         LEFT JOIN[dbo].[WjgcPlanfllow] AS follow ON(follow.projectplanGuid = proplan.Guid and follow.projectlineGuid=line.Guid)
                                         LEFT JOIN (SELECT * FROM [dbo].[PmcPlannote] where DATEDIFF(day,workdate,{0})=1) AS note1 ON (note1.projectplanGuid = proplan.Guid and note1.projectlineGuid=line.Guid)
                                         LEFT JOIN(SELECT * FROM[dbo].[PmcPlannote] where DATEDIFF(day, workdate,{0})= 2) AS note2 ON(note2.projectplanGuid = proplan.Guid and note2.projectlineGuid = line.Guid)
                                         LEFT JOIN(SELECT* FROM [dbo].[PmcPlannote] where DATEDIFF(day, workdate,{0})= 3) AS note3 ON(note3.projectplanGuid = proplan.Guid and note3.projectlineGuid = line.Guid)";*/
                    
                    //调用 sp_pmc_plannoteList存储过程
                    var strSql = string.Format(@"exec sp_pmc_plannoteList '{0}'", payload.day);
                    var query = _dbContext.PmcPlannoteList.FromSql(strSql);
                   

                    if (!string.IsNullOrEmpty(payload.Kw))
                    {
                        query = query.Where(x =>
                        (
                        x.Name.Contains(payload.Kw.Trim()) ||
                        x.custitemname.Contains(payload.Kw.Trim()) ||
                        x.custname.Contains(payload.Kw.Trim())
                        )
                        );
                    }
                    if (!string.IsNullOrEmpty(payload.department))
                    {
                        query = query.Where(x => x.department == payload.department);
                    }
                    if (payload.dutyStatus!=2)
                    {
                        //未完成
                        if (payload.dutyStatus==0) {
                            query = query.Where(x => x.proStatus<=x.dutyStatus);
                        }
                        //未完成
                        if (payload.dutyStatus == 1)
                        {
                            query = query.Where(x => x.proStatus >= x.dutyStatus);
                        }

                    }
                    if (payload.diffItem == 0 && payload.focusItem == 0)
                    {
                        query = query.Where(x => x.itype == 1);
                    }
                    else if (payload.diffItem == 1 && payload.focusItem == 0)
                    {
                        query = query.Where(x => x.itype == 1 || x.itype == 2);
                    }
                    else if(payload.diffItem == 0 && payload.focusItem == 1)
                    {
                        query = query.Where(x => x.itype == 1 || x.itype == 3);
                    }
                    
                    var totalCount = query.Count();
                    var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();

                    response.SetData(list, totalCount);
                    return Ok(response);


                }
            }
                catch (Exception e)
            {
                //return UserJsonModel(new { isSuccess = false, message = "保存失败:" }, "text/html");

                response.SetFailed("查询失败:" + e.Message);
                return Ok(response);
            }
           
         }
        //列表（不包含项目明细）
        [HttpPost]
        public IActionResult SimpleList(PlannoteRequestPayload payload)
        {
            
            var response = ResponseModelFactory.CreateResultInstance;
            try
            {
                using (_dbContext)
                {
                    /*var strSql = @"SELECT
                                        pro.Name,pro.custname,pro.custDate,isnull(pro.status,0) as proStatus,
                                        proplan.pbegindate,proplan.penddate,proplan.Guid as projectplanGuid,proplan.abegindate,proplan.aenddate,proplan.totalqty,proplan.status,
                                        note.workdate,isnull(note.workstatus,0) as workstatus,isnull(note.workqty,0) as workqty,note.remark,note.Guid as noteGuid,
                                        duty.department,isnull(duty.projectStatus,0) as dutyStatus,isnull(duty.iskeep,0) as iskeep,duty.Name as dutyName,duty.itype,
                                        follow.nodes,
                                        isnull(note1.workstatus,0) as prev1,
                                        isnull(note2.workstatus,0) as prev2,
                                        isnull(note3.workstatus,0) as prev3

                                        FROM [dbo].[PmcProjects] AS pro 
                                        INNER JOIN [dbo].[PmcProjectplan] AS proplan ON (proplan.projectsGuid = pro.Guid) 
                                        LEFT JOIN (select * from [dbo].[PmcPlannote] where credate in (select max(credate) from [dbo].[PmcPlannote] as a group by a.projectplanGuid,a.projectlineGuid)) AS note ON (note.projectplanGuid = proplan.Guid )
                                        LEFT JOIN [dbo].[PmcDuties] AS duty ON (duty.Guid = proplan.dutiesGuid)
                                        LEFT JOIN[dbo].[WjgcPlanfllow] AS follow ON(follow.projectplanGuid = proplan.Guid )
                                        LEFT JOIN (SELECT * FROM [dbo].[PmcPlannote] where DATEDIFF(day,workdate,{0})=1) AS note1 ON (note1.projectplanGuid = proplan.Guid)
                                        LEFT JOIN(SELECT * FROM[dbo].[PmcPlannote] where DATEDIFF(day, workdate,{0})= 2) AS note2 ON(note2.projectplanGuid = proplan.Guid)
                                        LEFT JOIN(SELECT* FROM [dbo].[PmcPlannote] where DATEDIFF(day, workdate,{0})= 3) AS note3 ON(note3.projectplanGuid = proplan.Guid)";*/

                    var strSql = string.Format(@"exec sp_pmc_plannoteList2 '{0}'", payload.day);
                    var query = _dbContext.PmcPlannoteList2.FromSql(strSql, payload.day);
                    if (!string.IsNullOrEmpty(payload.Kw))
                    {
                        query = query.Where(x =>
                        (
                        x.Name.Contains(payload.Kw.Trim()) ||
                        //x.custitemname.Contains(payload.Kw.Trim()) ||
                        x.custname.Contains(payload.Kw.Trim())
                        )
                        );
                    }
                    if (!string.IsNullOrEmpty(payload.department))
                    {
                        query = query.Where(x => x.department == payload.department);
                    }
                    if (payload.dutyStatus != 2)
                    {
                        //未完成
                        if (payload.dutyStatus == 0)
                        {
                            query = query.Where(x => x.proStatus <= x.dutyStatus);
                        }
                        //未完成
                        if (payload.dutyStatus == 1)
                        {
                            query = query.Where(x => x.proStatus >= x.dutyStatus);
                        }
                    }
                    if (payload.diffItem == 0 && payload.focusItem == 0)
                    {
                        query = query.Where(x => x.itype == 1);
                    }
                    else if (payload.diffItem == 1 && payload.focusItem == 0)
                    {
                        query = query.Where(x => x.itype == 1 || x.itype == 2);
                    }
                    else if (payload.diffItem == 0 && payload.focusItem == 1)
                    {
                        query = query.Where(x => x.itype == 1 || x.itype == 3);
                    }

                    var totalCount = query.Count();
                    var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();

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
        /// 创建类别
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(PlannoteCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            /*if (model.Code.Trim().Length <= 0)
            {
                response.SetFailed("请输入图标名称");
                return Ok(response);
            }*/
            using (_dbContext)
            {
               /* if (_dbContext.PmcProjects.Count(x => x.Code == model.Code) > 0)
                {
                    response.SetFailed("图标已存在");
                    return Ok(response);
                }*/
                var entity = _mapper.Map<PlannoteCreateViewModel, PmcProjects>(model);
                entity.moddate=entity.credate = DateTime.Now;
                entity.moduser=entity.creuser = AuthContextService.CurrentUser.DisplayName;
                
                _dbContext.PmcProjects.Add(entity);
                _dbContext.SaveChanges();

                response.SetSuccess();
                return Ok(response);
            }
        }

        /// <summary>
        /// 类别详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Detail(Guid guid)
        {
            using (_dbContext)
            {
                var entity = _dbContext.PmcProjects.FirstOrDefault(x => x.Guid == guid);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<PmcProjects, PlannoteCreateViewModel>(entity));
                return Ok(response);
            }
        }

        /// <summary>
        /// 修改类别
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(PlannoteCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            
            using (_dbContext)
            {
               
               /* var entity = _dbContext.PmcProjects.FirstOrDefault(x => x.Guid == model.Guid);
               
                entity.remark = model.remark;

                entity.credate = model.credate;
                entity.creuser = model.creuser;
                entity.moddate = DateTime.Now;
                entity.moduser = entity.creuser = AuthContextService.CurrentUser.DisplayName;

                _dbContext.SaveChanges();
                response.SetSuccess();*/
                return Ok(response);
            }
        }

        /// <summary>
        /// 删除(开立状态)
        /// </summary>
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
        /// 多选编辑
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult MultiEdit(PlannoteArrs model)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {
                foreach (PlannoteObj obj in model.list) {
                    Guid? lineGuid = null;
                    Guid? itemGuid = null;
                    if (obj.lineGuid.ToString() != "")
                    {
                        lineGuid = obj.lineGuid;
                    }
                    if (obj.itemGuid.ToString() != "")
                    {
                        itemGuid = obj.itemGuid;
                    }
                    var plannoteEnt = _dbContext.PmcPlannote.FirstOrDefault(x => x.projectplanGuid == obj.projectplanGuid && x.projectlineGuid == lineGuid && x.projectitemGuid == itemGuid && x.workdate == obj.workdate);
                    if (plannoteEnt != null)
                    {
                        multiSetPlanData(_dbContext, plannoteEnt, obj, "edit");
                        //编辑
                        plannoteEnt.remark = obj.remark;
                        plannoteEnt.workdate = obj.workdate;
                        plannoteEnt.workstatus = obj.workstatus;
                        plannoteEnt.workqty = obj.workqty;
                        plannoteEnt.moduser = AuthContextService.CurrentUser.DisplayName;
                        plannoteEnt.moddate = DateTime.Now;

                    }
                    else
                    {
                        //新增
                        PmcPlannote ent = new PmcPlannote();
                        ent.Guid = new Guid();

                        ent.projectplanGuid = obj.projectplanGuid;
                        ent.projectlineGuid = lineGuid;
                        ent.projectitemGuid = itemGuid;
                        ent.remark = obj.remark;
                        ent.workdate = obj.workdate;
                        ent.workstatus = obj.workstatus;
                        ent.workqty = obj.workqty;
                        ent.creuser = AuthContextService.CurrentUser.DisplayName;
                        ent.credate = DateTime.Now;

                        multiSetPlanData(_dbContext, ent, obj, "create");

                        _dbContext.PmcPlannote.Add(ent);
                    }

                    _dbContext.SaveChanges();
                    
                }

                response.SetSuccess();
                return Ok(response);
            }
        }

        /// <summary>
        /// 批量操作
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult BatchEdit(PlannoteCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {
                var ids = model.ids.Split(",");
                var lineIds = model.lineIds.Split(",");
                var planIds = model.planIds.Split(",");
                var itemIds = model.itemIds.Split(",");

                int index = 0;
                foreach (string str in ids) {
                    Guid? lineGuid = null;
                    Guid? itemGuid = null;
                    if (lineIds[index] != "") {
                        lineGuid = new Guid(lineIds[index]);
                    }
                    if (itemIds[index] != "")
                    {
                        itemGuid = new Guid(itemIds[index]);
                    }
                    var plannoteEnt = _dbContext.PmcPlannote.FirstOrDefault(x => x.projectplanGuid == new Guid(planIds[index]) && x.projectlineGuid== lineGuid && x.projectitemGuid == itemGuid && x.workdate== model.workdate);
                    if (plannoteEnt != null)
                    {
                        setPlanData(_dbContext, plannoteEnt, model, "edit");
                        //编辑
                        plannoteEnt.remark = model.remark;
                        plannoteEnt.workdate = model.workdate;
                        plannoteEnt.workstatus = model.workstatus;
                        plannoteEnt.workqty = model.workqty;
                        plannoteEnt.moduser = AuthContextService.CurrentUser.DisplayName;
                        plannoteEnt.moddate = DateTime.Now;
                        
                    }
                    else {
                        //新增
                        PmcPlannote ent = new PmcPlannote();
                        ent.Guid = new Guid();
                        
                        ent.projectplanGuid = new Guid(planIds[index]);
                        ent.projectlineGuid = lineGuid;
                        ent.projectitemGuid = itemGuid;
                        ent.remark = model.remark;
                        ent.workdate = model.workdate;
                        ent.workstatus = model.workstatus;
                        ent.workqty = model.workqty;
                        ent.creuser = AuthContextService.CurrentUser.DisplayName;
                        ent.credate = DateTime.Now;

                        setPlanData(_dbContext, ent, model,"create");

                        _dbContext.PmcPlannote.Add(ent);
                    }
                    
                    _dbContext.SaveChanges();
                    index++;
                }
                
                response.SetSuccess();
                return Ok(response);
            }
        }
        private void setPlanData(DncZeusDbContext dbContext,PmcPlannote noteEnt, PlannoteCreateViewModel model,string type) {
            var planEnt = _dbContext.PmcProjectplan.FirstOrDefault(x => x.Guid == noteEnt.projectplanGuid);
            //工作中
            if (noteEnt.workstatus == 2 )
            {
                planEnt.status = 2;
                if (type == "create")
                {
                    planEnt.totalqty = planEnt.totalqty + model.workqty;
                }
                else {
                    planEnt.totalqty = planEnt.totalqty - noteEnt.workqty + model.workqty;
                }
               

                if (planEnt.abegindate != null)
                {
                    DateTime workdate = Convert.ToDateTime(model.workdate);
                    DateTime abegindate = Convert.ToDateTime(planEnt.abegindate);
                    if (DateTime.Compare(workdate, abegindate) < 0)
                    {
                        planEnt.abegindate = model.workdate;
                    }
                }
                else {
                    planEnt.abegindate = model.workdate;
                }
            }
            //已完成
            if (noteEnt.workstatus == 4 )
            {
                planEnt.status = 3;
                planEnt.totalqty = planEnt.totalqty + model.workqty;

                if (planEnt.aenddate != null)
                {
                    DateTime workdate = Convert.ToDateTime(model.workdate);
                    DateTime aenddate = Convert.ToDateTime(planEnt.aenddate);
                    if (DateTime.Compare(workdate, aenddate) > 0)
                    {
                        planEnt.aenddate = model.workdate;
                    }
                }
                else {
                    planEnt.aenddate = model.workdate;
                }
            }
            _dbContext.SaveChanges();
        }

        private void multiSetPlanData(DncZeusDbContext dbContext, PmcPlannote noteEnt, PlannoteObj model, string type)
        {
            var planEnt = _dbContext.PmcProjectplan.FirstOrDefault(x => x.Guid == noteEnt.projectplanGuid);
            //工作中
            if (noteEnt.workstatus == 2)
            {
                planEnt.status = 2;
                if (type == "create")
                {
                    planEnt.totalqty = planEnt.totalqty + model.workqty;
                }
                else
                {
                    planEnt.totalqty = planEnt.totalqty - noteEnt.workqty + model.workqty;
                }


                if (planEnt.abegindate != null)
                {
                    DateTime workdate = Convert.ToDateTime(model.workdate);
                    DateTime abegindate = Convert.ToDateTime(planEnt.abegindate);
                    if (DateTime.Compare(workdate, abegindate) < 0)
                    {
                        planEnt.abegindate = model.workdate;
                    }
                }
                else
                {
                    planEnt.abegindate = model.workdate;
                }
            }
            //已完成
            if (noteEnt.workstatus == 4)
            {
                planEnt.status = 3;
                planEnt.totalqty = planEnt.totalqty + model.workqty;

                if (planEnt.aenddate != null)
                {
                    DateTime workdate = Convert.ToDateTime(model.workdate);
                    DateTime aenddate = Convert.ToDateTime(planEnt.aenddate);
                    if (DateTime.Compare(workdate, aenddate) > 0)
                    {
                        planEnt.aenddate = model.workdate;
                    }
                }
                else
                {
                    planEnt.aenddate = model.workdate;
                }
            }
            _dbContext.SaveChanges();
        }
        /// <summary>
        /// 问题上报
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult BatchReport(ReportViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {

                Guid followGuid;
                

                var entity = _dbContext.WjgcPlanfllow.FirstOrDefault(x => x.projectplanGuid == model.projectplanGuid && x.projectlineGuid == model.projectlineGuid && x.projectitemGuid == model.projectitemGuid);
                if (entity != null)
                {
                    //编辑
                    entity.fllowDesc = model.fllowDesc;
                    entity.isnote = model.isnote;
                    entity.nodes = model.nodes;
                    entity.Supremo = model.supremo;
                    entity.workmaster = model.workmaster;
                    entity.pmc = model.pmc;
                    entity.status = model.status;
                    entity.moduser = AuthContextService.CurrentUser.DisplayName;
                    entity.moddate = DateTime.Now;
                    _dbContext.SaveChanges();

                    var oldFollowers= _dbContext.WjgcPlanfllowuser.AsQueryable().Where(x => x.planfllowGuid == entity.Guid);
                    _dbContext.WjgcPlanfllowuser.RemoveRange(oldFollowers);
                    Secuser[] followers = model.followers;
                    foreach (Secuser user in followers)
                    {
                        WjgcPlanfllowuser ent2 = new WjgcPlanfllowuser();
                        ent2.Guid = new Guid();
                        ent2.planfllowGuid = entity.Guid;
                        ent2.isnotice = 0;
                        ent2.userno = user.userno;
                        ent2.status = 0;
                        ent2.senddate = null;
                        ent2.creuser = AuthContextService.CurrentUser.DisplayName;
                        ent2.credate = DateTime.Now;

                        _dbContext.WjgcPlanfllowuser.Add(ent2);
                        _dbContext.SaveChanges();

                    }
                }
                else {
                    //新建
                    WjgcPlanfllow ent = new WjgcPlanfllow();
                    followGuid = new Guid();
                    ent.Guid = followGuid;
                    ent.projectplanGuid = model.projectplanGuid;
                    ent.projectlineGuid = model.projectlineGuid;
                    ent.projectitemGuid = model.projectitemGuid;
                    ent.fllowDesc = model.fllowDesc;
                    ent.isnote = model.isnote;
                    ent.nodes = model.nodes;
                    ent.Supremo = model.supremo;
                    ent.workmaster = model.workmaster;
                    ent.pmc = model.pmc;
                    ent.status = model.status;

                    ent.creuser = AuthContextService.CurrentUser.DisplayName;
                    ent.credate = DateTime.Now;
                    
                    _dbContext.WjgcPlanfllow.Add(ent);
                    _dbContext.SaveChanges();

                    Secuser[] followers = model.followers;
                    foreach (Secuser user in followers) {
                        WjgcPlanfllowuser ent2 = new WjgcPlanfllowuser();
                        ent2.Guid = new Guid();
                        ent2.planfllowGuid = ent.Guid;
                        ent2.isnotice = 0;
                        ent2.userno = user.userno;
                        ent2.status = 0;
                        ent2.senddate = null;
                        ent2.creuser = AuthContextService.CurrentUser.DisplayName;
                        ent2.credate = DateTime.Now;

                        _dbContext.WjgcPlanfllowuser.Add(ent2);
                        _dbContext.SaveChanges();

                    }
                   

                }
                 
                //_dbContext.SaveChanges();

                response.SetSuccess();
                return Ok(response);
            }
        }
        /// <summary>
        /// 问题详情
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult loadReport(ReportViewModel model)
        {
            using (_dbContext)
            {
                var entity = _dbContext.WjgcPlanfllow.FirstOrDefault(x => x.projectplanGuid == model.projectplanGuid && x.projectlineGuid == model.projectlineGuid);

                if (entity != null)
                {
                    var followusers = _dbContext.WjgcPlanfllowuser.AsQueryable().Where(x => x.planfllowGuid == entity.Guid).ToList();

                    JArray userArr = new JArray();
                    JObject followInfo = new JObject(){
                        new JProperty("projectplanGuid",entity.projectplanGuid),//   
                        new JProperty("projectlineGuid",entity.projectlineGuid),//   
                        new JProperty("fllowDesc",entity.fllowDesc),//   
                        new JProperty("isnote",entity.isnote),//   
                        new JProperty("nodes",entity.nodes),//   
                        new JProperty("supremo",entity.Supremo),//   
                        new JProperty("workmaster",entity.workmaster),//   
                        new JProperty("pmc",entity.pmc),//   
                        new JProperty("status",entity.status),//   
                    };
                    foreach (WjgcPlanfllowuser user in followusers)
                    {
                        var userEnt = _dbContext.Secuser.FirstOrDefault(x => x.userno == user.userno);
                        JObject userInfo = new JObject(){
                        new JProperty("username",userEnt.username),//
                        new JProperty("wxId",userEnt.wxId),//
                        new JProperty("userno",userEnt.userno),//   
                        new JProperty("depname",userEnt.depname)//     
                    };
                        userArr.Add(userInfo);

                    }
                    JObject info = new JObject(){
                        new JProperty("follow",followInfo),//
                        new JProperty("followusers",userArr),//   
                    };

                    //var returnJson = (JObject)JsonConvert.DeserializeObject("{'data':" + info + "}");
                    var returnJson = (JObject)JsonConvert.DeserializeObject("" + info);


                    return Ok(returnJson);
                    //return Ok(response);
                }
                else {
                    JArray userArr = new JArray();
                    JObject followInfo = new JObject();
                    JObject info = new JObject(){
                        new JProperty("follow",null),//
                        new JProperty("followusers",userArr),//   
                    };

                    //var returnJson = (JObject)JsonConvert.DeserializeObject("{'data':" + info + "}");
                    var returnJson = (JObject)JsonConvert.DeserializeObject("" + info);


                    return Ok(returnJson);
                }

                return Ok("");
            }
        }
        /// <summary>
        /// 日报明细
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetDayReportList(Guid? projectplanGuid,Guid? projectlineGuid)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;


                var query = _dbContext.PmcPlannote.AsQueryable().Where(x=>x.projectplanGuid== projectplanGuid && x.projectlineGuid == projectlineGuid).OrderBy("workdate", true).GroupJoin(_dbContext.PmcProjectplan, note => note.projectplanGuid, plan => plan.Guid, (note, plan) => new {
                    Note = note,
                    Plan = plan
                }).SelectMany(x => x.Plan, (a, b) => new {
                    a.Note,
                    b.dutiesGuid
                }).GroupJoin(_dbContext.PmcDuties, a => a.dutiesGuid, duty => duty.Guid, (note, duty) => new {
                    note.Note, 
                    Duty = duty
                }).SelectMany(x => x.Duty, (a, b) => new {
                    a.Note.workdate,
                    a.Note.workstatus,
                    a.Note.workqty,
                    a.Note.remark,
                    a.Note.creuser,
                    a.Note.credate,
                    b.Name

                });

                // var query = _dbContext.PmcProjectplan.AsQueryable().Where(x => x.projectplanGuid == projectplanGuid && x.projectlineGuid== projectlineGuid).OrderBy("workdate",true);
                var list = query.ToList();
                var totalCount = query.Count();
               
                response.SetData(list,totalCount);
                return Ok(response);
            }
        }

        



    }
}