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
    /// 条码打印档
    /// </summary>
    /// 
    public class TmpBoardsPrint
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }
        /// <summary>
        /// 条码ID
        /// </summary>
        public Guid? itemBoardGuid { get; set; }
        /// <summary>
        /// 打印次数
        /// </summary>
        [Column(TypeName = "smallint")]
        public int prtnum { get; set; }
        /// <summary>
        /// 创建者ID
        /// </summary>
        public Guid? creUserGuid { get; set; }
        /// <summary>
        /// 创建者登录名
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string LoginName { get; set; }
        /// <summary>
        /// 创建者名称
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string creUserName { get; set; }
        /// <summary>
        /// 建立日期
        /// </summary>
        public DateTime? credate { get; set; }
        /// <summary>
        /// 打印日期
        /// </summary>
        [Column(Order = 123)]
        public DateTime? prtdate { get; set; }
        /// <summary>
        /// 打印类别
        /// </summary>
        [Column(TypeName = "tinyint")]
        public PrintType printType { get; set; }
        /// <summary>
        /// 创建者名称
        /// </summary>
        [Column(TypeName = "varchar(200)")]
        public string remark { get; set; }
    }

    public enum PrintType
    {
        /// <summary>
        /// 包装条码
        /// </summary>
        barCode = 1,
        /// <summary>
        /// 用户条码
        /// </summary>
        loginUser = 0,
    }

}
