/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/
using System;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.ViewModels.Sec.User
{
    /// <summary>
    ///分类的视图类(创建/编辑)
    /// </summary>
    public class PasswordViewModel
    {
        
        /// <summary>
        /// 自定义用户编号
        /// </summary>  
        public Guid? userId { get; set; }
        /// <summary>
        /// 原密码
        /// </summary>  
        public string oldpwd { get; set; }
        /// <summary>
        /// 原密码
        /// </summary>  
        public string newpwd { get; set; }
        



    }
}
