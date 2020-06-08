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
    public class SfGoods
    {
        /// <summary>
        /// 
        /// </summary>
      
        public string fNo { get; set; }
        public string fGoodsCode { get; set; }
        public string fGoodsName { get; set; }
        public Decimal fQty { get; set; }
        public string fUnitCode { get; set; }
        public Decimal fUP { get; set; }



    }
}
