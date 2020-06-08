/******************************************
 * AUTHOR:          LTB
 * CREATEDON:       2019-11-15
 * OFFICIAL_SITE:   
 ******************************************/

using System;

namespace DncZeus.Api.Entities.QueryModels.SF
{
    /// <summary>
    /// 
    /// </summary>
    public class SfClient
    {
        /// <summary>
        /// 客户编号
        /// </summary>
        public string fCCode { get; set; }
        /// <summary>
        /// 客户简称
        /// </summary>
        public string fCNName { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string fCName { get; set; }
        /// <summary>
        /// 客户状态
        /// </summary>
        public string fConditionFlag { get; set; }
       

    }
}
