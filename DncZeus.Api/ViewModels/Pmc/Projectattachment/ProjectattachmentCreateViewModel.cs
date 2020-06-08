/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * DESCRIPTION:     用户信息实体类
 ******************************************/
//using DncZeus.Api.Entities;
using System;


namespace DncZeus.Api.ViewModels.Pmc.Projectattachment
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectattachmentCreateViewModel
    {
        /// 系统Guid 
        public Guid Guid { get; set; }

        /// 父档类别
        public int parentType { get; set; }

        /// 父档ID
        public Guid? parentGuid { get; set; }

        ///类别
        public int attType { get; set; }

        /// 文件名
        public string fileName { get; set; }

        /// 附件Guid
        public Guid? attGuid { get; set; }

        /// 操作时间
        public DateTime? credate { get; set; }

        /// 操作人员
        public string creuser { get; set; }

    }
}
