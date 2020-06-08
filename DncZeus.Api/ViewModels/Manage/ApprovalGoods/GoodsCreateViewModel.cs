/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/
using System;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.ViewModels.Manage.ApprovalGoods
{
    /// <summary>
    ///(创建/编辑)
    /// </summary>
    public class GoodsCreateViewModel
    {
        /// <summary>
        /// 系统ID
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string department { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 审批单号
        /// </summary>
        public string ordernum { get; set; }
        /// <summary>
        /// 模板号
        /// </summary>
        public string tmpnum { get; set; }
        /// <summary>
        /// 用途
        /// </summary>
        public string purpose { get; set; }
        /// <summary>
        /// 物品名称
        /// </summary>
        public string goodsname { get; set; }
        /// <summary>
        /// 物品数量
        /// </summary>
        public int goodscount { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        public int attachment { get; set; }
        /// <summary>
        /// 建立日期
        /// </summary>
        public DateTime? credate { get; set; }



    }
}
