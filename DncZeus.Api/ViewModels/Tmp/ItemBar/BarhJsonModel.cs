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



namespace DncZeus.Api.ViewModels.Tmp.ItemBar
{
    /// <summary>
    /// 
    /// </summary>
    public class BarhJsonModel : MainJsonModel
    {
        /// <summary>
        /// 数夫包装编号
        /// </summary>
        public string fPackNo { get; set; }
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
        /// 板件总数
        /// </summary>
        public int boardqty { get; set; }
        /// <summary>
        /// 已包数量
        /// </summary>
        public int bagqty { get; set; }
        /// <summary>
        /// 未包数量
        /// </summary>
        public int unbagqty { get; set; }
        /// <summary>
        /// 分包数量
        /// </summary>
        public int spqty { get; set; }
        /// <summary>
        /// 包装状态
        /// </summary>
        public tmpEnum.SpStatus spStatus { get; set; }
        /// <summary>
        /// 单据状态
        /// </summary>
        public tmpEnum.Status status { get; set; }
    }
}
