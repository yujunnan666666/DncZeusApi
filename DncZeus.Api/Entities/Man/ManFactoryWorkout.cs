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
    /// 工厂日产值登记表
    /// </summary>
    /// 
    public class ManFactoryWorkout
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
        /// 日期
        public DateTime? wrokDate { get; set; }
        /// 目标产值
        [Column(TypeName = "decimal(12,4)")]
        public decimal targetOut { get; set; }
        /// 实际产值
        [Column(TypeName = "decimal(12,4)")]
        public decimal realityOut { get; set; }
        /// 合计工时数
        [Column(TypeName = "decimal(12,4)")]
        public decimal sumTime { get; set; }
        /// 状态
        [Column(TypeName = "tinyint")]
        public int status { get; set; }
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
