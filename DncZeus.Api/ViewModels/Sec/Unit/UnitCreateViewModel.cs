/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/
using System;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.ViewModels.Sec.Unit
{
    /// <summary>
    ///分类的视图类(创建/编辑)
    /// </summary>
    public class UnitCreateViewModel
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// 系统号
        /// </summary>  
        public string unitno { get; set; }
        /// <summary>
        /// 系统名称
        /// </summary>
        public string unitname { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int unitorder { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public int flag { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public string enabled { get; set; }
        



    }
}
