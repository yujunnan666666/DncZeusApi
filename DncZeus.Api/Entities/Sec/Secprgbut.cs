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
    /// secprgbut程式按钮
    /// </summary>
    /// 
    public class Secprgbut
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
        public Guid sysid { get; set; }
        /// <summary>
        /// 功能号
        /// </summary>
        public Guid funid { get; set; }
        /// <summary>
        /// 程式编号
        /// </summary>
        public Guid prgid { get; set; }
        /// <summary>
        /// 按钮id
        /// </summary>
        public Guid butid { get; set; }
        /// <summary>
        /// 按钮编号
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string butno { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Column(TypeName = "varchar(50)")]
        public string butname { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Column(TypeName = "smallint")]
        public int butorder { get; set; }
        
        


    }



}
