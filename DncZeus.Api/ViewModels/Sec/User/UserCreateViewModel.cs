/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/
using System;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.ViewModels.Sec.User
{
    /// <summary>
    ///分类的视图类(创建/编辑)
    /// </summary>
    public class UserCreateViewModel
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// 自定义用户编号
        /// </summary>  
        public string code { get; set; }
        /// <summary>
        /// 帐户编号
        /// </summary>  
        public string userno { get; set; }
        /// <summary>
        /// 帐户名称
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 用户类别 0-超级管理员;1-系统管理员;2-用户
        /// </summary>
        public int utype { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public string enabled { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string pwd { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string telno { get; set; }
        /// <summary>
        /// 邮箱
        public string email { get; set; }
        /// <summary>
        /// HR工号
        /// </summary>
        public string EmpNo { get; set; }
        /// <summary>
        /// HR姓名
        /// </summary>
        public string EmpName { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public string depno { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string depname { get; set; }
        /// <summary>
        /// 岗位编号
        /// </summary>
        public string jobno { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string jobname { get; set; }
        /// <summary>
        /// 用户头像图标
        /// </summary>
        public string userLogo { get; set; }
        /// <summary>
        /// 微信ID
        /// </summary>
        public string wxId { get; set; }
        /// <summary>
        /// 微信关联账号
        /// </summary>
        public string wxAccount { get; set; }
        /// <summary>
        /// 微信关联名称
        /// </summary>
        public string wxName { get; set; }
        /// <summary>
        /// 微信密码
        /// </summary>
        public string wxPwd { get; set; }
        /// <summary>
        /// 微信电话
        /// </summary>
        public string wxPhone { get; set; }
        /// <summary>
        /// 微信邮箱
        /// </summary>
        public string wxMail { get; set; }
        /// <summary>
        /// 建立人员
        /// </summary>
        public string creuser { get; set; }
        /// <summary>
        /// 建立日期
        /// </summary>
        public DateTime? credate { get; set; }
        /// <summary>
        /// 修改人员
        /// </summary>
        public string moduser { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? moddate { get; set; }


    }
}
