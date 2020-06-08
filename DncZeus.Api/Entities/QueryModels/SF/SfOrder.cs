/******************************************
 * AUTHOR:          LTB
 * CREATEDON:       2019-11-15
 * OFFICIAL_SITE:   
 ******************************************/

using System;

namespace DncZeus.Api.Entities.QueryModels.SF
{
    /// <summary>
    /// 
    /// </summary>
    public class SfOrder
    {
        public string fOrdNo { get; set; }
        public string fGoodsCode { get; set; }
        public string fGoodsName { get; set; }
        public decimal fOrdQty { get; set; }
        public string fcflag { get; set; }
       

    }
}
