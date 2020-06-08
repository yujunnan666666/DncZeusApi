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
    /// 任务基本表
    /// </summary>
    /// 
    public class PmcDuties
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
        public int OrgID { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Column(TypeName = "varchar(300)")]
        public string Name { get; set; }
        /// <summary>
        /// 责任部门
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string department { get; set; }
        /// <summary>
        /// 是否要报进度
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int iskeep { get; set; }
        /// <summary>
        /// 是否要报进度
        /// </summary>
        [Column(TypeName = "smallint")]
        public int keepdate { get; set; }
        /// <summary>
        /// 上级任务
        /// </summary>
        public Guid? dutiesGuid { get; set; }
        /// <summary>
        /// 执行天数
        /// </summary>
        [Column(TypeName = "smallint")]
        public int dodate { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int itype { get; set; }
        /// <summary>
        /// 执行类别
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int dotype { get; set; }
        /// <summary>
        /// 项目状态
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int projectStatus{ get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column(TypeName = "varchar(200)")]
        public string remark { get; set; }
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
        /// <summary>
        /// 审核人员
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string cfmuser { get; set; }
        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? cfmdate { get; set; }
    }



}
