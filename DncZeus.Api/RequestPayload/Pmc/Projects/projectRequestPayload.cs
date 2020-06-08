/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.RequestPayload.Sec.Button
{
    /// <summary>
    /// 请求参数实体
    /// </summary>
    public class ProjectRequestPayload : RequestPayload
    {

        /// <summary>
        /// 项目状态
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 难点物料状态
        /// </summary>
        public int diffStatus { get; set; }
        /// <summary>
        /// 重点物料状态
        /// </summary>
        public int focusStatus { get; set; }
    }
}
