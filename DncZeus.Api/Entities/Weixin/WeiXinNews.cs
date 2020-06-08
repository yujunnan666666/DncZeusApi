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
    /// 新闻/公告表
    /// </summary>
    /// 
    public class WeiXinNews
    {
        /// 系统Guid
        [Key]
        [Column(Order = 1)]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }
        /// 组织ID
        public int OrgID { get; set; }
        /// 标题
        [Column(TypeName = "varchar(50)")]
        public string NewsName { get; set; }
        /// 简述
        [Column(TypeName = "varchar(100)")]
        public string NewsSketch { get; set; }
        /// 图片路径
        [Column(TypeName = "varchar(200)")]
        public string pathUrl { get; set; }
        /// 图片压缩路径
        [Column(TypeName = "varchar(200)")]
        public string thumbUrl { get; set; }
        /// 是否滚动
        [Column(TypeName = "char(1)")]
        public string isRoll { get; set; }
        /// 状态
        [Column(TypeName = "char(1)")]
        public string status { get; set; }
        /// 类型
        [Column(TypeName = "char(1)")]
        public string type { get; set; }
        /// 内容
        [Column(TypeName = "nvarchar(MAX)")]
        public string HtmlContent { get; set; } 
        /// 创建人员
        [Column(TypeName = "nvarchar(20)")]
        public string creuser { get; set; }
        /// 创建日期
        public DateTime? credate { get; set; }
        /// 修改人员
        [Column(TypeName = "nvarchar(20)")]
        public string moduser { get; set; }
        /// 修改日期
        public DateTime? moddate { get; set; }
        /// 审核人员
        [Column(TypeName = "nvarchar(20)")]
        public string cfmuser { get; set; }
        /// 审核日期
        public DateTime? cfmdate { get; set; }
    }



}
