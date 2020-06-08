/******************************************
 * AUTHOR:          LTB
 * CREATEDON:       2019-11-08
 * Project:         
 ******************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.Entities.Pmc
{
    /// <summary>
    /// 项目附件档
    /// </summary>
    /// 
    public class PmcProjectAttachment
    {
        /// 系统Guid 
        [Key]
        [Column(Order = 1)]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }  

        /// 父档类别
        [Column(TypeName = "tinyint")]
        public int parentType { get; set; }

        /// 父档ID
        public Guid? parentGuid{ get; set; }

        ///类别
        [Column(TypeName = "tinyint")]
        public int attType{ get; set; }

        /// 文件名
        [Column(TypeName = "varchar(200)")]
        public string fileName{ get; set; }

        /// 附件Guid
        public Guid? attGuid { get; set; }

        /// 操作时间
        public DateTime? credate { get; set; }

        /// 操作人员
        [Column(TypeName = "nvarchar(20)")]
        public string creuser { get; set; }
       
    
        
        
    }



}
