using AutoMapper;
using DncZeus.Api.Entities;
using DncZeus.Api.Entities.SF;
using DncZeus.Api.Entities.Enums;
using DncZeus.Api.Extensions;
using DncZeus.Api.Extensions.AuthContext;
using DncZeus.Api.Extensions.CustomException;
using DncZeus.Api.Extensions.DataAccess;
using DncZeus.Api.Models.Response;
using DncZeus.Api.RequestPayload.Mis.Message;
using DncZeus.Api.ViewModels.Mis.DncMessage;
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
using DncZeus.Api.Entities.Mis;
using DncZeus.Api.Entities.Sec;
using DncZeus.Api.Entities.Pmc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;
using Ninject.Activation.Caching;

namespace DncZeus.Api.Controllers.Api.Mis
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [ApiController]
    [ApiVersion("5.0")] //V5
    [Route("api/v5/Mis/[controller]/[action]")]
    public class MessageController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        

        public MessageController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            
        }
        [HttpPost]
        public IActionResult List(MessageRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                
                var query = _dbContext.Mismessage.AsQueryable().Where(x=>x.receiveUserno == payload.receiveUserno);

                
                query = query.OrderBy("receiveTime", true);
                query = query.OrderBy("readStatus", false);


                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x =>
                    (
                    x.sendUserno.Contains(payload.Kw.Trim()) ||
                    x.sendUsername.Contains(payload.Kw.Trim())
                    )
                    );
                }
                if (payload.status != 99)
                {
                    query = query.Where(x => x.status == payload.status);
                }
                if (payload.readStatus != 99)
                {
                    query = query.Where(x => x.readStatus == payload.readStatus);
                }
                if (payload.mtype != 99)
                {
                    query = query.Where(x => x.mtype == payload.mtype);
                }

                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
               
                
                var totalCount = query.Count();
                //var data = list.Select(_mapper.Map<Mismessage, MessageJsonModel>);


                response.SetData(list, totalCount);

                return Ok(response);

            }

            
        }

        private void sendWxMsg(DncZeusDbContext dbContext, string members,string content)
        {

            var response = ResponseModelFactory.CreateResultInstance;
            CompanyWx wx = new CompanyWx();

            var msgTokenObj = dbContext.MisCode.FirstOrDefault(x => x.code == "msg-token");

            var tokenCreDate= msgTokenObj.credate;
            var token = msgTokenObj.cdesc;

            

                if (token != "0")
            {
                TimeSpan t = Convert.ToDateTime(DateTime.Now) - Convert.ToDateTime(tokenCreDate);
                if (t.TotalHours < 2)
                {
                    wx.SendWxMsg(token, members, content);
                }
                else {
                    string Corpid = "wwb88b795d4ec8d9b4";
                    string Secret = "WldjZLL_r_rWeBCcPY-lWHD8CQYry9o8bSC0nhPrAtI";//审批 密匙
                    token = wx.Get_Token(Corpid, Secret);

                    msgTokenObj.cdesc = token;
                    msgTokenObj.credate = DateTime.Now;
                    dbContext.SaveChanges();

                    wx.SendWxMsg(token, members, content);
                }
            }
            else {
                string Corpid = "wwb88b795d4ec8d9b4";
                string Secret = "WldjZLL_r_rWeBCcPY-lWHD8CQYry9o8bSC0nhPrAtI";//审批 密匙
                token = wx.Get_Token(Corpid, Secret);

                msgTokenObj.cdesc = token;
                msgTokenObj.credate = DateTime.Now;
                dbContext.SaveChanges();

                wx.SendWxMsg(token, members, content);

            }
            
        }
        

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="model">类别视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(MessageCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
          
            using (_dbContext)
            {
              
                var entity = _mapper.Map<MessageCreateViewModel, Mismessage>(model);
                /*entity.moddate=entity.credate = DateTime.Now;
                entity.moduser=entity.creuser = AuthContextService.CurrentUser.DisplayName;*/
                
                _dbContext.Mismessage.Add(entity);
                _dbContext.SaveChanges();

                response.SetSuccess();
                return Ok(response);
            }
        }
        /// <summary>
        /// 批量创建
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult BatchCreate(BatchMessageCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {
                
                var entity = _mapper.Map<BatchMessageCreateViewModel, Mismessage>(model);
                entity.Org = AuthContextService.CurrentUser.OrgID;

                List<String> userArr = new List<String>();
                foreach (Secuser user in model.users) {
                    entity.Guid = new Guid();
                   entity.receiveTime = DateTime.Now;
                   entity.receiveUserno = user.userno;
                   entity.receiveUsername = user.username;
                    _dbContext.Mismessage.Add(entity);
                    _dbContext.SaveChanges();
                    userArr.Add(user.wxId);
                }
                string wxStr = userArr.Join("|");
                sendWxMsg(_dbContext,wxStr, model.message);
                /*entity.moddate=entity.credate = DateTime.Now;
                entity.moduser=entity.creuser = AuthContextService.CurrentUser.DisplayName;*/

                response.SetSuccess();
                return Ok(response);
            }
        }
        /// <summary>
        /// 未上报排程消息提醒
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult CheckUndo(string day)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<String> depArr = new List<String>();
                var strSql = string.Format(@"exec sp_pmc_plannoteList '{0}'", day);
                var query = _dbContext.PmcPlannoteList.FromSql(strSql);
                var list = query.ToList();
                for (var i = 0; i < list.Count; i++)
                {
                    var dep = list[i].department;
                    var prev1 = list[i].prev1;
                    if (prev1 == 0)
                    {
                        depArr.Add(dep);
                    }
                }
                //部门去重
                string[] depArrs= depArr.GroupBy(p => p).Select(p => p.Key).ToArray();
                //根据部门名称获取人员微信号
                var userList = _dbContext.Secuser.AsQueryable().Where(m => depArrs.Contains(m.depname)).ToList();

                List<String> wxArr = new List<String>();
                for (var i = 0; i < userList.Count; i++)
                {
                    var wxId = userList[i].wxId;
                    wxArr.Add(wxId);
                }
                //微信号去重
                string[] wxArrs =  wxArr.GroupBy(p => p).Select(p => p.Key).ToArray();

                string wxStr = wxArrs.Join("|");
                sendWxMsg(_dbContext, wxStr, "MIS系统提示：项目计划排程昨日报告未处理,请及时处理!");



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
                var entity = _dbContext.Mismessage.FirstOrDefault(x => x.Guid == guid);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<Mismessage, MessageCreateViewModel>(entity));
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
        public IActionResult Edit(MessageCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            
            using (_dbContext)
            {
                /*if (_dbContext.Mismessage.Count(x => x.Code == model.Code && x.ID != model.ID) > 0)
                {
                    response.SetFailed("类别已存在");
                    return Ok(response);
                }*/
                var entity = _dbContext.Mismessage.FirstOrDefault(x => x.Guid == model.Guid);
               /* entity.Code = model.Code;
                entity.Icon = model.Icon;
                entity.Name = model.Name;
                entity.trseq = model.trseq;
                entity.credate = model.credate;
                entity.creuser = model.creuser;
                entity.moddate = DateTime.Now;*/
                //entity.moduser = entity.creuser = AuthContextService.CurrentUser.DisplayName;

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
                    var sql = string.Format("DELETE FROM Mismessage WHERE Guid IN ({0})", parameterNames);
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
                case "noread": //未读
                    response = UpdateReadStatus(1, ids);
                    break;
                case "isread": //已读
                    response = UpdateReadStatus(2, ids);
                    break;
                case "ishandle": //已处理
                    response = UpdateStatus(2, ids);
                    break;
               /* case "Valid": //有效
                    response = UpdateIsValid(isValid.Valid, ids);
                    break;*/
                default:
                    break;
            }
            return Ok(response);
        }
        /// <summary>
        /// 阅读状态
        /// </summary>
        /// <returns></returns>
        private ResponseModel UpdateReadStatus(int readStatus, string ids)
        {
            using (_dbContext)
            {
                var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                var sql = string.Format("UPDATE Mismessage SET readStatus=@readStatus WHERE Guid IN ({0})", parameterNames);

                parameters.Add(new SqlParameter("@readStatus", readStatus));
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
        /// 已处理未处理
        /// </summary>
        /// <returns></returns>
        private ResponseModel UpdateStatus(int status, string ids)
        {
            using (_dbContext)
            {
                var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                var sql = string.Format("UPDATE Mismessage SET status=@status WHERE receiveUserno IN ({0})", parameterNames);

                parameters.Add(new SqlParameter("@status", status));
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

        [HttpGet]
        public IActionResult GetUnreadCount(string userno)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = _dbContext.Mismessage.AsQueryable().Where(x => x.receiveUserno == userno);
                    query = query.Where(x => x.readStatus == 1);   
                var totalCount = query.Count();
                //var data = list.Select(_mapper.Map<Mismessage, MessageJsonModel>);

                response.SetData(totalCount);
                return Ok(response);

            }


        }



    }
}