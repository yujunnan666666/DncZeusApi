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
//using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.Entities.Tmp
{
    /// <summary>
    /// 条码表头档
    /// </summary>
    /// 
    public class TmpItembarHead : EntityMain
    {
        /// <summary>
        /// 数夫包装编号
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string fPackNo { get; set; }
        /// <summary>
        /// 生产包装编号
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string tmpPackNo { get; set; }
        /// <summary>
        /// 制令号
        /// </summary>
        [Column(TypeName = "varchar(50)")]
        public string fmono { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [Column(TypeName = "varchar(50)")]
        public string fordno { get; set; }
        /// <summary>
        /// 料品编号
        /// </summary>
        [Column(TypeName = "varchar(50)")]
        public string fgoodscode { get; set; }
        /// <summary>
        /// 料品名称
        /// </summary>
        [Column(TypeName = "nvarchar(150)")]
        public string fgoodsname { get; set; }
        /// <summary>
        /// 产品数量
        /// </summary>
        [Column(TypeName = "smallint")]
        public int fqty { get; set; }
        /// <summary>
        /// 板件总数
        /// </summary>
        [Column(TypeName = "smallint")]
        public int boardqty { get; set; }
        /// <summary>
        /// 已包数量
        /// </summary>
        [Column(TypeName = "smallint")]
        public int bagqty { get; set; }
        /// <summary>
        /// 未包数量
        /// </summary>
        [Column(TypeName = "smallint")]
        public int unbagqty { get; set; }
        /// <summary>
        /// 分包数量
        /// </summary>
        [Column(TypeName = "smallint")]
        public int spqty { get; set; }
        /// <summary>
        /// 包装状态
        /// </summary>
        [Column(TypeName = "tinyint")]
        public tmpEnum.SpStatus spStatus { get; set; }
        /// <summary>
        /// 单据状态
        /// </summary>
        [Column(TypeName = "tinyint")]
        public tmpEnum.Status status { get; set; }
    }



}
