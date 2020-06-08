/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * DESCRIPTION:     用户信息实体类
 ******************************************/
//using DncZeus.Api.Entities;
using System;
using DncZeus.Api.ViewModels;
using DncZeus.Api.Entities;
using static DncZeus.Api.Entities.Tmp.tmpEnum;



namespace DncZeus.Api.ViewModels.Mis.DncCategory
{
    /// <summary>
    /// 
    /// </summary>
    public class CategoryJsonModel : MainJsonModel
    {
        /// <summary>
        /// 系统ID
        /// </summary>

        public int ID { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        
        public string Code { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        
        public string Name { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int trseq { get; set; }
        /// <summary>
        /// 建立人员
        /// </summary>

        public string creuser { get; set; }
        /// <summary>
        /// 建立日期
        /// </summary>
        public DateTime? credate { get; set; }
        /// <summary>
        /// 修改人员
        /// </summary>

        public string moduser { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? moddate { get; set; }
    }
}
