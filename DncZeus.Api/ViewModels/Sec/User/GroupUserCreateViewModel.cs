/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/
using DncZeus.Api.ViewModels.Sec.Group;
using DncZeus.Api.ViewModels.Sec.User;
using System;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.ViewModels.Sec.User
{
    /// <summary>
    ///
    /// </summary>
    public class UserGroupCreateViewModel
    {
        
       
       
        public UserCreateViewModel user { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public GroupCreateViewModel[] groups { get; set; }

        


    }
}
