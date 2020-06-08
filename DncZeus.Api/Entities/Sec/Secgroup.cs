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
    /// secgroup群组表
    /// </summary>
    /// 
    public class Secgroup
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }
        /// <summary>
        /// 群组编号
        /// </summary>  
        [Column(TypeName = "varchar(8)")]
        public string groupno { get; set; }
        /// <summary>
        /// 群组类别    R-系统管理员;U-用户
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string gtype { get; set; }
        /// <summary>
        /// 群组名称
        /// </summary>
        [Column(TypeName = "varchar(48)")]
        public string groupname { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column(TypeName = "varchar(200)")]
        public string remark { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string enabled { get; set; }


    }



}
