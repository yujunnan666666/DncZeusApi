/******************************************
 * AUTHOR:          LTB
 * CREATEDON:       2019-11-15
 * OFFICIAL_SITE:   
 ******************************************/

using System;

namespace DncZeus.Api.Entities.QueryModels
{
    /// <summary>
    /// 
    /// </summary>
    public class FactoryWorkshopInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid? factoryGuid { get; set; }
        public decimal yearPlanOutQty { get; set; }
        public decimal monPlanOutQty { get; set; }
        public decimal monAddDoneOutQty { get; set; }
        public Int16 sumPeople { get; set; }
        public decimal yearAddPlanOutQty { get; set; }
        public decimal yearAddDoneOutQty { get; set; }
        public decimal todayDoneOutQty { get; set; }
        public decimal targetOut { get; set; }
        public decimal realityOut { get; set; }
        public decimal sumTime { get; set; }
        public Byte status { get; set; }


    }
}
