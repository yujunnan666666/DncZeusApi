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
    /// 操作记录表
    /// </summary>
    /// 
    public class PmcWorknote
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
        /// 类别
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int wtype { get; set; }
        /// <summary>
        /// 操作内容
        /// </summary>
        [Column(TypeName = "varchar(200)")]
        public string followRemark { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? credate { get; set; }
        /// <summary>
        /// 操作人员
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string creuser { get; set; }
       
    
        
        
    }



}
