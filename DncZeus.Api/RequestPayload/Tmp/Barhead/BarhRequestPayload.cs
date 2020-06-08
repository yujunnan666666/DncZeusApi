/******************************************
 * AUTHOR:          LTB
 * CREATEDON:       2019-11-09
 * OFFICIAL_SITE:   
 ******************************************/

using System;
using DncZeus.Api.Entities.Tmp;
//using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.RequestPayload.Tmp.Barhead
{
    /// <summary>
    /// 
    /// </summary>
    public class BarhRequestPayload : RequestPayload
    {
        /// <summary>
        /// 组织ID
        /// </summary>
        public int orgId { get; set; }
        /// <summary>
        /// 单据状态
        /// </summary>
        public tmpEnum.Status status { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public tmpEnum.isValid isValid { get; set; }

        /// <summary>
        /// 高级查询
        /// </summary>
        public bool isMore { get; set; }

        /// <summary>
        /// 高级查询
        /// </summary>
        public MoreItems moreItems { get; set; }

    }

    /// <summary>
    /// 高级查询实体对象
    /// </summary>
    public class MoreItems
    {
        /// <summary>
        /// 高级查询实体对象构造函数
        /// </summary>
        public MoreItems()
        {
            tmpPackNo = "";
            fmono = "";
            fgoodscode = "";
            fgoodsname = "";
            fPackNo = "";
            fBarcode = "";
            boardcode = "";
            boardname = "";
            creuser = "";
            credate1 = "";
            credate2 = "";
            barCode = "";
            imageNo = "";
        }
        /// <summary>
        /// 
        /// </summary>
        public string tmpPackNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fmono { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fgoodscode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fgoodsname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fPackNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fBarcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string boardcode { get; set; }

        public string boardname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string creuser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string credate1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string credate2 { get; set; }
        /// <summary>
        /// 房号
        /// </summary>
        public string barCode { get; set; }
        /// <summary>
        /// 图号
        /// </summary>
        public string imageNo { get; set; }

    }
}
