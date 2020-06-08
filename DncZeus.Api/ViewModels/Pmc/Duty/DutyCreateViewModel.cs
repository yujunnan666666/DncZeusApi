/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * DESCRIPTION:     用户信息实体类
 ******************************************/
//using DncZeus.Api.Entities;
using DncZeus.Api.Entities.Pmc;
using System;
using System.Collections.Generic;

namespace DncZeus.Api.ViewModels.Pmc.Duty
{
    /// <summary>
    /// 
    /// </summary>
    public class DutyCreateViewModel
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// 组织ID
        /// </summary>
        public int OrgID { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 责任部门
        /// </summary>
        public string department { get; set; }
        /// <summary>
        /// 是否要报进度
        /// </summary>
        public int iskeep { get; set; }
        /// <summary>
        /// 是否要报进度
        /// </summary>
        public int keepdate { get; set; }
        /// <summary>
        /// 上级任务
        /// </summary>
        public Guid? dutiesGuid { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public int itype { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public int dotype { get; set; }
        /// <summary>
        /// 项目状态
        /// </summary>
        public int projectStatus { get; set; }
        /// <summary>
        /// 执行天数
        /// </summary>
        public int dodate { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 创建人员
        /// </summary>
        public string creuser { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? credate { get; set; }
        /// <summary>
        /// 修改人员
        /// </summary>
        public string moduser { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? moddate { get; set; }
        /// <summary>
        /// 审核人员
        /// </summary>
        public string cfmuser { get; set; }
        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? cfmdate { get; set; }

        public List<PmcDuties> secondDutys { get; set; }

    }

}