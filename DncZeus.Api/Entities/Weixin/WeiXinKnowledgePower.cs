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

namespace DncZeus.Api.Entities.Weixin
{
    /// <summary>
    /// 知识权限表
    /// </summary>
    /// 
    public class WeiXinKnowledgePower
    {
        /// 系统Guid
        [Key]
        [Column(Order = 1)]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }
        /// 知识Guid
        public Guid? knowledgeGuid { get; set; }
        /// 状态
        [Column(TypeName = "char(1)")]
        public string usetype { get; set; }
        /// 内容
        [Column(TypeName = "varchar(10)")]
        public string userno { get; set; }
        /// 内容
        [Column(TypeName = "char(1)")]
        public string candown { get; set; }
        /// 创建人员
        [Column(TypeName = "nvarchar(20)")]
        public string creuser { get; set; }
        /// 创建日期
        public DateTime? credate { get; set; }
       
    }



}
