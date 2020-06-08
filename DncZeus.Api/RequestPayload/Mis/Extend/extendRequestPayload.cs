/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.RequestPayload.Mis.Extend
{
    /// <summary>
    /// 类别扩展请求参数实体
    /// </summary>
    public class ExtendRequestPayload : RequestPayload
    {

        /// <summary>
        /// 二级分类Id
        /// </summary>
        public int misCKindId { get; set; }


    }
}
