/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.RequestPayload.Manage.ApprovalGoods
{
    /// <summary>
    /// 请求参数实体
    /// </summary>
    public class ApprovalGoodsRequestPayload : RequestPayload
    {
        
        /// <summary>
        /// 用户名
        /// </summary>
        public string username { get; set; }
       
    }
}
