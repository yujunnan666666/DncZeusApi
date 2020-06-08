/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * DESCRIPTION:     用户信息实体类
 ******************************************/
//using DncZeus.Api.Entities;
using System;


namespace DncZeus.Api.ViewModels.Pmc.Jobuser
{
    /// <summary>
    /// 
    /// </summary>
    public class JobuserCreateViewModel
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// 组织ID
        /// </summary>
        public int OrgID { get; set; }
        /// <summary>
        /// 岗位Guid(废弃)
        /// </summary>
        public Guid? jobGuid { get; set; }
        /// <summary>
        /// 岗位jobId
        /// </summary>
        public int jobId { get; set; }
        /// <summary>
        /// 用户Guid 0-否；1-是
        /// </summary>
        public Guid? userGuid { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public int isvalid { get; set; }
        /// <summary>
        /// 创建人员
        /// </summary>
        public string creuser { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? credate { get; set; }
        /// <summary>
        /// 修改人员
        /// </summary>
        public string moduser { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? moddate { get; set; }

    }
}
