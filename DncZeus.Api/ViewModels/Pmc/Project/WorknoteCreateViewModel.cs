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
        public class WorknoteCreateViewModel
        {
        /// <summary>
        /// 系统Guid
        /// </summary>
        public Guid projectsGuid { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public int wtype { get; set; }
        /// <summary>
        /// 操作内容
        /// </summary>
        public string followRemark { get; set; }
        /// <summary>
        ///操作人
        /// </summary>
        public string creuser { get; set; }
        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime credate { get; set; }



    }
}
