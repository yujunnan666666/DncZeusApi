/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using System.Collections.Generic;

namespace DncZeus.Api.RequestPayload.Rbac.Role
{
    /// <summary>
    /// 角色分配权限的请求载体类
    /// </summary>
    public class RoleAssignPermissionPayload
    {
        /// <summary>
        /// 
        /// </summary>
        public RoleAssignPermissionPayload()
        {
            Permissions = new List<string>();
        }
        /// <summary>
        /// 角色编码
        /// </summary>
        public string RoleCode { get; set; }
        /// <summary>
        /// 权限列表
        /// </summary>
        public List<string> Permissions { get; set; }
    }
}
