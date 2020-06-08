/******************************************
 * AUTHOR:          LTB
 * CREATEDON:       2019-10-10
 * Project:         资料管理项目
 ******************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.Entities
{
    /// <summary>
    /// 款式基本档
    /// </summary>
    public class TechItemMaster
    {
        /// <summary>
        /// 用户GUID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }
        /// <summary>
        /// 组织ID
        /// </summary>
        public int OrgID { get; set; }
        /// <summary>
        /// 款式编号
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string itemcode { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string custcode { get; set; }
        /// <summary>
        /// 产品大类
        /// </summary>
        [Column(TypeName = "nvarchar(10)")]
        public string category { get; set; }
        /// <summary>
        /// 产品小类
        /// </summary>
        [Column(TypeName = "nvarchar(10)")]
        public string subcategory { get; set; }
        /// <summary>
        /// 外形特征
        /// </summary>
        [Column(TypeName = "nvarchar(10)")]
        public string contour { get; set; }
        /// <summary>
        /// 座位数
        /// </summary>
        public Int16 seatqty { get; set; }
        /// <summary>
        /// 是否有扶手
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string havehandrail { get; set; }
        /// <summary>
        /// 包布形式
        /// </summary>
        [Column(TypeName = "nvarchar(10)")]
        public string wraptype { get; set; }
        /// <summary>
        /// 是否有玻璃
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string haveclass { get; set; }
        /// <summary>
        /// 是否有大理石
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string havemarble { get; set; }
        /// <summary>
        /// 是否有金属
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string havemetal { get; set; }
        /// <summary>
        /// 是否有雕刻
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string havecarve { get; set; }
        /// <summary>
        /// 是否有彩绘
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string havedrawing { get; set; }
        /// <summary>
        /// 风格
        /// </summary>
        [Column(TypeName = "nvarchar(10)")]
        public string style { get; set; }
        /// <summary>
        /// 视图方向
        /// </summary>
        [Column(TypeName = "nvarchar(10)")]
        public string direction { get; set; }
        /// <summary>
        /// 项目类型
        /// </summary>
        [Column(TypeName = "nvarchar(10)")]
        public string protype { get; set; }
        /// <summary>
        /// 图片来源
        /// </summary>
        [Column(TypeName = "nvarchar(10)")]
        public string picsource { get; set; }
        /// <summary>
        /// 款式备注
        /// </summary>
        [Column(TypeName = "nvarchar(200)")]
        public string remark { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public IsDeleted isdel { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string status { get; set; }
        /// <summary>
        /// 建立人员
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string creuser { get; set; }
        /// <summary>
        /// 建立日期
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
        /// 删除人员
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string deluser { get; set; }
        /// <summary>
        /// 删除日期
        /// </summary>
        public DateTime? deldate { get; set; }

    }


}
