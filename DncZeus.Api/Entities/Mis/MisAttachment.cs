/******************************************
 * AUTHOR:          LTB
 * CREATEDON:       2019-10-08
 * Project:         系统档案
 ******************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.Entities
{
    /// <summary>
    /// 附件基本档
    /// </summary>
    public class MisAttachment
    {
        /// <summary>
        /// GUID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }
        /// <summary>
        /// 主路径
        /// </summary>
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string mainUrl { get; set; }
        /// <summary>
        /// 分类路径
        /// </summary>
        [Column(TypeName = "nvarchar(100)")]
        public string folderName { get; set; }
        /// <summary>
        /// 年份
        /// </summary>
        [Column(TypeName = "nvarchar(4)")]
        public string yyyy { get; set; }
        /// <summary>
        /// 月日
        /// </summary>
        [Column(TypeName = "nvarchar(4)")]
        public string mmdd { get; set; }
        /// <summary>
        /// 完整路径
        /// </summary>
        [Column(TypeName = "nvarchar(300)")]
        public string pathUrl { get; set; }
        /// <summary>
        /// 压缩文件路径
        /// </summary>
        [Column(TypeName = "nvarchar(300)")]
        public string thumbUrl { get; set; }
        /// <summary>
        /// 来源路径
        /// </summary>
        [Column(TypeName = "nvarchar(300)")]
        public string frPathUrl { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        [Column(TypeName = "nvarchar(200)")]
        public string fillName { get; set; }
        /// <summary>
        /// 文件扩展名
        /// </summary>
        [Column(TypeName = "nvarchar(200)")]
        public string fillType { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public int fillSize { get; set; }
        /// <summary>
        /// 上传日期
        /// </summary>
        public DateTime? credate { get; set; }
        /// <summary>
        /// 上传人员
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string creuser { get; set; }
    }

}
