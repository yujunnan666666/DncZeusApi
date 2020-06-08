/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.RequestPayload.Sec.Fun
{
    /// <summary>
    /// 请求参数实体
    /// </summary>
    public class FunRequestPayload : RequestPayload
    {
        
        /// <summary>
        /// 编码
        /// </summary>
        public string funno { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string funname { get; set; }
        /// <summary>
        /// 有效
        /// </summary>
        public string enabled { get; set; }
    }
}
