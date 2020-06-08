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
    /// 组织基本档
    /// </summary>
    public class ApprovalGoods
    {
        /// <summary>
        /// 系统ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Guid { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [Column(TypeName = "nvarchar(10)")]
        public string username { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        [Column(TypeName = "nvarchar(40)")]
        public string department { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string type { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string status { get; set; }
        /// <summary>
        /// 审批单号
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string ordernum { get; set; }
        /// <summary>
        /// 模板号
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string tmpnum { get; set; }
        /// <summary>
        /// 用途
        /// </summary>
        [Column(TypeName = "nvarchar(200)")]
        public string purpose { get; set; }
        /// <summary>
        /// 物品名称
        /// </summary>
        [Column(TypeName = "nvarchar(200)")]
        public string goodsname { get; set; }
        /// <summary>
        /// 物品数量
        /// </summary>
        [Column(TypeName = "nvarchar(200)")]
        public string goodscount { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        [Column(TypeName = "nvarchar(1000)")]
        public string attachment { get; set; }
        /// <summary>
        /// 建立日期
        /// </summary>
        public DateTime? credate { get; set; }
       
    }

}
