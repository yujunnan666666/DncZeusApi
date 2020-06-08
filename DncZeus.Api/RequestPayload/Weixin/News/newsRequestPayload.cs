/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.RequestPayload.Weixin.News
{
    /// <summary>
    /// 请求参数实体
    /// </summary>
    public class NewsRequestPayload : RequestPayload
    {
        /// <summary>
        /// 是否滚动
        /// </summary>
        public string isRoll { get; set; }
        


    }
}
