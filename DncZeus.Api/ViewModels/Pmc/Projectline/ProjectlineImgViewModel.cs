/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * DESCRIPTION:     用户信息实体类
 ******************************************/
//using DncZeus.Api.Entities;
using System;


namespace DncZeus.Api.ViewModels.Pmc.Projectline
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectlineImgViewModel
    {
        /// <summary>
        /// Guid
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// 主图guids
        /// </summary>
        public string attGuids { get; set; }
        /// <summary>
        /// 副图guids
        /// </summary>
        public string attGuids2 { get; set; }


    }
}
