/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using DncZeus.Api.Entities.QueryModels.DncPermission;
using Microsoft.EntityFrameworkCore;
using DncZeus.Api.Entities.Tmp;
using DncZeus.Api.Entities.QueryModels.SF;
using DncZeus.Api.Entities.QueryModels;
using DncZeus.Api.Entities.QueryModels.Tmp;
using DncZeus.Api.Entities.Sec;
using DncZeus.Api.Entities.Pmc;
using DncZeus.Api.Entities.Mis;
using DncZeus.Api.Entities.Weixin;
using DncZeus.Api.Entities.Man;

namespace DncZeus.Api.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class DncZeusDbContext : DbContext
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public DncZeusDbContext(DbContextOptions<DncZeusDbContext> options) : base(options)
        {
            this.Database.SetCommandTimeout(6000000);
        }
        /// <summary>
        /// 行政管理-審批-员工返程报备
        /// </summary>
        public DbSet<ApprovalStaffBack> ApprovalStaffBack { get; set; }
        /// <summary>
        /// 行政管理-審批-物品领用
        /// </summary>
        public DbSet<ApprovalGoods> ApprovalGoods { get; set; }
        /// <summary>
        /// 行政管理-審批-出勤登记
        /// </summary>
        public DbSet<ApprovalDuty> ApprovalDuty { get; set; }
        /// <summary>
        /// 条码打印档
        /// </summary>
        public DbSet<TmpBoardsPrint> TmpBoardsPrint { get; set; }
        /// <summary>
        /// 条码清单
        /// </summary>
        public DbSet<TmpItemBoards> TmpItemBoards { get; set; }
        /// <summary>
        /// 条码清单中间表
        /// </summary>
        public DbSet<TmpItemBoardsMid> TmpItemBoardsMid { get; set; }

        public DbSet<TmpItemBoardsH> TmpItemBoardsH { get; set; }
        /// <summary>
        /// 条码行
        /// </summary>
        public DbSet<TmpItembarLine> TmpItembarLine { get; set; }
        /// <summary>
        /// 条码表头
        /// </summary>
        public DbSet<TmpItembarHead> TmpItembarHead { get; set; }
        /// <summary>
        /// MIS类别目录
        /// </summary>
        public DbSet<MisCatalog> MisCatalog { get; set; }
        /// <summary>
        /// MIS基础档案类别
        /// </summary>
        public DbSet<MisCkind> MisCkind { get; set; }
        /// <summary>
        /// MIS基础档案类别扩展表
        /// </summary>
        public DbSet<MisCkindExtend> MisCkindExtend { get; set; }
        /// <summary>
        /// MIS基础档案类别权限表
        /// </summary>
        public DbSet<MisCkindPower> MisCkindPower { get; set; }
        /// <summary>
        /// MIS基础档案值集
        /// </summary>
        public DbSet<MisCode> MisCode { get; set; }
        /// <summary>
        /// MIS组织
        /// </summary>
        public DbSet<MisOrganization> MisOrganization { get; set; }
        /// <summary>
        /// 附件基本档
        /// </summary>
        public DbSet<MisAttachment> MisAttachment { get; set; }
        /// <summary>
        /// 款式基本档
        /// </summary>
        public DbSet<TechItemMaster> TechItemMaster { get; set; }
        /// <summary>
        /// 产品附件档
        /// </summary>
        public DbSet<TechItemmAttachment> TechItemmAttachment { get; set; }
       
        /// <summary>
        /// secuser 用户表
        /// </summary>
        public DbSet<Secuser> Secuser { get; set; }
        /// <summary>
        /// secsys 系统模组
        /// </summary>
        public DbSet<Secsys> Secsys { get; set; }
        /// <summary>
        /// secfun 功能模组
        /// </summary>
        public DbSet<Secfun> Secfun { get; set; }
        /// <summary>
        /// secprg 程式模组
        /// </summary>
        public DbSet<Secprg> Secprg { get; set; }
        /// <summary>
        /// secprgbut 程式按钮
        /// </summary>
        public DbSet<Secprgbut> Secprgbut { get; set; }
        /// <summary>
        /// secbutton 按钮基本档
        /// </summary>
        public DbSet<Secbutton> Secbutton { get; set; }
        /// <summary>
        /// secunit 单位基本档
        /// </summary>
        public DbSet<Secunit> Secunit { get; set; }
        /// <summary>
        ///  secuprg 用户权限表
        /// </summary>
        public DbSet<Secuprg> Secuprg { get; set; }
        /// <summary>
        ///  secgroup 群组表
        /// </summary>
        public DbSet<Secgroup> Secgroup { get; set; }
        /// <summary>
        ///  secguser 群组用户表
        /// </summary>
        public DbSet<Secguser> Secguser { get; set; }
        /// <summary>
        ///  secgprg 群组权限表
        /// </summary>
        public DbSet<Secgprg> Secgprg { get; set; }
        /// <summary>
        ///  secuserorg 用户组织表
        /// </summary>
        public DbSet<Secuserorg> Secuserorg { get; set; }

        /// <summary>
        ///   任务基本表
        /// </summary>
        public DbSet<PmcDuties> PmcDuties { get; set; }
        /// <summary>
        ///   次级任务表
        /// </summary>
        public DbSet<PmcDutiesecond> PmcDutiesecond { get; set; }
        /// <summary>
        ///   部门工作日程表
        /// </summary>
        public DbSet<PmcDeporderday> PmcDeporderday { get; set; }
        /// <summary>
        ///   工厂日程表
        /// </summary>
        public DbSet<PmcOrderday> PmcOrderday { get; set; }
        /// <summary>
        ///   岗位人员表
        /// </summary>
        public DbSet<PmcJobuser> PmcJobuser {get; set; }
        /// <summary>
        ///   项目立项资料表
        /// </summary>
        public DbSet<PmcProjects> PmcProjects { get; set; }
        /// <summary>
        ///   项目明细行表
        /// </summary>
        public DbSet<PmcProjectline> PmcProjectline { get; set; }
        /// <summary>
        ///   难点物料需求表
        /// </summary>
        public DbSet<PmcProjectitem> PmcProjectitem { get; set; }
        /// <summary>
        ///   项目计划表
        /// </summary>
        public DbSet<PmcProjectplan> PmcProjectplan { get; set; }
        /// <summary>
        ///   项目计划日程表
        /// </summary>
        public DbSet<PmcProjectplanday> PmcProjectplanday { get; set; }
        /// <summary>
        ///   工作计划日登记表
        /// </summary>
        public DbSet<PmcPlannote> PmcPlannote { get; set; }
        /// <summary>
        ///   计划跟进表
        /// </summary>
        public DbSet<WjgcPlanfllow> WjgcPlanfllow { get; set; }
        /// <summary>
        ///   计划跟进人
        /// </summary>
        public DbSet<WjgcPlanfllowuser> WjgcPlanfllowuser { get; set; }
        /// <summary>
        ///   消息基本档
        /// </summary>
        public DbSet<Mismessage> Mismessage { get; set; }
        /// <summary>
        ///   操作记录表
        /// </summary>
        public DbSet<PmcWorknote> PmcWorknote { get; set; }
        /// <summary>
        ///   项目附件档
        /// </summary>
        public DbSet<PmcProjectAttachment> PmcProjectAttachment { get; set; }

        /// <summary>
        ///   新闻/公告表
        /// </summary>
        public DbSet<WeiXinNews> WeiXinNews { get; set; }
        /// <summary>
        ///   经典案例表
        /// </summary>
        public DbSet<WeiXinCases> WeiXinCases { get; set; }
        /// <summary>
        ///   知识表
        /// </summary>
        public DbSet<WeiXinKnowledge> WeiXinKnowledge { get; set; }
        /// <summary>
        ///   知识权限表
        /// </summary>
        public DbSet<WeiXinKnowledgePower> WeiXinKnowledgePower { get; set; }

        /// <summary>
        ///   工厂基本表
        /// </summary>
        public DbSet<ManFactory> ManFactory { get; set; }
        /// <summary>
        ///   工厂月度计划表
        /// </summary>
        public DbSet<ManFactoryMonPlan> ManFactoryMonPlan { get; set; }
        /// <summary>
        ///   工厂人员出勤表
        /// </summary>
        public DbSet<ManFactoryWorkdate> ManFactoryWorkdate { get; set; }
        /// <summary>
        ///   工厂日产值登记表
        /// </summary>
        public DbSet<ManFactoryWorkout> ManFactoryWorkout { get; set; }
        /// <summary>
        ///   订单产值分配表
        /// </summary>
        public DbSet<ManOutSplit> ManOutSplit { get; set; }
        /// <summary>
        ///   订单产值分配子表
        /// </summary>
        public DbSet<ManOutSplitLine> ManOutSplitLine { get; set; }
        /// <summary>
        ///   工厂工序表
        /// </summary>
        public DbSet<ManWorkProcess> ManWorkProcess { get; set; }
        /// <summary>
        ///   车间生产日报表
        /// </summary>
        public DbSet<ManWorkshopOut> ManWorkshopOut { get; set; }
        /// <summary>
        ///   车间生产日报子表
        /// </summary>
        public DbSet<ManWorkshopOutLine> ManWorkshopOutLine { get; set; }
        /// <summary>
        ///   销售计划表
        /// </summary>
        public DbSet<ReportSaleplan> ReportSaleplan { get; set; }


        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<DncUser> DncUser { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public DbSet<DncRole> DncRole { get; set; }
        /// <summary>
        /// 菜单
        /// </summary>
        public DbSet<DncMenu> DncMenu { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public DbSet<DncIcon> DncIcon { get; set; }

        /// <summary>
        /// 用户-角色多对多映射
        /// </summary>
        public DbSet<DncUserRoleMapping> DncUserRoleMapping { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public DbSet<DncPermission> DncPermission { get; set; }
        /// <summary>
        /// 角色-权限多对多映射
        /// </summary>
        public DbSet<DncRolePermissionMapping> DncRolePermissionMapping { get; set; }

        #region DbQuery
        /// <summary>
        /// 
        /// </summary>
        public DbQuery<DncPermissionWithAssignProperty> DncPermissionWithAssignProperty { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbQuery<DncPermissionWithMenu> DncPermissionWithMenu { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbQuery<retValue> retValue { get; set; }
        //返回狀態

        /// <summary>
        /// 数夫制令信息
        /// </summary>
        public DbQuery<SfMOItem> SfMOItem { get; set; }
        /// <summary>
        /// 数夫订单信息
        /// </summary>
        public DbQuery<SfOrder> SfOrder { get; set; }
        /// <summary>
        /// 数夫客户信息
        /// </summary>
        public DbQuery<SfClient> SfClient { get; set; }
        /// <summary>
        /// 数夫物料信息
        /// </summary>
        public DbQuery<SfItem> SfItem { get; set; }
        /// <summary>
        /// 数夫重点物料信息
        /// </summary>
        public DbQuery<SfFocusItem> SfFocusItem { get; set; }

        /// <summary>
        /// 板件查询列表
        /// </summary>
        public DbQuery<boardList> boardList { get; set; }

        /// <summary>
        /// pmc工作计划日登记列表
        /// </summary>
        public DbQuery<PmcPlannoteList> PmcPlannoteList { get; set; }
        /// <summary>
        /// pmc工作计划日登记列表(不含明细)
        /// </summary>
        public DbQuery<PmcPlannoteList2> PmcPlannoteList2 { get; set; }
        /// <summary>
        /// pmc工作计划排程列表(不含明细)
        /// </summary>
        public DbQuery<PmcDutyPlanList> PmcDutyPlanList { get; set; }
        /// <summary>
        /// 财务管理部门销售计划达成汇总
        /// </summary>
        public DbQuery<FinanceSaleList> FinanceSaleList { get; set; }
        /// <summary>
        /// 财务管理年度销售计划达成汇总
        /// </summary>
        public DbQuery<FinanceYearSaleList> FinanceYearSaleList { get; set; }

        /// <summary>
        /// 车间生产日报表
        /// </summary>
        public DbQuery<WorkshopList> WorkshopList { get; set; }
        /// <summary>
        /// 历史车间生产日报表
        /// </summary>
        public DbQuery<HistoryWorkshopList> HistoryWorkshopList { get; set; }
        /// <summary>
        /// 工厂生产总览
        /// </summary>
        public DbQuery<FactoryWorkshopInfo> FactoryWorkshopInfo { get; set; }

        /// <summary>
        /// 数夫料品信息
        /// </summary>
        public DbQuery<SfGoods> SfGoods { get; set; }

        #endregion

        /// <summary>
        /// 自定义DbContext实体属性名与数据库表对应名称（默认 表名与属性名对应是 User与Users）
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<DncUser>()
            //    .Property(x => x.Status);
            //modelBuilder.Entity<DncUser>()
            //    .Property(x => x.IsDeleted);


            modelBuilder.Entity<DncRole>(entity =>
            {
                entity.HasIndex(x => x.Code).IsUnique();
            });

            modelBuilder.Entity<DncMenu>(entity =>
            {
                //entity.haso
            });


            modelBuilder.Entity<DncUserRoleMapping>(entity =>
            {
                entity.HasKey(x => new
                {
                    x.UserGuid,
                    x.RoleCode
                });

                entity.HasOne(x => x.DncUser)
                    .WithMany(x => x.UserRoles)
                    .HasForeignKey(x => x.UserGuid)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.DncRole)
                    .WithMany(x => x.UserRoles)
                    .HasForeignKey(x => x.RoleCode)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<DncPermission>(entity =>
            {
                entity.HasIndex(x => x.Code)
                    .IsUnique();

                entity.HasOne(x => x.Menu)
                    .WithMany(x => x.Permissions)
                    .HasForeignKey(x => x.MenuGuid);
            });

            modelBuilder.Entity<DncRolePermissionMapping>(entity =>
            {
                entity.HasKey(x => new
                {
                    x.RoleCode,
                    x.PermissionCode
                });

                entity.HasOne(x => x.DncRole)
                    .WithMany(x => x.Permissions)
                    .HasForeignKey(x => x.RoleCode)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.DncPermission)
                    .WithMany(x => x.Roles)
                    .HasForeignKey(x => x.PermissionCode)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
