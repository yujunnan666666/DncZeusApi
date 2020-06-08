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
    public class ApprovalStaffBack
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
        /// 返程日期
        /// </summary>
        public DateTime? backDate { get; set; }
        /// <summary>
        /// 到达中山日期
        /// </summary>
        public DateTime? zsDate { get; set; }
        /// <summary>
        /// 交通工具
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string vehicle { get; set; }
        /// <summary>
        /// 返程是否经过疫区
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string isPass { get; set; }
        /// <summary>
        /// 中山居住地详细地址
        /// </summary>
        [Column(TypeName = "nvarchar(200)")]
        public string address { get; set; }
        /// <summary>
        /// 外地回粤后是否有向居委报备
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string isReport { get; set; }
        /// <summary>
        /// 是否接触疫区人员
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string isTouch { get; set; }
        /// <summary>
        /// 是否发烧
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string isFever { get; set; }
        /// <summary>
        /// 是否感染
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string isIll { get; set; }
        /// <summary>
        /// 出发地
        /// </summary>
        [Column(TypeName = "nvarchar(200)")]
        public string startPlace { get; set; }
        /// <summary>
        /// 目的地
        /// </summary>
        [Column(TypeName = "nvarchar(200)")]
        public string endPlace { get; set; }
        /// <summary>
        /// 居民居住在性质
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string liveType { get; set; }
        /// <summary>
        /// 有效居家隔离开始时间
        /// </summary>
        public DateTime? quarantine { get; set; }
        /// <summary>
        /// 建立日期
        /// </summary>
        public DateTime? credate { get; set; }
       
    }

}
