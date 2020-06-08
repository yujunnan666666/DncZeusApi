/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.RequestPayload.Sec.Prg
{
    /// <summary>
    /// 请求参数实体
    /// </summary>
    public class PrgRequestPayload : RequestPayload
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
        /// <summary>
        /// 有效
        /// </summary>
        public string enabled { get; set; }
        // <summary>
        /// 开窗类型
        /// </summary>
        public string runtype { get; set; }
    }
}
