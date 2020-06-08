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

namespace DncZeus.Api.Entities.Sec
{
    /// <summary>
    /// secuser用户表
    /// </summary>
    /// 
    public class Secuser
    {/// <summary>
     /// 系统Guid
     /// </summary>
        [Key]
        [Column(Order = 1)]
        [DefaultValue("newid()")]
        public Guid Guid { get; set; }
        /// <summary>
        /// 自定义用户编号
        /// </summary>  
        [Column(TypeName = "varchar(10)")]
        public string code { get; set; }
        /// <summary>
        /// 帐户编号
        /// </summary>  
        [Column(TypeName = "varchar(20)")]
        public string userno { get; set; }
        /// <summary>
        /// 帐户名称
        /// </summary>
        [Column(TypeName = "varchar(200)")]
        public string username { get; set; }
        /// <summary>
        /// 用户类别 0-超级管理员;1-系统管理员;2-用户
        /// </summary>
        public int utype { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string enabled { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Column(TypeName = "varchar(45)")]
        public string pwd { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        [Column(TypeName = "varchar(45)")]
        public string telno { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [Column(TypeName = "varchar(64)")]
        public string email { get; set; }
        /// <summary>
        /// HR工号
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string EmpNo { get; set; }
        /// <summary>
        /// HR姓名
        /// </summary>
        [Column(TypeName = "varchar(30)")]
        public string EmpName { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string depno { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        [Column(TypeName = "varchar(100)")]
        public string depname { get; set; }
        /// <summary>
        /// 岗位编号
        /// </summary>
        [Column(TypeName = "varchar(20)")]
        public string jobno { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        [Column(TypeName = "varchar(30)")]
        public string jobname { get; set; }
        /// <summary>
        /// 用户头像图标
        /// </summary>
        [Column(TypeName = "varchar(100)")]
        public string userLogo { get; set; }
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
        ///微信id
        /// </summary>
        [Column(TypeName = "varchar(100)")]
        public string wxId { get; set; }
        /// <summary>
        ///微信关联账号
        /// </summary>
        [Column(TypeName = "varchar(100)")]
        public string wxAccount { get; set; }
        /// <summary>
        ///微信名称
        /// </summary>
        [Column(TypeName = "varchar(100)")]
        public string wxName { get; set; }
        /// <summary>
        ///微信密码
        /// </summary>
        [Column(TypeName = "varchar(100)")]
        public string wxPwd { get; set; }
        /// <summary>
        ///微信电话
        /// </summary>
        [Column(TypeName = "varchar(100)")]
        public string wxPhone { get; set; }
        /// <summary>
        ///微信邮箱
        /// </summary>
        [Column(TypeName = "varchar(100)")]
        public string wxMail { get; set; }

    }



}
