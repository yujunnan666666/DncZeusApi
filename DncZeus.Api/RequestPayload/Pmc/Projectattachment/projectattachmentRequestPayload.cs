/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using System;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.RequestPayload.Pmc.Projectattachment
{
    /// <summary>
    /// 请求参数实体
    /// </summary>
    public class ProjectattachmentRequestPayload : RequestPayload
    {
        public Guid? projectGuid { get; set; }
        public int attType { get; set; }

    }
}
