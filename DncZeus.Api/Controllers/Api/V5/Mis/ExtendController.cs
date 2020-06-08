using AutoMapper;
using DncZeus.Api.Entities;
using DncZeus.Api.Entities.SF;
using DncZeus.Api.Entities.Enums;
using DncZeus.Api.Extensions;
using DncZeus.Api.Extensions.AuthContext;
using DncZeus.Api.Extensions.CustomException;
using DncZeus.Api.Extensions.DataAccess;
using DncZeus.Api.Models.Response;
using DncZeus.Api.RequestPayload.Mis.Extend;
using DncZeus.Api.ViewModels.Mis.DncExtend;
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
using DncZeus.Api.ViewModels.Mis.DncPower;

namespace DncZeus.Api.Controllers.Api.Mis
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [ApiController]
    [ApiVersion("5.0")] //V5
    [Route("api/v5/Mis/[controller]/[action]")]
    public class ExtendController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        

        public ExtendController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            
        }
        [HttpPost]
        public IActionResult List(ExtendRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = _dbContext.MisCkindExtend.AsQueryable();
                
                    query = query.Where(x => x.MisCKindId == payload.misCKindId);
                /* if (!string.IsNullOrEmpty(payload.ckind))
                 {
                     query = query.Where(x => x.ckind.Contains(payload.ckind.Trim()));
                 }
                 if (!string.IsNullOrEmpty(payload.ckdesc))
                 {
                     query = query.Where(x => x.ckdesc.Contains(payload.ckdesc.Trim()));
                 }
                 if (payload.isOrgValid != -1)
                 {
                     query = query.Where(x => x.isOrgValid == payload.isOrgValid);
                 }
                 if (payload.isEnabled != -1)
                 {
                     query = query.Where(x => x.isEnabled == payload.isEnabled);
                 }*/


                var list = query.Paged(payload.CurrentPage, payload.PageSize).OrderBy("Num", false).ToList();
               
                
                var totalCount = query.Count();
                //var data = list.Select(_mapper.Map<MisCatalog, CategoryJsonModel>);

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
        public IActionResult Create(ExtendCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
           
            using (_dbContext)
            {
               /* if (_dbContext.MisCatalog.Count(x => x.Code == model.Code) > 0)
                {
                    response.SetFailed("图标已存在");
                    return Ok(response);
                }*/
                var entity = _mapper.Map<ExtendCreateViewModel, MisCkindExtend>(model);

                var query = _dbContext.MisCkindExtend.AsQueryable();
                var list = query.Where(x => x.MisCKindId == model.MisCKindId).Where(x => x.Num == model.Num).ToList();

                if (list.Count > 0)
                {
                    //当前目录扩展序号已存在
                    response.SetFailed("扩展序号已存在");
                }
                else {
                    entity.moddate = entity.credate = DateTime.Now;
                    entity.moduser = entity.creuser = AuthContextService.CurrentUser.DisplayName;
                    _dbContext.MisCkindExtend.Add(entity);
                    _dbContext.SaveChanges();
                    response.SetSuccess();
                }
                
                return Ok(response);
            }
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id">类别ID</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Detail(int id)
        {
            using (_dbContext)
            {
                var entity = _dbContext.MisCkind.FirstOrDefault(x => x.ID == id);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<MisCkind, ExtendCreateViewModel>(entity));
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
        public IActionResult Edit(ExtendCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            /*if (model.ckdesc.Trim().Length <= 0)
            {
                response.SetFailed("请输入名称");
                return Ok(response);
            }*/
            using (_dbContext)
            {
                /*if (_dbContext.MisCkind.Count(x => x.ckind == model.ckind && x.ID != model.ID) > 0)
                {
                    response.SetFailed("类别已存在");
                    return Ok(response);
                }*/
                var query = _dbContext.MisCkindExtend.AsQueryable();
                var list = query.Where(x => x.MisCKindId == model.MisCKindId).Where(x => x.Num == model.Num).ToList();
                Boolean isValid = true;
                if (list.Count > 0)
                {
                    if (list[0].ID != model.ID)
                    {
                        //当前目录扩展序号已存在
                        response.SetFailed("扩展序号已存在");
                        isValid = false;
                    }
                }
               
                if (isValid) {
                    var entity = _dbContext.MisCkindExtend.FirstOrDefault(x => x.ID == model.ID);
                    entity.MisCKindId = model.MisCKindId;
                    entity.colName = model.colName;
                    entity.Num = model.Num;
                    entity.colType = model.colType;
                    entity.isEnabled = model.isEnabled;
                    entity.sourceId = model.sourceId;

                    entity.credate = model.credate;
                    //entity.creuser = model.creuser;
                    entity.moddate = DateTime.Now;
                    entity.moduser = AuthContextService.CurrentUser.DisplayName;

                    _dbContext.SaveChanges();
                    response.SetSuccess();
                }

                
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
                var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                var sql = string.Format("DELETE FROM MisCkindExtend WHERE Id IN ({0})", parameterNames);
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
                /* case "Cfm": //审核
                     response = UpdateStatus(Status.Cfm, ids);
                     break;
                 case "Open": //弃审
                     response = UpdateStatus(Status.Open, ids);
                     break;*/
                case "Invalid": //失效
                    response = UpdateIsValid(0, ids);
                    break;
                case "Valid": //有效
                    response = UpdateIsValid(1, ids);
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
        private ResponseModel UpdateIsValid(int isValid, string ids)
        {
            using (_dbContext)
            {
                var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                var sql = string.Format("UPDATE MisCkind SET isEnabled=@isValid WHERE Id IN ({0})", parameterNames);

                parameters.Add(new SqlParameter("@isValid", isValid));
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