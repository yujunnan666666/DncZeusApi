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
    /// 工厂基本表
    /// </summary>
    /// 
    public class ManFactory
    {
        /// 系统Guid
        [Key]
        [Column(Order = 1)]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }
        /// 组织ID
        public int OrgID { get; set; }
        /// 工厂编号
        [Column(TypeName = "varchar(20)")]
        public string code { get; set; }
        /// 工厂名称
        [Column(TypeName = "varchar(20)")]
        public string name { get; set; }
        /// 计产方式
        [Column(TypeName = "tinyint")]
        public int countType { get; set; }
        /// 干部人数
        [Column(TypeName = "smallint")]
        public int cadre { get; set; }
        /// 后勤人数
        [Column(TypeName = "smallint")]
        public int logistic { get; set; }
        /// 生产人数
        [Column(TypeName = "smallint")]
        public int worker { get; set; }
        /// 目标月人均产值
        [Column(TypeName = "decimal(12,4)")]
        public decimal pertargetOut { get; set; }
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
