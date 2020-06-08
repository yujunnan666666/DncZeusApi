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
    /// secuserorg用户组织表
    /// </summary>
    /// 
    public class Secuserorg
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }
        /// <summary>
        /// 帐户编号
        /// </summary>  
        public Guid userid { get; set; }
        /// <summary>
        /// 组织编号  
        /// </summary>
        public int orgid { get; set; }

        



    }



}
