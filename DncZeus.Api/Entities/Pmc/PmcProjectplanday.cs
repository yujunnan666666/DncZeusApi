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
    /// 项目计划日程表
    /// </summary>
    /// 
    public class PmcProjectplanday
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        public Guid? projectsGuid { get; set; }
        /// <summary>
        /// 任务ID
        /// </summary>
        public Guid? dutiesGuid { get; set; }
        /// <summary>
        /// 原前置天数
        /// </summary>
        [Column(TypeName = "smallint")]
        public int prkeepdate { get; set; }
        /// <summary>
        /// 原执行天数
        /// </summary>
        [Column(TypeName = "smallint")]
        public int prdodate { get; set; }
        /// <summary>
        /// 现前置天数
        /// </summary>
        [Column(TypeName = "smallint")]
        public int keepdate { get; set; }
        /// <summary>
        /// 现执行天数
        /// </summary>
        [Column(TypeName = "smallint")]
        public int dodate { get; set; }

    }



}
