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
using DncZeus.Api.RequestPayload.Sec.Prg;
using DncZeus.Api.ViewModels.Sec.Prg;
using Newtonsoft.Json.Linq;

namespace DncZeus.Api.Controllers.Api.Sec
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [ApiController]
    [ApiVersion("5.0")] //V5
    [Route("api/v5/Sec/[controller]/[action]")]
    public class PrgController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        

        public PrgController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            
        }
        [HttpPost]
        public IActionResult List(PrgRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = _dbContext.Secprg.AsQueryable();
               

                //query = query.Where(x => (x.enabled == payload.enabled));

                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x =>
                    (
                    x.prgno.Contains(payload.Kw.Trim()) ||
                    x.prgname.Contains(payload.Kw.Trim())
                    )
                    );
                }
                if (!string.IsNullOrEmpty(payload.runtype))
                {
                    query = query.Where(x => x.runtype == payload.runtype);
                }
                query =query.OrderBy("prgorder", false);
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
        public IActionResult Create(PrgCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
          
            using (_dbContext)
            {
               
                var entity = _mapper.Map<PrgCreateViewModel, Secprg>(model);

                if (_dbContext.Secprg.Count(x => x.prgno == model.prgno) > 0)
                {
                    response.SetFailed("程式编号已存在");
                    return Ok(response);
                }
               
                entity.Guid = new Guid();

                //子窗口 继承父窗口配置按钮
                if (model.runtype == "B") {
                    extendPrgbut(_dbContext,entity.Guid,model.mainid);
                }

                _dbContext.Secprg.Add(entity);
                _dbContext.SaveChanges();

                response.SetSuccess();
                response.SetData(entity.Guid);
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
                var entity = _dbContext.Secprg.FirstOrDefault(x => x.Guid == guid);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(_mapper.Map<Secprg, PrgCreateViewModel>(entity));
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
        public IActionResult Edit(PrgCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
           /* if (model.Code.Trim().Length <= 0)
            {
                response.SetFailed("请输入名称");
                return Ok(response);
            }*/
            using (_dbContext)
            {
                if (_dbContext.Secprg.Count(x => x.prgno == model.prgno && x.Guid != model.Guid) > 0)
                {
                    response.SetFailed("编号已存在");
                    return Ok(response);
                }
                var entity = _dbContext.Secprg.FirstOrDefault(x => x.Guid == model.Guid);
                entity.prgno = model.prgno;
                entity.prgname = model.prgname;
                entity.program = model.program;
                entity.prgcomp = model.prgcomp;
                entity.runtype = model.runtype;
                entity.enabled = model.enabled;
                entity.prgtype = model.prgtype;
                entity.prgorder = model.prgorder;
                entity.sysid = model.sysid;
                entity.funid = model.funid;
                entity.icon = model.icon;

                if (model.runtype == "A")
                {
                    //主窗口
                    entity.mainid = null;
                    clearExtendPrgbut(_dbContext, entity.Guid);
                }
                else {
                    //子窗口
                    entity.mainid = model.mainid;

                    //继承父级按钮权限
                    extendPrgbut(_dbContext,entity.Guid, entity.mainid);

                    //移除配置按钮
                    /*var list = _dbContext.Secprgbut.AsQueryable().Where(x=>x.prgid== entity.Guid).ToList();
                    foreach(Secprgbut obj in list) {
                        _dbContext.Secprgbut.Remove(obj);
                    }*/
                    
                }



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
                    var sql = string.Format("DELETE FROM Secprg WHERE Guid IN ({0})", parameterNames);
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
                var sql = string.Format("UPDATE Secprg SET enabled=@enabled WHERE Guid IN ({0})", parameterNames);

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
        public IActionResult GetPrgButtonList(Guid sysid, Guid funid, Guid prgid)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = _dbContext.Secprgbut.AsQueryable();
                    query = query.Where(x => (x.sysid == sysid) && (x.funid == funid) && (x.prgid == prgid));
                var list = query.ToList();
                response.SetData(list);
                return Ok(response);

            }
        }
        /// <summary>
        /// 创建程式按钮
        /// </summary>
        /// <param name="model">类别视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult CreatePrgButton(PrgButtonCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {
                var entity = new Secprgbut(); 
                entity.sysid = model.sysid;
                entity.funid = model.funid;
                entity.prgid = model.prgid;
                entity.butorder = model.butorder;
                for (int i = 0; i < model.btns.Length; i++)
                {
                    if (_dbContext.Secprgbut.Count(x => x.sysid == model.sysid && x.funid == model.funid && x.prgid == model.prgid && x.butid == model.btns[i].Guid) ==0)
                    {
                        entity.Guid = new Guid();
                        entity.butid = model.btns[i].Guid;
                        entity.butno = model.btns[i].butno;
                        entity.butname = model.btns[i].butname;
                        _dbContext.Secprgbut.Add(entity);
                        _dbContext.SaveChanges();
                    }
                }
                response.SetSuccess();
                return Ok(response);
            }
        }
        /// <summary>
        /// 修改程式按钮名称
        /// </summary>
        /// <param name="model">类别视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult EditPrgButton(PrgButtonCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {
                var entity = _dbContext.Secprgbut.FirstOrDefault(x => x.Guid == model.Guid);
                entity.butname = model.butname;
                _dbContext.SaveChanges();
                response.SetSuccess();
                return Ok(response);
            }
        }

        /// <summary>
        /// 删除程式按钮
        /// </summary>
        /// <param name="model">类别视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult RemovePrgButton(PrgButtonCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {
                var entity = _dbContext.Secprgbut.FirstOrDefault(x => x.Guid == model.Guid);
                _dbContext.Secprgbut.Remove(entity);
                _dbContext.SaveChanges();
                response.SetSuccess();
                return Ok(response);
            }
        }

        /// <summary>
        /// 继承父窗口按钮权限
        /// </summary>
        /// <param name="model">类别视图实体</param>
        /// <returns></returns>
      
        private void  extendPrgbut(DncZeusDbContext context, Guid guid,Guid? parentId)
        {
            var response = ResponseModelFactory.CreateInstance;

                var parentPrglist = context.Secuprg.AsQueryable().Where(x => x.prgid == parentId).ToList();
                var extendList= context.Secuprg.AsQueryable().Where(x => x.Guid == guid).ToList();
                foreach (Secuprg uprg in extendList) {
                    context.Secuprg.Remove(uprg);
                    context.SaveChanges();
                }

                foreach (Secuprg uprg in parentPrglist)
                {
                    Secuprg ent = new Secuprg();
                    ent.Guid = new Guid();
                    ent.orgid = uprg.orgid;
                    ent.butid = uprg.butid;
                    ent.userid = uprg.userid;
                    ent.prgid = guid;

                    context.Secuprg.Add(ent);
                context.SaveChanges();
            }
            
            
        }

        /// <summary>
        /// 清除继承父窗口按钮权限
        /// </summary>
        /// <param name="model">类别视图实体</param>
        /// <returns></returns>

        private void clearExtendPrgbut(DncZeusDbContext context, Guid guid)
        {
            var response = ResponseModelFactory.CreateInstance;

            var extendList = context.Secuprg.AsQueryable().Where(x => x.Guid == guid).ToList();
            foreach (Secuprg uprg in extendList)
            {
                context.Secuprg.Remove(uprg);
                context.SaveChanges();
            }

        }

    }

}