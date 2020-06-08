/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using DncZeus.Api.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace DncZeus.Api.Extensions.AuthContext
{
    /// <summary>
    /// 
    /// </summary>
    public static class AuthContextService
    {
        private static IHttpContextAccessor _context;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _context = httpContextAccessor;
        }
        /// <summary>
        /// 
        /// </summary>
        public static HttpContext Current => _context.HttpContext;
        /// <summary>
        /// 
        /// </summary>
        public static AuthContextUser CurrentUser
        {
            get
            {
                var user = new AuthContextUser
                {
                    //LoginName = Current.User.FindFirstValue(ClaimTypes.NameIdentifier),
                    LoginName = Current.User.FindFirstValue("LoginName"),
                    DisplayName = Current.User.FindFirstValue("displayName"),
                    EmailAddress = Current.User.FindFirstValue("emailAddress"),
                    UserType = (UserType)Convert.ToInt32(Current.User.FindFirstValue("userType")),
                    Avator= Current.User.FindFirstValue("avator"),
                    Guid= new Guid(Current.User.FindFirstValue("guid")),
                    OrgID = Convert.ToInt32(Current.User.FindFirstValue("OrgID")),
                    GroupID = Current.User.FindFirstValue("groupid")
                    
                };
                return user;
            }
        }

        /// <summary>
        /// 是否已授权
        /// </summary>
        public static bool IsAuthenticated
        {
            get
            {
                return Current.User.Identity.IsAuthenticated;
            }
        }

        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        public static bool IsSupperAdministator
        {
            get
            {
                return ((UserType)Convert.ToInt32(Current.User.FindFirstValue("userType"))== UserType.SuperAdministrator);
            }
        }
    }
}
