/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/
using System;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.ViewModels.Sec.Prg
{
    /// <summary>
    ///
    /// </summary>
    public class PrgCreateViewModel
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// 系统号
        /// </summary>  
        public Guid sysid { get; set; }
        /// <summary>
        /// 功能号
        /// </summary>
        public Guid funid { get; set; }
        /// <summary>
        /// 程式编号
        /// </summary>
        public string prgno { get; set; }
        /// <summary>
        /// 程式名称
        /// </summary>
        public string prgname { get; set; }
        /// <summary>
        /// 程式路径
        /// </summary>
        public string program { get; set; }
        /// <summary>
        /// 程式组件
        /// </summary>
        public string prgcomp{ get; set; }
        /// <summary>
        /// 开窗方式 A-主窗口;B-弹出窗号;C-外挂
        /// </summary>
        public string runtype { get; set; }
        /// <summary>
        /// 授权方式 0-开放；1-不开放
        /// </summary>
        public string prgtype { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int prgorder { get; set; }
        /// <summary>
        /// 是否有效Y;N
        /// </summary>
        public string enabled { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 主窗口id
        /// </summary>
        public Guid? mainid { get; set; }


    }
}
