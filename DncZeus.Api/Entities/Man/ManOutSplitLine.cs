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
    /// 订单产值分配子表
    /// </summary>
    /// 
    public class ManOutSplitLine
    {
        /// 系统Guid
        [Key]
        [Column(Order = 1)]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }
        /// 订单产值分配表Guid 
        public Guid? workProcessGuid { get; set; }
        /// 工厂工序Guid 
        public Guid? ProcessGuid { get; set; }
        /// 产值比例
        [Column(TypeName = "decimal(12,4)")]
        public decimal rate { get; set; }
        
    }



}
