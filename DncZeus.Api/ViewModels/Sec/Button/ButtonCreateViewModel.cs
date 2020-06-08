/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/
using System;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.ViewModels.Sec.Button
{
    /// <summary>
    ///分类的视图类(创建/编辑)
    /// </summary>
    public class ButtonCreateViewModel
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// 系统号
        /// </summary>  
        public string butno { get; set; }
        /// <summary>
        /// 系统名称
        /// </summary>
        public string butname { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int butorder { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public string enabled { get; set; }
        



    }
}
