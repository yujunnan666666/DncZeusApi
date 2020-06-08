/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * DESCRIPTION:     用户信息实体类
 ******************************************/
//using DncZeus.Api.Entities;
using System;
using DncZeus.Api.ViewModels;
using DncZeus.Api.Entities.Tmp;
using static DncZeus.Api.Entities.Tmp.tmpEnum;



namespace DncZeus.Api.ViewModels.Tmp.ItemBar
{
    /// <summary>
    /// 
    /// </summary>
    public class BoardJsonModel : MainJsonModel
    {
        /// <summary>
        /// 生产包装编号
        /// </summary>
        public string tmpPackNo { get; set; }
        /// <summary>
        /// 制令号
        /// </summary>
        public string fmono { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string fordno { get; set; }
        /// <summary>
        /// 料品编号
        /// </summary>
        public string fgoodscode { get; set; }
        /// <summary>
        /// 料品名称
        /// </summary>
        public string fgoodsname { get; set; }
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
        /// 分件类别
        /// </summary>
        public boardType boardType { get; set; }
        /// <summary>
        /// 板件序号
        /// </summary>
        public int boardnum { get; set; }
        /// <summary>
        /// 板件条码
        /// </summary>
        public string boardcode { get; set; }
        /// <summary>
        /// 板件名称
        /// </summary>
        public string boardname { get; set; }
        /// <summary>
        /// 板件条码已打印数量
        /// </summary>
        public int lprintnum { get; set; }
        /// <summary>
        /// 失效状态
        /// </summary>
        public isValid isValid { get; set; }
        /// <summary>
        /// 失效人员
        /// </summary>
        public string valuser { get; set; }
        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime? valmdate { get; set; }
        /// <summary>
        /// 单据状态
        /// </summary>
        public Status status { get; set; }
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
        public string packIndex { get; set; }
        /// <summary>
        /// 包数
        /// </summary>
        public int packQty { get; set; }
    }
}
