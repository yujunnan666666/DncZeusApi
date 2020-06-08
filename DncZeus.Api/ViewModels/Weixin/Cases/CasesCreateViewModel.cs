/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * DESCRIPTION:     用户信息实体类
 ******************************************/
//using DncZeus.Api.Entities;
using System;
using System.Collections.Generic;

namespace DncZeus.Api.ViewModels.Weixin.Cases
{
    /// <summary>
    /// 
    /// </summary>
    public class CasesCreateViewModel
    {
        /// 系统Guid
        public Guid Guid { get; set; }
        /// 组织ID
        public int OrgID { get; set; }
        /// 分类Guid
        public int catalogId { get; set; }
        /// 分类
        public string catalog { get; set; }
        /// 标题
        public string CaseName { get; set; }
        /// 简述
        public string CaseSketch { get; set; }
        /// 图片路径
        public string pathUrl { get; set; }
        /// 图片压缩路径
        public string thumbUrl { get; set; }

        /// 状态
        public string status { get; set; }
        /// 首页显示
        public string homeShow { get; set; }

        /// 内容
        public string HtmlContent { get; set; }
        /// 创建人员
        public string creuser { get; set; }
        /// 创建日期
        public DateTime? credate { get; set; }
        /// 修改人员
        public string moduser { get; set; }
        /// 修改日期
        public DateTime? moddate { get; set; }
        /// 审核人员
        public string cfmuser { get; set; }
        /// 审核日期
        public DateTime? cfmdate { get; set; }

    }

}