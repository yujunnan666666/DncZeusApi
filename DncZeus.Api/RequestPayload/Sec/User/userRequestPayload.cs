/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.RequestPayload.Sec.User
{
    /// <summary>
    /// 请求参数实体
    /// </summary>
    public class UserRequestPayload : RequestPayload
    {
        
        /// <summary>
        /// 编码
        /// </summary>
        public string userno { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 有效
        /// </summary>
        public string enabled { get; set; }
    }
}
