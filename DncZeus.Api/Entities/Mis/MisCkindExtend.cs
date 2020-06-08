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
    /// 基础档案类别扩展
    /// </summary>
    public class MisCkindExtend
    {
        /// <summary>
        /// 系统ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 类别id
        /// </summary>
        /*public MisCatalog MisCatalog { get; set; }*/
        public int MisCKindId { get; set; }
        /// <summary>
        /// 扩展序号
        /// </summary>
        /// 
        [Column(TypeName = "tinyint")]
        public int Num { get; set; }
        /// <summary>
        /// 栏位名称
        /// </summary>  
        [Column(TypeName = "nvarchar(10)")]
        public string colName { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int colType { get; set; }
        /// <summary>
        /// 来源id
        /// </summary>
        public int sourceId { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        [Column(TypeName = "char(1)")]
        public int isEnabled { get; set; }
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
