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
using DncZeus.Api.RequestPayload.Pmc.Projectline;
using DncZeus.Api.ViewModels.Pmc.Projectline;
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
    public class ProjectlineController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        

        public ProjectlineController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            
        }
        [HttpPost]
        public IActionResult List(ProjectlineRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = _dbContext.PmcProjectline.AsQueryable().Where(x=>x.projectsGuid==payload.projectsGuid);
              

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
        /// 获取附件基本档详情
        /// </summary>

        private string GetFile(Guid guid)
        {
            var entity = _dbContext.MisAttachment.FirstOrDefault(x => x.Guid == guid);
            string url = entity.mainUrl + entity.pathUrl;
            return url;
        }

        /// <summary>
        /// 创建类别
        /// </summary>
        /// <param name="model">类别视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(ProjectlineCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
          
            using (_dbContext)
            {
               
                var entity = _mapper.Map<ProjectlineCreateViewModel, PmcProjectline>(model);

                /*if (_dbContext.PmcProjectline.Count(x => x.Code == model.Code) > 0)
                {
                    response.SetFailed("编号已存在");
                    return Ok(response);
                }*/
                entity.Guid = new Guid();
                entity.moddate = entity.credate = DateTime.Now;
                entity.moduser = entity.creuser = AuthContextService.CurrentUser.DisplayName;

                _dbContext.PmcProjectline.Add(entity);
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
                var entity = _dbContext.PmcProjectline.FirstOrDefault(x => x.Guid == guid);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<PmcProjectline, ProjectlineCreateViewModel>(entity));
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
        public IActionResult Edit(ProjectlineCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
         
            using (_dbContext)
            {
                
                var entity = _dbContext.PmcProjectline.FirstOrDefault(x => x.Guid == model.Guid);
            
                entity.custitemcode = model.custitemcode;
                entity.custitemname = model.custitemname;
                entity.styleNO = model.styleNO; 
                entity.imageNO = model.imageNO; 
                entity.factory = model.factory; 
                entity.desc = model.desc; 
                entity.qty = model.qty; 
                entity.price = model.price; 
                entity.amount = model.amount; 
                entity.reqDate = model.reqDate; 
                entity.remark = model.remark; 
               

                entity.moddate  = DateTime.Now;
                entity.moduser  = AuthContextService.CurrentUser.DisplayName;

                _dbContext.SaveChanges();
                response.SetSuccess();
                return Ok(response);
            }
        }
        /// <summary>
        /// 添加图片
        /// </summary>
        /// <param name="model">图标视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult AddImages(ProjectlineImgViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {

                var entity = _dbContext.PmcProjectline.FirstOrDefault(x => x.Guid == model.Guid);
                var imgSrc = model.attGuids.Split(",");
                var imgSrc2 = model.attGuids2.Split(",");
                string src = "";
                string src2 = "";
                if (model.attGuids != "") {
                    foreach (String str in imgSrc)
                    {
                        var imgsrc = GetFile(new Guid(str));
                        src = src + imgsrc + ",";
                    }
                    src = src.Substring(0, src.Length - 1);
                }
                if (model.attGuids2 != "")
                {
                    foreach (String str in imgSrc2)
                    {
                        var imgsrc = GetFile(new Guid(str));
                        src2 = src2 + imgsrc + ",";
                    }
                    src2 = src2.Substring(0, src2.Length - 1);
                }
                   
                entity.images = model.attGuids;
                entity.imgSrc = src;
                entity.otherImages = model.attGuids2;
                entity.otherImgSrc = src2;



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
                    var sql = string.Format("DELETE FROM PmcProjectline WHERE Guid IN ({0})", parameterNames);
                    int li_ret = _dbContext.Database.ExecuteSqlCommand(sql, parameters);

                    //关联删除
                    var sql2 = string.Format("DELETE FROM PmcProjectitem WHERE projectlineGuid IN ({0})", parameterNames);
                    int li_ret2 = _dbContext.Database.ExecuteSqlCommand(sql2, parameters);
                    var sql3 = string.Format("DELETE FROM WjgcPlanfllow WHERE projectlineGuid IN ({0})", parameterNames);
                    int li_ret3 = _dbContext.Database.ExecuteSqlCommand(sql3, parameters);

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
                        sql = "UPDATE PmcProjectline SET status=2, cfmuser='{1}', cfmdate=getdate() WHERE  Guid IN ({0})";
                        sql = string.Format(sql, parameterNames, AuthContextService.CurrentUser.DisplayName);
                    }
                    else
                    {
                        sql = "UPDATE PmcProjectline SET status=3, cfmuser='{1}',cfmdate=getdate() WHERE  Guid IN ({0})";
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
                var sql = string.Format("UPDATE PmcProjectline SET enabled=@enabled WHERE Guid IN ({0})", parameterNames);

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