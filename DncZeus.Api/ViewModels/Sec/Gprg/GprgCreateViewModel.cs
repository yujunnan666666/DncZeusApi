/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/
using System;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.ViewModels.Sec.Gprg
{
    /// <summary>
    ///
    /// </summary>
    public class GprgCreateViewModelArr {
        
        /// <summary>
        /// 帐户编号
        /// </summary>
        public Guid groupid { get; set; }
        public GprgCreateViewModel[] arr { get; set; }
    }
    public class GprgCreateViewModel
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        public Guid Guid { get; set; }
        
        
        /// <summary>
        /// 账户编号
        /// </summary>
        public Guid groupid { get; set; }
        /// <summary>
        /// 程式编号
        /// </summary>
        public Guid prgid { get; set; }
        /// <summary>
        /// 按钮编号
        /// </summary>
        public Guid butid { get; set; }
        /// <summary>
        /// 建立人员
        /// </summary>
        public string creuser { get; set; }
        /// <summary>
        /// 建立日期
        /// </summary>
        public DateTime? credate { get; set; }



    }
}
