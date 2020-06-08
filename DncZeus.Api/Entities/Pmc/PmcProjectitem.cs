/******************************************
 * AUTHOR:          LTB
 * CREATEDON:       2019-11-08
 * Project:         
 ******************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.Entities.Pmc
{
    /// <summary>
    /// 难点物料需求表
    /// </summary>
    /// 
    public class PmcProjectitem
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }
        /// <summary>
        /// 系统Guid
        /// </summary>
        public Guid? projectlineGuid { get; set; }
        /// <summary>
        /// 物料编号
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string itemcode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        [Column(TypeName = "varchar(200)")]
        public string itemname { get; set; }
        /// <summary>
        /// erp名称
        /// </summary>
        [Column(TypeName = "varchar(200)")]
        public string erpname { get; set; }
        /// <summary>
        /// 需求数量
        /// </summary>
        [Column(TypeName = "decimal(10,2)")]
        public decimal qty { get; set; }
        /// <summary>
        /// 需求单位
        /// </summary>
        [Column(TypeName = "varchar(10)")]
        public string unitcode { get; set; }
        /// <summary>
        /// 需求日期
        /// </summary>
        public DateTime? reqDate { get; set; }
        /// <summary>
        /// 请购数量
        /// </summary>
        [Column(TypeName = "decimal(10,2)")]
        public decimal reqqty { get; set; }
        /// <summary>
        /// 采购数量
        /// </summary>
        [Column(TypeName = "decimal(10,2)")]
        public decimal purqty { get; set; }
        /// <summary>
        /// 入库数量
        /// </summary>
        [Column(TypeName = "decimal(10,2)")]
        public decimal inqty { get; set; }
        /// <summary>
        /// 物料状态
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int status { get; set; }
        /// <summary>
        /// 物料类别    1：难点物料 2：重点物料
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int itemType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column(TypeName = "varchar(200)")]
        public string remark { get; set; }
        /// <summary>
        /// 创建人员
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string creuser { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? credate { get; set; }
        /// <summary>
        /// 修改人员
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string moduser { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? moddate { get; set; }
        
    }



}
