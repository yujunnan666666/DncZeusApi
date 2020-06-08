/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using System;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.RequestPayload.Man.OutSplit
{
    /// <summary>
    /// 请求参数实体
    /// </summary>
    public class OutsplitRequestPayload : RequestPayload
    {
       
        public Guid? outsplitGuid { get; set; }
        public Guid? factoryGuid { get; set; }
        public int status { get; set; }

    }
}
