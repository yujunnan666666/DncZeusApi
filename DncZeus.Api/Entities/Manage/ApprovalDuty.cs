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
    /// 
    /// </summary>
    public class ApprovalDuty
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
        /// 人员名称
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string name { get; set; }
        /// <summary>
        /// 体温
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string temperature { get; set; }
        /// <summary>
        /// 口罩领取
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string getMask { get; set; }
        /// <summary>
        /// 建立日期
        /// </summary>
        public DateTime? credate { get; set; }
       
    }

}
