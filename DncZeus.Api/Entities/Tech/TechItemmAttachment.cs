/******************************************
 * AUTHOR:          LTB
 * CREATEDON:       2019-10-10
 * Project:         资料管理项目
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
    /// 产品附件档
    /// </summary>
    public class TechItemmAttachment
    {
        /// <summary>
        /// 用户GUID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }
        /// <summary>
        /// 款式基本档实体
        /// </summary>
        public TechItemMaster TechItemMaster { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string attType { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        [Column(TypeName = "nvarchar(200)")]
        public string fileName { get; set; }
        /// <summary>
        /// 附件实体
        /// </summary>
        public MisAttachment MisAttachment { get; set; }
        /// <summary>
        /// 视图方向
        /// </summary>
        [Column(TypeName = "nvarchar(10)")]
        public string direction { get; set; }
        /// <summary>
        /// 图片来源
        /// </summary>
        [Column(TypeName = "nvarchar(10)")]
        public string picsource { get; set; }
        /// <summary>
        /// 百度智能云上的log_id
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string log_id { get; set; }
        /// <summary>
        /// 百度智能云上的cont_sign
        /// </summary>
        [Column(TypeName = "nvarchar(100)")]
        public string cont_sign { get; set; }
        /// <summary>
        /// 款式备注
        /// </summary>
        [Column(TypeName = "nvarchar(200)")]
        public string remark { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string isdel { get; set; }
        /// <summary>
        /// 建立人员
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string creuser { get; set; }
        /// <summary>
        /// 建立日期
        /// </summary>
        public DateTime? credate { get; set; }
        /// <summary>
        /// 修改人员
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string moduser { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? moddate { get; set; }
        /// <summary>
        /// 删除人员
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string deluser { get; set; }
        /// <summary>
        /// 删除日期
        /// </summary>
        public DateTime? deldate { get; set; }

    }


}
