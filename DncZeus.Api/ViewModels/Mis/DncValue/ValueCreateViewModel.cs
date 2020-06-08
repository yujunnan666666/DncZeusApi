/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/
using DncZeus.Api.Entities;
using System;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.ViewModels.Mis.DncValue
{
    /// <summary>
    ///分类的视图类(创建/编辑)
    /// </summary>
    public class ValueCreateViewModel
    {
        public int ID { get; set; }
        /// <summary>
        /// 类别目录实体
        /// </summary>
        public int misCkindId { get; set; }
        /// <summary>
        /// 组织实体
        /// </summary>
        public int misOrganizationId { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string cdesc { get; set; }
        /// <summary>
        /// 自定义编号1
        /// </summary>
        public string extendCode1 { get; set; }
        /// <summary>
        /// 自定义编号2
        /// </summary>
       
        public string extendCode2 { get; set; }
        /// <summary>
        /// 自定义编号3
        /// </summary>
       
        public string extendCode3 { get; set; }
        /// <summary>
        /// 自定义编号4
        /// </summary>
        
        public string extendCode4 { get; set; }
        /// <summary>
        /// 自定义编号5
        /// </summary>
        
        public string extendCode5 { get; set; }
        /// <summary>
        /// 自定义编号6
        /// </summary>
       
        public string extendCode6 { get; set; }
        /// <summary>
        /// 自定义编号7
        /// </summary>
       
        public string extendCode7 { get; set; }
        /// <summary>
        /// 自定义编号8
        /// </summary>
       
        public string extendCode8 { get; set; }
        /// <summary>
        /// 自定义编号9
        /// </summary>
        
        public string extendCode9 { get; set; }
        /// <summary>
        /// 自定义名称1
        /// </summary>
       
        public string extendName1 { get; set; }
        /// <summary>
        /// 自定义名称2
        /// </summary>
       
        public string extendName2 { get; set; }
        /// <summary>
        /// 自定义名称3
        /// </summary>
        
        public string extendName3 { get; set; }
        /// <summary>
        /// 自定义名称4
        /// </summary>
        
        public string extendName4 { get; set; }
        /// <summary>
        /// 自定义名称5
        /// </summary>
       
        public string extendName5 { get; set; }
        /// <summary>
        /// 自定义名称6
        /// </summary>
       
        public string extendName6 { get; set; }
        /// <summary>
        /// 自定义名称7
        /// </summary>
       
        public string extendName7 { get; set; }
        /// <summary>
        /// 自定义名称8
        /// </summary>
        
        public string extendName8 { get; set; }
        /// <summary>
        /// 自定义名称9
        /// </summary>
      
        public string extendName9 { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
     
        public string isEnabled { get; set; }
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
