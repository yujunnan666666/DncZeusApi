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
    /// 计划跟进人
    /// </summary>
    /// 
    public class WjgcPlanfllowuser
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }
        /// <summary>
        /// 计划跟进表Guid
        /// </summary>
        public Guid? planfllowGuid { get; set; }
        /// <summary>
        /// 是否信息提醒
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int isnotice { get; set; }
        /// <summary>
        /// 人员
        /// </summary>
        public string userno { get; set; }
        /// <summary>
        /// 信息发送状态
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int status { get; set; }
        /// <summary>
        /// 信息发送时间
        /// </summary>
        public DateTime? senddate { get; set; }
        /// <summary>
        /// 创建人员
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string creuser { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? credate { get; set; }
    
        
        
    }



}
