/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.RequestPayload.Manage.ApprovalStaffBack
{
    /// <summary>
    /// 请求参数实体
    /// </summary>
    public class ApprovalStaffBackRequestPayload : RequestPayload
    {
        
        /// <summary>
        /// 用户名
        /// </summary>
        public string username { get; set; }
       
    }
}
