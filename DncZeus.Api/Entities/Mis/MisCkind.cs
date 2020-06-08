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
    /// 基础档案类别
    /// </summary>
    public class MisCkind
    {
        /// <summary>
        /// 系统ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 类别目录实体
        /// </summary>
        public MisCatalog MisCatalog { get; set; }
        /// <summary>
        /// 是否属于多组织
        /// </summary>
        /// 
        [Column(TypeName = "char(1)")]
        public string isOrgValid { get; set; }
        /// <summary>
        /// 类别编号
        /// </summary>
        [Required]
        [Column(TypeName = "nvarchar(2)")]
        public string ckind { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        [Column(TypeName = "nvarchar(30)")]
        public string ckdesc { get; set; }
        /// <summary>
        /// 数据长度
        /// </summary>
        public Int16 clen { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public Int16 trseq { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string isEnabled { get; set; }
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
        /// 图标
        /// </summary>
        [Column(TypeName = "varchar(128)")]
        public string Icon { get; set; }

    }

}
