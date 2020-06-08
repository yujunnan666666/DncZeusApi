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
    public class QuerySfModel
    {
        ///号码
        public string no { get; set; }
        /// 编码
        public int code { get; set; }
        

        
    }
    public class QueryWorkshopout
    {
        ///
        public Guid? workshopOutGuid { get; set; }
        /// 
        public DateTime workDate { get; set; }
        /// 
        public Guid? lineGuid { get; set; }

        public int qty { get; set; }
        public decimal output { get; set; }
    }

    public class QueryHistoryWorkshopout
    {
        ///
        public Guid? factoryGuid { get; set; }
        /// 
        public DateTime workDate { get; set; }
        /// 
    }

}