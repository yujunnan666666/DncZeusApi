﻿/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.RequestPayload.Pmc.Deporderday
{
    /// <summary>
    /// 请求参数实体
    /// </summary>
    public class DeporderdayRequestPayload : RequestPayload
    {
        /// <summary>
        /// 是否休息
        /// </summary>
        public int isRest { get; set; }


    }
}
