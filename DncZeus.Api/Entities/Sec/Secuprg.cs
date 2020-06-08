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
    /// secuprg用户权限表
    /// </summary>
    /// 
    public class Secuprg
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }
        /// <summary>
        /// 组织编号
        /// </summary>  
        public int orgid { get; set; }
        /// <summary>
        /// 帐户编号
        /// </summary>
        public Guid userid { get; set; }
        /// <summary>
        /// 程式编号
        /// </summary>
        public Guid prgid { get; set; }
        /// <summary>
        /// 按钮编号
        /// </summary>
        public Guid butid { get; set; }
        /// <summary>
        /// 建立日期
        /// </summary>
        public DateTime? credate { get; set; }
        /// <summary>
        /// 建立人员
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string creuser { get; set; }


    }



}
