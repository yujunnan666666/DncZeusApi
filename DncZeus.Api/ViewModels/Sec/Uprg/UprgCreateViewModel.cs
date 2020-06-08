/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/
using System;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.ViewModels.Sec.Uprg
{
    /// <summary>
    ///
    /// </summary>
    public class UprgCreateViewModelArr {
        public int orgid { get; set; }
        /// <summary>
        /// 帐户编号
        /// </summary>
        public Guid userid { get; set; }
        public UprgCreateViewModel[] arr { get; set; }
    }
    public class UprgCreateViewModel
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// 组织编号
        /// </summary>  
        public int orgid { get; set; }
        /// <summary>
        /// 帐户编号
        /// </summary>
        public Guid userid { get; set; }
        /// <summary>
        /// 程式编号
        /// </summary>
        public Guid prgid { get; set; }
        /// <summary>
        /// 按钮编号
        /// </summary>
        public Guid butid { get; set; }
        /// <summary>
        /// 建立日期
        /// </summary>
        public DateTime? credate { get; set; }
        /// <summary>
        /// 建立人员
        /// </summary>
        public string creuser { get; set; }



    }
}
