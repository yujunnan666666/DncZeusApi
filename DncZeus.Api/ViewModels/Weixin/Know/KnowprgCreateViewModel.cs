/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * DESCRIPTION:     用户信息实体类
 ******************************************/
//using DncZeus.Api.Entities;
using System;
using System.Collections.Generic;

namespace DncZeus.Api.ViewModels.Weixin.Know
{
    /// <summary>
    /// 
    /// </summary>
    public class KnowprgCreateViewModel
    {
        /// 系统Guid
        public Guid Guid { get; set; }
        /// 知识Guid
        public Guid? knowledgeGuid { get; set; }
        /// 状态
        public string usetype { get; set; }
        /// 内容
        public string userno { get; set; }
        /// 内容
        public string candown { get; set; }
        /// 创建人员
        public string creuser { get; set; }
        /// 创建日期
        public DateTime? credate { get; set; }

    }

}