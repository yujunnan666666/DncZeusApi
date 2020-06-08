/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/
using DncZeus.Api.ViewModels.Sec.User;
using System;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.ViewModels.Sec.Group
{
    /// <summary>
    ///
    /// </summary>
    public class GroupUserCreateViewModel
    {
        
        /// <summary>
        /// 群组类别    R-系统管理员;U-用户
        /// </summary>
        public Guid groupid { get; set; }
        /// <summary>
        /// 群组类别    R-系统管理员;U-用户
        /// </summary>
        public UserCreateViewModel[] users { get; set; }

        public Guid userid { get; set; }


    }
}
