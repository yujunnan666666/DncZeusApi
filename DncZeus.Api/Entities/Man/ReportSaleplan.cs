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

namespace DncZeus.Api.Entities.Man
{
    /// <summary>
    /// 销售计划表
    /// </summary>
    /// 
    public class ReportSaleplan
    {
        /// 系统Guid
        [Key]
        [Column(Order = 1)]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }
        /// 部门名称
        [Column(TypeName = "varchar(20)")]
        public string DeptName { get; set; }
        /// 业务员
        [Column(TypeName = "varchar(20)")]
        public string SalesMan { get; set; }
        /// 年
        [Column(TypeName = "varchar(10)")]
        public string YY { get; set; }
        /// 月
        [Column(TypeName = "varchar(10)")]
        public string MM { get; set; }
        /// 金额
        [Column(TypeName = "decimal(14,2)")]
        public decimal amount { get; set; }
        /// 创建人员
        [Column(TypeName = "nvarchar(20)")]
        public string creuser { get; set; }
        /// 创建日期
        public DateTime? credate { get; set; }
        
    }



}
