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
    /// secgprg群组权限表
    /// </summary>
    /// 
    public class Secgprg
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }
        /// <summary>
        /// 组织id
        /// </summary>
       
        public int orgid { get; set; }
        /// <summary>
        /// 账户编号
        /// </summary>

        public Guid groupid { get; set; }
        /// <summary>
        /// 程式编号
        /// </summary>
        
        public Guid prgid { get; set; }
        /// <summary>
        /// 按钮编号
        /// </summary>
        
        public Guid butid { get; set; }
        /// <summary>
        /// 建立人员
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string creuser { get; set; }
        /// <summary>
        /// 建立日期
        /// </summary>
        public DateTime? credate { get; set; }



    }



}
