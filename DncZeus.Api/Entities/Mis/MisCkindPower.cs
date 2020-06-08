/******************************************
 * AUTHOR:          LTB
 * CREATEDON:       2019-10-08
 * Project:         系统档案
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
    /// 基础档案类别权限
    /// </summary>
    public class MisCkindPower
    {
        /// <summary>
        /// 系统ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 类别id
        /// </summary>
        public int MisKindID { get; set; }
        /// <summary>
        /// 组织id
        /// </summary>
        public int orgId { get; set; }
        /// <summary>
        /// 账户编号
        /// </summary>
        /// 
        [Column(TypeName = "varchar(50)")]
        public string userno { get; set; }
        /// <summary>
        /// 账户名称
        /// </summary>  
        [Column(TypeName = "nvarchar(20)")]
        public string username { get; set; }
        /// <summary>
        /// 新增
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string padd { get; set; }
        /// <summary>
        /// 删除
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string pdelete { get; set; }
        /// <summary>
        /// 修改
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string pmodify { get; set; }
        /// <summary>
        /// 有效
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string inuse { get; set; }
        /// <summary>
        /// 建立人员
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string creuser { get; set; }
        /// <summary>
        /// 建立日期
        /// </summary>
        public DateTime? credate { get; set; }
       

    }

}
