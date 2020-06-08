/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using System;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.RequestPayload.Pmc.Projects
{
    /// <summary>
    /// 请求参数实体
    /// </summary>
    public class ProjectPlanRequestPayload : RequestPayload
    {

        /// <summary>
        /// 项目id
        /// </summary>
        public Guid? projectId { get; set; }
        /// <summary>
        /// 难点物料状态
        /// </summary>
        public int diffItem { get; set; }
        /// <summary>
        /// 重点物料状态
        /// </summary>
        public int focusitem{ get; set; }
    }
}
