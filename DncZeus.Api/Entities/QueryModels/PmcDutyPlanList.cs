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
    public class PmcDutyPlanList
    {
        /// <summary>
        /// 
        /// </summary>
       
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public DateTime? abegindate { get; set; }
        public DateTime? aenddate { get; set; }
        public Guid? projectplanGuid { get; set; } 
        public decimal totalqty { get; set; }
        public Byte status { get; set; }
        public string restDesc { get; set; }
        public Decimal restDay { get; set; }

        public Guid? dutiesGuid { get; set; }
        public string department { get; set; }
        public string name { get; set; }
        public Byte dutyStatus { get; set; }
        public Byte iskeep { get; set; }
        public Byte itype { get; set; }
        public Byte dotype { get; set; }
        public Int16 keepdate { get; set; }
        public Int16 dodate { get; set; }






    }
}
