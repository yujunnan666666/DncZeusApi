/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/
using System;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.ViewModels.Mis.DncCategory
{
    /// <summary>
    ///分类的视图类(创建/编辑)
    /// </summary>
    public class CategoryCreateViewModel
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
        /// 图标
        /// </summary>

        public string Icon { get; set; }
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
        /// 修改人员
        /// </summary>
        public string moduser { get; set; }
        public DateTime credate { get; set; }
        public DateTime moddate { get; set; }
        public string isEnabled { get; set; }


    }
}
