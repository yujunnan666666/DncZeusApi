/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.RequestPayload.Finance.Sale
{
    /// <summary>
    /// 请求参数实体
    /// </summary>
    public class SaleRequestPayload : RequestPayload
    {
       public string dept { get; set; }
        public string day { get; set; }
        public string year { get; set; }



    }
}
