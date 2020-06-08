/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * DESCRIPTION:     用户信息实体类
 ******************************************/
//using DncZeus.Api.Entities;
using System;
using System.Collections.Generic;

namespace DncZeus.Api.ViewModels.Man.Workshop
{
    /// <summary>
    /// 
    /// </summary>
    public class WorkshopCreateViewModel
    {
        ///Guid
        public Guid Guid { get; set; }
        /// 组织ID
        public int OrgID { get; set; }
        /// 工厂Guid  
        public Guid? factoryGuid { get; set; }
        /// 工厂工序Guid  
        public Guid? workProcessGuid { get; set; }
        /// 计产方式
        public int countType { get; set; }
        /// 单据编号
        public string ordCode { get; set; }
        /// 料品编号
        public string itemCode { get; set; }
        /// 料品名称
        public string itemName { get; set; }
        /// 工厂价
        public decimal factoryPrice { get; set; }
        /// 需求数量
        public int ordQty { get; set; }
        /// 累计完成数量
        public int sumQty { get; set; }
        /// 创建人员
        public string creuser { get; set; }
        /// 创建日期
        public DateTime? credate { get; set; }

        //工作日期
        public DateTime? workDate { get; set; }
        //数量
        public int qty { get; set; }
        //产值
        public decimal output { get; set; }
        //说明
        public string remark { get; set; }
        //子表Guid
        public Guid? lineGuid { get; set; }

        
    }
    

}