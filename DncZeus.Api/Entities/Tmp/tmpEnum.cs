/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

namespace DncZeus.Api.Entities.Tmp
{
    /// <summary>
    /// 通用枚举类
    /// </summary>
    public class tmpEnum
    {
        /// <summary>
        /// 包装状态  
        /// </summary>
        public enum SpStatus
        {
            /// <summary>
            /// 未包装
            /// </summary>
            Not = 1,
            /// <summary>
            /// 部分包装
            /// </summary>
            Part = 2,
            /// <summary>
            /// 全部包装
            /// </summary>
            All = 3,
            /// <summary>
            /// 打印外包条码
            /// </summary>
            Out = 4,
            /// <summary>
            /// 非扫码打印
            /// </summary>
            Noscan = 5,
            /// <summary>
            /// 已备入仓
            /// </summary>
            In = 6
        }

        /// <summary>
        /// 单据状态  1-未审核；2-已审核；
        /// </summary>
        public enum Status
        {
            /// <summary>
            /// 全部
            /// </summary>
            All = -1,
            /// <summary>
            /// 开立
            /// </summary>
            Open = 1,
            /// <summary>
            /// 已审核
            /// </summary>
            Cfm = 2,
        }

        /// <summary>
        /// 分件类别  
        /// </summary>
        public enum boardType
        {
            /// <summary>
            /// 全部
            /// </summary>
            All = -1,
            /// <summary>
            /// 板件
            /// </summary>
            Board = 1,
            /// <summary>
            /// 五金包
            /// </summary>
            Metal = 2
        }

        /// <summary>
        /// 失效状态  
        /// </summary>
        public enum isValid
        {
            /// <summary>
            /// 全部
            /// </summary>
            All = -1,
            /// <summary>
            /// 有效
            /// </summary>
            Valid = 1,
            /// <summary>
            /// 无效
            /// </summary>
            Invalid = 0
        }
    }
}
