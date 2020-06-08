using AutoMapper;
using DncZeus.Api.Entities;
using DncZeus.Api.Entities.SF;
using DncZeus.Api.Entities.QueryModels.SF;
using DncZeus.Api.Entities.Enums;
using DncZeus.Api.Extensions;
using DncZeus.Api.Extensions.DataAccess;
using DncZeus.Api.Models.Response;
using DncZeus.Api.RequestPayload.Tmp.Barhead;
using DncZeus.Api.ViewModels.Tmp.ItemBar;
using DncZeus.Api.ViewModels.Rbac.DncUser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using OfficeOpenXml;

/******************************************
 * AUTHOR:          LTB
 * CREATEDON:       2019-11-09
 * OFFICIAL_SITE:   
 ******************************************/

namespace DncZeus.Api.Controllers.Api.SF
{
    /// <summary>
    /// 数夫ERP数据
    /// </summary>
    [ApiController]
    [ApiVersion("4.0")] //V3
    [Route("api/v4/SF/[controller]/[action]")]
    public class SfDataController : ControllerBase
    {
        private readonly SFAuthDbContext _dbContext;
        private readonly IMapper _mapper;
        private IHostingEnvironment _host;

        /// <summary>
        /// 数夫ERP接口
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        /// <param name="host"></param>
        public SfDataController(SFAuthDbContext dbContext, IMapper mapper, IHostingEnvironment host)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _host = host;
        }

        /// <summary>
        /// 获取数夫制令信息
        /// </summary>
        /// <param name="fMONo"></param>
        /// <param name="fGoodsCode"></param>
        /// <returns>object</returns>
        //[HttpGet()]
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetSfMoItem(string fMONo, string fGoodsCode)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateInstance;
                var sql = @"select top 1 t1.fMONo,t1.fOrdNo, t3.fGoodsCode, t3.fGoodsName,t3.fGoodsID
                            from t_MPSM_MOMst t1
                            inner join t_MPSM_MOItem t2 on t1.fMONo = t2.fMONo
                            inner join t_BOMM_GoodsMst t3 on t2.fGoodsID = t3.fGoodsID
                            where t1.fMONo={0} and t3.fGoodsCode = {1}";
                var queryData = _dbContext.SfMOItem.FromSql(sql, fMONo, fGoodsCode).ToList();
                response.SetData(queryData);
                return Ok(response);
            }
        }
    }
}