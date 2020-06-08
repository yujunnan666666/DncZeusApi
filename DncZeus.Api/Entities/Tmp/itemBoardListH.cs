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
    
    public class TmpItemBoardsH
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
        /// 数夫包装编号
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string fPackNo{ get; set; }
        /// <summary>
        /// 制令号
        /// </summary>
        [Column(TypeName = "varchar(50)")]
        public string fmono { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string fordno{ get; set; }

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
        /// 单据状态
        /// </summary>
        [Column(TypeName = "tinyint")]
        public Status status { get; set; }
        /// <summary>
        /// 导入人员
        /// </summary>
        [Column(Order = 120, TypeName = "nvarchar(20)")]
        public string creuser { get; set; }
        /// <summary>
        /// 导入日期
        /// </summary>
        [Column(Order = 121)]
        public DateTime? credate { get; set; }
       
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
        /// 已包数量
        /// </summary> 
        public int ordQty { get; set; }
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
        /// 批次号
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string batchNum { get; set; }
        /// <summary>
        /// 包装编号
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string tmpPackNo { get; set; }
        /// <summary>
        /// 板件总数
        /// </summary>
        [Column(TypeName = "smallint")]
        public int boardqty { get; set; }
        /// <summary>
        /// 已包板数量
        /// </summary>
        [Column(TypeName = "smallint")]
        public int bagqty { get; set; }
        /// <summary>
        /// 未包板数量
        /// </summary>
        [Column(TypeName = "smallint")]
        public int unbagqty { get; set; }
        /// <summary>
        /// 分包板数量
        /// </summary>
        [Column(TypeName = "smallint")]
        public int spqty { get; set; }
        /// <summary>
        /// 单号
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string trno { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int trseq { get; set; }
        /// <summary>
        /// 包装状态
        /// </summary>
       [Column(TypeName = "tinyint")]
        public SpStatus spstatus { get; set; }



    }

}
