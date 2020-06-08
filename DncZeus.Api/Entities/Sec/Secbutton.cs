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
    /// secbutton按钮基本档
    /// </summary>
    /// 
    public class Secbutton
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }
        /// <summary>
        /// 按钮编号
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string butno { get; set; }
        /// <summary>
        /// 按钮名称
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string butname { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Column(TypeName = "smallint")]
        public int butorder { get; set; }
        /// <summary>
        /// 是否有效Y;N
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string enabled { get; set; }


    }



}
