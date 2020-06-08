/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.RequestPayload.Mis.Category
{
    /// <summary>
    /// 分类目录请求参数实体
    /// </summary>
    public class CategoryRequestPayload : RequestPayload
    {
        
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        public string isEnabled { get; set; }

    }
}
