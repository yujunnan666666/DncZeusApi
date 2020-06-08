/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * DESCRIPTION:     用户信息实体类
 ******************************************/
//using DncZeus.Api.Entities;
using System;


namespace DncZeus.Api.ViewModels.Pmc.Project
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectCreateViewModel
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// 组织ID
        /// </summary>
        public int OrgID { get; set; }
        /// <summary>
        /// 项目编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        public string area { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        public string custcode { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string custname { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal qty { get; set; }
        /// <summary>
        /// 项目总金额
        /// </summary>
        public decimal totalmoney { get; set; }
        /// <summary>
        /// 立项时间
        /// </summary>
        public DateTime? procreDate { get; set; }
        /// <summary>
        /// 分批出货
        /// </summary>
        public int isbatch { get; set; }
        /// <summary>
        /// 客户交期
        /// </summary>
        public DateTime? custDate { get; set; }
        /// <summary>
        /// 中山交期
        /// </summary>
        public DateTime? factoryDate1 { get; set; }
        /// <summary>
        /// 越秀交期
        /// </summary>
        public DateTime? factoryDate2 { get; set; }
        /// <summary>
        /// 跟进等级
        /// </summary>
        public int followgrade { get; set; }
        /// <summary>
        /// 项目状态
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 难点物料状态
        /// </summary>
        public int diffStatus { get; set; }
        /// <summary>
        /// 重点物料状态
        /// </summary>
        public int focusStatus { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 创建人员
        /// </summary>
        public string creuser { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? credate { get; set; }
        /// <summary>
        /// 修改人员
        /// </summary>
        public string moduser { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? moddate { get; set; }
        /// <summary>
        /// 审核人员
        /// </summary>
        public string cfmuser { get; set; }
        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? cfmdate { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string contacter { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string phone { get; set; }
        //有难点物料
        public int isdiff { get; set; }
        //有难点物料
        public int isfocus { get; set; }
        /// <summary>
        /// 数夫已下订单
        /// </summary>
        public int iscreorder { get; set; }
        /// <summary>
        /// 数夫订单状态
        /// </summary>
        public int sfStatus { get; set; }

    }
}
