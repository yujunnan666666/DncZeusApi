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
    public class SfItem
    {
        /// <summary>
        /// 物料名称
        /// </summary>
        public string fGoodsName { get; set; }
        /// <summary>
        /// 物料编号
        ///</summary>
        public string fGoodsCode { get; set; }
        /// <summary>
        /// 规格描述
        ///</summary>
        public string fSizeDesc { get; set; }
        /// <summary>
        /// 产品类型
        /// </summary>
        public string fFgType { get; set; }
       
       

    }
}
