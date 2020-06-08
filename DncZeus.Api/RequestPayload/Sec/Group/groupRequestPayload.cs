/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.RequestPayload.Sec.Group
{
    /// <summary>
    /// 请求参数实体
    /// </summary>
    public class GroupRequestPayload : RequestPayload
    {

        /// <summary>
        /// 群组编号
        /// </summary>
        public string groupno { get; set; }
        /// <summary>
        /// 群组名称
        /// </summary>
        public string groupname { get; set; }
       
        /// <summary>
        /// 有效
        /// </summary>
        public string enabled { get; set; }
    }
}
