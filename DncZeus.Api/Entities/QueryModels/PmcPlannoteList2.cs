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
    public class PmcPlannoteList2
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        public string Code { get; set; }
        public string custname { get; set; }
        public DateTime? custdate { get; set; }
        public Int32 proStatus { get; set; }
        public Guid? proGuid { get; set; }


        public DateTime? pbegindate { get; set; }
        public DateTime? penddate { get; set; }
        public Guid? projectplanGuid { get; set; }
        public DateTime? abegindate { get; set; }
        public DateTime? aenddate { get; set; }
        public decimal totalqty { get; set; }
        public Int32 status { get; set; }
        
        
        
        public Guid? noteGuid { get; set; }
        public DateTime? workdate { get; set; }
        public Int32 workstatus { get; set; }
        public decimal workqty { get; set; }
        public string remark { get; set; }
        
        public string department { get; set; }
        public string dutyName { get; set; }
        public Int32 dutyStatus { get; set; }
        public Int32 iskeep { get; set; }
        public Int32 itype { get; set; }


        public string nodes { get; set; }
        
        public Int32 prev1 { get; set; }
        public Int32 prev2 { get; set; }
        public Int32 prev3 { get; set; }


    }
}
