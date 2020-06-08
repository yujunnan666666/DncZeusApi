/******************************************
 * AUTHOR:          LTB
 * CREATEDON:       2019-11-08
 * Project:         车间条码扫描打包项目
 ******************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static DncZeus.Api.Entities.Tmp.tmpEnum;

namespace DncZeus.Api.Entities.Tmp
{
    /// <summary>
    /// 条码行档
    /// </summary>
    /// 
    public class TmpItembarLine : EntityLine
    {
        /// <summary>
        /// 条码表头实体
        /// </summary>
        public TmpItembarHead ItembarHead { get; set; }
        /// <summary>
        /// 分包序号
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string fspcode { get; set; }
        /// <summary>
        /// 分包名称
        /// </summary>
        [Column(TypeName = "nvarchar(150)")]
        public string fspname { get; set; }
        /// <summary>
        /// 分包条码
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string fBarcode { get; set; }
        /// <summary>
        /// 板件类别
        /// </summary>
        [Column(TypeName = "tinyint")]
        public boardType boardType { get; set; }
        /// <summary>
        /// 板件序号
        /// </summary>
        [Column(TypeName = "smallint")]
        public int boardnum { get; set; }
        /// <summary>
        /// 板件编号
        /// </summary>
        [Column(TypeName = "varchar(30)")]
        public string boardcode { get; set; }
        /// <summary>
        /// 板件名称
        /// </summary>
        [Column(TypeName = "varchar(200)")]
        public string boardname { get; set; }
        /// <summary>
        /// 包装数量
        /// </summary>
        [Column(TypeName = "smallint")]
        public int fqty { get; set; }
        /// <summary>
        /// 分包条码已打印数量
        /// </summary>
        [Column(TypeName = "smallint")]
        public int hprintnum { get; set; }
        /// <summary>
        /// 板件条码已打印数量
        /// </summary>
        [Column(TypeName = "smallint")]
        public int lprintnum { get; set; }
        /// <summary>
        /// 失效状态
        /// </summary>
        [Column(TypeName = "tinyint")]
        public isValid isValid { get; set; }
        /// <summary>
        /// 失效人员
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string valuser { get; set; }
        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime? valmdate { get; set; }

    }





}
