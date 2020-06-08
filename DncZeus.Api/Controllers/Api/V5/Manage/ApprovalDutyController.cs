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
using DncZeus.Api.RequestPayload.Sec.Fun;
using DncZeus.Api.ViewModels.Sec.Fun;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using DncZeus.Api.ViewModels.Manage.ApprovalDuty;
using System.Collections;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.Reflection;
using DncZeus.Api.RequestPayload.Manage.ApprovalDuty;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.StaticFiles;
using System.Threading.Tasks;

namespace DncZeus.Api.Controllers.Api.Manage
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [ApiController]
    [ApiVersion("5.0")] //V5
    [Route("api/v5/Manage/[controller]/[action]")]
    public class ApprovalDutyController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        

        public ApprovalDutyController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            
        }
        [HttpPost]
        public IActionResult List(ApprovalDutyRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = _dbContext.ApprovalDuty.AsQueryable();
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

        //数据入库
        [HttpGet]
        public IActionResult ImportData()
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = _dbContext.ApprovalDuty.AsQueryable();
                CompanyWx wx = new CompanyWx();

                DateTime curDate = DateTime.Now;
                DateTime oldDate = curDate.AddDays(-30);
                string curTimestamp = wx.GetTimeStamp(curDate).ToString();
                string oldTimestamp = wx.GetTimeStamp(oldDate).ToString();

                String tmpid = "3TkapVXjeCqJJytQjtCL2Z5HxoFH8oFQYi4dHoVA";
                var spList = wx.GetApprovalInfo(oldTimestamp, curTimestamp, 0, 100, tmpid);


                foreach (string ordernum in spList)
                {

                    JObject obj = wx.GetApprovalDetail(ordernum);
                    JObject applyData = (JObject)obj["apply_data"];
                    JArray detail = (JArray)applyData["contents"][0]["value"]["children"][0]["list"];

                    String credate = obj["apply_time"].ToString();
                    String type = obj["sp_name"].ToString();
                    String status = obj["sp_status"].ToString();//审批节点状态：1-审批中；2-已同意；3-已驳回；4-已转审


                    String name = "";
                    String temperature = "";
                    String getMask = "";



                    //详情
                    name = detail[0]["value"]["text"].ToString();//名字
                    temperature = detail[1]["value"]["new_number"].ToString();//体温
                    getMask = detail[2]["value"]["selector"]["options"][0]["value"][0]["text"].ToString();//口罩领取

                    String userid = obj["applyer"]["userid"].ToString();
                    String depid = obj["applyer"]["partyid"].ToString();

                    JObject user = wx.GetUser(userid);
                    String username = user["name"].ToString();
                    String dep = wx.GetDepartment(depid);

                    var result = query.Where(x => x.ordernum == ordernum);
                    int resultCount = result.Count();
                    if (resultCount == 0) {
                        var entity = new ApprovalDuty();
                        entity.Guid = new Guid();
                        entity.username = username;
                        entity.department = dep;
                        entity.type = type;
                        entity.ordernum = ordernum;
                        entity.status = wx.RedenerStatus(status);
                        entity.name = name;
                        entity.temperature = temperature;
                        entity.getMask = getMask;
                        entity.credate = wx.GetDateTime(credate);

                        _dbContext.ApprovalDuty.Add(entity);
                    }
                       
                }
                _dbContext.SaveChanges();

                response.SetSuccess();
                return Ok(response);
                

            }
        }
       

       /* [HttpGet]
        public async Task<IActionResult> GetFile(String fileId)
        {
            CompanyWx wx = new CompanyWx();
            var client = new HttpClient();
            var token = wx.Get_Token();
            var Token = token;
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}", Token, fileId);
            var resp = await client.GetAsync(url);
            var con_dis = resp.Content.Headers.Where(o => o.Key == "Content-Disposition").Select(m => m.Value).FirstOrDefault().FirstOrDefault();
            Regex regex = new Regex("filename=(.+);");
            //var filename = regex.Match(con_dis).Groups[1].Value;
            var filename = "1582078959812.jpg";
            var ext = filename.Split('.').Reverse().First();
            new FileExtensionContentTypeProvider().Mappings.TryGetValue("." + ext, out var contenttype);
            var buffer = await resp.Content.ReadAsByteArrayAsync();
            return File(buffer, contenttype, filename);
        }*/

        /*[HttpGet]
        public IActionResult GetFile(string fileId)
        {
            CompanyWx wx = new CompanyWx();
            var fileinfo= wx.DwonloadFile(fileId);
            return Ok();
            *//*if (string.IsNullOrEmpty(filepath)) filepath = "D:\\测试文件.jpg";
            var provider = new FileExtensionContentTypeProvider();
            FileInfo fileInfo = new FileInfo(filepath);
            var ext = fileInfo.Extension;
            new FileExtensionContentTypeProvider().Mappings.TryGetValue(ext, out var contenttype);
            return File(System.IO.File.ReadAllBytes(filepath), contenttype ?? "application/octet-stream", fileInfo.Name);*//*
        }*/


    }
}