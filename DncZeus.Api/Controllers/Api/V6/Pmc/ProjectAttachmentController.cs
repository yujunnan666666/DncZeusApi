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
using DncZeus.Api.RequestPayload.Sec.User;
using DncZeus.Api.ViewModels.Sec.User;
using System.Collections.Generic;
using DncZeus.Api.ViewModels.Sec.Group;
using File = DncZeus.Api.Entities.File;
using DncZeus.Api.RequestPayload.Pmc.Projectattachment;
using DncZeus.Api.ViewModels.Pmc.Projectattachment;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using DncZeus.Api.Entities.Pmc;

namespace DncZeus.Api.Controllers.Api.Pmc
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [ApiController]
    [ApiVersion("6.0")] //V5
    [Route("api/v6/Pmc/[controller]/[action]")]
    public class ProjectAttachmentController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        private IHostingEnvironment _host;

        public ProjectAttachmentController(DncZeusDbContext dbContext, IMapper mapper, IHostingEnvironment host)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _host = host;
        }
        [HttpPost]
        public IActionResult List(ProjectattachmentRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                //技术中心附件 && 产品文件
                if (payload.attType == 5 || payload.attType == 6)
                {
                    var  query = _dbContext.PmcProjectline.AsQueryable().Where(x => x.projectsGuid == payload.projectGuid).GroupJoin(_dbContext.PmcProjectAttachment, line => line.Guid, att => att.parentGuid, (line, att) => new
                    {
                        Line=line,
                        Att=att
                    }).SelectMany(x => x.Att, (a, b) => new
                    {
                        a.Line,
                        PmcAtt=b
                    }).Where(x=>x.PmcAtt.attType== payload.attType).GroupJoin(_dbContext.MisAttachment, a => a.PmcAtt.attGuid, b => b.Guid, (a, b) => new {
                        a.Line,
                        a.PmcAtt,
                        MisAtt = b
                    }).SelectMany(a => a.MisAtt, (a, b) => new {
                         a.Line.styleNO,
                         a.Line.custitemname,
                        a.PmcAtt.Guid,
                        a.PmcAtt.attGuid,
                        a.PmcAtt.parentType,
                        a.PmcAtt.parentGuid,
                        a.PmcAtt.attType,
                        a.PmcAtt.creuser,
                        a.PmcAtt.credate,
                        a.PmcAtt.fileName,
                        fileInfo=b 
                    });
                    query = query.OrderBy("styleNO", true);
                    query = query.OrderBy("fileName", true);

                    var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                    var totalCount = query.Count();
                    response.SetData(list, totalCount);
                    return Ok(response);
                }
                //其他附件
                else {

                    var query = _dbContext.PmcProjectAttachment.AsQueryable().Where(x => x.parentGuid == payload.projectGuid && x.attType == payload.attType).GroupJoin(_dbContext.MisAttachment, a => a.attGuid, b => b.Guid, (a, b) => new
                    {
                        PmcAtt = a,
                        MisAtt = b
                    }).SelectMany(a => a.MisAtt, (a, b) => new { 
                        a.PmcAtt.Guid,
                        a.PmcAtt.attGuid,
                        a.PmcAtt.parentType,
                        a.PmcAtt.parentGuid,
                        a.PmcAtt.attType,
                        a.PmcAtt.creuser,
                        a.PmcAtt.credate,
                        a.PmcAtt.fileName,
                        fileInfo = b
                    });
                    query = query.OrderBy("fileName", true);
                    var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                    var totalCount = query.Count();
                    response.SetData(list, totalCount);
                    return Ok(response);
                }
               
         
                
                
               
                
               

            }
        }

        /// <summary>
        /// 创建类别
        /// </summary>
        /// <param name="model">类别视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(ProjectattachmentCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
          
            using (_dbContext)
            {
               
                var entity = _mapper.Map<ProjectattachmentCreateViewModel, PmcProjectAttachment>(model);


                entity.Guid = new Guid();
                entity.credate = DateTime.Now;
               entity.creuser = AuthContextService.CurrentUser.DisplayName;

                _dbContext.PmcProjectAttachment.Add(entity);
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
                var entity = _dbContext.PmcProjectAttachment.FirstOrDefault(x => x.Guid == guid);
                var response = ResponseModelFactory.CreateInstance;
                if (entity.parentType == 1)
                {
                    var entity2 = _dbContext.PmcProjects.FirstOrDefault(x => x.Guid == entity.parentGuid);
                    response.SetSuccess(entity2.Name);
                }
                else {
                    var entity2 = _dbContext.PmcProjectline.FirstOrDefault(x => x.Guid == entity.parentGuid);
                    response.SetSuccess(entity2.styleNO);
                }
                
                response.SetData(_mapper.Map<PmcProjectAttachment, ProjectattachmentCreateViewModel>(entity));
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
        public IActionResult Edit(ProjectattachmentCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
           /* if (model.Code.Trim().Length <= 0)
            {
                response.SetFailed("请输入名称");
                return Ok(response);
            }*/
            using (_dbContext)
            {
                /*if (_dbContext.MisCatalog.Count(x => x.Code == model.Code && x.ID != model.ID) > 0)
                {
                    response.SetFailed("类别已存在");
                    return Ok(response);
                }*/
                var entity = _dbContext.PmcProjectAttachment.FirstOrDefault(x => x.Guid == model.Guid);
               

                entity.parentType = model.parentType;
                entity.parentGuid = model.parentGuid;
                entity.attType = model.attType;
                entity.fileName = model.fileName;
                entity.attGuid = model.attGuid; 
                
                
                

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
                    /*var idsArr = ids.Split(",");*/
                    var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                    var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                    var sql = string.Format("DELETE FROM PmcProjectAttachment WHERE Guid IN ({0})", parameterNames);
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
                /*case "Cfm": //审核
                    response = UpdateStatus(Status.Cfm, ids);
                    break;
                case "Open": //弃审
                    response = UpdateStatus(Status.Open, ids);
                    break;*/
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
                var sql = string.Format("UPDATE PmcProjectAttachment SET enabled=@enabled WHERE Guid IN ({0})", parameterNames);

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
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult UploadFile()
        {
            //var response = ResponseModelFactory.CreateInstance;
            var response = ResponseModelFactory.CreateResultInstance;

            var oFile = Request.Form.Files["file"];
            string fn = oFile.FileName.ToString();
            
            string ext=fn.Substring(fn.IndexOf("."),fn.Length-fn.IndexOf("."));
            string filename = fn.Substring(0,fn.IndexOf("."));

            Guid guid = Guid.NewGuid();
            string uuid = guid.ToString("N");
            string folderName = "PmcAttachment";
            string yyyy = DateTime.Now.ToString("yyyy");
            string mmdd = DateTime.Now.ToString("MMdd");
            string pathUrl= folderName+ "/" + yyyy + "/" + mmdd + "/" + uuid ;

            var path = Path.Combine(_host.ContentRootPath + "\\Upload\\file\\"+ pathUrl);
                 var allPath = Path.Combine(path, fn);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            using (var stream = System.IO.File.Create(allPath))
                 {
                      oFile.CopyTo(stream);
                     stream.Close();
                     stream.Dispose();
                 }

            MisAttachment Att = new MisAttachment();
            Att.Guid = guid;
            Att.mainUrl = "/Upload/file/";
            Att.folderName = folderName;
            Att.yyyy = yyyy;
            Att.mmdd = mmdd;
            Att.pathUrl = pathUrl+"/"+fn;
            Att.fillName = filename;
            Att.fillType = ext;
            Att.fillSize = int.Parse(oFile.Length.ToString());
            Att.credate = DateTime.Now;
            Att.creuser = AuthContextService.CurrentUser.DisplayName;

            _dbContext.MisAttachment.Add(Att);
            _dbContext.SaveChanges();

           
            

            var filePath = "/Upload/file/" + pathUrl + "/" + fn;

            JObject info = new JObject(){
                        new JProperty("attGuid",guid),//
                        new JProperty("path",filePath),//
                        new JProperty("fileName",fn),//
                       
                    };

            var returnJson = (JObject)JsonConvert.DeserializeObject("{'data':" + info + "}");
            return Ok(returnJson);
        }

        public class uploadResult
        {
            public string fileName { get; set; }
            public string error { get; set; }
        }

        /// <summary>
        /// 获取附件基本档详情
        /// </summary>
        
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetFile(Guid guid)
        {
            using (_dbContext)
            {
                var entity = _dbContext.MisAttachment.FirstOrDefault(x => x.Guid == guid);
                var response = ResponseModelFactory.CreateInstance;
               
                response.SetData(entity);
                return Ok(response);
            }
        }

    }
}