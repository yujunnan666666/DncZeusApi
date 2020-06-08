/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * DESCRIPTION:     用户信息实体类
 ******************************************/
//using DncZeus.Api.Entities;
using System;
using System.Collections.Generic;

namespace DncZeus.Api.ViewModels.Man.Workprocess
{
    /// <summary>
    /// 
    /// </summary>
    public class WorkprocessCreateViewModel
    {
        /// 系统Guid
        public Guid Guid { get; set; }
        /// 组织ID
        public int OrgID { get; set; }
        /// 工厂Guid  
        public Guid? factoryGuid { get; set; }
        /// 工序编号
        public string workCode { get; set; }
        /// 工序名称
        public string workName { get; set; }
        /// 产值比例
        public decimal rate { get; set; }
        /// 有效
        public int isvalid { get; set; }
        /// 创建人员
        public string creuser { get; set; }
        /// 创建日期
        public DateTime? credate { get; set; }
        /// 修改人员
        public string moduser { get; set; }
        /// 修改日期
        public DateTime? moddate { get; set; }


    }

}