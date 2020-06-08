/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/
using System;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.ViewModels.Manage.ApprovalStaffBack
{
    /// <summary>
    ///(创建/编辑)
    /// </summary>
    public class StaffBackCreateViewModel
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
        /// 返程日期
        /// </summary>
        public DateTime? backDate { get; set; }
        /// <summary>
        /// 到达中山日期
        /// </summary>
        public DateTime? zsDate { get; set; }
        /// <summary>
        /// 交通工具
        /// </summary>
        public string vehicle { get; set; }
        /// <summary>
        /// 返程是否经过疫区
        /// </summary>
        public string isPass { get; set; }
        /// <summary>
        /// 是否接触疫区人员
        /// </summary>
        public string isTouch { get; set; }
        /// <summary>
        /// 是否发烧
        /// </summary>
        public string isFever { get; set; }
        /// <summary>
        /// 是否感染
        /// </summary>
        public string isIll { get; set; }
        /// <summary>
        /// 外地回粤后是否有向居委报备
        /// </summary>
        public string isReport { get; set; }
        /// <summary>
        /// 中山居住地详细地址
        /// </summary>
        public string address { get; set; }
        
        
        /// <summary>
        /// 出发地
        /// </summary>
        public string startPlace { get; set; }
        /// <summary>
        /// 目的地
        /// </summary>
        public string endPlace { get; set; }
        /// <summary>
        /// 居民居住在性质
        /// </summary>
        public string liveType { get; set; }
        /// <summary>
        /// 有效居家隔离开始时间
        /// </summary>
        public DateTime? quarantine { get; set; }
        /// <summary>
        /// 建立日期
        /// </summary>
        public DateTime? credate { get; set; }



    }
}
