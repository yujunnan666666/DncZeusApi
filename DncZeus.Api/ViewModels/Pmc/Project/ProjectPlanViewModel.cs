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
    public class ProjectPlanViewModel
    {
        
        /// <summary>
        /// 难点物料状态
        /// </summary>
        public int diffItem { get; set; }
        /// <summary>
        /// 重点物料状态
        /// </summary>
        public int focusitem { get; set; }
        public Guid? projectGuid { get; set; }
        public int curPage { get; set; }
        public int pageSize { get; set; }

    }
}
