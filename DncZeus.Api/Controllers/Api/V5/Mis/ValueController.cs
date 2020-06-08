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
using DncZeus.Api.ViewModels.Mis.DncValue;
using DncZeus.Api.RequestPayload.Mis.Extend;
using DncZeus.Api.RequestPayload.Mis.Value;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

namespace DncZeus.Api.Controllers.Api.Mis
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [ApiController]
    [ApiVersion("5.0")] //V5
    [Route("api/v5/Mis/[controller]/[action]")]
    public class ValueController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        

        public ValueController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            
        }
        [HttpPost]
        public IActionResult List(ValueRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var query = _dbContext.MisCode.AsQueryable();
                if (payload.misKindId != -1) {
                    query = query.Where(x => x.MisCkind.ID == payload.misKindId);
                }


               var list = query.Paged(payload.CurrentPage, payload.PageSize).Include(x => x.MisCkind).Include(x => x.MisOrganization).ToList();
               
                var totalCount = query.Count();
                //var data = list.Select(_mapper.Map<MisCatalog, CategoryJsonModel>);

                response.SetData(list, totalCount);
                return Ok(response);

            }

        }

        //获取系统参数时间
        [HttpGet]
        public IActionResult ValueList(int misKindId)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var ent = _dbContext.MisCode.FirstOrDefault(x => x.MisCkind.ID == misKindId);

                response.SetData(ent);
                return Ok(response);

            }

        }

        //更改系统参数时间
        [HttpGet]
        public IActionResult UpdateMsgTime(string newTime)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                
                var ent = _dbContext.MisCode.FirstOrDefault(x => x.MisCkind.ID == 18);
                //设置每天早上8点
                ent.cdesc = DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + " 8:00:00";
                
                ent.moddate = DateTime.Now;

                _dbContext.SaveChanges();

                response.SetSuccess();
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
        public IActionResult Create(ValueCreateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
           
            using (_dbContext)
            {
                /* if (_dbContext.MisCatalog.Count(x => x.Code == model.Code) > 0)
                 {
                     response.SetFailed("图标已存在");
                     return Ok(response);
                 }*/
                var MisKindEntity = _dbContext.MisCkind.FirstOrDefault(x => x.ID == model.misCkindId);
                var MisOrgEntity = _dbContext.MisOrganization.FirstOrDefault(x => x.ID == model.misOrganizationId);
                var entity = new MisCode();
                entity.code = model.code;
                entity.cdesc = model.cdesc;
                entity.isEnabled = model.isEnabled;
                entity.moddate = entity.credate = DateTime.Now;
                entity.moduser = entity.creuser = AuthContextService.CurrentUser.DisplayName;
                entity.MisCkind = MisKindEntity;
                entity.MisOrganization = MisOrgEntity;
                entity.extendCode1 = model.extendCode1;
                entity.extendCode2 = model.extendCode2;
                entity.extendCode3 = model.extendCode3;
                entity.extendCode4 = model.extendCode4;
                entity.extendCode5 = model.extendCode5;
                entity.extendCode6 = model.extendCode6;
                entity.extendCode7 = model.extendCode7;
                entity.extendCode8 = model.extendCode8;
                entity.extendCode9 = model.extendCode9;
                entity.extendName1 = model.extendName1;
                entity.extendName2 = model.extendName2;
                entity.extendName3 = model.extendName3;
                entity.extendName4 = model.extendName4;
                entity.extendName5 = model.extendName5;
                entity.extendName6 = model.extendName6;
                entity.extendName7 = model.extendName7;
                entity.extendName8 = model.extendName8;
                entity.extendName9 = model.extendName9;

                /*var entity = _mapper.Map<ValueCreateViewModel,MisCode>(model);*/
                entity.moddate=entity.credate = DateTime.Now;
                entity.moduser=entity.creuser = AuthContextService.CurrentUser.DisplayName;
                
                _dbContext.MisCode.Add(entity);
                _dbContext.SaveChanges();

                response.SetSuccess();
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
                response.SetData(_mapper.Map<MisCkind, ValueCreateViewModel>(entity));
                return Ok(response);
            }
        }

        /// <summary>
        /// 根据表头id获取对应扩展列表
        /// </summary>
        /// <param name="id">类别ID</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult ExtendListByKind(int id)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var query = _dbContext.MisCkindExtend.AsQueryable();
                var list = query.Where(x => x.MisCKindId == id).OrderBy("Num",false).ToList();


                
                response.SetData(list);
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
        public IActionResult Edit(ValueCreateViewModel model)
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
                var MisKindEntity = _dbContext.MisCkind.FirstOrDefault(x => x.ID == model.misCkindId);
                var MisOrgEntity = _dbContext.MisOrganization.FirstOrDefault(x => x.ID == model.misOrganizationId);
                var entity = _dbContext.MisCode.FirstOrDefault(x => x.ID == model.ID);
                /* entity.MisCKindId= model.MisCKindId;
                 entity.colName = model.colName;
                 entity.colType = model.colType;
                 entity.isEnabled = model.isEnabled;
                 entity.sourceId = model.sourceId;*/

                entity.code = model.code;
                entity.cdesc = model.cdesc;
                entity.isEnabled = model.isEnabled;
                entity.moddate = entity.credate = DateTime.Now;
                entity.moduser = entity.creuser = AuthContextService.CurrentUser.DisplayName;
                entity.MisCkind = MisKindEntity;
                entity.MisOrganization = MisOrgEntity;
                entity.extendCode1 = model.extendCode1;
                entity.extendCode2 = model.extendCode2;
                entity.extendCode3 = model.extendCode3;
                entity.extendCode4 = model.extendCode4;
                entity.extendCode5 = model.extendCode5;
                entity.extendCode6 = model.extendCode6;
                entity.extendCode7 = model.extendCode7;
                entity.extendCode8 = model.extendCode8;
                entity.extendCode9 = model.extendCode9;
                entity.extendName1 = model.extendName1;
                entity.extendName2 = model.extendName2;
                entity.extendName3 = model.extendName3;
                entity.extendName4 = model.extendName4;
                entity.extendName5 = model.extendName5;
                entity.extendName6 = model.extendName6;
                entity.extendName7 = model.extendName7;
                entity.extendName8 = model.extendName8;
                entity.extendName9 = model.extendName9;
                //entity.credate = model.credate;
                //entity.creuser = model.creuser;
                entity.moddate = DateTime.Now;
                entity.moduser = AuthContextService.CurrentUser.DisplayName;

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
                var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                var sql = string.Format("DELETE FROM MisCode WHERE Id IN ({0})", parameterNames);
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
                     break;
                case "Invalid": //失效
                    response = UpdateIsValid(0, ids);
                    break;
                case "Valid": //有效
                    response = UpdateIsValid(1, ids);
                    break;*/
                default:
                    break;
            }
            return Ok(response);
        }

       


    }
}