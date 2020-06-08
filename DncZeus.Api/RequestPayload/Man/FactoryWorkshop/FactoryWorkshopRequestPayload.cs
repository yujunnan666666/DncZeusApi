/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using System;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.RequestPayload.Man.FactoryWorkshop
{
    /// <summary>
    /// 请求参数实体
    /// </summary>
    public class FactoryWorkshopRequestPayload : RequestPayload
    {
       public Guid factoryGuid { get; set; }
        public DateTime workDate { get; set; }
        public string status { get; set; }
        public string yy { get; set; }
        public string mm { get; set; }



    }
}
