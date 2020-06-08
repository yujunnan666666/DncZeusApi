/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using DncZeus.Api.Entities.QueryModels.SF;
using Microsoft.EntityFrameworkCore;


namespace DncZeus.Api.Entities.SF
{
    /// <summary>
    /// 数夫ERP数据
    /// </summary>
    public class SFAuthDbContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public SFAuthDbContext(DbContextOptions<SFAuthDbContext> options) : base(options)
        {

        }

        #region DbQuery
        /// <summary>
        /// 数夫制令信息
        /// </summary>
        public DbQuery<SfMOItem> SfMOItem { get; set; }
        #endregion

    }
}
