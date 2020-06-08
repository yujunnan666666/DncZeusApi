/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * DESCRIPTION:     用户信息实体类
 ******************************************/
//using DncZeus.Api.Entities;
using System;


namespace DncZeus.Api.ViewModels.Pmc.Project
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public class ProjectPlanDayCreateModel {
            public string col { get; set; }
            public ProjectPlanDayViewModel row { get; set; }
    }
        public class ProjectPlanDayViewModel
    {
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
        public int prkeepdate { get; set; }
        /// <summary>
        /// 原执行天数
        /// </summary>
        public int prdodate { get; set; }
        /// <summary>
        /// 现前置天数
        /// </summary>
        public int keepdate { get; set; }
        /// <summary>
        /// 现执行天数
        /// </summary>
        public int dodate { get; set; }


    }
}
