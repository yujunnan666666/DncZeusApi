/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using System;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.RequestPayload.Sec.Gprg
{
    /// <summary>
    /// 请求参数实体
    /// </summary>
    public class GprgRequestPayload : RequestPayload
    {
        
        /// <summary>
        /// 系统编码
        /// </summary>
        public string sysno { get; set; }
        /// <summary>
        /// 功能编号
        /// </summary>
        public string funno { get; set; }
        /// <summary>
        /// 程式编号
        /// </summary>
        public string prgno { get; set; }
        public Guid prgid { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string userno { get; set; }
        public string groupno { get; set; }
        public Guid groupid { get; set; }
        /// <summary>
        /// 有效
        /// </summary>
        public string enabled { get; set; }
    }
}
