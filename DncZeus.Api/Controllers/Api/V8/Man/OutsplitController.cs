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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace DncZeus.Api.Controllers.Api.Weixin
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [ApiController]
    [ApiVersion("8.0")] //V6
    [Route("api/v8/Man/[controller]/[action]")]
    public class OutsplitController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        

        public OutsplitController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            
        }
        [HttpPost]
        public IActionResult List(OutsplitRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            
            using (_dbContext)
            {
                //var query = _dbContext.ManOutSplit.AsQueryable();

                String rateEmpty = "false";

                var query1 = _dbContext.ManOutSplit.AsQueryable().Where(x=>x.factoryGuid==payload.factoryGuid && x.status==payload.status);
                var queryList1 = query1.ToList();
               
                JArray list1 = new JArray();

                for (var i=0;i< queryList1.Count;i++) {
                    var item = queryList1[i];

                    var query2 = _dbContext.ManOutSplit.AsQueryable().Where(x=>x.Guid== item.Guid).GroupJoin(_dbContext.ManOutSplitLine, a => a.Guid, b => b.workProcessGuid, (a, b) => new
                    {
                        OutSplit = a,
                        OutSplitLine = b
                    }).SelectMany(x => x.OutSplitLine, (a, b) => new
                    {
                        Out = a.OutSplit,
                        OutLine = b,
                        b.ProcessGuid,
                        b.rate
                    }).GroupJoin(_dbContext.ManWorkProcess, a => a.ProcessGuid, b => b.Guid, (a, b) => new
                    {
                        a.Out,
                        a.OutLine,
                        Process = b
                    }).SelectMany(x => x.Process, (a, b) => new
                    {
                        a.Out.status,
                        a.OutLine.Guid,
                        processGuid = b.Guid,
                        b.workCode,
                        b.workName,
                        b.rate,
                        rate2=a.OutLine.rate
                    });

                    var queryList2 = query2.ToList();

                    JArray list2 = new JArray();
                    for (var j = 0; j < queryList2.Count; j++)
                    {
                        var item2 = queryList2[j];
                        JObject info2 = new JObject(){
                            new JProperty("lineGuid",item2.Guid),
                             new JProperty("guid",item2.processGuid),
                              new JProperty("workCode",item2.workCode),
                               new JProperty("workName",item2.workName),
                               new JProperty("rate",item2.rate),
                               new JProperty("rate2",item2.rate2),
                        };
                        list2.Add(info2);

                    }
                    
                        JObject info1 = new JObject(){
                        new JProperty("guid",item.Guid),
                        new JProperty("orgID",item.OrgID),
                        new JProperty("factoryGuid",item.factoryGuid),
                        new JProperty("countType",item.countType),
                        new JProperty("ordCode",item.ordCode),
                        new JProperty("itemCode",item.itemCode),
                        new JProperty("itemName",item.itemName),
                        new JProperty("status",item.status),
                        new JProperty("creuser",item.creuser),
                        new JProperty("credate",item.credate),
                        new JProperty("moduser",item.moduser),
                        new JProperty("moddate",item.moddate),
                        new JProperty("ratelist",list2)
                    };
                    if (list2.Count == 0)
                    {
                        rateEmpty = "true";
                    }
                    list1.Add(info1);
                }

                    var returnJson = (JObject)JsonConvert.DeserializeObject("{'rateEmpty':" + rateEmpty + ",'data':" + list1 + ",'totalCount':"+ list1.Count+ "}");
                return Ok(returnJson);


            }

        }

        //检测未审核比例分配是否完善
        [HttpPost]
        public IActionResult CheckRate(OutsplitRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;

            using (_dbContext)
            {
                

                JObject returnJson = new JObject();


                if (payload.outsplitGuid != null)
                {
                    String rateEmpty = "false";
                    var query1 = _dbContext.ManOutSplit.AsQueryable().Where(x => x.factoryGuid == payload.factoryGuid && x.status == 0 && x.Guid == payload.outsplitGuid);
                    var queryList1 = query1.ToList();
                    JArray list1 = new JArray();
                    for (var i = 0; i < queryList1.Count; i++)
                    {
                        var item = queryList1[i];
                        var query2 = _dbContext.ManOutSplit.AsQueryable().Where(x => x.Guid == item.Guid).GroupJoin(_dbContext.ManOutSplitLine, a => a.Guid, b => b.workProcessGuid, (a, b) => new
                        {
                            OutSplit = a,
                            OutSplitLine = b
                        }).SelectMany(x => x.OutSplitLine, (a, b) => new
                        {
                            Out = a.OutSplit,
                            OutLine = b,
                            b.ProcessGuid,
                            b.rate
                        }).GroupJoin(_dbContext.ManWorkProcess, a => a.ProcessGuid, b => b.Guid, (a, b) => new
                        {
                            a.Out,
                            a.OutLine,
                            Process = b
                        }).SelectMany(x => x.Process, (a, b) => new
                        {
                            a.Out.status,
                            a.OutLine.Guid,
                            processGuid = b.Guid,
                            b.workCode,
                            b.workName,
                            b.rate,
                            rate2 = a.OutLine.rate
                        });

                        var queryList2 = query2.ToList();
                        if (queryList2.Count == 0)
                        {
                            rateEmpty = "true";
                        }
                    }
                    returnJson = (JObject)JsonConvert.DeserializeObject("{'rateEmpty':" + rateEmpty + "}");
                    
                }
                else {
                    string hasOpen = "false";
                    var query = _dbContext.ManOutSplit.AsQueryable().Where(x => x.factoryGuid == payload.factoryGuid && x.status == 0);
                    var queryList = query.ToList();
                    if (queryList.Count > 0) {
                        hasOpen = "true";
                    }
                    returnJson = (JObject)JsonConvert.DeserializeObject("{'hasOpen':" + hasOpen + "}");
                }
                return Ok(returnJson);
            }

        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="model">类别视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(OutSplitCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
          
            using (_dbContext)
            {
                var entity = _dbContext.ManOutSplit.FirstOrDefault(x => x.factoryGuid == model.factoryGuid && x.ordCode == model.ordCode && x.itemCode == model.itemCode);
                if (entity != null)
                {
                    response.SetFailed("已存在");
                    return Ok(response);  
                }
                else { 
                    var ent = new ManOutSplit(); 
                    var outsplitGuid = Guid.NewGuid();
                    ent.Guid = outsplitGuid;
                    ent.OrgID = AuthContextService.CurrentUser.OrgID;
                    ent.factoryGuid = model.factoryGuid;
                    ent.countType = model.countType;
                    ent.ordCode = model.ordCode;
                    ent.itemCode = model.itemCode;
                    ent.itemName = model.itemName;
                    ent.status = model.status;
                    ent.moddate = ent.credate = DateTime.Now;
                    ent.moduser = ent.creuser = AuthContextService.CurrentUser.DisplayName;
                    _dbContext.ManOutSplit.Add(ent);

                    foreach (RateList item in model.ratelist){
                        //插入关系到子表
                        var ent2 = new ManOutSplitLine();
                        ent2.Guid = Guid.NewGuid();
                        ent2.workProcessGuid = outsplitGuid;
                        ent2.ProcessGuid = item.guid;
                        if (item.rate2 == 0)
                        {
                            ent2.rate = item.rate;
                        }
                        else {
                            ent2.rate = item.rate2;
                        }
                        _dbContext.ManOutSplitLine.Add(ent2);
                    }


                _dbContext.SaveChanges();
                    response.SetSuccess();
              
                return Ok(response);
                }
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
                var entity = _dbContext.ManFactory.FirstOrDefault(x => x.Guid == guid);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<ManFactory, OutSplitCreateViewModel>(entity));
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
        public IActionResult Edit(OutSplitCreateViewModel model)
        {
           
           
            using (_dbContext)
            {
                var entity = _dbContext.ManOutSplit.FirstOrDefault(x => x.Guid == model.Guid);
                entity.countType = model.countType;
                entity.ordCode = model.ordCode;
                entity.itemCode = model.itemCode;
                entity.itemName = model.itemName;
                entity.moddate = DateTime.Now;
                entity.moduser = AuthContextService.CurrentUser.DisplayName;


                var query = _dbContext.ManOutSplitLine.AsQueryable().Where(x => x.workProcessGuid == model.Guid);
                _dbContext.ManOutSplitLine.RemoveRange(query);
                _dbContext.SaveChanges();

                //修改子表
                foreach (RateList item in model.ratelist)
                {
                    //插入关系到子表
                    var ent2 = new ManOutSplitLine();
                    ent2.Guid = Guid.NewGuid();
                    ent2.workProcessGuid = entity.Guid;
                    ent2.ProcessGuid = item.guid;
                    ent2.rate = item.rate2;
                    
                    _dbContext.ManOutSplitLine.Add(ent2);
                    _dbContext.SaveChanges();
                }
                var response = ResponseModelFactory.CreateInstance;

                //_dbContext.SaveChanges();
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
                    var sql = string.Format("DELETE FROM ManOutSplit WHERE Guid IN ({0})", parameterNames);
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
                        sql = "UPDATE ManOutSplit SET status=1  WHERE  Guid IN ({0})";
                        sql = string.Format(sql, parameterNames, AuthContextService.CurrentUser.DisplayName);
                    }
                    else
                    {
                        sql = "UPDATE ManOutSplit SET status=0  WHERE  Guid IN ({0})";
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
        
        

    }
}