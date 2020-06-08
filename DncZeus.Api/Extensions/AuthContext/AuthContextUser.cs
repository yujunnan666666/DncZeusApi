/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using DncZeus.Api.Entities;
using System;

namespace DncZeus.Api.Extensions.AuthContext
{
    /// <summary>
    /// 登录用户上下文
    /// </summary>
    public class AuthContextUser
    {
        /// <summary>
        /// 用户GUID
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// 显示名
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public UserType UserType { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        public string Avator { get; set; }
        /// <summary>
        /// 登录组织ID
        /// </summary>
        public int OrgID { get; set; }
        public string GroupID { get; internal set; }
    }
}