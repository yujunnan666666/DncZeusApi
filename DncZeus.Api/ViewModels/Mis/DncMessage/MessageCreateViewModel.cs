/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/
using DncZeus.Api.Entities.Sec;
using System;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.ViewModels.Mis.DncMessage
{
    public class BatchMessageCreateViewModel
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// 组织ID
        /// </summary>
        public int Org { get; set; }
        /// <summary>
        /// 消息类别
        /// </summary>
        public int mtype { get; set; }
        /// <summary>
        /// 来源单据ID
        /// </summary> 
        public int SourceID { get; set; }

        /// <summary>
        /// 发送人帐号
        /// </summary> 
        public string sendUserno { get; set; }
        /// <summary>
        /// 发送人姓名
        /// </summary> 
        public string sendUsername { get; set; }

        /// <summary>
        /// 接收人帐号
        /// </summary> 
        public string receiveUserno { get; set; }
        /// <summary>
        /// 接收人姓名
        /// </summary> 
        public string receiveUsername { get; set; }
        /// <summary>
        /// 内容
        /// </summary> 
        public string message { get; set; }

        /// <summary>
        /// 收到时间
        /// </summary>
        public DateTime? receiveTime { get; set; }
        /// <summary>
        /// 阅读状态
        /// </summary>
        public int readStatus { get; set; }
        /// <summary>
        /// 阅读时间
        /// </summary>
        public DateTime? readTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; }
        public Secuser[] users { get; set; }
        /// <summary>
        /// 总裁
        /// </summary>
        public int supremo { get; set; }
        /// <summary>
        /// 生产总监
        /// </summary>
        public int workmaster { get; set; }
        /// <summary>
        /// pmc
        /// </summary>
        public int pmc { get; set; }
    }

    public class MessageCreateViewModel
    {
        /// <summary>
        /// 系统Guid
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// 组织ID
        /// </summary>
        public int Org { get; set; }
        /// <summary>
        /// 消息类别
        /// </summary>
        public int mtype { get; set; }
        /// <summary>
        /// 来源单据ID
        /// </summary> 
        public int SourceID { get; set; }

        /// <summary>
        /// 发送人帐号
        /// </summary> 
        public string sendUserno { get; set; }
        /// <summary>
        /// 发送人姓名
        /// </summary> 
        public string sendUsername { get; set; }

        /// <summary>
        /// 接收人帐号
        /// </summary> 
        public string receiveUserno { get; set; }
        /// <summary>
        /// 接收人姓名
        /// </summary> 
        public string receiveUsername { get; set; }
        /// <summary>
        /// 内容
        /// </summary> 
        public string message { get; set; }

        /// <summary>
        /// 收到时间
        /// </summary>
        public DateTime? receiveTime { get; set; }
        /// <summary>
        /// 阅读状态
        /// </summary>
        public int readStatus { get; set; }
        /// <summary>
        /// 阅读时间
        /// </summary>
        public DateTime? readTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; }



    }
}
