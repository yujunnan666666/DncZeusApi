/******************************************
 * AUTHOR:          LTB
 * CREATEDON:       2019-11-15
 * OFFICIAL_SITE:   
 ******************************************/

using System;

namespace DncZeus.Api.Entities.QueryModels
{
    /// <summary>
    /// 
    /// </summary>
    public class WorkshopList
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid? Guid { get; set; }
        public Guid? factoryGuid { get; set; }
        public Byte countType { get; set; }
        public string ordCode { get; set; }
        public string itemCode { get; set; }
        public string itemName { get; set; }
        public decimal factoryPrice { get; set; }
        public string creuser { get; set; }
        public DateTime? credate { get; set; }
        public Int32 ordQty { get; set; }
        public decimal sumQty { get; set; }
        public Guid? lineGuid { get; set; }
        public DateTime? workDate { get; set; }
        public decimal qty { get; set; }
        public string remark { get; set; }
        public decimal rate { get; set; }


    }
}
