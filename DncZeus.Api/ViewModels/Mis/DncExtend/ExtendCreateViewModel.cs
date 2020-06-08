/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/
using DncZeus.Api.Entities;
using System;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.ViewModels.Mis.DncExtend
{
    /// <summary>
    ///分类的视图类(创建/编辑)
    /// </summary>
    public class ExtendCreateViewModel
    {
        /// <summary>
        /// 系统ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 类别id
        /// </summary>
        /*public MisCatalog MisCatalog { get; set; }*/
        public int MisCKindId { get; set; }
        /// <summary>
        /// 扩展序号
        /// </summary>
        /// 
        public int Num { get; set; }
        /// <summary>
        /// 栏位名称
        /// </summary>  
        public string colName { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public int colType { get; set; }
        /// <summary>
        /// 来源id
        /// </summary>
        public int sourceId { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public int isEnabled { get; set; }
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
