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
    /// 工厂工序表
    /// </summary>
    /// 
    public class ManWorkProcess
    {
        /// 系统Guid
        [Key]
        [Column(Order = 1)]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }
        /// 组织ID
        public int OrgID { get; set; }
        /// 工厂Guid  
        public Guid? factoryGuid { get; set; }
        /// 工序编号
        [Column(TypeName = "varchar(10)")]
        public string workCode { get; set; }
        /// 工序名称
        [Column(TypeName = "varchar(30)")]
        public string workName { get; set; }
        /// 产值比例
        [Column(TypeName = "decimal(12,4)")]
        public decimal rate { get; set; }
        /// 有效
        [Column(TypeName = "tinyint")]
        public int isvalid { get; set; }
        /// 创建人员
        [Column(TypeName = "nvarchar(20)")]
        public string creuser { get; set; }
        /// 创建日期
        public DateTime? credate { get; set; }
        /// 修改人员
        [Column(TypeName = "nvarchar(20)")]
        public string moduser { get; set; }
        /// 修改日期
        public DateTime? moddate { get; set; }
    }



}
