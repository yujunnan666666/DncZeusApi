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
    /// 项目立项资料表
    /// </summary>
    /// 
    public class PmcProjects
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }
        /// <summary>
        /// 组织ID
        /// </summary>
        public int OrgID { get; set; }
        /// <summary>
        /// 项目编码
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string Code { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [Column(TypeName = "varchar(300)")]
        public string Name { get; set; }
        /// <summary>
        /// 项目地区
        /// </summary>
        [Column(TypeName = "varchar(300)")]
        public string area { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [Column(TypeName = "varchar(300)")]
        public string address { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        [Column(TypeName = "varchar(50)")]
        public string custcode { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        [Column(TypeName = "varchar(200)")]
        public string custname { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Column(TypeName = "decimal(10,2)")]
        public decimal qty { get; set; }
        /// <summary>
        /// 项目总金额
        /// </summary>
        [Column(TypeName = "decimal(14,2)")]
        public decimal totalmoney { get; set; }
        /// <summary>
        /// 立项时间
        /// </summary>
        public DateTime? procreDate { get; set; }
        /// <summary>
        /// 分批出货
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int isbatch { get; set; }
        /// <summary>
        /// 客户交期
        /// </summary>
        public DateTime? custDate { get; set; }
        /// <summary>
        /// 中山交期
        /// </summary>
        public DateTime? factoryDate1 { get; set; }
        /// <summary>
        /// 越秀交期
        /// </summary>
        public DateTime? factoryDate2 { get; set; }
        /// <summary>
        /// 跟进等级
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int followgrade { get; set; }
        /// <summary>
        /// 项目状态
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int status { get; set; }
        /// <summary>
        /// 难点物料状态
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int diffStatus { get; set; }
        /// <summary>
        /// 重点物料状态
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int focusStatus { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column(TypeName = "varchar(200)")]
        public string remark { get; set; }
        /// <summary>
        /// 创建人员
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string creuser { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? credate { get; set; }
        /// <summary>
        /// 修改人员
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string moduser { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? moddate { get; set; }
        /// <summary>
        /// 审核人员
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string cfmuser { get; set; }
        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? cfmdate { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        [Column(TypeName = "varchar(200)")]
        public string contacter { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [Column(TypeName = "varchar(200)")]
        public string phone { get; set; }
        /// <summary>
        /// 数夫已下订单
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int  iscreorder { get; set; }
        /// <summary>
        /// 数夫订单状态
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int sfStatus { get; set; }
        /// <summary>
        /// 有难点物料
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int isdiff { get; set; }
        /// <summary>
        /// 有重点物料
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int isfocus { get; set; }

    }



}
