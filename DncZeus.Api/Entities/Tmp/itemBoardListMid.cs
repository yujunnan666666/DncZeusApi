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
    /// 条码表头档
    /// </summary>
    /// 
    
    public class TmpItemBoardsMid
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }
        /// <summary>
        /// 组织实体
        /// </summary>
        public int Org { get; set; }
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
        /// 料品ID
        /// </summary>
        [Column(TypeName = "int")]
        public int fGoodsID { get; set; }
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
        /// 数夫包装编号
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string fPackNo { get; set; }
        /// <summary>
        /// 分包条码
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string fBarcode { get; set; }
        /// <summary>
        /// 分包名称
        /// </summary>
        [Column(TypeName = "nvarchar(150)")]
        public string fspname { get; set; }
        /// <summary>
        /// 分件类别
        /// </summary>
        [Column(TypeName = "tinyint")]
        public boardType boardType { get; set; }
        /// <summary>
        /// 板件序号
        /// </summary>
        [Column(TypeName = "smallint")]
        public int boardnum { get; set; }
        /// <summary>
        /// 板件条码
        /// </summary>
        [Column(TypeName = "varchar(30)")]
        public string boardcode { get; set; }
        /// <summary>
        /// 板件名称
        /// </summary>
        [Column(TypeName = "varchar(200)")]
        public string boardname { get; set; }
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
        /// <summary>
        /// 单据状态
        /// </summary>
        [Column(TypeName = "tinyint")]
        public Status status { get; set; }
        /// <summary>
        /// 建立人员
        /// </summary>
        [Column(Order = 120, TypeName = "nvarchar(20)")]
        public string creuser { get; set; }
        /// <summary>
        /// 建立日期
        /// </summary>
        [Column(Order = 121)]
        public DateTime? credate { get; set; }
        /// <summary>
        /// 修改人员
        /// </summary>
        [Column(Order = 122, TypeName = "nvarchar(20)")]
        public string moduser { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        [Column(Order = 123)]
        public DateTime? moddate { get; set; }
        /// <summary>
        /// 审核人员
        /// </summary>
        [Column(Order = 124, TypeName = "nvarchar(20)")]
        public string cfmuser { get; set; }
        /// <summary>
        /// 审核日期
        /// </summary>
        [Column(Order = 125)]
        public DateTime? cfmdate { get; set; }
        /// <summary>
        /// 产品数量
        /// </summary> 
        public int fOrdQty { get; set; }
        /// <summary>
        /// 包装编号/房号
        /// </summary>
        [Column(Order = 124, TypeName = "varchar(50)")]
        public string barCode { get; set; }
        /// <summary>
        /// 图号
        /// </summary>
        [Column(Order = 124, TypeName = "varchar(50)")]
        public string imageNo { get; set; }
        /// <summary>
        /// 部件数量
        /// </summary>
        public int boardQty { get; set; }
        /// <summary>
        /// 分包数量
        /// </summary>
        [Column(TypeName = "smallint")]
        public int packQty { get; set; }
        /// <summary>
        /// 分包序号
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string packIndex { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string batchNum { get; set; }
        /// <summary>
        /// 尺寸
        /// </summary>
        [Column(TypeName = "varchar(100)")]
        public string boardsize { get; set; }
        /// <summary>
        /// 成套分包
        /// </summary>
        [Column(TypeName = "tinyint")]
        public int isSplit { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int rowIndex { get; set; }
    }

}
