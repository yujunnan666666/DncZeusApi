/******************************************
 * AUTHOR:          LTB
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    
 * DESCRIPTION:     公共类
 ******************************************/
using DncZeus.Api.Entities;
using System;


namespace DncZeus.Api.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class LineJsonModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string creuser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? credate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string moduser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? moddate { get; set; }
    }
}
