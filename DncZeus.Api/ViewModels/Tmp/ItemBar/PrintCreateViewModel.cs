/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * DESCRIPTION:     用户信息实体类
 ******************************************/
//using DncZeus.Api.Entities;
using System;


namespace DncZeus.Api.ViewModels.Tmp.ItemBar
{
    /// <summary>
    /// 
    /// </summary>
    public class PrintCreateViewModel
    {
        /// <summary>
        /// 打印次数
        /// </summary>
        public int prtnum { get; set; }
        /// <summary>
        /// 创建者ID
        /// </summary>
        public Guid? itemBoardGuid { get; set; }
        /// <summary>
        /// 创建者名称
        /// </summary>
        public string creUserName { get; set; }
        /// <summary>
        /// 建立日期
        /// </summary>
        public DateTime? credate { get; set; }
        /// <summary>
        /// 打印日期
        /// </summary>
        public DateTime? prtdate { get; set; }
    }
}
