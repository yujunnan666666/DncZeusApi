/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * DESCRIPTION:     用户信息实体类
 ******************************************/
//using DncZeus.Api.Entities;
using System;


namespace DncZeus.Api.ViewModels.Pmc.Projectitem
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectitemCreateViewModel
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// 系统Guid
        /// </summary>
        public Guid? projectlineGuid { get; set; }
        /// <summary>
        /// 物料编号
        /// </summary>
        public string itemcode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string itemname { get; set; }
        /// <summary>
        /// erp名称
        /// </summary>
        public string erpname { get; set; }
        /// <summary>
        /// 需求数量
        /// </summary>
        public decimal qty { get; set; }
        /// <summary>
        /// 需求单位
        /// </summary>
        public string unitcode { get; set; }
        /// <summary>
        /// 需求日期
        /// </summary>
        public DateTime? reqDate { get; set; }
        /// <summary>
        /// 请购数量
        /// </summary>
        public decimal reqqty { get; set; }
        /// <summary>
        /// 采购数量
        /// </summary>
        public decimal purqty { get; set; }
        /// <summary>
        /// 入库数量
        /// </summary>
        public decimal inqty { get; set; }
        /// <summary>
        /// 物料状态
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 物料类别
        /// </summary>
        public int itemType { get; set; }
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

    }
}
