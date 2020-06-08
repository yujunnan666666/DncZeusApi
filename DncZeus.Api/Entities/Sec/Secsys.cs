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

namespace DncZeus.Api.Entities.Sec
{
    /// <summary>
    /// secsys系统模组
    /// </summary>
    /// 
    public class Secsys 
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }
        /// <summary>
        /// 系统号
        /// </summary>  
        [Column(TypeName = "varchar(2)")]
        public string sysno { get; set; }
        /// <summary>
        /// 系统名称
        /// </summary>
        [Column(TypeName = "varchar(48)")]
        public string sysname { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Column(TypeName = "smallint")]
        public int sysorder { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string enabled { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [Column(TypeName = "varchar(128)")]
        public string icon { get; set; }


    }



}
