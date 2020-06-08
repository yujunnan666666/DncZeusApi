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
    public class MisOrganization
    {
        /// <summary>
        /// 系统ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        [Required]
        [Column(TypeName = "nvarchar(10)")]
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Column(TypeName = "nvarchar(40)")]
        public string Name { get; set; }
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
    }

}
