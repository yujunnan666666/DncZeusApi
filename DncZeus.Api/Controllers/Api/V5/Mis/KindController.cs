using AutoMapper;
using DncZeus.Api.Entities;
using DncZeus.Api.Entities.SF;
using DncZeus.Api.Entities.Enums;
using DncZeus.Api.Extensions;
using DncZeus.Api.Extensions.AuthContext;
using DncZeus.Api.Extensions.CustomException;
using DncZeus.Api.Extensions.DataAccess;
using DncZeus.Api.Models.Response;
using DncZeus.Api.RequestPayload.Mis.Kind;
using DncZeus.Api.ViewModels.Mis.DncKind;
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




namespace DncZeus.Api.Controllers.Api.Mis
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [ApiController]
    [ApiVersion("5.0")] //V5
    [Route("api/v5/Mis/[controller]/[action]")]
    public class KindController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        

        public KindController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            
        }
        [HttpPost]
        public IActionResult List(KindRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
               // var query = _dbContext.MisCkind.
                var query = _dbContext.MisCkind.AsQueryable();
                /*if (!string.IsNullOrEmpty(payload.ckind))
                {
                    query = query.Where(x => x.ckind.Contains(payload.ckind.Trim()));
                }
                if (!string.IsNullOrEmpty(payload.ckdesc))
                {
                    query = query.Where(x => x.ckdesc.Contains(payload.ckdesc.Trim()));
                }*/
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x =>
                    (
                    x.ckind.Contains(payload.Kw.Trim()) ||
                    x.ckdesc.Contains(payload.Kw.Trim()) 
                    )
                    );
                }

                if (payload.isOrgValid != "-1"&& payload.isOrgValid != null)
                {
                    query = query.Where(x => x.isOrgValid == payload.isOrgValid);
                }
                if (payload.isEnabled != "-1" && payload.isEnabled != null)
                {
                    query = query.Where(x => x.isEnabled == payload.isEnabled);
                }
                query = query.OrderBy("trseq", false);
                var list = query.Paged(payload.CurrentPage, payload.PageSize).Include(x => x.MisCatalog).ToList();
               
                
                var totalCount = query.Count();
                //var data = list.Select(_mapper.Map<MisCatalog, CategoryJsonModel>);

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
        public IActionResult Create(KindCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            /*if (model.Code.Trim().Length <= 0)
            {
                response.SetFailed("请输入图标名称");
                return Ok(response);
            }*/
            using (_dbContext)
            {
                /* if (_dbContext.MisCatalog.Count(x => x.Code == model.Code) > 0)
                 {
                     response.SetFailed("图标已存在");
                     return Ok(response);
                 }*/
                var MisCatalogEntity = _dbContext.MisCatalog.FirstOrDefault(x => x.ID == model.MisCatalogId);
                //var entity = _mapper.Map<KindCreateViewModel, MisCkind>(model);
                var entity = new MisCkind();
                entity.ckind = model.ckind;
                entity.ckdesc = model.ckdesc;
                entity.clen = model.clen;
                entity.isEnabled = model.isEnabled;
                entity.isOrgValid = model.isOrgValid;
                entity.trseq = model.trseq;
                entity.moddate=entity.credate = DateTime.Now;
                entity.moduser=entity.creuser = AuthContextService.CurrentUser.DisplayName;
                entity.MisCatalog = MisCatalogEntity;


                _dbContext.MisCkind.Add(entity);
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
        public IActionResult Detail(int id)
        {
            using (_dbContext)
            {
                var query = _dbContext.MisCkind.AsQueryable();
                    query = query.Where(x => x.ID==id);
                var obj = query.Include(x => x.MisCatalog).ToList()[0];

               // var entity = _dbContext.MisCkind.FirstOrDefault(x => x.ID == id);

                var response = ResponseModelFactory.CreateInstance;
                response.SetData(obj);
               // response.SetData(_mapper.Map<MisCkind, KindCreateViewModel>(entity));
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
        public IActionResult Edit(KindCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (model.ckdesc.Trim().Length <= 0)
            {
                response.SetFailed("请输入名称");
                return Ok(response);
            }
            using (_dbContext)
            {
                /*if (_dbContext.MisCkind.Count(x => x.ckind == model.ckind && x.ID != model.ID) > 0)
                {
                    response.SetFailed("类别已存在");
                    return Ok(response);
                }*/
                var MisCatalogEntity = _dbContext.MisCatalog.FirstOrDefault(x => x.ID == model.MisCatalogId);
                var entity = _dbContext.MisCkind.FirstOrDefault(x => x.ID == model.ID);
                entity.ckind = model.ckind;
                entity.Icon = model.Icon;
                entity.MisCatalog = MisCatalogEntity;
                entity.ckdesc = model.ckdesc;
                entity.clen = model.clen;
                entity.trseq = model.trseq;
                entity.isEnabled = model.isEnabled;
                entity.isOrgValid = model.isOrgValid;
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
                try
                {
                    var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                    var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                    var sql = string.Format("DELETE FROM MisCkind WHERE Id IN ({0})", parameterNames);
                    int li_ret = _dbContext.Database.ExecuteSqlCommand(sql, parameters);

                    
                    response.SetData(new
                    {
                        affectCount = li_ret
                    });
                    return response;
                }
                catch (Exception e) {
                    response.SetFailed("当前分类已使用，无法删除！");
                    return response;
                }
                
            }
        }
        /// <summary>
        /// 删除及对应的扩展权限字段
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel DeleteUnder(string id)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateInstance;
                try
                {
                    //var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                    //var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                    var extend_sql = string.Format("DELETE FROM MisCkindExtend WHERE MisCKindId={0}",id);
                    var power_sql= string.Format("DELETE FROM MisCkindPower WHERE MisKindID ={0}",id);
                    var value_sql = string.Format("DELETE FROM MisCode WHERE MisCKindID ={0}", id);
                    var kind_sql = string.Format("DELETE FROM MisCkind WHERE ID={0}", id);
                    var extendCommand = _dbContext.Database.ExecuteSqlCommand(extend_sql);
                    var powerCommand = _dbContext.Database.ExecuteSqlCommand(power_sql);
                    var valueCommand = _dbContext.Database.ExecuteSqlCommand(value_sql);
                    var kindCommand = _dbContext.Database.ExecuteSqlCommand(kind_sql);



                    response.SetSuccess("操作成功！");
                    return response;
                }
                catch (Exception e)
                {
                    response.SetFailed("操作失败！");
                    return response;
                }

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