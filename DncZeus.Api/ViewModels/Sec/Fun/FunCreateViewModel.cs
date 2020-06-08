/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/
using System;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.ViewModels.Sec.Fun
{
    /// <summary>
    ///分类的视图类(创建/编辑)
    /// </summary>
    public class FunCreateViewModel
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// 系统号
        /// </summary>  
        public Guid sysid { get; set; }
        /// <summary>
        /// 功能号
        /// </summary>
        public string funno { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string funname { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int funorder { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public string enabled { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string icon { get; set; }



    }
}
