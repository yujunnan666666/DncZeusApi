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
using DncZeus.Api.RequestPayload.Sec.Uprg;
using DncZeus.Api.ViewModels.Sec.Uprg;
using System.Collections.Generic;

namespace DncZeus.Api.Controllers.Api.Sec
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [ApiController]
    [ApiVersion("5.0")] //V5
    [Route("api/v5/Sec/[controller]/[action]")]
    public class UprgController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        

        public UprgController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            
        }
        [HttpPost]
        public IActionResult List(UprgRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            { 
                var query = _dbContext.Secuprg.AsQueryable();


                //query = query.Where(x => (x.enabled == payload.enabled));
                query = query.Where(x => x.userid == payload.userid);

                /*if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x =>
                    (
                    x.prgno.Contains(payload.Kw.Trim()) ||
                    x.butno.Contains(payload.Kw.Trim())
                    )
                    );
                }*/
                //query=query.OrderBy("prgorder", false);
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
        public IActionResult Create(UprgCreateViewModelArr model)
        {
            var response = ResponseModelFactory.CreateInstance;
          
            using (_dbContext)
            {
                var prgQuery= _dbContext.Secprg.AsQueryable().Where(x=>x.runtype=="B");
                var query = _dbContext.Secuprg.AsQueryable();
                var userorg = _dbContext.Secuserorg.FirstOrDefault(x => x.userid == model.userid);
                if (userorg == null) {
                    response.SetFailed("用户未绑定组织");
                    return Ok(response);
                }
                var list = query.Where(x => x.userid == model.userid).ToList();
                foreach (Secuprg obj in list) {
                   

                    _dbContext.Secuprg.Remove(obj);
                    _dbContext.SaveChanges();
                }

                for (int i = 0; i < model.arr.Length; i++)
                {
                    var prgid = model.arr[i].prgid;
                    var entList = prgQuery.Where(x => x.mainid == prgid).ToList();

                    //程式子窗口权限继承
                    foreach (var obj in entList) {
                        var entity2 = _mapper.Map<UprgCreateViewModel, Secuprg>(model.arr[i]);
                        entity2.Guid = new Guid();
                        entity2.orgid = userorg.orgid;
                        entity2.userid = model.userid;
                        entity2.prgid = obj.Guid;
                        entity2.credate = DateTime.Now;
                        entity2.creuser = AuthContextService.CurrentUser.DisplayName;

                        _dbContext.Secuprg.Add(entity2);
                        _dbContext.SaveChanges();
                    }

                    var entity = _mapper.Map<UprgCreateViewModel, Secuprg>(model.arr[i]);
                    entity.Guid = new Guid();
                    entity.orgid = userorg.orgid;
                    entity.userid = model.userid;
                    entity.credate = DateTime.Now;
                    entity.creuser = AuthContextService.CurrentUser.DisplayName;

                    _dbContext.Secuprg.Add(entity);
                    _dbContext.SaveChanges();
                }
              
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
                var entity = _dbContext.Secuprg.FirstOrDefault(x => x.Guid == guid);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<Secuprg, UprgCreateViewModel>(entity));
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
        public IActionResult Edit(UprgCreateViewModel model)
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
                var entity = _dbContext.Secuprg.FirstOrDefault(x => x.Guid == model.Guid);
                entity.prgid = model.prgid;



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
                    var sql = string.Format("DELETE FROM Secuprg WHERE Guid IN ({0})", parameterNames);
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
                var sql = string.Format("UPDATE Secuprg SET enabled=@enabled WHERE Guid IN ({0})", parameterNames);

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
        [HttpGet]
        //加载树
        public IActionResult LoadTree()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateInstance;

                var sysList = _dbContext.Secsys.AsQueryable().ToList();
                var funList = _dbContext.Secfun.AsQueryable().ToList();
                var prgList = _dbContext.Secprg.AsQueryable().Where(x => x.runtype == "A" && x.enabled=="Y" && x.prgtype=="0").ToList();



                var btnList = _dbContext.Secprgbut.AsQueryable().ToList();

                var sysListAll = _dbContext.Secsys.AsQueryable().ToList();
                var funListAll = _dbContext.Secfun.AsQueryable().ToList();
                var prgListAll = _dbContext.Secprg.AsQueryable().ToList();
                var btnListAll = _dbContext.Secprgbut.AsQueryable().ToList();

                response.SetData(new { sysList, sysListAll, funList, funListAll ,prgList, prgListAll ,btnList , btnListAll });
                return Ok(response);
            }
            
            
        }
        [HttpGet]
        //加载菜单树
        public IActionResult LoadMenuTree()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateInstance;

                var sysList = _dbContext.Secsys.AsQueryable().OrderBy("sysorder",false).ToList();
                var funList = _dbContext.Secfun.AsQueryable().OrderBy("funorder", false).ToList();
                //var prgList = _dbContext.Secprg.AsQueryable().Where(x => x.runtype == "" && x.enabled == "Y").ToList();
                var prgList = _dbContext.Secprg.AsQueryable().Where(x => x.enabled == "Y").OrderBy("prgorder", false).ToList();
                var btnList = _dbContext.Secprgbut.AsQueryable().OrderBy("butorder", false).ToList();

                

                response.SetData(new { sysList, funList, prgList, btnList});
                return Ok(response);
            }


        }

    }
    
}

