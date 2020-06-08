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
    /// 车间生产日报子表
    /// </summary>
    /// 
    public class ManWorkshopOutLine
    {
        /// 系统Guid
        [Key]
        [Column(Order = 1)]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }
        /// 车间生产日报表Guid
        public Guid? workshopOutGuid { get; set; }
        /// 日期
        public DateTime? workDate { get; set; }
        /// 价值折算数量
        [Column(TypeName = "decimal(12,4)")]
        public decimal qty { get; set; }
        /// 产值
        [Column(TypeName = "decimal(12,4)")]
        public decimal output { get; set; }
        /// 说明
        [Column(TypeName = "varchar(200)")]
        public string remark { get; set; }
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
