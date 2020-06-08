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
    /// secprg程式模组
    /// </summary>
    /// 
    public class Secprg
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
        [Column(TypeName = "varchar(16)")]
        public string prgno { get; set; }
        /// <summary>
        /// 程式名称
        /// </summary>
        [Column(TypeName = "varchar(48)")]
        public string prgname { get; set; }
        /// <summary>
        /// 程式路径
        /// </summary>
        [Column(TypeName = "varchar(255)")]
        public string program { get; set; }
        /// <summary>
        /// 程式组件
        /// </summary>
        [Column(TypeName = "varchar(255)")]
        public string prgcomp { get; set; }
        /// <summary>
        /// 开窗方式 A-主窗口;B-弹出窗号;C-外挂
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string runtype { get; set; }
        /// <summary>
        /// 授权方式 0-开放；1-不开放
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string prgtype { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Column(TypeName = "smallint")]
        public int prgorder { get; set; }
        /// <summary>
        /// 是否有效Y;N
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string enabled { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [Column(TypeName = "varchar(128)")]
        public string icon { get; set; }

        /// <summary>
        /// 主窗口id
        /// </summary>  
        public Guid? mainid { get; set; }

    }



}
