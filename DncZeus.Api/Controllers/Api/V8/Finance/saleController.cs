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
using DncZeus.Api.RequestPayload.Pmc.Duty;
using DncZeus.Api.ViewModels.Pmc.Duty;
using DncZeus.Api.Entities.Pmc;
using System.Collections.Generic;
using DncZeus.Api.ViewModels.Weixin.Cases;

using DncZeus.Api.Entities.Weixin;
using DncZeus.Api.RequestPayload.Weixin.Cases;
using DncZeus.Api.RequestPayload.Finance.Sale;

namespace DncZeus.Api.Controllers.Api.Weixin
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [ApiController]
    [ApiVersion("8.0")] //V8
    [Route("api/v8/Finance/[controller]/[action]")]
    public class SaleController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        

        public SaleController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            
        }
        [HttpPost]
        public IActionResult List(SaleRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var strSql = string.Format(@"exec [dbo].[sp_report_saleReach] '{0}','{1}'", payload.dept,payload.day);
                var query = _dbContext.FinanceSaleList.FromSql(strSql);
                var list = query.ToList();
                var totalCount = query.Count();
                response.SetData(list, totalCount);
                return Ok(response);
            }
        }
        [HttpPost]
        public IActionResult YearList(SaleRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            using (_dbContext)
            {
                var strSql = string.Format(@"exec [dbo].[sp_report_saleReach_sum] '{0}'", payload.year);
                var query = _dbContext.FinanceYearSaleList.FromSql(strSql);
                var list = query.ToList();
                var totalCount = query.Count();
                response.SetData(list, totalCount);
                return Ok(response);
            }
        }
    }
}