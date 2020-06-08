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
    /// 车间生产日报表
    /// </summary>
    /// 
    public class ManWorkshopOut
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
        /// 工厂工序Guid  
        public Guid? workProcessGuid { get; set; }
        /// 计产方式
        [Column(TypeName = "tinyint")]
        public int countType { get; set; }
        /// 单据编号
        [Column(TypeName = "varchar(20)")]
        public string ordCode { get; set; }
        /// 料品编号
        [Column(TypeName = "varchar(20)")]
        public string itemCode { get; set; }
        /// 料品名称
        [Column(TypeName = "varchar(200)")]
        public string itemName { get; set; }
        /// 工厂价
        [Column(TypeName = "decimal(12,4)")]
        public decimal factoryPrice { get; set; }
        /// 需求数量
        public int ordQty { get; set; }
        /// 累计完成数量
        public int sumQty { get; set; }
        /// 创建人员
        [Column(TypeName = "nvarchar(20)")]
        public string creuser { get; set; }
        /// 创建日期
        public DateTime? credate { get; set; }
        
    }



}
