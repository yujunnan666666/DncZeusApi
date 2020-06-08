/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.RequestPayload.Sec.Unit
{
    /// <summary>
    /// 请求参数实体
    /// </summary>
    public class UnitRequestPayload : RequestPayload
    {
        
        /// <summary>
        /// 编码
        /// </summary>
        public string unitno { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string unitname { get; set; }
        /// <summary>
        /// 有效
        /// </summary>
        public string enabled { get; set; }
    }
}
