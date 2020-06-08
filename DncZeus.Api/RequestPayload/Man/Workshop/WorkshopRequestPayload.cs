/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using System;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.RequestPayload.Man.Workshop
{
    /// <summary>
    /// 请求参数实体
    /// </summary>
    public class WorkshopRequestPayload : RequestPayload
    {
       
        public Guid? factoryGuid { get; set; }
        public Guid? workProcessGuid { get; set; }
        public DateTime workDate { get; set; }
        public Guid? lineGuid { get; set; }
        public int countType { get; set; }
    }
}
