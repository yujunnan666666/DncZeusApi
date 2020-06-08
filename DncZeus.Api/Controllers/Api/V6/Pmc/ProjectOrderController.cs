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
using DncZeus.Api.RequestPayload.Sec.Button;
using DncZeus.Api.ViewModels.Pmc.Project;
using DncZeus.Api.Entities.Pmc;
using DncZeus.Api.RequestPayload.Pmc.Projects;
using DncZeus.Api.RequestPayload.Pmc.Worknote;

namespace DncZeus.Api.Controllers.Api.Pmc

{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [ApiController]
    [ApiVersion("6.0")] //V6
    [Route("api/v6/Pmc/[controller]/[action]")]
    public class ProjectOrderController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        

        public ProjectOrderController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            
        }
        
        /// <summary>
        /// 获取数夫订单数据
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult List(int curPage,int pageSize,string proNo)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var strSql = @"select top 100 percent t1.fOrdNo, t3.fGoodsCode,t3.fGoodsName,t2.fOrdQty,t1.fcflag 
                                from SF.DCF19_MY.dbo.t_copd_ordmst t1 
                                inner join SF.DCF19_MY.dbo.t_COPD_OrdItem t2 on t1.fOrdNo = t2.fOrdNo
                                inner join SF.DCF19_MY.dbo.t_BOMM_GoodsMst t3 on t2.fGoodsID = t3.fGoodsID
                                where _x_xmbh ={0}
                                order by t2.fSNo";
                var query = _dbContext.SfOrder.FromSql(strSql, proNo);
               /* if (!string.IsNullOrEmpty(kw))
                {
                    query = query.Where(x =>
                    (
                    x.fCNName.Contains(kw.Trim()) ||
                    x.fCName.Contains(kw.Trim())
                    )
                    );
                }*/
                var totalCount = query.Count();
                var list = query.Paged(curPage, pageSize).ToList();
               
                response.SetData(list,totalCount);
                return Ok(response);
               
            }
        }


    }
}