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
using DncZeus.Api.Entities.Sec;
using static DncZeus.Api.Entities.Tmp.tmpEnum;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using DncZeus.Api.ViewModels.Manage.ApprovalStaffBack;
using System.Collections;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.Reflection;
using DncZeus.Api.RequestPayload.Manage.ApprovalStaffBack;

namespace DncZeus.Api.Controllers.Api.Manage
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [ApiController]
    [ApiVersion("5.0")] //V5
    [Route("api/v5/Manage/[controller]/[action]")]
    public class ApprovalStaffBackController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        

        public ApprovalStaffBackController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            
        }
        [HttpPost]
        public IActionResult List(ApprovalStaffBackRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = _dbContext.ApprovalStaffBack.AsQueryable();
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x =>
                    (
                        x.username.Contains(payload.Kw.Trim()) ||
                        x.department.Contains(payload.Kw.Trim())
                    )
                    );
                }
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();


                var totalCount = query.Count();
                

                response.SetData(list, totalCount);
                return Ok(response);
            }
                
        }
        
        [HttpGet]
        public IActionResult Detail(String ordernum)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                
                CompanyWx wx = new CompanyWx();
               
                    JObject obj = wx.GetApprovalDetail(ordernum);
                    JObject applyData = (JObject)obj["apply_data"];
                    JArray detail = (JArray)applyData["contents"];
                    String ordernumDate = ordernum.Substring(0, 8);

                String backDate = "";
                String zsDate = "";
                String vehicle = "";
                String address = "";
                String isPass = "";
                String isTouch = "";
                String isFever = "";
                String isIll = "";
                String isReport = "";
                String quarantine = "";
                String startPlace = "";
                String endPlace = "";
                String liveType = "";

                if (ordernumDate == "20200215")
                {
                    //详情
                     backDate = detail[1]["value"]["date"]["s_timestamp"].ToString();//返程日期
                     zsDate = detail[2]["value"]["date"]["s_timestamp"].ToString();//到达中山日期
                     vehicle = detail[3]["value"]["selector"]["options"][0]["value"][0]["text"].ToString();//交通工具
                     address = detail[4]["value"]["text"].ToString();//中山居住地址
                     isPass = detail[5]["value"]["selector"]["options"][0]["value"][0]["text"].ToString();//返程是否经过疫区
                     isTouch = detail[6]["value"]["selector"]["options"][0]["value"][0]["text"].ToString();//是否接触疫区人员
                     isFever = detail[7]["value"]["selector"]["options"][0]["value"][0]["text"].ToString();//是否发烧喉咙痛
                     isIll = detail[8]["value"]["selector"]["options"][0]["value"][0]["text"].ToString();//是否感染冠状病毒 
                     isReport = detail[9]["value"]["selector"]["options"][0]["value"][0]["text"].ToString();//外地回粤后是否有向居委报备
                     quarantine = detail[10]["value"]["date"]["s_timestamp"].ToString();//报备日期
                }
                else {
                    //详情
                     backDate = detail[1]["value"]["date"]["s_timestamp"].ToString();//返程日期
                     zsDate = detail[2]["value"]["date"]["s_timestamp"].ToString();//到达中山日期
                     vehicle = detail[3]["value"]["selector"]["options"][0]["value"][0]["text"].ToString();//交通工具
                     startPlace = detail[4]["value"]["text"].ToString();//出发地
                     endPlace = detail[5]["value"]["text"].ToString();//目的地
                     address = detail[6]["value"]["text"].ToString();//中山居住地详细地址
                     liveType = detail[7]["value"]["selector"]["options"][0]["value"][0]["text"].ToString();//中山居住在性质
                     isPass = detail[8]["value"]["selector"]["options"][0]["value"][0]["text"].ToString();//返程是否经过疫区
                     isTouch = detail[9]["value"]["selector"]["options"][0]["value"][0]["text"].ToString();//是否接触疫区人员
                     isFever = detail[10]["value"]["selector"]["options"][0]["value"][0]["text"].ToString();//是否发烧喉咙痛
                     isIll = detail[11]["value"]["selector"]["options"][0]["value"][0]["text"].ToString();//是否感染冠状病毒 
                     isReport = detail[12]["value"]["selector"]["options"][0]["value"][0]["text"].ToString();//外地回粤后是否有向居委报备
                     quarantine = detail[13]["value"]["date"]["s_timestamp"].ToString();//报备日期
                }
                


                    String credate = obj["apply_time"].ToString();
                    String type = obj["sp_name"].ToString();
                    String status = obj["sp_status"].ToString();//审批节点状态：1-审批中；2-已同意；3-已驳回；4-已转审

                    String userid = obj["applyer"]["userid"].ToString();
                    String depid = obj["applyer"]["partyid"].ToString();

                    JObject user = wx.GetUser(userid);
                    String username = user["name"].ToString();
                    String dep = wx.GetDepartment(depid);

                    JObject info = new JObject(){
                        new JProperty("username",username),//用户名
                        new JProperty("department",dep),//部门
                        new JProperty("type",type),//类型
                        new JProperty("ordernum",ordernum),//单号
                        new JProperty("status",wx.RedenerStatus(status)),//状态
                        new JProperty("credate",credate),//创建时间
                        new JProperty("backDate",wx.GetDateTime(backDate)),//
                        new JProperty("zsDate",wx.GetDateTime(zsDate)),//
                        new JProperty("vehicle",vehicle),//
                        new JProperty("address",address),//
                        new JProperty("isPass",isPass),//
                        new JProperty("isTouch",isTouch),//
                        new JProperty("isFever",isFever),//
                        new JProperty("isIll",isIll),//
                        new JProperty("isReport",isReport),//
                         new JProperty("startPlace",startPlace),//
                        new JProperty("endPlace",endPlace),//
                        new JProperty("liveType",liveType),//
                        new JProperty("quarantine",wx.GetDateTime(quarantine))//

                    };



                var returnJson = (JObject)JsonConvert.DeserializeObject("{'data':" + info + "}");
                return Ok(returnJson);

            }
        }

        //数据入库
        [HttpGet]
        public IActionResult ImportData()
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = _dbContext.ApprovalStaffBack.AsQueryable();

                CompanyWx wx = new CompanyWx();

                DateTime curDate = DateTime.Now;
                DateTime oldDate = curDate.AddDays(-30);
                string curTimestamp = wx.GetTimeStamp(curDate).ToString();
                string oldTimestamp = wx.GetTimeStamp(oldDate).ToString();

                String tmpid = "Bs5NGcveph9hwEnnhT2pvZX3kpZbtj27o5J1bpj3b";
                JArray spList = wx.GetApprovalInfo(oldTimestamp, curTimestamp, 0, 100, tmpid);
                IList<JToken> deleteList = new List<JToken>();
                deleteList = wx.GetApprovalInfoByDelete(oldTimestamp, curTimestamp, 0, 100, tmpid);

                //过滤已删除单号
                foreach (var deletenum in deleteList)
                {
                    spList.RemoveAt(0);
                }

                foreach (string ordernum in spList)
                {

                    String ordernumDate = ordernum.Substring(0, 8);

                    JObject obj = wx.GetApprovalDetail(ordernum);
                    JObject applyData = (JObject)obj["apply_data"];
                    JArray detail = (JArray)applyData["contents"];

                    String backDate = "";
                    String zsDate = "";
                    String vehicle = "";
                    String address = "";
                    String isPass = "";
                    String isTouch = "";
                    String isFever = "";
                    String isIll = "";
                    String isReport = "";
                    String quarantine = "";
                    String startPlace = "";
                    String endPlace = "";
                    String liveType = "";

                    if (ordernumDate == "20200215")
                    {
                        //详情
                        backDate = detail[1]["value"]["date"]["s_timestamp"].ToString();//返程日期
                        zsDate = detail[2]["value"]["date"]["s_timestamp"].ToString();//到达中山日期
                        vehicle = detail[3]["value"]["selector"]["options"][0]["value"][0]["text"].ToString();//交通工具
                        address = detail[4]["value"]["text"].ToString();//中山居住地址
                        isPass = detail[5]["value"]["selector"]["options"][0]["value"][0]["text"].ToString();//返程是否经过疫区
                        isTouch = detail[6]["value"]["selector"]["options"][0]["value"][0]["text"].ToString();//是否接触疫区人员
                        isFever = detail[7]["value"]["selector"]["options"][0]["value"][0]["text"].ToString();//是否发烧喉咙痛
                        isIll = detail[8]["value"]["selector"]["options"][0]["value"][0]["text"].ToString();//是否感染冠状病毒 
                        isReport = detail[9]["value"]["selector"]["options"][0]["value"][0]["text"].ToString();//外地回粤后是否有向居委报备
                        quarantine = detail[10]["value"]["date"]["s_timestamp"].ToString();//报备日期
                    }
                    else
                    {
                        //详情
                        backDate = detail[1]["value"]["date"]["s_timestamp"].ToString();//返程日期
                        zsDate = detail[2]["value"]["date"]["s_timestamp"].ToString();//到达中山日期
                        vehicle = detail[3]["value"]["selector"]["options"][0]["value"][0]["text"].ToString();//交通工具
                        startPlace = detail[4]["value"]["text"].ToString();//出发地
                        endPlace = detail[5]["value"]["text"].ToString();//目的地
                        address = detail[6]["value"]["text"].ToString();//中山居住地详细地址
                        liveType = detail[7]["value"]["selector"]["options"][0]["value"][0]["text"].ToString();//中山居住在性质
                        isPass = detail[8]["value"]["selector"]["options"][0]["value"][0]["text"].ToString();//返程是否经过疫区
                        isTouch = detail[9]["value"]["selector"]["options"][0]["value"][0]["text"].ToString();//是否接触疫区人员
                        isFever = detail[10]["value"]["selector"]["options"][0]["value"][0]["text"].ToString();//是否发烧喉咙痛
                        isIll = detail[11]["value"]["selector"]["options"][0]["value"][0]["text"].ToString();//是否感染冠状病毒 
                        isReport = detail[12]["value"]["selector"]["options"][0]["value"][0]["text"].ToString();//外地回粤后是否有向居委报备
                        quarantine = detail[13]["value"]["date"]["s_timestamp"].ToString();//报备日期
                    }




                    String credate = obj["apply_time"].ToString();
                    String type = obj["sp_name"].ToString();
                    String status = obj["sp_status"].ToString();//审批节点状态：1-审批中；2-已同意；3-已驳回；4-已转审

                    String userid = obj["applyer"]["userid"].ToString();
                    String depid = obj["applyer"]["partyid"].ToString();

                    JObject user = wx.GetUser(userid);
                    String username = user["name"].ToString();
                    String dep = wx.GetDepartment(depid);

                    var result = query.Where(x => x.ordernum == ordernum);
                    int resultCount = result.Count();
                    if (resultCount == 0)
                    {
                        var entity = new ApprovalStaffBack();
                        entity.Guid = new Guid();
                        entity.username = username;
                        entity.department = dep;
                        entity.type = type;
                        entity.ordernum = ordernum;
                        entity.status = wx.RedenerStatus(status);
                        entity.credate = wx.GetDateTime(credate);
                        entity.backDate = wx.GetDateTime(backDate);
                        entity.zsDate = wx.GetDateTime(zsDate);
                        entity.vehicle = vehicle;
                        entity.isPass = isPass;
                        entity.address = address;
                        entity.isReport = isReport;
                        entity.isTouch = isTouch;
                        entity.isFever = isFever;
                        entity.isIll = isIll;
                        entity.startPlace = startPlace;
                        entity.endPlace = endPlace;
                        entity.liveType = liveType;
                        entity.quarantine = wx.GetDateTime(quarantine);

                        _dbContext.ApprovalStaffBack.Add(entity);
                    }

                    _dbContext.SaveChanges();
                }
                response.SetSuccess();
                return Ok(response);
                

            }
        }

    }
}