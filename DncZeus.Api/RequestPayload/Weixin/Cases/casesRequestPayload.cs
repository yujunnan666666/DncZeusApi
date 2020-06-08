/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.RequestPayload.Weixin.Cases
{
    /// <summary>
    /// 请求参数实体
    /// </summary>
    public class CasesRequestPayload : RequestPayload
    {
        public string catalog { get; set; }
        public string homeShow { get; set; }
        public string title { get; set; }
        public string desc { get; set; }


    }
}
