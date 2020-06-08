/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * DESCRIPTION:     用户信息实体类
 ******************************************/
//using DncZeus.Api.Entities;
using System;


namespace DncZeus.Api.ViewModels.Pmc.Projectline
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectlineCreateViewModel
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// 项目Guid
        /// </summary>
        public Guid? projectsGuid { get; set; }
        /// <summary>
        /// 客户料号
        /// </summary>
        public string custitemcode { get; set; }
        /// <summary>
        /// 客户料名
        /// </summary>
        public string custitemname { get; set; }
        /// <summary>
        /// 公司款号
        /// </summary>
        public string styleNO { get; set; }
        /// <summary>
        /// 图号
        /// </summary>
        public string imageNO { get; set; }
        /// <summary>
        /// 制造工厂
        /// </summary>
        public string factory { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string desc { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal qty { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal amount { get; set; }
        /// <summary>
        /// 需求日期
        /// </summary>
        public DateTime? reqDate { get; set; }
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
        /// 图片
        /// </summary>
        public string images { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string imgSrc { get; set; }
        /// <summary>
        /// 辅图片
        /// </summary>
        public string otherImages
        { get; set; }
        /// <summary>
        /// 辅图片路径
        /// </summary>
        public string otherImgSrc
        { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? moddate { get; set; }

    }
}
