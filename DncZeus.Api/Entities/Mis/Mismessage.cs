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

namespace DncZeus.Api.Entities.Mis
{
    /// <summary>
    /// 消息基本档
    /// </summary>
    /// 
    public class Mismessage
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }
        /// <summary>
        /// 组织ID
        /// </summary>
        public int Org { get; set; }
        /// <summary>
        /// 消息类别
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int mtype { get; set; }
        /// <summary>
        /// 来源单据ID
        /// </summary> 
        public int SourceID { get; set; }

        /// <summary>
        /// 发送人帐号
        /// </summary> 
        [Column(TypeName = "varchar(20)")]
        public string sendUserno { get; set; }
        /// <summary>
        /// 发送人姓名
        /// </summary> 
        [Column(TypeName = "varchar(20)")]
        public string sendUsername { get; set; }

        /// <summary>
        /// 接收人帐号
        /// </summary> 
        [Column(TypeName = "varchar(20)")]
        public string receiveUserno { get; set; }
        /// <summary>
        /// 接收人姓名
        /// </summary> 
        [Column(TypeName = "varchar(20)")]
        public string receiveUsername { get; set; }
        /// <summary>
        /// 内容
        /// </summary> 
        [Column(TypeName = "varchar(250)")]
        public string message { get; set; }

        /// <summary>
        /// 收到时间
        /// </summary>
        public DateTime? receiveTime { get; set; }
        /// <summary>
        /// 阅读状态
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int readStatus { get; set; }
        /// <summary>
        /// 阅读时间
        /// </summary>
        public DateTime? readTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int status { get; set; }
        

    }



}
