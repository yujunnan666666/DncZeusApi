/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using System;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.RequestPayload.Pmc.Planfollow
{
    /// <summary>
    /// 请求参数实体
    /// </summary>
    public class PlanfollowRequestPayload : RequestPayload
    {

        public int projectStatus { get; set; }
        public int dutyStatus { get; set; }
        public string department { get; set; }
        public Guid? projectGuid { get; set; }
        


    }
}
