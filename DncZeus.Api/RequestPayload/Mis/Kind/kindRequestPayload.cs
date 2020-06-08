/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.RequestPayload.Mis.Kind
{
    /// <summary>
    /// 分类目录请求参数实体
    /// </summary>
    public class KindRequestPayload : RequestPayload
    {

        /// <summary>
        /// 是否多组织
        /// </summary>
        public string isOrgValid { get; set; }
        /// <summary>
        /// 类别编号
        /// </summary>
       
        public string ckind { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        
        public string ckdesc { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        
        public string isEnabled { get; set; }
        
        
    }
}
