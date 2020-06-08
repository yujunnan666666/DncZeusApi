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
    /// 
    public class MisCode
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
        public MisCkind MisCkind { get; set; }
        /// <summary>
        /// 组织实体
        /// </summary>
        public MisOrganization MisOrganization { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Column(TypeName = "varchar(500)")]
        public string cdesc { get; set; }
        /// <summary>
        /// 自定义编号1
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string extendCode1 { get; set; }
        /// <summary>
        /// 自定义编号2
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string extendCode2 { get; set; }
        /// <summary>
        /// 自定义编号3
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string extendCode3 { get; set; }
        /// <summary>
        /// 自定义编号4
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string extendCode4 { get; set; }
        /// <summary>
        /// 自定义编号5
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string extendCode5 { get; set; }
        /// <summary>
        /// 自定义编号6
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string extendCode6 { get; set; }
        /// <summary>
        /// 自定义编号7
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string extendCode7 { get; set; }
        /// <summary>
        /// 自定义编号8
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string extendCode8 { get; set; }
        /// <summary>
        /// 自定义编号9
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string extendCode9 { get; set; }
        /// <summary>
        /// 自定义名称1
        /// </summary>
        [Column(TypeName = "nvarchar(200)")]
        public string extendName1 { get; set; }
        /// <summary>
        /// 自定义名称2
        /// </summary>
        [Column(TypeName = "nvarchar(200)")]
        public string extendName2 { get; set; }
        /// <summary>
        /// 自定义名称3
        /// </summary>
        [Column(TypeName = "nvarchar(200)")]
        public string extendName3 { get; set; }
        /// <summary>
        /// 自定义名称4
        /// </summary>
        [Column(TypeName = "nvarchar(200)")]
        public string extendName4 { get; set; }
        /// <summary>
        /// 自定义名称5
        /// </summary>
        [Column(TypeName = "nvarchar(200)")]
        public string extendName5 { get; set; }
        /// <summary>
        /// 自定义名称6
        /// </summary>
        [Column(TypeName = "nvarchar(200)")]
        public string extendName6 { get; set; }
        /// <summary>
        /// 自定义名称7
        /// </summary>
        [Column(TypeName = "nvarchar(200)")]
        public string extendName7 { get; set; }
        /// <summary>
        /// 自定义名称8
        /// </summary>
        [Column(TypeName = "nvarchar(200)")]
        public string extendName8 { get; set; }
        /// <summary>
        /// 自定义名称9
        /// </summary>
        [Column(TypeName = "nvarchar(200)")]
        public string extendName9 { get; set; }
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

    }

}
