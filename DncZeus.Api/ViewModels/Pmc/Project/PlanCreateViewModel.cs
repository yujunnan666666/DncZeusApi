/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * DESCRIPTION:     用户信息实体类
 ******************************************/
//using DncZeus.Api.Entities;
using System;


namespace DncZeus.Api.ViewModels.Pmc.Project
{
    public class PlanCreateArr
    {
        public Guid? projectsGuid { get; set; }
        public PlanCreateViewModel[] planList { get; set; }
    }
        /// <summary>
        /// 
        /// </summary>
        /// 
        public class PlanCreateViewModel
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
        /// 执行天数
        /// </summary>
        public int dodate { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public int itype { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime startDate { get; set; }
        /// <summary>
        ///结束日期
        /// </summary>
        public DateTime endDate { get; set; }
        /// <summary>
        /// 任务状态
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 休息说明
        /// </summary>
        public string restDesc { get; set; }
        /// <summary>
        /// 休息天数
        /// </summary>
        public int restCount { get; set; }

    }
}
