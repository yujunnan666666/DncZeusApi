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
    /// 工作计划日登记表
    /// </summary>
    /// 
    public class PmcPlannote
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }
        /// <summary>
        /// 项目明细表Guid
        /// </summary>
        public Guid? projectlineGuid { get; set; }
        /// <summary>
        /// 项目计划表Guid
        /// </summary>
        public Guid? projectplanGuid { get; set; }
        /// <summary>
        /// 难点重点物料Guid
        /// </summary>
        public Guid? projectitemGuid { get; set; }
        /// <summary>
        /// 是否工厂
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int isfactory { get; set; }
        /// <summary>
        /// 数夫制令号
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string mono { get; set; }
        /// <summary>
        /// 制造工厂
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string factory { get; set; }
        /// <summary>
        /// 工作日
        /// </summary>
        public DateTime? workdate { get; set; }
        /// <summary>
        /// 工作状态
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int workstatus { get; set; }
        /// <summary>
        /// 当日完工数量
        /// </summary>
        [Column(TypeName = "smallint")]
        public int workqty { get; set; }
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
