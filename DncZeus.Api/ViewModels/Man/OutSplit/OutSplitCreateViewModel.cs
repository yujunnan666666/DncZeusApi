/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * DESCRIPTION:     用户信息实体类
 ******************************************/
//using DncZeus.Api.Entities;
using System;
using System.Collections.Generic;

namespace DncZeus.Api.ViewModels.Man.OutSplit
{
    /// <summary>
    /// 
    /// </summary>
    public class OutSplitCreateViewModel
    {
        /// 系统Guid
        public Guid Guid { get; set; }
        /// 组织ID
        public int OrgID { get; set; }
        /// 工厂Guid  
        public Guid? factoryGuid { get; set; }
        /// 计产方式
        public int countType { get; set; }
        /// 单据编号
        public string ordCode { get; set; }
        /// 料品编号
        public string itemCode { get; set; }
        /// 料品名称
        public string itemName { get; set; }
        /// 状态
        public int status { get; set; }
        /// 创建人员
        public string creuser { get; set; }
        /// 创建日期
        public DateTime? credate { get; set; }
        /// 修改人员
        public string moduser { get; set; }
        /// 修改日期
        public DateTime? moddate { get; set; }
        /// 工序ID
        public Guid? processGuid { get; set; }
        /// 产值比例
        public decimal rate { get; set; }
        /// 子表ID
        public Guid? lineGuid { get; set; }

        public RateList[] ratelist { get; set; }



    }

    public class RateList
    {
        public Guid? guid { get; set; }
        public Guid? lineGuid { get; set; }
        public decimal rate { get; set; }
        public decimal rate2 { get; set; }
    }
}