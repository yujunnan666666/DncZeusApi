using AutoMapper;
using DncZeus.Api.Entities;
using DncZeus.Api.Entities.SF;
using DncZeus.Api.Entities.Tmp;
using DncZeus.Api.Entities.Enums;
using DncZeus.Api.Extensions;
using DncZeus.Api.Extensions.AuthContext;
using DncZeus.Api.Extensions.CustomException;
using DncZeus.Api.Extensions.DataAccess;
using DncZeus.Api.Models.Response;
using DncZeus.Api.RequestPayload.Tmp.Barhead;
using DncZeus.Api.ViewModels.Tmp.ItemBar;
using DncZeus.Api.ViewModels.Rbac.DncUser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using OfficeOpenXml;
using DncZeus.Api.Entities.QueryModels;
using DncZeus.Api.Entities.QueryModels.SF;
using DncZeus.Api.Entities.QueryModels.Tmp;
using static DncZeus.Api.Entities.Tmp.tmpEnum;
using static DncZeus.Api.Entities.Tmp.PrintType;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data;




/******************************************
 * AUTHOR:          LTB
 * CREATEDON:       2019-11-09
 * OFFICIAL_SITE:   
 ******************************************/

namespace DncZeus.Api.Controllers.Api.Tmp
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [ApiVersion("3.0")] //V3
    [Route("api/v3/Tmp/[controller]/[action]")]
    public class ItemBoardController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        private IHostingEnvironment _host;
        //private Guid hid;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        /// <param name="host"></param>
        public ItemBoardController(DncZeusDbContext dbContext, IMapper mapper, IHostingEnvironment host)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _host = host;
        }

        /// <summary>
        /// 列表查询
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult List(BarhRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            try
            {
                using (_dbContext)
                {


                    var query = _dbContext.TmpItemBoards.AsQueryable();
                    string where = "";
                    if (payload.isMore == true)
                    {   
                        query = query.Where(x => x.Org == payload.orgId &&
                                           (x.tmpPackNo.Equals(payload.moreItems.tmpPackNo) || payload.moreItems.tmpPackNo == "") &&
                                           (x.fmono.Equals(payload.moreItems.fmono) || payload.moreItems.fmono == "") &&
                                           (x.fgoodscode.Equals(payload.moreItems.fgoodscode) || payload.moreItems.fgoodscode == "") &&
                                           (x.fgoodsname.Contains(payload.moreItems.fgoodsname) || payload.moreItems.fgoodsname == "") &&
                                           (x.fPackNo.Equals(payload.moreItems.fPackNo) || payload.moreItems.fPackNo == "") &&
                                           (x.fBarcode.Equals(payload.moreItems.fBarcode) || payload.moreItems.fBarcode == "") &&
                                           (x.boardcode.Equals(payload.moreItems.boardcode) || payload.moreItems.boardcode == "") &&
                                           (x.boardcode.Equals(payload.moreItems.boardname) || payload.moreItems.boardname == "") &&
                                           (x.creuser.Equals(payload.moreItems.creuser) || payload.moreItems.creuser == "") &&
                                           (payload.moreItems.credate1 == "" || x.credate >= Convert.ToDateTime(payload.moreItems.credate1)) &&
                                           (payload.moreItems.credate2 == "" || x.credate <= Convert.ToDateTime(payload.moreItems.credate2)) &&
                                           (x.barCode.Equals(payload.moreItems.barCode) || payload.moreItems.barCode == "") &&
                                           (x.imageNo.Equals(payload.moreItems.imageNo) || payload.moreItems.imageNo == "") &&
                                           (x.isValid == payload.isValid || payload.isValid == isValid.All) &&
                                           (x.status == payload.status || payload.status == Status.All)
                        );
                        where += "(isValid=1 ";
                        if (payload.moreItems.tmpPackNo.Trim() != "") {
                            where += "and tmpPackNo=''" + payload.moreItems.tmpPackNo.Trim() + "'' ";
                        }
                        if (payload.moreItems.fmono.Trim() != "")
                        {
                            where += "and fmono=''" + payload.moreItems.fmono.Trim() + "'' ";
                        }
                        if (payload.moreItems.fgoodscode.Trim() != "")
                        {
                            where += "and fgoodscode=''" + payload.moreItems.fgoodscode.Trim() + "'' ";
                        }
                        if (payload.moreItems.fgoodsname.Trim() != "")
                        {
                            where += "and fgoodsname=''" + payload.moreItems.fgoodsname.Trim() + "'' ";
                        }
                        if (payload.moreItems.fPackNo.Trim() != "")
                        {
                            where += "and fPackNo=''" + payload.moreItems.fPackNo.Trim() + "'' ";
                        }
                        if (payload.moreItems.fBarcode.Trim() != "")
                        {
                            where += "and fBarcode=''" + payload.moreItems.fBarcode.Trim() + "'' ";
                        }
                        if (payload.moreItems.boardcode.Trim() != "")
                        {
                            where += "and boardcode=''" + payload.moreItems.boardcode.Trim() + "'' ";
                        }
                        if (payload.moreItems.boardname.Trim() != "")
                        {
                            where += "and boardname=''" + payload.moreItems.boardname.Trim() + "'' ";
                        }
                        if (payload.moreItems.creuser.Trim() != "")
                        {
                            where += "and creuser=''" + payload.moreItems.creuser.Trim() + "'' ";
                        }
                        if (payload.moreItems.credate1.Trim() != "")
                        {
                            where += "and credate1=''" + payload.moreItems.credate1.Trim() + "'' ";
                        }
                        if (payload.moreItems.credate2.Trim() != "")
                        {
                            where += "and credate2=''" + payload.moreItems.credate2.Trim() + "'' ";
                        }
                        if (payload.moreItems.barCode.Trim() != "")
                        {
                            where += "and barCode=''" + payload.moreItems.barCode.Trim() + "'' ";
                        }
                        if (payload.moreItems.imageNo.Trim() != "")
                        {
                            where += "and imageNo=''" + payload.moreItems.imageNo.Trim() + "'' ";
                        }
                        where += " ) ";

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(payload.Kw))
                        {
                            query = query.Where(x => x.Org == payload.orgId &&
                            (
                            x.tmpPackNo.Contains(payload.Kw.Trim()) ||
                            x.fmono.Contains(payload.Kw.Trim()) ||
                            x.fgoodscode.Contains(payload.Kw.Trim()) ||
                            x.fgoodsname.Contains(payload.Kw.Trim()) ||
                            x.fBarcode.Contains(payload.Kw.Trim()) ||
                            x.boardcode.Contains(payload.Kw.Trim()) ||
                            x.fPackNo.Contains(payload.Kw.Trim())
                            )
                            );
                            where +="(tmpPackNo=''" + payload.Kw.Trim() +"'' or ";
                            where += " fmono=''" + payload.Kw.Trim() + "'' or ";
                            where += " fgoodscode=''" + payload.Kw.Trim() + "'' or ";
                            where += " fgoodsname=''" + payload.Kw.Trim() + "'' or ";
                            where += " fBarcode=''" + payload.Kw.Trim() + "'' or ";
                            where += " boardcode=''" + payload.Kw.Trim() + "'' or ";
                            where += " fPackNo=''" + payload.Kw.Trim()+"'') ";
                        }
                        else
                        {
                            query = query.Where(x => x.Org == payload.orgId);
                            where += " Org=" + payload.orgId;
                        }
                        if (payload.isValid > isValid.All)
                        {
                            query = query.Where(x => x.isValid == payload.isValid && x.Org == payload.orgId);
                            if (payload.isValid.ToString().Equals("Valid"))
                            {
                                where+= " and isValid=1";
                            }
                            else
                            {
                                where+= " and isValid=0";
                            }

                        }
                        else {
                            where += "";
                        }
                        if (payload.status > Status.All)
                        {
                            query = query.Where(x => x.status == payload.status && x.Org == payload.orgId);

                            if (payload.status.ToString().Equals("Open"))
                            {
                                where += " and status=1";
                            }
                            else
                            {
                                where += " and status=2";
                            }
                        }
                        else {
                            where += "";
                        }
                    }


                    /*if (payload.FirstSort != null)
                    {
                        //query = query.OrderBy(payload.FirstSort.Field, payload.FirstSort.Direct == "DESC");
                        query = query.OrderBy("credate", payload.FirstSort.Direct == "DESC");
                    }*/
                    if (payload.Sort != null)
                    {
                        foreach (RequestPayload.Sort sort in payload.Sort)
                        {
                            query = query.OrderBy(sort.Field, sort.Direct == "DESC");
                        }

                        //query = query.OrderBy("boardnum", false);
                    }

                    List<TmpItemBoards> list = new List<TmpItemBoards>();
                   /* if (payload.Kw != "") {
                        list=query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                    }
                    else {*/
                    
                       list= getPageList(_dbContext, "TmpItemBoards", payload.CurrentPage, payload.PageSize, where);
                    //var list2 = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                    /* } */
                    var totalCount = query.Count();
                    //var totalCount = getPageCount(_dbContext, "TmpItemBoards", payload.CurrentPage, payload.PageSize, where);
                   // var data = list.Select(_mapper.Map<TmpItemBoards, BoardJsonModel>);

                    /*foreach (BoardJsonModel item in data)
                    {
                        string ls_a = item.creuser;
                        item.fBarcode = "test";
                    }*/


                    response.SetData(list, totalCount);
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                //return UserJsonModel(new { isSuccess = false, message = "保存失败:" }, "text/html");

                response.SetFailed("保存失败:" + e.Message);
                return Ok(response);
            }
        }
        /// <summary>
        /// 列表查询
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult ListGroupByPackno(BarhRequestPayload payload)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            try
            {
                using (_dbContext)
                {
                    var query = _dbContext.TmpItemBoardsH.AsQueryable();
                    /*var sql = @"select s.*  
                                from (
                                    select *, row_number() over (partition by [tmpPackNo] order by [tmpPackNo]) as group_idx  
                                    from TmpItemBoardsMid
                                ) s
                                where s.group_idx = 1";
                    var query = _dbContext.TmpItemBoardsMid.FromSql(sql);*/

                    if (payload.isMore == true)
                    {
                        query = query.Where(x => x.Org == payload.orgId &&
                                           /*(x.tmpPackNo.Equals(payload.moreItems.tmpPackNo) || payload.moreItems.tmpPackNo == "") &&*/
                                           (x.fmono.Equals(payload.moreItems.fmono) || payload.moreItems.fmono == "") &&
                                           (x.fgoodscode.Equals(payload.moreItems.fgoodscode) || payload.moreItems.fgoodscode == "") &&
                                           (x.fgoodsname.Contains(payload.moreItems.fgoodsname) || payload.moreItems.fgoodsname == "") &&
                                           /*(x.fPackNo.Equals(payload.moreItems.fPackNo) || payload.moreItems.fPackNo == "") &&
                                           (x.fBarcode.Equals(payload.moreItems.fBarcode) || payload.moreItems.fBarcode == "") &&
                                           (x.boardcode.Equals(payload.moreItems.boardcode) || payload.moreItems.boardcode == "") &&*/
                                           (x.creuser.Equals(payload.moreItems.creuser) || payload.moreItems.creuser == "") &&
                                           (payload.moreItems.credate1 == "" || x.credate >= Convert.ToDateTime(payload.moreItems.credate1)) &&
                                           (payload.moreItems.credate2 == "" || x.credate <= Convert.ToDateTime(payload.moreItems.credate2)) &&
                                           (x.barCode.Equals(payload.moreItems.barCode) || payload.moreItems.barCode == "") &&
                                           (x.imageNo.Equals(payload.moreItems.imageNo) || payload.moreItems.imageNo == "") &&
                                           //(x.isValid == payload.isValid || payload.isValid == isValid.All) &&
                                           (x.status == payload.status || payload.status == Status.All)
                        );
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(payload.Kw))
                        {
                            query = query.Where(x => x.Org == payload.orgId &&
                            (
                            x.tmpPackNo.Contains(payload.Kw.Trim()) ||
                            x.fmono.Contains(payload.Kw.Trim()) ||
                            x.fgoodscode.Contains(payload.Kw.Trim()) ||
                            x.fgoodsname.Contains(payload.Kw.Trim()) 
                           /* x.fBarcode.Contains(payload.Kw.Trim()) ||
                            x.boardcode.Contains(payload.Kw.Trim()) ||
                            x.fPackNo.Contains(payload.Kw.Trim())*/
                            )
                            );
                        }
                        else
                        {
                            query = query.Where(x => x.Org == payload.orgId);
                        }
                        if (payload.isValid > isValid.All)
                        {
                            //query = query.Where(x => x.isValid == payload.isValid && x.Org == payload.orgId);
                        }
                        if (payload.status > Status.All)
                        {
                            query = query.Where(x => x.status == payload.status && x.Org == payload.orgId);
                        }
                    }


                    /*if (payload.FirstSort != null)
                    {
                        //query = query.OrderBy(payload.FirstSort.Field, payload.FirstSort.Direct == "DESC");
                        //query = query.OrderBy("credate", payload.FirstSort.Direct == "DESC");
                        query = query.OrderBy("trno", true);
                        query = query.OrderBy("trseq", true);
                    }*/
                    query = query.OrderBy("trseq", false);
                    query = query.OrderBy("trno", false);
                    query = query.OrderBy("batchNum",true);

                    var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                    
                    var totalCount = query.Count();
                    //var data = list.Select(_mapper.Map<TmpItemBoards, BoardJsonModel>);

                  /*  foreach (BoardJsonModel item in data)
                    {
                        string ls_a = item.creuser;
                        item.fBarcode = "test";
                    }
*/

                    response.SetData(list, totalCount);
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                //return UserJsonModel(new { isSuccess = false, message = "保存失败:" }, "text/html");

                response.SetFailed("保存失败:" + e.Message);
                return Ok(response);
            }
        }
        

        /// <summary>
        /// 上传导入数据
        /// </summary>
        /// <returns>object</returns>
        [HttpPost]
        public IActionResult ImportExcel(IFormCollection form)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;

                //int li_org = AuthContextService.CurrentUser.OrgID;
                try
                {
                    //var files = Request.Form.Files.Where(x => x.Name.Equals("uploadExcel"));
                    var files = Request.Form.Files.Where(x => x.Name.Contains("uploadExcel"));

                    //非空限制
                    if (files == null || files.Count() <= 0)
                    {
                        response.SetFailed("请选择要上传的Excel文件");
                        return Ok(response);
                    }
                    
                    //格式限制
                    var allowType = new string[] { "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "application/vnd.ms-excel"}; //, "application/vnd.ms-excel" 
                    if (files.Any(b => !allowType.Contains(b.ContentType)))
                    {
                        response.SetFailed("只能上传Excel 2007 格式文件");
                        return Ok(response);
                    }

                    //大小限制
                    if (files.Sum(b => b.Length) >= 1024 * 1024 * 10)
                    {
                        response.SetFailed("上传文件的总大小只能在10M以下");
                        return Ok(response);
                    }

                    //写入服务器磁盘
                    foreach (var file in files)
                    {
                        string creUser = "";
                        string orgId = "";
                        string[] name = file.Name.Split(",");
                        if(name.Count() >= 3)
                        {
                            creUser = name[1];
                            orgId = name[2];
                        }

                        var fileName = file.FileName;
                        var path = Path.Combine(_host.ContentRootPath + "\\Upload");
                        var allPath = Path.Combine(path, fileName);
                        using (var stream = System.IO.File.Create(allPath))
                        {
                            file.CopyTo(stream);
                            stream.Close();
                            stream.Dispose();

                            string ls_ret=UpdateByMid(allPath, creUser, orgId); //写入上传批次中间表
                           
                           
                            if (ls_ret != "OK")
                            {
                                response.SetFailed(ls_ret);
                                return Ok(response);
                            }

                        }
                   
                    }
                    response.SetSuccess();
                    return Ok(response);

                }
                catch (Exception e)
                {
                    //return UserJsonModel(new { isSuccess = false, message = "保存失败:" }, "text/html");

                    response.SetFailed("保存失败:" + e.Message);
                    return BadRequest(response);
                }
            }
        }


        /// <summary>
        /// 批量操作
        /// </summary>
        /// <param name="command"></param>
        /// <param name="ids">用户ID,多个以逗号分隔</param>
        /// <param name="printType">打印时传入参数：打印类型</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Batch(string command, string colType, string ids, int printType)
        {
            var response = ResponseModelFactory.CreateInstance;
            switch (command)
            {
                case "delete": //删除
                    response = Delete(ids, colType);
                    break;
                case "print": //打印
                    response = Print(ids, colType,printType);
                    break;

                case "Cfm": //审核
                    response = UpdateStatus(Status.Cfm, ids, colType);
                    break;
                case "Open": //弃审
                    response = UpdateStatus(Status.Open, ids, colType);
                    break;
                case "Invalid": //失效
                    response = UpdateIsValid(isValid.Invalid, ids, colType);
                    break;
                case "Valid": //有效
                    response = UpdateIsValid(isValid.Valid, ids, colType);
                    break;
                default:
                    break;
            }
            return Ok(response);
        }
        #region 私有方法
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="ids">条码ID字符串,多个以逗号隔开</param>
        /// <param name="printType">打印类别</param>
        /// <param name="colType">类型</param>
        /// <returns></returns>
        private ResponseModel Print(string ids, string colType,int printType)
        {
            var idList = ids.Split(",").ToList();
            ResponseModel response = ResponseModelFactory.CreateInstance;
            try
            {
                using (_dbContext)
                {
                    //根据ids
                    if (colType == "ids" || colType == null)
                    {
                        
                        foreach (string guid in idList)
                        {
                            TmpBoardsPrint entity = new TmpBoardsPrint();
                            PrintType pt = (PrintType)printType;

                            entity.Guid = Guid.NewGuid();
                            entity.itemBoardGuid = Guid.Parse(guid);
                            //entity.itemBoardGuid = Guid.Parse("guid");
                            //gv = new Guid(str);
                            entity.prtnum = 0;
                            entity.creUserGuid = AuthContextService.CurrentUser.Guid;
                            entity.LoginName = AuthContextService.CurrentUser.LoginName;
                            entity.creUserName = AuthContextService.CurrentUser.DisplayName;
                            entity.credate = DateTime.Now;
                            entity.printType = pt;
                            //entity.prtdate = DateTime.Now;
                            _dbContext.TmpBoardsPrint.Add(entity);
                        }
                       
                    }
                    //根据分组
                    else if (colType == "group") {
                        var query = _dbContext.TmpItemBoards.AsQueryable();

                        foreach (string tmpPackNo in idList)
                        {
                            var queryList = query.Where(x => x.tmpPackNo == tmpPackNo).Where(x => x.boardType != boardType.Metal).ToList();
                            foreach (TmpItemBoards obj in queryList)
                            {
                               
                                TmpBoardsPrint entity = new TmpBoardsPrint();
                                PrintType pt = (PrintType)printType;

                                entity.Guid = Guid.NewGuid();
                                entity.itemBoardGuid = obj.Guid;
                                entity.prtnum = 0;
                                entity.creUserGuid = AuthContextService.CurrentUser.Guid;
                                entity.LoginName = AuthContextService.CurrentUser.LoginName;
                                entity.creUserName = AuthContextService.CurrentUser.DisplayName;
                                entity.credate = DateTime.Now;
                                entity.printType = pt;
                                
                                _dbContext.TmpBoardsPrint.Add(entity);
                            } 
                        }
                    }
                    int li_ret = _dbContext.SaveChanges();
                    response.SetData(new
                    {
                        affectCount = li_ret
                    });

                    return response;

                }
            }
            catch (Exception e)
            {
                response.SetData(new
                {
                    affectCount = 0,
                    errorMsg = "保存失败:" + e.Message
                });
                return response;
            }
        }

        /// <summary>
        /// 删除(开立状态，未打印才能删除)
        /// </summary>
        /// <param name="ids">条码ID字符串,多个以逗号隔开</param>
        /// <param name="colType">类型</param>
        /// <returns></returns>
        private ResponseModel Delete(string ids,string colType)
        {
            using (_dbContext)
            {
          
                var sql="";
                var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));

                //根据id
                if (colType == "ids"|| colType==null)
                { 
                    sql = string.Format("DELETE FROM TmpItemBoards WHERE status = 1 and lprintnum = 0 and Guid IN ({0})", parameterNames);
                }
                //根据分组
                else if(colType == "group")
                {
                    sql = "DELETE FROM TmpItemBoards WHERE status = 1 and  tmpPackNo IN ({0}) ";
                    sql += "DELETE FROM TmpItemBoardsH WHERE status = 1  and tmpPackNo IN ({0}) ";
                    sql = string.Format(sql, parameterNames);
 
                }

                int li_ret = _dbContext.Database.ExecuteSqlCommand(sql, parameters);

                var response = ResponseModelFactory.CreateInstance;
                response.SetData(new
                {
                    affectCount = li_ret
                });
                return response;
            }
        }

        /// <summary>
        /// 审核/弃审
        /// </summary>
        /// <param name="status"></param>
        /// <param name="ids">条码ID字符串,多个以逗号隔开</param>
        /// <param name="colType">类型</param>
        /// <returns></returns>
        private ResponseModel UpdateStatus(Status status, string ids,string colType)
        {
            using (_dbContext)
            {
                var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                var sql = "";
                //根据id
                if (colType == "ids" || colType==null)
                {
                    if (status == Status.Cfm)
                    {
                        sql = "UPDATE TmpItemBoards SET status=2, cfmuser='{1}', cfmdate=getdate() WHERE status = 1 and Guid IN ({0})";
                        sql = string.Format(sql, parameterNames, AuthContextService.CurrentUser.DisplayName);
                    }
                    else
                    {
                        sql = "UPDATE TmpItemBoards SET status=1, cfmuser='', cfmdate=null WHERE status = 2 and Guid IN ({0})";
                        sql = string.Format(sql, parameterNames);
                    }
                }
                //根据分组
                else if (colType == "group") 
                {
                    if (status == Status.Cfm)
                    {
                        sql = "UPDATE TmpItemBoards SET status=2, cfmuser='{1}', cfmdate=getdate() WHERE status = 1 and tmpPackNo IN ({0}) ";
                        sql+= "UPDATE TmpItemBoardsH SET status=2, cfmuser='{1}', cfmdate=getdate() WHERE status = 1 and tmpPackNo IN ({0}) ";
                        sql = string.Format(sql, parameterNames, AuthContextService.CurrentUser.DisplayName);
                    }
                    else
                    {
                        sql = "UPDATE TmpItemBoards SET status=1, cfmuser='', cfmdate=null WHERE status = 2 and tmpPackNo IN ({0}) ";
                        sql += "UPDATE TmpItemBoardsH SET status=1, cfmuser='', cfmdate=null WHERE status = 2 and tmpPackNo IN ({0}) ";
                        sql = string.Format(sql, parameterNames);
                    }
                }
                    
                int li_ret = _dbContext.Database.ExecuteSqlCommand(sql, parameters);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(new
                {
                    affectCount = li_ret
                });
                return response;
            }
        }

        /// <summary>
        /// 失效条码
        /// </summary>
        /// <param name="isValid"></param>
        /// <param name="ids">条码ID字符串,多个以逗号隔开</param>
        /// <param name="colType">类型</param>
        /// <returns></returns>
        private ResponseModel UpdateIsValid(isValid isValid, string ids,string colType)
        {
            using (_dbContext)
            {
                var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                var sql = "";

                //根据id
                if (colType == "ids"|| colType == null)
                {
                    sql = string.Format("UPDATE TmpItemBoards SET isValid=@isValid,valuser=@valuser,valmdate=getdate() WHERE Guid IN ({0})", parameterNames);
                }
                //根据分组
                else if (colType == "group")
                {
                    sql = "UPDATE TmpItemBoards SET isValid=@isValid,valuser=@valuser,valmdate=getdate() WHERE tmpPackNo IN ({0}) ";
                    sql += "UPDATE TmpItemBoardsMid SET isValid=@isValid,valuser=@valuser,valmdate=getdate() WHERE tmpPackNo IN ({0}) ";
                    sql = string.Format(sql, parameterNames);
                }

                parameters.Add(new SqlParameter("@isValid", (int)isValid));
                parameters.Add(new SqlParameter("@valuser", AuthContextService.CurrentUser.DisplayName));

                int li_ret = _dbContext.Database.ExecuteSqlCommand(sql, parameters);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(new
                {
                    affectCount = li_ret
                });
                return response;
            }
        }

        /// <summary>
        /// 上传数据到数据库
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        //private async System.Threading.Tasks.Task<string> UpdateAsync(IFormFile file)
        //private string Update(string path, string creUser, string orgId)
        //{
        //    try
        //    {
        //        using (_dbContext)
        //        {
        //            //FileInfo newFile = new FileInfo(@"D:\tmp3562.xlsx");
        //            FileInfo newFile = new FileInfo(path);
        //            using (var ep = new ExcelPackage(newFile))
        //            {
                        
        //                var ws = ep.Workbook.Worksheets["Sheet1"]; //第1张Sheet
        //                int li_colCount = 14; //总列数
        //                int li_beginRow = 10;  //开始行
        //                int li_curRow = li_beginRow; //当前行
        //                bool lb_isRowEnd = false; //行结束标志
        //                string ls_code = "-1";
        //                string ls_prmono = "";
        //                string ls_prgoodscode = "";
        //                string ls_prbarcode = "";
        //                string ls_curmono = "";
        //                string ls_curgoodscode = "";
        //                string ls_curbarcode = "";
                        
        //                string ls_fOrdNo = "";
        //                int li_fGoodsID = 0;
        //                string ls_fGoodsName = "";
                        
        //                //string ls_value;
        //                int li_boardnum = 0;
        //                int li_wnum = 0;
        //                string ls_lcode = "";
        //                string ls_tmpPackNo = "";
        //                //string ls_curtmpPackNo = "";                        
        //                string ls_fOrdQty;
        //                string ls_prfOrdQty="1";
        //                string ls_boardQty;
        //                string ls_barCode;
        //                string ls_imageNo;


        //                if (ws == null)
        //                {
        //                    return "没找到Excel的‘Sheet1’";
        //                }
        //                li_wnum = getMaxnum(_dbContext, orgId, ref ls_lcode);

        //                while (!lb_isRowEnd)
        //                {
        //                    li_curRow++;

        //                    ls_curmono = getColValue(ref ls_code, ws, li_beginRow, li_curRow, li_colCount, "fmono");
        //                    if (ls_code == "-1")
        //                    {
        //                        return "没有制令号栏位!";
        //                    }
        //                    ls_curgoodscode = getColValue(ref ls_code, ws, li_beginRow, li_curRow, li_colCount, "fgoodscode");

        //                    if (ls_code == "-1")
        //                    {
        //                        return "没有品号栏位!";
        //                    }

        //                    if (ls_curmono == "" && ls_curgoodscode == "")
        //                    {
        //                        lb_isRowEnd = true; //结束
        //                        break;
        //                    }
        //                    else if (ls_curmono == "")
        //                    {
        //                        return "第" + li_curRow.ToString() + "行的制令不可为空!";
        //                    }
        //                    else if (ls_curgoodscode == "")
        //                    {
        //                        return "第" + li_curRow.ToString() + "行的品号不可为空!";
        //                    }

        //                    ls_fOrdQty = getColValue(ref ls_code, ws, li_beginRow, li_curRow, li_colCount, "fOrdQty").Trim();
        //                    ls_curbarcode=ls_barCode = getColValue(ref ls_code, ws, li_beginRow, li_curRow, li_colCount, "barCode").Trim();
        //                    ls_imageNo = getColValue(ref ls_code, ws, li_beginRow, li_curRow, li_colCount, "imageNo").Trim();
        //                    ls_boardQty = getColValue(ref ls_code, ws, li_beginRow, li_curRow, li_colCount, "boardQty").Trim();
        //                    //根据fOrdQty * boardQty数量循环输出数据
        //                    if (ls_fOrdQty == "-1")
        //                    {
        //                        if (ls_curmono == ls_prmono && ls_curgoodscode == ls_prgoodscode&& ls_prfOrdQty=="-1")
        //                        {
        //                            ls_fOrdQty = ls_prfOrdQty;
        //                        }
        //                        else {
        //                            SfMOItem sfmoItem = getSfMoQty(_dbContext, ls_curmono, ls_curgoodscode);
        //                            ls_fOrdQty = Convert.ToString((sfmoItem.fMoQty).ToString());
        //                            //ls_fOrdQty = sfmoItem.fMoQty;
        //                        }
        //                    }
        //                    ls_prfOrdQty = ls_fOrdQty;
        //                    if (ls_fOrdQty == "" && ls_boardQty == "")
        //                    {
        //                        lb_isRowEnd = true; //结束
        //                        break;
        //                    }
        //                    int int_ls_fOrdQty = Convert.ToInt32(Convert.ToDouble(ls_fOrdQty));
        //                    int int_ls_boardQty = int.Parse(ls_boardQty);
        //                    int dataLength = int_ls_fOrdQty * int_ls_boardQty;
        //                    for (int i = 1; i <= dataLength; i++)
        //                    {

        //                    li_wnum++;

        //                    //ls_curtmpPackNo = getColValue(ref ls_code, ws, li_beginRow, li_curRow, li_colCount, "tmpPackNo");
        //                    //if (ls_code == "-1")
        //                    //{
        //                    //    return "没有生产包装编号栏位!";
        //                    //}
                            
        //                    //if (ls_curtmpPackNo == "")
        //                    //{
        //                    //    return "第" + li_curRow.ToString() + "行的生产包装编号不可为空!";
        //                    //}

        //                    string ls_boardType = getColValue(ref ls_code, ws, li_beginRow, li_curRow, li_colCount, "boardType").Trim();
        //                    if (ls_boardType != "1" && ls_boardType != "2")
        //                    {
        //                        return "第" + li_curRow.ToString() + "行的分件类别不正确!";
        //                    }
        //                    string ls_boardcode = getColValue(ref ls_code, ws, li_beginRow, li_curRow, li_colCount, "boardcode").Trim();
        //                    if (ls_boardcode != "-1" && ls_boardType == "")
        //                    {
        //                        return "第" + li_curRow.ToString() + "行的分件条码不正确!";
        //                    }
        //                    if (ls_boardcode != "-1")
        //                    {
        //                        int li_ret = checkboardcode(_dbContext, ls_boardcode);
        //                        if (li_ret == 0)
        //                        {
        //                            return "第" + li_curRow.ToString() + "行的分件条码重复!";
        //                        }
        //                    }
        //                    else
        //                    {
        //                        ls_boardcode = "00000" + li_wnum.ToString();
        //                        ls_boardcode = ls_lcode + ls_boardcode.Substring(ls_boardcode.Length - 5);
        //                    }
        //                        //mono不同，goodscode不同,barcode不同,结果：正常分包
        //                        Boolean judge1 = ls_prmono != ls_curmono && ls_prgoodscode != ls_curgoodscode && ls_prbarcode != ls_curbarcode;
        //                        //mono同，goodscode同,barcode同,结果：正常同包
        //                        Boolean judge2 = ls_prmono == ls_curmono && ls_prgoodscode == ls_curgoodscode && ls_prbarcode == ls_curbarcode;

        //                        //mono同，goodscode同,barcode不同，正常分包
        //                        Boolean judge3 = ls_prmono == ls_curmono && ls_prgoodscode == ls_curgoodscode && ls_prbarcode != ls_curbarcode; 

        //                        //mono不同，goodscode同,barcode同
        //                        Boolean judge4 = ls_prmono != ls_curmono && ls_prgoodscode == ls_curgoodscode && ls_prbarcode == ls_curbarcode;
        //                        //mono同，goodscode不同,barcode同
        //                        Boolean judge5 = ls_prmono == ls_curmono && ls_prgoodscode != ls_curgoodscode && ls_prbarcode == ls_curbarcode;
        //                        Boolean judge45_1 = ls_curbarcode == "-1"; //judge4、5前提下，barcode = -1，正常分包
        //                        Boolean judge45_2 = ls_curbarcode != "-1"; //judge4、5前提下，barcode != -1,提示分包

        //                        //mono同，goodscode不同,barcode不同,结果：正常分包
        //                        Boolean judge6 = ls_prmono == ls_curmono && ls_prgoodscode != ls_curgoodscode && ls_prbarcode != ls_curbarcode;
        //                        //mono不同，goodscode同,barcode不同,结果：正常分包
        //                        Boolean judge7 = ls_prmono != ls_curmono && ls_prgoodscode == ls_curgoodscode && ls_prbarcode != ls_curbarcode;

        //                        if (judge1 || judge3 || judge6 || judge7)//正常分包
        //                        {
        //                            li_boardnum = 0;
        //                            SfMOItem moItem = getSfMoItem(_dbContext, ls_curmono, ls_curgoodscode);
        //                            if (moItem == null)
        //                            {
        //                                return "第" + li_curRow.ToString() + "行的制令：" + ls_curmono + " 品号:" + ls_curgoodscode + "在数夫制令里不存在或不正确!";
        //                            }
        //                            ls_tmpPackNo = getPackNo(_dbContext, orgId);
        //                            ls_fOrdNo = moItem.fOrdNo;
        //                            ls_fGoodsName = moItem.fGoodsName;
        //                            li_fGoodsID = moItem.fGoodsID;
        //                        }
        //                        else if (judge4 || judge5)
        //                        {
        //                            if (judge45_1)
        //                            {
        //                                li_boardnum = 0;
        //                                SfMOItem moItem = getSfMoItem(_dbContext, ls_curmono, ls_curgoodscode);
        //                                if (moItem == null)
        //                                {
        //                                    return "第" + li_curRow.ToString() + "行的制令：" + ls_curmono + " 品号:" + ls_curgoodscode + "在数夫制令里不存在或不正确!";
        //                                }
        //                                ls_tmpPackNo = getPackNo(_dbContext, orgId);
        //                                ls_fOrdNo = moItem.fOrdNo;
        //                                ls_fGoodsName = moItem.fGoodsName;
        //                                li_fGoodsID = moItem.fGoodsID;
        //                            }
        //                            else {
        //                                return "第" + li_curRow.ToString() + "行的制令：barCode值域不合法，应分包处理 !";
        //                            }  
        //                        }   

        //                    string ls_boardname = getColValue(ref ls_code, ws, li_beginRow, li_curRow, li_colCount, "boardname").Trim();
        //                    if (ls_boardname == "")
        //                    {
        //                        return "第" + li_curRow.ToString() + "行的分件名称不正确!";
        //                    }

        //                    List<TmpItemBoards> TmpItemBoardsList = new List<TmpItemBoards>();

        //                        li_boardnum++;
        //                    var entity = new TmpItemBoards();
        //                    entity.Guid = Guid.NewGuid();
        //                    entity.Org = Convert.ToInt32(orgId);
        //                    entity.tmpPackNo = ls_tmpPackNo;
        //                    entity.fmono = ls_curmono;
        //                    entity.fordno = ls_fOrdNo;
        //                    entity.fGoodsID = li_fGoodsID;
        //                    entity.fgoodscode = ls_curgoodscode;
        //                    entity.fgoodsname = ls_fGoodsName;
        //                    entity.boardType = (boardType)Convert.ToInt32(ls_boardType);
        //                    entity.boardcode = ls_boardcode;
        //                    entity.boardname = ls_boardname;
        //                    entity.creuser = creUser;
        //                    entity.credate = DateTime.Now;
        //                    entity.status = Status.Open;
        //                    entity.isValid = isValid.Valid;
        //                    entity.boardnum = li_boardnum;
        //                    entity.fOrdQty =1;
        //                    //entity.boardQty = ls_boardQty;
        //                    entity.barCode = ls_barCode;
        //                    entity.imageNo = ls_imageNo;

                             
        //                    //_dbContext.Configuration.AutoDetectChangesEnabled = false
        //                    //_dbContext.TmpItemBoards.Add(entity);
                           
        //                    ls_prmono = ls_curmono;
        //                    ls_prgoodscode = ls_curgoodscode;
        //                    ls_prbarcode = ls_curbarcode;

        //                    TmpItemBoardsList.Add(entity);
                                
        //                        //_dbContext.TmpItemBoards.BulkInsert(TmpItemBoardsList);
        //                        //_dbContext.BulkInsert(List);
        //                        //object p1 = _dbContext.TmpItemBoards.BulkSaveChanges();


        //                        /* _dbContext.BulkInsert(TmpItemBoardsList);
        //                         _dbContext.BulkSaveChanges();*/


        //                    }
        //                    //for循环结束
        //                    //_dbContext.SaveChanges();

        //                }
                       
                       

        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        string msg = e.Message;
        //        return msg;
        //    }
        //    return "OK";
        //}

        /// <summary>
        /// 上传数据到中间表
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        //private async System.Threading.Tasks.Task<string> UpdateAsync(IFormFile file)
        private string UpdateByMid(string path, string creUser, string orgId)
        {
            var returnStatus = "";
            try
            {
                using (_dbContext)
                {
                    //FileInfo newFile = new FileInfo(@"D:\tmp3562.xlsx");
                    FileInfo newFile = new FileInfo(path);
                    using (var ep = new ExcelPackage(newFile))
                    {
                        var ws = ep.Workbook.Worksheets["Sheet1"]; //第1张Sheet
                        int li_colCount = 14; //总列数
                        int li_beginRow = 10;  //开始行
                        int li_curRow = li_beginRow; //当前行
                        bool lb_isRowEnd = false; //行结束标志
                        string ls_code = "-1";
                        string ls_prmono = "";
                        string ls_prgoodscode = "";
                        string ls_curmono = "";
                        string ls_curgoodscode = "";
                        string ls_fOrdNo = "";
                        int li_fGoodsID = 0;
                        string ls_fGoodsName = "";

                        //string ls_value;
                        int li_boardnum = 0;
                        int li_wnum = 0;
                        string ls_lcode = "";
                        //string ls_tmpPackNo = "";
                        //string ls_curtmpPackNo = "";
                        string ls_fOrdQty;
                        string ls_prfOrdQty = "1";
                        string ls_boardQty;
                        string ls_barCode;
                        string ls_imageNo;
                        long ls_batchNum = getBatchNo(_dbContext)+1;
                        int pre_fOrdQty = 0;
                        string ls_prbarcode = "";
                        string ls_curbarcode = "";
                        string ls_boardsize = "";
                        string ls_isSplit = "";



                        if (ws == null)
                        {
                            return "没找到Excel的‘Sheet1’";
                        }

                        //li_wnum = getMaxnum(_dbContext, orgId, ref ls_lcode);
                        int rowIndex = 0;
                        while (!lb_isRowEnd)
                        {
                            rowIndex++;
                            li_curRow++;

                            //li_wnum = li_wnum + pre_fOrdQty;


                            
                            ls_fOrdQty = getColValue(ref ls_code, ws, li_beginRow, li_curRow, li_colCount, "fOrdQty").Trim();
                            ls_curbarcode=ls_barCode = getColValue(ref ls_code, ws, li_beginRow, li_curRow, li_colCount, "barCode").Trim();
                            ls_imageNo = getColValue(ref ls_code, ws, li_beginRow, li_curRow, li_colCount, "imageNo").Trim();
                            ls_boardQty = getColValue(ref ls_code, ws, li_beginRow, li_curRow, li_colCount, "boardQty");
                            //ls_curtmpPackNo = getColValue(ref ls_code, ws, li_beginRow, li_curRow, li_colCount, "tmpPackNo");
                            //if (ls_code == "-1")
                            //{
                            //    return "没有生产包装编号栏位!";
                            //}
                            
                            ls_curmono = getColValue(ref ls_code, ws, li_beginRow, li_curRow, li_colCount, "fmono");
                            if (ls_code == "-1")
                            {
                                return "没有制令号栏位!";
                            }
                            ls_curgoodscode = getColValue(ref ls_code, ws, li_beginRow, li_curRow, li_colCount, "fgoodscode");
                            if (ls_code == "-1")
                            {
                                return "没有品号栏位!";
                            }

                            if (ls_curmono == "" && ls_curgoodscode == "")
                            {
                                lb_isRowEnd = true; //结束
                                break;
                            }
                            else if (ls_curmono == "")
                            {
                                return "第" + li_curRow.ToString() + "行的制令不可为空!";
                            }
                            else if (ls_curgoodscode == "")
                            {
                                return "第" + li_curRow.ToString() + "行的品号不可为空!";
                            }
                            //if (ls_curtmpPackNo == "")
                            //{
                            //    return "第" + li_curRow.ToString() + "行的生产包装编号不可为空!";
                            //}

                            string ls_boardType = getColValue(ref ls_code, ws, li_beginRow, li_curRow, li_colCount, "boardType").Trim();
                            if (ls_boardType != "1" && ls_boardType != "2")
                            {
                                return "第" + li_curRow.ToString() + "行的分件类别不正确!";
                            }
                            string ls_boardcode = getColValue(ref ls_code, ws, li_beginRow, li_curRow, li_colCount, "boardcode").Trim();
                            if (ls_boardcode != "-1" && ls_boardType == "")
                            {
                                return "第" + li_curRow.ToString() + "行的分件条码不正确!";
                            }
                            if (ls_boardcode != "-1")
                            {
                                int li_ret = checkboardcode(_dbContext, ls_boardcode);
                                if (li_ret == 0)
                                {
                                    return "第" + li_curRow.ToString() + "行的分件条码重复!";
                                }
                            }
                            else
                            {
                                /*ls_boardcode = "000000" + li_wnum.ToString();
                                ls_boardcode = ls_lcode + ls_boardcode.Substring(ls_boardcode.Length - 6);*/
                            }

                            /* if (ls_prmono != ls_curmono || ls_prgoodscode != ls_curgoodscode)
                             {
                                 li_boardnum = 0;
                                 SfMOItem moItem = getSfMoItem(_dbContext, ls_curmono, ls_curgoodscode);
                                 if (moItem == null)
                                 {
                                     return "第" + li_curRow.ToString() + "行的制令：" + ls_curmono + " 品号:" + ls_curgoodscode + "在数夫制令里不存在或不正确!";
                                 }
                                 ls_tmpPackNo = getPackNo(_dbContext, orgId);
                                 ls_fOrdNo = moItem.fOrdNo;
                                 ls_fGoodsName = moItem.fGoodsName;
                                 li_fGoodsID = moItem.fGoodsID;
                             }*/
                             string ls_boardname = getColValue(ref ls_code, ws, li_beginRow, li_curRow, li_colCount, "boardname").Trim();
                             if (ls_boardname == "")
                             {
                                 return "第" + li_curRow.ToString() + "行的分件名称不正确!";
                             }

                           // mono不同，goodscode不同,barcode不同,结果：正常分包
                            Boolean judge1 = ls_prmono != ls_curmono && ls_prgoodscode != ls_curgoodscode && ls_prbarcode != ls_curbarcode;
                            //mono同，goodscode同,barcode同,结果：正常同包
                            Boolean judge2 = ls_prmono == ls_curmono && ls_prgoodscode == ls_curgoodscode && ls_prbarcode == ls_curbarcode;

                            //mono同，goodscode同,barcode不同，正常分包
                            Boolean judge3 = ls_prmono == ls_curmono && ls_prgoodscode == ls_curgoodscode && ls_prbarcode != ls_curbarcode;

                            //mono不同，goodscode同,barcode同
                            Boolean judge4 = ls_prmono != ls_curmono && ls_prgoodscode == ls_curgoodscode && ls_prbarcode == ls_curbarcode;
                            //mono同，goodscode不同,barcode同
                            Boolean judge5 = ls_prmono == ls_curmono && ls_prgoodscode != ls_curgoodscode && ls_prbarcode == ls_curbarcode;
                            Boolean judge45_1 = ls_curbarcode == "-1"; //judge4、5前提下，barcode = -1，正常分包
                            Boolean judge45_2 = ls_curbarcode != "-1"; //judge4、5前提下，barcode != -1,提示分包

                            //mono同，goodscode不同,barcode不同,结果：正常分包
                            Boolean judge6 = ls_prmono == ls_curmono && ls_prgoodscode != ls_curgoodscode && ls_prbarcode != ls_curbarcode;
                            //mono不同，goodscode同,barcode不同,结果：正常分包
                            Boolean judge7 = ls_prmono != ls_curmono && ls_prgoodscode == ls_curgoodscode && ls_prbarcode != ls_curbarcode;

                            if (judge1 || judge3 || judge6 || judge7)//正常分包
                            {
                                li_boardnum = 0;
                                SfMOItem moItem = getSfMoItem(_dbContext, ls_curmono, ls_curgoodscode);
                                if (moItem == null)
                                {
                                    return "第" + li_curRow.ToString() + "行的制令：" + ls_curmono + " 品号:" + ls_curgoodscode + "在数夫制令里不存在或不正确!";
                                }
                                //ls_tmpPackNo = getPackNo(_dbContext, orgId);
                                ls_fOrdNo = moItem.fOrdNo;
                                ls_fGoodsName = moItem.fGoodsName;
                                li_fGoodsID = moItem.fGoodsID;
                            }
                            else if (judge4 || judge5)
                            {
                                if (judge45_1)
                                {
                                    li_boardnum = 0;
                                    SfMOItem moItem = getSfMoItem(_dbContext, ls_curmono, ls_curgoodscode);
                                    if (moItem == null)
                                    {
                                        return "第" + li_curRow.ToString() + "行的制令：" + ls_curmono + " 品号:" + ls_curgoodscode + "在数夫制令里不存在或不正确!";
                                    }
                                    //ls_tmpPackNo = getPackNo(_dbContext, orgId);
                                    ls_fOrdNo = moItem.fOrdNo;
                                    ls_fGoodsName = moItem.fGoodsName;
                                    li_fGoodsID = moItem.fGoodsID;
                                }
                                else
                                {
                                    return "第" + li_curRow.ToString() + "行的制令：barCode值域不合法，应分包处理 !";
                                }
                            }


                            if (ls_fOrdQty == "-1")
                            {
                                if (ls_curmono == ls_prmono && ls_curgoodscode == ls_prgoodscode && ls_prfOrdQty == "-1")
                                {
                                    ls_fOrdQty = ls_prfOrdQty;
                                }
                                else
                                {
                                    SfMOItem sfmoItem = getSfMoQty(_dbContext, ls_curmono, ls_curgoodscode);
                                    ls_fOrdQty = Convert.ToString((sfmoItem.fMoQty).ToString());
                                    //ls_fOrdQty = sfmoItem.fMoQty;

                                }
                            }
                            ls_prfOrdQty = ls_fOrdQty;
                            pre_fOrdQty= Convert.ToInt32(Convert.ToDouble(ls_fOrdQty));

                            ls_boardsize = getColValue(ref ls_code, ws, li_beginRow, li_curRow, li_colCount, "boardsize").Trim();
                            ls_isSplit = getColValue(ref ls_code, ws, li_beginRow, li_curRow, li_colCount, "isSplit").Trim();

                            li_boardnum++;
                            var entity = new TmpItemBoardsMid();
                            //var entity = new TmpItemBoards();
                            
                            entity.Guid = Guid.NewGuid();
                            entity.Org = Convert.ToInt32(orgId);
                            //entity.tmpPackNo = ls_tmpPackNo;
                            entity.fmono = ls_curmono;
                            entity.fordno = ls_fOrdNo;
                            entity.fGoodsID = li_fGoodsID;
                            entity.fgoodscode = ls_curgoodscode;
                            entity.fgoodsname = ls_fGoodsName;
                            entity.boardType = (boardType)Convert.ToInt32(ls_boardType);
                            entity.boardcode = ls_boardcode;
                            entity.boardname = ls_boardname;
                            entity.creuser = creUser;
                            entity.credate = DateTime.Now;
                            entity.status = Status.Open;
                            entity.isValid = isValid.Valid;
                            entity.boardnum = li_boardnum;
                            entity.batchNum = ls_batchNum.ToString();
                            //_dbContext.TmpItemBoards.Add(entity);

                            entity.fOrdQty = Convert.ToInt32(Convert.ToDouble(ls_fOrdQty));
                            entity.boardQty = int.Parse(ls_boardQty);
                            entity.barCode = ls_barCode;
                            entity.imageNo = ls_imageNo;

                            entity.boardsize = ls_boardsize;
                            entity.isSplit = int.Parse(ls_isSplit);
                            entity.rowIndex = rowIndex;

                            _dbContext.TmpItemBoardsMid.Add(entity);
                            //_dbContext.TmpItemBoards.Add(entity);

                            ls_prmono = ls_curmono;
                            ls_prgoodscode = ls_curgoodscode;
                            ls_prbarcode = ls_curbarcode;



                            
                        }
                        _dbContext.SaveChanges();
                        returnStatus = "OK";
                        //returnStatus = batchInsert(_dbContext, ls_batchNum.ToString());
                    }
                    
                }
            }
            catch (Exception e)
            {
                string msg = e.Message;
                return msg;
            }
            return returnStatus;
        }

        private string getColValue(ref string rs_code, ExcelWorksheet ws, int vi_beginRow, int vi_curRow, int vi_colCount, string vs_colName)
        {
            string ls_value = "";
            rs_code = "-1";
            //retValue ret = new retValue() { code = "-1", msg = "" };
            int li_curCol = 0;
            for(int i= 1; i <= vi_colCount; i++)
            {
                if (ws.Cells[10, i].Text.Trim() == vs_colName)
                {
                    li_curCol = i;
                    break;
                }
            }
            if(li_curCol > 0)
            {
                rs_code = "0";
                ls_value = ws.Cells[vi_curRow, li_curCol].Text; //内容
            }
            return ls_value;
        }

        private SfMOItem getSfMoItem(DncZeusDbContext dbContext, string fMONo, string fGoodsCode)
        {
            var response = ResponseModelFactory.CreateInstance;
            var sql = @"select t1.fMONo,t1.fOrdNo, t3.fGoodsCode, t3.fGoodsName, t3.fGoodsID,t2.fMoQty
                        from SF.DCF19_MY.dbo.t_MPSM_MOMst t1
                        inner join SF.DCF19_MY.dbo.t_MPSM_MOItem t2 on t1.fMONo = t2.fMONo
                        inner join SF.DCF19_MY.dbo.t_BOMM_GoodsMst t3 on t2.fGoodsID = t3.fGoodsID
                        where t1.fMONo={0} and t3.fGoodsCode = {1}";
            var queryData = dbContext.SfMOItem.FromSql(sql, fMONo, fGoodsCode).ToList();
            foreach(SfMOItem item in queryData)
            {
                return item;
            }
            return null;
        }

        //根据制令获取数量
        private SfMOItem getSfMoQty(DncZeusDbContext dbContext, string fMONo,string fGoodsCode)
        {
            var response = ResponseModelFactory.CreateInstance;
            var sql = @"select t1.fMONo,t1.fOrdNo, t3.fGoodsCode, t3.fGoodsName, t3.fGoodsID,t2.fMoQty
                        from SF.DCF19_MY.dbo.t_MPSM_MOMst t1
                        inner join SF.DCF19_MY.dbo.t_MPSM_MOItem t2 on t1.fMONo = t2.fMONo
                        inner join SF.DCF19_MY.dbo.t_BOMM_GoodsMst t3 on t2.fGoodsID = t3.fGoodsID
                        where t1.fMONo={0} and t3.fGoodsCode = {1}";
            var queryData = dbContext.SfMOItem.FromSql(sql, fMONo, fGoodsCode).ToList();
            
            foreach (SfMOItem item in queryData)
            {
                return item;
            }
            return null;
        }

        private int getMaxnum(DncZeusDbContext dbContext, string Orgid, ref string rs_lcode)
        {
            string ls_year = DateTime.Now.Year.ToString().Substring(2);
            //string ls_year = DateTime.Now.Year.ToString();
            string ls_month = "00" + DateTime.Now.Month.ToString();
            string ls_day = "00" + DateTime.Now.Day.ToString();
            ls_month = ls_month.Substring(ls_month.Length - 2);
            ls_day = ls_day.Substring(ls_day.Length - 2);
            rs_lcode = "A" + ls_year + ls_month + ls_day;

            var response = ResponseModelFactory.CreateInstance;
            var sql = string.Format(@"select max(t1.boardcode) as val from TmpItemBoards t1
                                    where t1.Org = {0} and t1.boardcode like '{1}%'", Orgid, rs_lcode);
            var queryData = dbContext.retValue.FromSql(sql).ToList();
            foreach (retValue item in queryData)
            {
                if(item.val == null)
                {
                    return 0;
                }
                return Convert.ToInt32(item.val.Substring(item.val.Length - 6));
            }
            return 0;
        }

        //批次号
        private long getBatchNo(DncZeusDbContext dbContext)
        {
            var sql = string.Format(@"select max(t1.batchNum) as val from TmpItemBoardsMid t1 ");
            
            string ls_year = DateTime.Now.Year.ToString().Substring(2); ;
            string ls_month = "00" + DateTime.Now.Month.ToString();
            string ls_day = "00" + DateTime.Now.Day.ToString();
            ls_month = ls_month.Substring(ls_month.Length - 2);
            ls_day = ls_day.Substring(ls_day.Length - 2);
            string index = "001";
            string ls_batchNum = ls_year + ls_month + ls_day+ index;
            long returnVal = 0;

            var queryData = dbContext.retValue.FromSql(sql).ToList();
            foreach (retValue item in queryData)
            {
                if (item.val == null)
                {
                    returnVal=Convert.ToInt64(ls_batchNum);
                    
                }
                else {
                    returnVal=Convert.ToInt64(item.val);
                }
                
            }

            return returnVal;
        }
        //執行插入存儲過程
        private string batchInsert(DncZeusDbContext dbContext,string batchNum)
        {
            
            var sql = string.Format(@"exec sp_tmp_InsertBarcode_2 {0}", batchNum);
            string returnVal = "Error";
            var queryData = dbContext.retValue.FromSql(sql).ToList();
            foreach (retValue item in queryData)
            {
                if (item.val == "OK")
                {
                    returnVal = "OK";

                }
            }

            return returnVal;
        }
        //執行插入存儲過程
        private int getPageCount(DncZeusDbContext dbContext,string tabname,int curpage,int pagesize,string where)
        {
            int total=0;
            int totalpage = 0;
            string Conn_Str = "server=192.168.0.20;database=DncZeus;uid=sa;password=notebook@123;pooling=true;min pool size=1;max pool size=200;connect timeout = 360;";
            SqlConnection conn = new SqlConnection(Conn_Str);
            SqlCommand cmd = new SqlCommand("sp_CommQueryProcb", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StrSelect","*");
            cmd.Parameters.AddWithValue("@StrFrom", tabname);
            cmd.Parameters.AddWithValue("@StrWhere", where);
            cmd.Parameters.AddWithValue("@StrOrder", ""); 
            cmd.Parameters.AddWithValue("@PageIndex", curpage);
            cmd.Parameters.AddWithValue("@PageSize", pagesize);
            cmd.Parameters.Add("@ItemCount", SqlDbType.BigInt,50).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@PageCount", SqlDbType.Int,20).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@SqlQuery", SqlDbType.VarChar,200).Direction = ParameterDirection.Output;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                total = (int)cmd.Parameters["@ItemCount"].Value;
                /*totalpage = (int)cmd.Parameters["@PageCount"].Value;*/
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return total;

        }

        private List<TmpItemBoards> getPageList(DncZeusDbContext dbContext, string tabname, int curpage, int pagesize, string where)
        {
            List<TmpItemBoards> pageList = new List<TmpItemBoards>();
            where = where.Replace("\"", "\'");
            var sql = string.Format(@"exec sp_CommQueryProcb '*', '"+ tabname + "','"+ where + "', 'Guid',"+ curpage + ","+ pagesize + ", '', '',''");
            pageList = dbContext.TmpItemBoards.FromSql(sql).ToList(); 
            return pageList;
        }

        private string getPackNo(DncZeusDbContext dbContext, string Orgid)
        {
            string ls_year = DateTime.Now.Year.ToString().Substring(2);
            string ls_month = "00" + DateTime.Now.Month.ToString();
            string ls_day = "00" + DateTime.Now.Day.ToString();
            ls_month = ls_month.Substring(ls_month.Length - 2);
            ls_day = ls_day.Substring(ls_day.Length - 2);
            string ls_lcode = "T" + ls_year + ls_month + ls_day;
            string ls_return = ls_lcode + "0001";
            int li_maxnum = 0;

            var response = ResponseModelFactory.CreateInstance;
            var sql = string.Format(@"select max(t1.tmpPackNo) as val
                        from TmpItemBoardsMid t1 
                        where t1.Org = {0} and t1.tmpPackNo like '{1}%'", Orgid, ls_lcode);
            var queryData = dbContext.retValue.FromSql(sql).ToList();
            foreach (retValue item in queryData)
            {
                if (item.val == null)
                {
                    return ls_return;
                }
                li_maxnum = Convert.ToInt32(item.val.Substring(item.val.Length - 4));
            }

            li_maxnum++;
            string ls_boardcode = "00000" + li_maxnum.ToString();
            ls_return = ls_lcode + ls_boardcode.Substring(ls_boardcode.Length - 4);

            return ls_return;
        }

        private int checkboardcode(DncZeusDbContext dbContext, string vs_boardcode)
        {
            var response = ResponseModelFactory.CreateInstance;
            var sql = string.Format(@"select max(t1.boardname) as val
                        from TmpItemBoards t1 
                        where t1.boardcode = '{0}'", vs_boardcode);
            var queryData = dbContext.retValue.FromSql(sql).ToList();
            return queryData.Count();
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult getPackNoDetail(Guid guid)
        {
            using (_dbContext)
            {
                var query = _dbContext.TmpItemBoardsH.AsQueryable();
                query = query.Where(x => x.Guid==guid);
                var obj = query.ToList()[0];

                // var entity = _dbContext.MisCkind.FirstOrDefault(x => x.ID == id);

                var response = ResponseModelFactory.CreateInstance;
                response.SetData(obj);
                // response.SetData(_mapper.Map<MisCkind, KindCreateViewModel>(entity));
                return Ok(response);
            }
        }

        //public class retValue
        //{
        //    public string code { get; set; }
        //    public string msg { get; set; }
        //}
        #endregion
    }
}