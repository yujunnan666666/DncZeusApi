/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using DncZeus.Api.Entities;
using DncZeus.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Security.Claims;
using DncZeus.Api.Auth;
using static DncZeus.Api.Entities.Enums.CommonEnum;
using DncZeus.Api.Entities.Sec;
using System;

namespace DncZeus.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OauthController : ControllerBase
    {
        private readonly AppAuthenticationSettings _appSettings;
        private readonly DncZeusDbContext _dbContext;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appSettings"></param>
        public OauthController(IOptions<AppAuthenticationSettings> appSettings, DncZeusDbContext dbContext)
        {
            _appSettings = appSettings.Value;
            _dbContext = dbContext;
        }

        /// <summary>
        /// 登录旧
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="orgid"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Auth6(string username, string password, int orgid)
        {
            var response = ResponseModelFactory.CreateInstance;
            DncUser user;
            MisOrganization org;
            
            //TechItemmAttachment att;

            using (_dbContext)
            {
                user = _dbContext.DncUser.FirstOrDefault(x => x.LoginName == username.Trim());
                if (user == null || user.IsDeleted == IsDeleted.Yes)
                {
                    response.SetFailed("用户不存在");
                    return Ok(response);
                }
                if (user.Password != password.Trim())
                {
                    response.SetFailed("密码不正确");
                    return Ok(response);
                }
                if (user.IsLocked == IsLocked.Locked)
                {
                    response.SetFailed("账号已被锁定");
                    return Ok(response);
                }
                if (user.Status == UserStatus.Forbidden)
                {
                    response.SetFailed("账号已被禁用");
                    return Ok(response);
                }


                //add by ltb 2019-10-10
                org = _dbContext.MisOrganization.FirstOrDefault(x => x.ID == orgid);
                if (org == null)
                {
                    response.SetFailed("组织不存在");
                    return Ok(response);
                }

                

            }
            var claimsIdentity = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim("guid",user.Guid.ToString()),
                    new Claim("avatar",""),
                    new Claim("displayName",user.DisplayName),
                    new Claim("loginName",user.LoginName),
                    new Claim("emailAddress",""),
                    new Claim("guid",user.Guid.ToString()),
                    new Claim("userType",((int)user.UserType).ToString()),
                    new Claim("OrgID",orgid.ToString()),
                    
                });
            var token = JwtBearerAuthenticationExtension.GetJwtAccessToken(_appSettings, claimsIdentity);

            response.SetData(token);
            return Ok(response);
        }

        /// <summary>
        /// 登录-new
        /// </summary>
        /// <param name="userno"></param>
        /// <param name="password"></param>
        /// <param name="orgid"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Auth(string userno, string password, int orgid)
        {
            var response = ResponseModelFactory.CreateInstance;
            Secuser user;
            Secuserorg userorg;
            MisOrganization org;
            Secguser guser;
            Guid? groupid;
            //TechItemmAttachment att;

            using (_dbContext)
            {
                user = _dbContext.Secuser.FirstOrDefault(x => x.userno == userno.Trim());
               

                if (user == null)
                {
                    response.SetFailed("用户不存在");
                    return Ok(response);
                }
               
                if (user.pwd != password.Trim())
                {
                    response.SetFailed("密码不正确");
                    return Ok(response);
                } 
                if (user.enabled =="N")
                {
                    response.SetFailed("账号已被禁用");
                    return Ok(response);
                }
                userorg = _dbContext.Secuserorg.FirstOrDefault(x => x.userid == user.Guid);
                if (userorg == null) {
                    response.SetFailed("用户未绑定组织，请联系管理员！");
                    return Ok(response);
                }
                if (userorg!=null && userorg.orgid != orgid)
                {
                    response.SetFailed("当前用户组织不正确");
                    return Ok(response);
                }


                //add by ltb 2019-10-10
                org = _dbContext.MisOrganization.FirstOrDefault(x => x.ID == orgid);
                if (org == null)
                {
                    response.SetFailed("组织不存在");
                    return Ok(response);
                }

                guser = _dbContext.Secguser.FirstOrDefault(x => x.userid == user.Guid);
                if (guser == null) {
                    groupid = null;
                }
                else {
                    groupid = guser.groupid;
                }
            }
            var claimsIdentity = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userno),
                    new Claim("guid",user.Guid.ToString()),
                    new Claim("avatar",""),
                    new Claim("displayName",user.username),
                    new Claim("loginName",user.userno),
                    new Claim("emailAddress",""),
                    new Claim("guid",user.Guid.ToString()),
                    new Claim("userType",user.utype.ToString()),
                    new Claim("OrgID",orgid.ToString()),
                    new Claim("OrgName",org.Name.ToString()),
                    new Claim("groupid",groupid.ToString()),
                });
            var token = JwtBearerAuthenticationExtension.GetJwtAccessToken(_appSettings, claimsIdentity);

            response.SetData(token);
            return Ok(response);
        }
    }
}