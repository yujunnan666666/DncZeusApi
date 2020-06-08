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

namespace DncZeus.Api.Entities.Man
{
    /// <summary>
    /// 工厂人员出勤表
    /// </summary>
    /// 
    public class ManFactoryWorkdate
    {
        /// 系统Guid
        [Key]
        [Column(Order = 1)]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }
        /// 组织ID
        public int OrgID { get; set; }
        /// 工厂Guid  
        public Guid? factoryGuid { get; set; }
        /// 日期
        public DateTime? workDate { get; set; }
        /// 干部人数
        [Column(TypeName = "smallint")]
        public int cadre { get; set; }
        /// 后勤人数
        [Column(TypeName = "smallint")]
        public int logistic { get; set; }
        /// 生产人数
        [Column(TypeName = "smallint")]
        public int worker { get; set; }
        /// 干部请假人数
        [Column(TypeName = "smallint")]
        public int cadreLeave { get; set; }
        /// 后勤请假人数
        [Column(TypeName = "smallint")]
        public int logisticLeave { get; set; }
        /// 生产请假人数
        [Column(TypeName = "smallint")]
        public int workerLeave { get; set; }
        /// 干部调休人数
        [Column(TypeName = "smallint")]
        public int cadreRest { get; set; }
        /// 后勤调休人数
        [Column(TypeName = "smallint")]
        public int logisticRest { get; set; }
        /// 生产调休人数
        [Column(TypeName = "smallint")]
        public int workerRest { get; set; }
        /// 创建人员
        [Column(TypeName = "nvarchar(20)")]
        public string creuser { get; set; }
        /// 创建日期
        public DateTime? credate { get; set; }
        /// 修改人员
        [Column(TypeName = "nvarchar(20)")]
        public string moduser { get; set; }
        /// 修改日期
        public DateTime? moddate { get; set; }
    }



}
