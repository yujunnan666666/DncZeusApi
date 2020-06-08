/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/
using DncZeus.Api.Entities;
using System;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.ViewModels.Mis.DncKind
{
    /// <summary>
    ///分类的视图类(创建/编辑)
    /// </summary>
    public class KindCreateViewModel
    {
        /// <summary>
        /// 系统ID
        /// </summary>
       
        public int ID { get; set; }
        /// <summary>
        /// 类别目录实体
        /// </summary>
        //public MisCatalog MisCatalog { get; set; }
        public int MisCatalogId { get; set; }
        /// <summary>
        /// 是否属于多组织
        /// </summary>
        /// 

        public string isOrgValid { get; set; }
        /// <summary>
        /// 类别编号
        /// </summary>
       
        public string ckind { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        
        public string ckdesc { get; set; }
        /// <summary>
        /// 数据长度
        /// </summary>
        public Int16 clen { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public Int16 trseq { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        
        public string isEnabled { get; set; }
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

        /// <summary>
        /// 图标
        /// </summary>

        public string Icon { get; set; }



    }
}
