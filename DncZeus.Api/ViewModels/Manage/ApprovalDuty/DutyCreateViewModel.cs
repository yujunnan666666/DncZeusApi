/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/
using System;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.ViewModels.Manage.ApprovalDuty
{
    /// <summary>
    ///(创建/编辑)
    /// </summary>
    public class DutyCreateViewModel
    {
        /// <summary>
        /// 系统ID
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// 用户名
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
        /// 人员名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 体温
        /// </summary>
        public string temperature { get; set; }
        /// <summary>
        /// 口罩领取
        /// </summary>
        public string getMask { get; set; }
        /// <summary>
        /// 建立日期
        /// </summary>
        public DateTime? credate { get; set; }



    }
}
