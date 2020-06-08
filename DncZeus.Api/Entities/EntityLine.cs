/******************************************
 * AUTHOR:          LTB
 * CREATEDON:       2019-11-08
 * Project:         系统架构
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
    /// 行实体公共字段
    /// </summary>
    /// 
    public class EntityLine
    {
        /// <summary>
        /// 系统ID
        /// </summary>
        [Key]
        [DefaultValue("newid()")]
        [Column(Order = 1)]
        public Guid Guid { get; set; }
        /// <summary>
        /// 建立人员
        /// </summary>
        [Column(Order = 120, TypeName = "nvarchar(20)")]
        public string creuser { get; set; }
        /// <summary>
        /// 建立日期
        /// </summary>
        [Column(Order = 121)]
        public DateTime? credate { get; set; }
        /// <summary>
        /// 修改人员
        /// </summary>
        [Column(Order = 122, TypeName = "nvarchar(20)")]
        public string moduser { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        [Column(Order = 123)]
        public DateTime? moddate { get; set; }
    }

}
