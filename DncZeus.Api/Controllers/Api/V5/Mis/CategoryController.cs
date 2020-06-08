using AutoMapper;
using DncZeus.Api.Entities;
using DncZeus.Api.Entities.SF;
using DncZeus.Api.Entities.Enums;
using DncZeus.Api.Extensions;
using DncZeus.Api.Extensions.AuthContext;
using DncZeus.Api.Extensions.CustomException;
using DncZeus.Api.Extensions.DataAccess;
using DncZeus.Api.Models.Response;
using DncZeus.Api.RequestPayload.Mis.Category;
using DncZeus.Api.ViewModels.Mis.DncCategory;
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
    public class CategoryController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        

        public CategoryController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            
        }
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult List(CategoryRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = _dbContext.MisCatalog.AsQueryable();


                if (payload.isEnabled != "99") {
                   query = query.Where(x => x.isEnabled == payload.isEnabled);
                }

                /*if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x =>
                    (
                    x.Name.Contains(payload.Kw.Trim()) ||
                    x.Code.Contains(payload.Kw.Trim())
                    )
                    );
                }*/
                /*query = query.OrderBy("trseq", false);*/
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
               
                
                var totalCount = query.Count();
                

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
        public IActionResult Create(CategoryCreateViewModel model)
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
                var entity = _mapper.Map<CategoryCreateViewModel, MisCatalog>(model);
                entity.moddate=entity.credate = DateTime.Now;
                entity.moduser=entity.creuser = AuthContextService.CurrentUser.DisplayName;
                
                _dbContext.MisCatalog.Add(entity);
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
                var entity = _dbContext.MisCatalog.FirstOrDefault(x => x.ID == id);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<MisCatalog, CategoryCreateViewModel>(entity));
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
        public IActionResult Edit(CategoryCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (model.Code.Trim().Length <= 0)
            {
                response.SetFailed("请输入名称");
                return Ok(response);
            }
            using (_dbContext)
            {
                if (_dbContext.MisCatalog.Count(x => x.Code == model.Code && x.ID != model.ID) > 0)
                {
                    response.SetFailed("类别已存在");
                    return Ok(response);
                }
                var entity = _dbContext.MisCatalog.FirstOrDefault(x => x.ID == model.ID);
                entity.Code = model.Code;
                entity.Icon = model.Icon;
                entity.Name = model.Name;
                entity.isEnabled = model.isEnabled;
                entity.trseq = model.trseq;
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
                    var sql = string.Format("DELETE FROM MisCatalog WHERE ID IN ({0})", parameterNames);
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
                var sql = string.Format("UPDATE MisCatalog SET isEnabled=@isValid WHERE Id IN ({0})", parameterNames);

                parameters.Add(new SqlParameter("@isValid", isValid));
               
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