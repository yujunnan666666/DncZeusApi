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
using DncZeus.Api.ViewModels.Weixin.News;
using DncZeus.Api.RequestPayload.Weixin.News;
using DncZeus.Api.Entities.Weixin;
using DncZeus.Api.ViewModels.Sec.User;

namespace DncZeus.Api.Controllers.Api.Weixin
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [ApiController]
    [ApiVersion("7.0")] //V7
    [Route("api/v7/Weixin/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        

        public UserController(DncZeusDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            
        }
        [HttpGet]
        public IActionResult Login(string account, string password)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                Secuser user = _dbContext.Secuser.FirstOrDefault(x => x.wxAccount == account.Trim());
                if (user == null)
                {
                    response.SetFailed("用户不存在");
                    return Ok(response);
                }
                if (user.wxPwd != password.Trim())
                {
                    response.SetFailed("密码不正确");
                    return Ok(response);
                }
                response.SetData(_mapper.Map<Secuser, UserCreateViewModel>(user));

            }

            
            return Ok(response);
        }
        [HttpGet]
        public IActionResult UpdatePwd(string phone,string oldPwd, string newPwd)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                Secuser user = _dbContext.Secuser.FirstOrDefault(x => x.wxPhone == phone.Trim());
                if (user == null)
                {
                    response.SetFailed("用户不存在");
                    return Ok(response);
                }
                if (user.wxPwd != oldPwd.Trim())
                {
                    response.SetFailed("旧密码不正确");
                    return Ok(response);
                }
                else {
                    user.wxPwd = newPwd;
                    _dbContext.SaveChanges();
                    response.SetSuccess("修改成功");
                    return Ok(response);
                }
                

            }


            
        }

    }
}