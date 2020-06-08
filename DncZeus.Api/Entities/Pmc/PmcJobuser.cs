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
    /// 岗位人员表
    /// </summary>
    /// 
    public class PmcJobuser
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
        /// 岗位Guid （飞废弃)
        /// </summary>
        public Guid jobGuid { get; set; }
        /// <summary>
        /// 岗位ID
        /// </summary>
        public int jobId { get; set; }
        /// <summary>
        /// 用户Guid 0-否；1-是
        /// </summary>
        public Guid? userGuid { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int isvalid { get; set; }
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
