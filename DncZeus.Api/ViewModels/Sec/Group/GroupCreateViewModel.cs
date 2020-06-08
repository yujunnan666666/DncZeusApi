/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/
using System;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.ViewModels.Sec.Group
{
    /// <summary>
    ///
    /// </summary>
    public class GroupCreateViewModel
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// 群组编号
        /// </summary>  
        public string groupno { get; set; }
        /// <summary>
        /// 群组类别    R-系统管理员;U-用户
        /// </summary>
        public string gtype { get; set; }
        /// <summary>
        /// 群组名称
        /// </summary>
        public string groupname { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public string enabled { get; set; }



    }
}
