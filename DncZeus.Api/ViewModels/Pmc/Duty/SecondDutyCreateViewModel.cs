/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * DESCRIPTION:     用户信息实体类
 ******************************************/
//using DncZeus.Api.Entities;
using System;


namespace DncZeus.Api.ViewModels.Pmc.Duty
{
    /// <summary>
    /// 
    /// </summary>
    public class SecondDutyCreateViewModel
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// 上级任务
        /// </summary>
        public Guid? dutiesGuid { get; set; }
        /// <summary>
        /// 次级任务
        /// </summary>
        public Guid? secondGuid { get; set; }
        /// <summary>
        /// 创建人员
        /// </summary>
        public string creuser { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? credate { get; set; }
       
    }
}
