/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.RequestPayload.Mis.Power
{
    /// <summary>
    /// 类别扩展请求参数实体
    /// </summary>
    public class PowerRequestPayload : RequestPayload
    {

        /// <summary>
        /// 二级分类Id
        /// </summary>
        public int misKindId { get; set; }


    }
}
