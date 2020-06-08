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
    public class SfMOItem
    {
        /// <summary>
        /// 制令号
        /// </summary>
        public string fMONo { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string fOrdNo { get; set; }
        /// <summary>
        /// 品号ID
        /// </summary>
        public int fGoodsID { get; set; }
        /// <summary>
        /// 品号
        /// </summary>
        public string fGoodsCode { get; set; }
        /// <summary>
        /// 品名
        /// </summary>
        public string fGoodsName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal fMoQty { get; set; }

    }
}
