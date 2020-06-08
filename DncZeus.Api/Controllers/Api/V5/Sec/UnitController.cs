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
using DncZeus.Api.RequestPayload.Sec.Unit;
using DncZeus.Api.ViewModels.Sec.Unit;

namespace DncZeus.Api.Controllers.Api.Sec
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [ApiController]
    [ApiVersion("5.0")] //V5
    [Route("api/v5/Sec/[controller]/[action]")]
    public class UnitController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        

        public UnitController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            
        }
        [HttpPost]
        public IActionResult List(UnitRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = _dbContext.Secunit.AsQueryable();
                /*if (!string.IsNullOrEmpty(payload.Code))
                {
                    query = query.Where(x => x.Code.Contains(payload.Code.Trim()));
                }
                if (!string.IsNullOrEmpty(payload.Name))
                {
                    query = query.Where(x => x.Name.Contains(payload.Name.Trim()));
                }*/

                query = query.Where(x => (x.enabled == payload.enabled));

                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x =>
                    (
                    x.unitno.Contains(payload.Kw.Trim()) ||
                    x.unitname.Contains(payload.Kw.Trim())
                    )
                    );
                }
                query=query.OrderBy("unitorder", false);
               // query = query.OrderBy("trseq", false);
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
               
                
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
        public IActionResult Create(UnitCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
          
            using (_dbContext)
            {
               
                var entity = _mapper.Map<UnitCreateViewModel, Secunit>(model);
                entity.Guid = new Guid();
                
                _dbContext.Secunit.Add(entity);
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
                var entity = _dbContext.Secunit.FirstOrDefault(x => x.Guid == guid);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<Secunit, UnitCreateViewModel>(entity));
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
        public IActionResult Edit(UnitCreateViewModel model)
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
                var entity = _dbContext.Secunit.FirstOrDefault(x => x.Guid == model.Guid);
                entity.flag = model.flag;
                entity.unitno = model.unitno;
                entity.unitname = model.unitname;
                entity.unitorder = model.unitorder;
                entity.enabled = model.enabled; 
                
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
                    var sql = string.Format("DELETE FROM Secunit WHERE Guid IN ({0})", parameterNames);
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
                var sql = string.Format("UPDATE Secunit SET enabled=@enabled WHERE Guid IN ({0})", parameterNames);

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