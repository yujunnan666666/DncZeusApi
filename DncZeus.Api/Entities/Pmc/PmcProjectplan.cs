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
    /// 项目计划表
    /// </summary>
    /// 
    public class PmcProjectplan
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }
        /// <summary>
        /// 组织ID
        /// </summary>
        public Guid? projectsGuid { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int ptype { get; set; }
        /// <summary>
        /// 任务
        /// </summary>
        public Guid? dutiesGuid { get; set; }
        /// <summary>
        /// 最新进展
        /// </summary>
        [Column(TypeName = "varchar(300)")]
        public string curProgress { get; set; }
        /// <summary>
        /// 是否要报进度
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int iskeep { get; set; }
        /// <summary>
        /// 执行天数
        /// </summary>
        [Column(TypeName = "smallint")]
        public int dodate { get; set; }
        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime? pbegindate { get; set; }
        /// <summary>
        /// 计划完成时间
        /// </summary>
        public DateTime? penddate { get; set; }
        /// <summary>
        /// 实际开始时间
        /// </summary>
        public DateTime? abegindate { get; set; }
        /// <summary>
        /// 实际完成时间
        /// </summary>
        public DateTime? aenddate { get; set; }
        /// <summary>
        /// 累计完成数量
        /// </summary>
        [Column(TypeName = "decimal(10,2)")]
        public decimal totalqty { get; set; }
        /// <summary>
        /// 假期天数
        /// </summary>
        [Column(TypeName = "decimal(10,2)")]
        public decimal restCount { get; set; }
        /// <summary>
        /// 任务完成状态
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int status { get; set; } 
        /// <summary>
        /// 备注
        /// </summary>
        [Column(TypeName = "varchar(200)")]
        public string remark { get; set; }
        /// <summary>
        /// 休息说明
        /// </summary>
        [Column(TypeName = "varchar(200)")]
        public string restDesc { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int isdel { get; set; }
    }



}
