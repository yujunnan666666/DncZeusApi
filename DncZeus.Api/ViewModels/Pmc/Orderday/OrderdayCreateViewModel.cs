/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * DESCRIPTION:     用户信息实体类
 ******************************************/
//using DncZeus.Api.Entities;
using System;


namespace DncZeus.Api.ViewModels.Pmc.Orderday
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderdayCreateViewModel
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
        /// 类别
        /// </summary>
        public int dtype { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime day { get; set; }
        /// <summary>
        /// 是否休息
        /// </summary>
        public int isrest { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
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
