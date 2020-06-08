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
    /// 工厂日程表
    /// </summary>
    /// 
    public class PmcOrderday
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
        /// 类别
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int dtype { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime day { get; set; }
        /// <summary>
        /// 是否休息
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int isrest { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column(TypeName = "varchar(50)")]
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
        
    }



}
