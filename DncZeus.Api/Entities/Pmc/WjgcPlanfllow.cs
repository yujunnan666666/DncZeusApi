/******************************************
 * AUTHOR:          LTB
 * CREATEDON:       2019-11-08
 * Project:         
 ******************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.Entities.Pmc
{
    /// <summary>
    /// 计划跟进表
    /// </summary>
    /// 
    public class WjgcPlanfllow
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }
        /// <summary>
        /// 项目计划表Guid
        /// </summary>
        public Guid? projectplanGuid { get; set; }
        /// <summary>
        /// 项目明细表Guid
        /// </summary>
        public Guid? projectlineGuid { get; set; }
        /// <summary>
        /// 难点重点物料Guid
        /// </summary>
        public Guid? projectitemGuid { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [Column(TypeName = "varchar(200)")]
        public string fllowDesc { get; set; }
        /// <summary>
        /// 是否更新任务状况
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int isnote { get; set; }
        /// <summary>
        /// 项目状况内容
        /// </summary>
        [Column(TypeName = "varchar(200)")]
        public string nodes { get; set; }
        /// <summary>
        /// 总裁关注
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int Supremo { get; set; }
        /// <summary>
        /// 生产总监关注
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int workmaster { get; set; }
        /// <summary>
        /// PMC关注
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int pmc { get; set; }
        /// <summary>
        /// 任务状态
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int status { get; set; }
        /// <summary>
        /// 创建人员
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string creuser { get; set; }
        /// <summary>
        /// 创建日期
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
