/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * DESCRIPTION:     用户信息实体类
 ******************************************/
//using DncZeus.Api.Entities;
using DncZeus.Api.Entities.Sec;
using System;


namespace DncZeus.Api.ViewModels.Pmc.Plannote
{
    /// <summary>
    /// 
    /// </summary>
    public class ReportViewModel
    {
       
        /// <summary>
        /// 
        /// </summary>
        public Guid? projectplanGuid { get; set; }
        public Guid? projectlineGuid { get; set; }
        public Guid? projectitemGuid { get; set; }
        public string fllowDesc { get; set; }
        public int isnote { get; set; }
        public string nodes { get; set; }
        public int supremo { get; set; }
        public int workmaster { get; set; }
        public int pmc { get; set; }
        public int status { get; set; }
        public Secuser[] followers { get; set; }
        
        
    }
}
