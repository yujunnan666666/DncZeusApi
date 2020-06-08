/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * DESCRIPTION:     用户信息实体类
 ******************************************/
//using DncZeus.Api.Entities;
using System;


namespace DncZeus.Api.ViewModels.Pmc.Plannote
{
    /// <summary>
    /// 
    /// </summary>
    public class PlannoteCreateViewModel
    {
       
        /// <summary>
        /// 项目计划表Guid
        /// </summary>
        public string ids { get; set; }
        public string planIds { get; set; }
        public string lineIds { get; set; }
        public string itemIds { get; set; }
        public DateTime? workdate { get; set; }
        public int workstatus { get; set; }
        public int workqty { get; set; }
        public string remark { get; set; }
        
    }

    public class PlannoteArrs
    {
        public PlannoteObj[] list { get; set; }

    }

    public class PlannoteObj
    {
        
        /// 项目明细表Guid
        public Guid? lineGuid { get; set; }
        /// 项目计划表Guid
        public Guid? projectplanGuid { get; set; }
        /// 难点重点物料Guid
        public Guid? itemGuid { get; set; }
        /// 是否工厂
        public int isfactory { get; set; }
        /// 数夫制令号
        public string mono { get; set; }
        /// 制造工厂
        public string factory { get; set; }
        /// 工作日
        public DateTime? workdate { get; set; }
        /// 工作状态
        public int workstatus { get; set; }
        /// 当日完工数量
        public int workqty { get; set; }
        /// 备注
        public string remark { get; set; }

    }

}
