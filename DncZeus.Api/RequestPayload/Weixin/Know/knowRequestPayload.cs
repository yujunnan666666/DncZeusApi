/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.RequestPayload.Weixin.Know
{
    /// <summary>
    /// 请求参数实体
    /// </summary>
    public class KnowRequestPayload : RequestPayload
    {
       public string catalog { get; set; }
        public string userno { get; set; }




    }
}
