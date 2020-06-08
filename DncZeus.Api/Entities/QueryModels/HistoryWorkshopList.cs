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
    public class HistoryWorkshopList
    {
        /// <summary>
        /// 
        /// </summary>
        
        
        public string ordCode { get; set; }
        public string itemCode { get; set; }
        public string itemName { get; set; }
        public decimal factoryPrice { get; set; }
        public Int32 ordQty { get; set; }
        


    }
}
