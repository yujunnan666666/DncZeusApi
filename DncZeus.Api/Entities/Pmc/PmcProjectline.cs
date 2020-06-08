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
    /// 项目明细行表
    /// </summary>
    /// 
    public class PmcProjectline
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }
        /// <summary>
        /// 项目Guid
        /// </summary>
        public Guid? projectsGuid { get; set; }
        /// <summary>
        /// 客户料号
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string custitemcode { get; set; }
        /// <summary>
        /// 客户料名
        /// </summary>
        [Column(TypeName = "varchar(200)")]
        public string custitemname { get; set; }
        /// <summary>
        /// 公司款号
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string styleNO { get; set; }
        /// <summary>
        /// 图号
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string imageNO { get; set; }
        /// <summary>
        /// 制造工厂
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string factory { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        [Column(TypeName = "varchar(200)")]
        public string desc { get; set; }
        /// <summary>
        /// 主图片
        /// </summary>
        [Column(TypeName = "varchar(200)")]
        public string images { get; set; }
        /// <summary>
        /// 主图片路径
        /// </summary>
        [Column(TypeName = "varchar(300)")]
        public string imgSrc { get; set; }
        /// <summary>
        /// 其他图片
        /// </summary>
        [Column(TypeName = "varchar(200)")]
        public string otherImages { get; set; }
        /// <summary>
        /// 其他图片路径
        /// </summary>
        [Column(TypeName = "varchar(2000)")]
        public string otherImgSrc { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Column(TypeName = "decimal(10,2)")]
        public decimal qty { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        [Column(TypeName = "decimal(10,4)")]
        public decimal price { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        [Column(TypeName = "decimal(10,2)")]
        public decimal amount { get; set; }
        /// <summary>
        /// 需求日期
        /// </summary>
        public DateTime? reqDate { get; set; }
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
