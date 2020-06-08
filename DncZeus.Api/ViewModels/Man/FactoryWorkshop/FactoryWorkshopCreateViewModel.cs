/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * DESCRIPTION:     用户信息实体类
 ******************************************/
//using DncZeus.Api.Entities;
using System;
using System.Collections.Generic;

namespace DncZeus.Api.ViewModels.Man.FactoryWorkshop
{
    /// <summary>
    /// 
    /// </summary>
    public class FactoryWorkshopCreateViewModel
    {
        public DateTime? workdate { get; set; }
        public Guid? factoryGuid { get; set; }
        public string status { get; set; }
        public FactoryWorkout output { get; set; }
        public FactoryWorkdate factoryWorkDate { get; set; }

    }

    public class FactoryWorkout
    {
        /// 系统Guid
        public Guid? Guid { get; set; }
        /// 工厂Guid  
        public Guid? factoryGuid { get; set; }
        /// 日期
        public DateTime? wrokDate { get; set; }
        /// 目标产值
        public decimal targetOut { get; set; }
        /// 实际产值
        public decimal realityOut { get; set; }
        /// 合计工时数
        public decimal sumTime { get; set; }
        /// 状态
        public int status { get; set; }
    }

    public class FactoryWorkdate {
        /// 工厂Guid  
        public Guid? factoryGuid { get; set; }
        /// 日期
        public DateTime? workDate { get; set; }
        /// 干部人数
        public int cadre { get; set; }
        /// 后勤人数
        public int logistic { get; set; }
        /// 生产人数
        public int worker { get; set; }
        /// 干部请假人数
        public int cadreLeave { get; set; }
        /// 后勤请假人数
        public int logisticLeave { get; set; }
        /// 生产请假人数
        public int workerLeave { get; set; }
        /// 干部调休人数
        public int cadreRest { get; set; }
        /// 后勤调休人数
        public int logisticRest { get; set; }
        /// 生产调休人数
        public int workerRest { get; set; }
    }

    public class MonthPlanCreateModel{

        public string YY { get; set; }
        public Guid? factoryGuid { get; set; }
        public MonthPlan[] list { get; set; }
    }

    public class MonthPlan {
        /// 系统Guid
        public Guid Guid { get; set; }
        /// 组织ID
        public int OrgID { get; set; }
        /// 工厂Guid  
        public Guid? factoryGuid { get; set; }
        /// 年
        public string YY { get; set; }
        //月
        public string MM { get; set; }
        //计划产值
        public decimal outqty { get; set; }

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