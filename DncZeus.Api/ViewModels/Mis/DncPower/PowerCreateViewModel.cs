/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/
using DncZeus.Api.Entities;
using System;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.ViewModels.Mis.DncPower
{
    /// <summary>
    ///分类的视图类(创建/编辑)
    /// </summary>
    public class PowerCreateViewModel
    {
        /// <summary>
        /// 系统ID
        /// </summary>
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
      
        public string userno { get; set; }
        /// <summary>
        /// 账户名称
        /// </summary>  
       
        public string username { get; set; }
        /// <summary>
        /// 新增
        /// </summary>
       
        public string padd { get; set; }
        /// <summary>
        /// 删除
        /// </summary>
        
        public string pdelete { get; set; }
        /// <summary>
        /// 修改
        /// </summary>
       
        public string pmodify { get; set; }
        /// <summary>
        /// 生效
        /// </summary>

        public string inuse { get; set; }
        
        /// <summary>
        /// 建立人员
        /// </summary>

        public string creuser { get; set; }
        /// <summary>
        /// 建立日期
        /// </summary>
        public DateTime? credate { get; set; }



    }
}
