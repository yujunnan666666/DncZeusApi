/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/
using System;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.ViewModels.Sec.Prg
{
    /// <summary>
    ///
    /// </summary>
    public class PrgButtonCreateViewModel
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
        public Guid funid { get; set; }
        /// <summary>
        /// 程式编号
        /// </summary>
        public Guid prgid { get; set; }
        /// <summary>
        /// 按钮id
        /// </summary>
        public Guid butid { get; set; }
        /// <summary>
        /// 按钮编号
        /// </summary>
        public string butno { get; set; }
        /// <summary>
        /// 按钮名称
        /// </summary>
        public string butname { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int butorder { get; set; }
        /// <summary>
        /// 是否有效Y;N
        /// </summary>
        public string enabled { get; set; }

        public PrgButtonCreateViewModel[] btns { get; set; }

    }

    
}
