/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using DncZeus.Api.Entities;
using DncZeus.Api.Entities.Sec;
using DncZeus.Api.Extensions;
using DncZeus.Api.Extensions.AuthContext;
using DncZeus.Api.Extensions.DataAccess;
using DncZeus.Api.ViewModels.Rbac.DncMenu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using static DncZeus.Api.Entities.Enums.CommonEnum;

namespace DncZeus.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public AccountController(DncZeusDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Profile()
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                string orgid = AuthContextService.CurrentUser.OrgID.ToString(); //add by ltb 2019-11-16
                var guid = AuthContextService.CurrentUser.Guid;
                var groupid= AuthContextService.CurrentUser.GroupID;
                //var user = _dbContext.DncUser.FirstOrDefaultAsync(x => x.Guid == guid).Result;//old
                var org= _dbContext.MisOrganization.FirstOrDefaultAsync(x => x.ID == int.Parse(orgid)).Result;//new
                var user = _dbContext.Secuser.FirstOrDefaultAsync(x => x.Guid == guid).Result;//new
                var menus = _dbContext.DncMenu.Where(x => x.IsDeleted == IsDeleted.No && x.Status == Status.Normal).ToList();

                //查询当前登录用户拥有的权限集合(非超级管理员)
               /*  var sqlPermission = @"SELECT P.Code AS PermissionCode,P.ActionCode AS PermissionActionCode,P.Name AS PermissionName,P.Type AS PermissionType,M.Name AS MenuName,M.Guid AS MenuGuid,M.Alias AS MenuAlias,M.IsDefaultRouter FROM DncRolePermissionMapping AS RPM 
 LEFT JOIN DncPermission AS P ON P.Code = RPM.PermissionCode
 INNER JOIN DncMenu AS M ON M.Guid = P.MenuGuid
 WHERE P.IsDeleted=0 AND P.Status=1 AND EXISTS (SELECT 1 FROM DncUserRoleMapping AS URM WHERE URM.UserGuid={0} AND URM.RoleCode=RPM.RoleCode)";*/

                var sqlPermission = "";
               //if (user.UserType == UserType.SuperAdministrator)
                if ((UserType)user.utype == UserType.SuperAdministrator)
                {
                    //如果是超级管理员
                   /* sqlPermission = @"SELECT P.Code AS PermissionCode,P.ActionCode AS PermissionActionCode,P.Name AS PermissionName,P.Type AS PermissionType,M.Name AS MenuName,M.Guid AS MenuGuid,M.Alias AS MenuAlias,M.IsDefaultRouter FROM DncPermission AS P 
INNER JOIN DncMenu AS M ON M.Guid = P.MenuGuid
WHERE P.IsDeleted=0 AND P.Status=1";*/
                }
                //var permissions = _dbContext.DncPermissionWithMenu.FromSql(sqlPermission, user.Guid).ToList();
                //子窗口权限
                
                
                
                //用户权限
                var permissions = _dbContext.Secuprg.AsQueryable().Where(x => x.userid == user.Guid).GroupJoin(_dbContext.Secbutton, a => a.butid, b => b.Guid, (a,b) => new{
                    a.prgid, 
                    Secbutton=b
                }).SelectMany(a=>a.Secbutton, (a,b)=>new {
                    a.prgid,
                    b.butno
                }).GroupJoin(_dbContext.Secprg,a=>a.prgid,b=>b.Guid,(a,b)=>new { 
                    a.butno,
                    Secprg=b
                }).SelectMany(a => a.Secprg, (a, b) => new {
                    a.butno,
                    b.prgno
                });

                //群组权限
                var Gpermissions = _dbContext.Secgprg.AsQueryable().Where(x => x.groupid.ToString() == groupid).GroupJoin(_dbContext.Secbutton, a => a.butid, b => b.Guid, (a, b) => new {
                    a.prgid,
                    Secbutton = b
                }).SelectMany(a => a.Secbutton, (a, b) => new {
                    b.butno,
                    a.prgid
                }).GroupJoin(_dbContext.Secprg, a => a.prgid, b => b.Guid, (a, b) => new {
                    a.butno,
                    Secprg = b
                }).SelectMany(a => a.Secprg, (a, b) => new {
                    a.butno,
                    b.prgno
                });


                //var pagePermissions = permissions.GroupBy(x => x.MenuAlias).ToDictionary(g => g.Key, g => g.Select(x => x.PermissionActionCode).Distinct());

                var pagePermissions = permissions.GroupBy(x => x.prgno).ToDictionary(g => g.Key, g => g.Select(x => x.butno).Distinct());
                var GpagePermissions = Gpermissions.GroupBy(x => x.prgno).ToDictionary(g => g.Key, g => g.Select(x => x.butno).Distinct());
                response.SetData(new
                {
                    access = new string[] { },

                    /*avator = user.Avatar,
                    user_guid = user.Guid,
                    user_name = user.DisplayName,
                    user_type = user.UserType,*/

                    avator = user.userLogo,
                    user_guid = user.Guid,
                    user_no=user.userno,
                    user_name = user.username,
                    user_type = user.utype,
                    user_group= groupid,
                    user_orgid = orgid,
                    org_name= org.Name,
                    permissions = pagePermissions,
                    Gpermissions = GpagePermissions

                });
            }

            return Ok(response);
        }

        private List<string> FindParentMenuAlias(List<DncMenu> menus, Guid? parentGuid)
        {
            var pages = new List<string>();
            var parent = menus.FirstOrDefault(x => x.Guid == parentGuid);
            if (parent != null)
            {
                if (!pages.Contains(parent.Alias))
                {
                    pages.Add(parent.Alias);
                }
                else
                {
                    return pages;
                }
                if (parent.ParentGuid != Guid.Empty)
                {
                    pages.AddRange(FindParentMenuAlias(menus, parent.ParentGuid));
                }
            }

            return pages.Distinct().ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Menu()
        {
            var strSql = @"SELECT M.* FROM DncRolePermissionMapping AS RPM 
LEFT JOIN DncPermission AS P ON P.Code = RPM.PermissionCode
INNER JOIN DncMenu AS M ON M.Guid = P.MenuGuid
WHERE P.IsDeleted=0 AND P.Status=1 AND P.Type=0 AND M.IsDeleted=0 AND M.Status=1 AND EXISTS (SELECT 1 FROM DncUserRoleMapping AS URM WHERE URM.UserGuid={0} AND URM.RoleCode=RPM.RoleCode)";

            if (AuthContextService.CurrentUser.UserType == UserType.SuperAdministrator)
            {
                //如果是超级管理员
                strSql = @"SELECT * FROM DncMenu WHERE IsDeleted=0 AND Status=1";
            }
            var menus = _dbContext.DncMenu.FromSql(strSql, AuthContextService.CurrentUser.Guid).ToList();
            var rootMenus = _dbContext.DncMenu.Where(x => x.IsDeleted == IsDeleted.No && x.Status == Status.Normal && x.ParentGuid == Guid.Empty).ToList();
            
            
            foreach (var root in rootMenus)
            {
                if (menus.Exists(x => x.Guid == root.Guid))
                {
                    continue;
                }
                menus.Add(root);
            }
            menus = menus.OrderBy(x => x.Sort).ThenBy(x=>x.CreatedOn).ToList();
            var menu = MenuItemHelper.LoadMenuTree(menus, "0");
            return Ok(menu);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class MenuItemHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="selectedGuid"></param>
        /// <returns></returns>
        public static List<MenuItem> BuildTree(this List<MenuItem> menus, string selectedGuid = null)
        {
            var lookup = menus.ToLookup(x => x.ParentId);

            List<MenuItem> Build(string pid)
            {
                return lookup[pid]
                    .Select(x => new MenuItem()
                    {
                        Guid = x.Guid,
                        ParentId = x.ParentId,
                        Children = Build(x.Guid),
                        Component = x.Component ?? "Main",
                        Name = x.Name,
                        Path = x.Path,
                        Meta = new MenuMeta
                        {
                            BeforeCloseFun = x.Meta.BeforeCloseFun,
                            HideInMenu = x.Meta.HideInMenu,
                            Icon = x.Meta.Icon,
                            NotCache = x.Meta.NotCache,
                            Title = x.Meta.Title,
                            Permission = x.Meta.Permission
                        }
                    }).ToList();
            }

            var result = Build(selectedGuid);
            return result;
        }

        public static List<MenuItem> LoadMenuTree(List<DncMenu> menus, string selectedGuid = null)
        {
            var temp = menus.Select(x => new MenuItem
            {
                Guid = x.Guid.ToString(),
                ParentId = x.ParentGuid != null && ((Guid)x.ParentGuid) == Guid.Empty ? "0" : x.ParentGuid?.ToString(),
                Name = x.Alias,
                Path = $"/{x.Url}",
                Component = x.Component,
                Meta = new MenuMeta
                {
                    BeforeCloseFun = x.BeforeCloseFun ?? "",
                    HideInMenu = x.HideInMenu == YesOrNo.Yes,
                    Icon = x.Icon,
                    NotCache = x.NotCache == YesOrNo.Yes,
                    Title = x.Name
                }
            }).ToList();
            var tree = temp.BuildTree(selectedGuid);
            return tree;
        }
    }
}