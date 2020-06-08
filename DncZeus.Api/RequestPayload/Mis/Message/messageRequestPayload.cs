/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.RequestPayload.Mis.Message
{
    /// <summary>
    /// 分类目录请求参数实体
    /// </summary>
    public class MessageRequestPayload : RequestPayload
    {
        
        /// <summary>
        /// 发送人编码
        /// </summary>
        public string sendUserno { get; set; }
        /// <summary>
        /// 发送人名称
        /// </summary>
        public string sendUsername { get; set; }
        /// <summary>
        /// 接收人编号
        /// </summary>
        public string receiveUserno { get; set; }
        /// <summary>
        /// 信息类型
        /// </summary>
        public int mtype { get; set; }
        /// <summary>
        /// 接收人编号
        /// </summary>
        public int readStatus { get; set; }
        /// <summary>
        /// 接收人编号
        /// </summary>
        public int status { get; set; }
    }
}
