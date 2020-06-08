/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * DESCRIPTION:     用户信息实体类
 ******************************************/
//using DncZeus.Api.Entities;
using System;
using System.Collections.Generic;

namespace DncZeus.Api.ViewModels.Man.Factory
{
    /// <summary>
    /// 
    /// </summary>
    public class FactoryCreateViewModel
    {
        /// 系统Guid
        public Guid Guid { get; set; }
        /// 组织ID
        public int OrgID { get; set; }
        /// 工厂编号
        public string code { get; set; }
        /// 工厂名称
        public string name { get; set; }
        /// 计产方式
        public int countType { get; set; }
        /// 干部人数
        public int cadre { get; set; }
        /// 后勤人数
        public int logistic { get; set; }
        /// 生产人数
        public int worker { get; set; }
        /// 目标月人均产值
        public decimal pertargetOut { get; set; }
        /// 创建人员
        public string creuser { get; set; }
        /// 创建日期
        public DateTime? credate { get; set; }
        /// 修改人员
        public string moduser { get; set; }
        /// 修改日期
        public DateTime? moddate { get; set; }


    }

}