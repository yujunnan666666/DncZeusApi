/******************************************
 * AUTHOR:          LTB
 * CREATEDON:       2019-11-18
 * OFFICIAL_SITE:   
 ******************************************/

using System;

namespace DncZeus.Api.Entities.QueryModels.Tmp
{
    /// <summary>
    /// 查询列表
    /// </summary>
    public class boardList
    {
        /// <summary>
        /// 生产包装编号
        /// </summary>
        public string tmpPackNo { get; set; }
        /// <summary>
        /// 制令号
        /// </summary>
        public string fMONo { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string fOrdNo { get; set; } 
        /// <summary>
        /// 品号
        /// </summary>
        public string fGoodsCode { get; set; }
        /// <summary>
        /// 品名
        /// </summary>
        public string fGoodsName { get; set; }
        /// <summary>
        /// 数夫包装编号
        /// </summary>
        public string fPackNo { get; set; }
        /// <summary>
        /// 分包条码
        /// </summary>
        public string fBarcode { get; set; }
        /// <summary>
        /// 分包名称
        /// </summary>
        public string fspname { get; set; }
        /// <summary>
        /// 板件分类
        /// </summary>
        public int boardType { get; set; }
        /// <summary>
        /// 板件编号  
        /// </summary>
        public string boardcode { get; set; }
        /// <summary>
        /// 板件名称  
        /// </summary>
        public string boardname { get; set; }
        /// <summary>
        /// 板件打印  
        /// </summary>
        public int lprintnum { get; set; }
        /// <summary>
        /// 板件有效  
        /// </summary>
        public int isValid { get; set; }
        /// <summary>
        /// 包装状态  
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 审核人员  
        /// </summary>
        public string cfmuser { get; set; }
        /// <summary>
        /// 审核日期  
        /// </summary>
        public DateTime? cfmdate { get; set; }
        /// <summary>
        /// 产品数量
        /// </summary>
        public int fOrdQty { get; set; }
        /// <summary>
        /// 包装编号/房号
        /// </summary>
        public string barCode { get; set; }
        /// <summary>
        /// 图号
        /// </summary>
        public string imageNo { get; set; }
        /// <summary>
        /// 部件数量
        /// </summary>
        public int boardQty { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int packIndex { get; set; }
        /// <summary>
        /// 包数量
        /// </summary>
        public int packQty { get; set; }
    }
}
