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
    public class ItemBarController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        private IHostingEnvironment _host;
        private Guid hid;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        /// <param name="host"></param>
        public ItemBarController(DncZeusDbContext dbContext, IMapper mapper, IHostingEnvironment host)
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
        public IActionResult boardList(BarhRequestPayload payload)
        {
            using (_dbContext)
            {
                var query = _dbContext.TmpItemBoardsMid.AsQueryable();
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    int li_org = AuthContextService.CurrentUser.OrgID;

                    query = query.Where(x => x.Org == li_org &&
                    (
                    x.fmono.Contains(payload.Kw.Trim()) ||
                    x.fgoodscode.Contains(payload.Kw.Trim()) ||
                    x.fgoodsname.Contains(payload.Kw.Trim()) ||
                    x.fPackNo.Contains(payload.Kw.Trim())
                    )
                    );
                }
                else
                {
                    query = query.Where(x => x.Org == AuthContextService.CurrentUser.OrgID);
                }

                if (payload.FirstSort != null)
                {
                    query = query.OrderBy(payload.FirstSort.Field, payload.FirstSort.Direct == "DESC");
                }

                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();

                var totalCount = query.Count();
                //var data = list.Select(_mapper.Map<TmpItemBoards, BoardJsonModel>);

               /* foreach (BoardJsonModel item in data)
                {
                    string ls_a = item.creuser;
                    item.fBarcode = "test";
                }*/

                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(list, totalCount);
                return Ok(response);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(BarhRequestPayload payload)
        {
            using (_dbContext)
            {
                var query = _dbContext.TmpItembarHead.AsQueryable();
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    int li_org = AuthContextService.CurrentUser.OrgID;

                    query = query.Where(x => x.Org == li_org &&
                    (
                    x.fmono.Contains(payload.Kw.Trim()) ||
                    x.fgoodscode.Contains(payload.Kw.Trim()) ||
                    x.fgoodsname.Contains(payload.Kw.Trim()) ||
                    x.fPackNo.Contains(payload.Kw.Trim()) 
                    )
                    );
                }
                else
                {
                    query = query.Where(x => x.Org == AuthContextService.CurrentUser.OrgID);
                }

                if (payload.FirstSort != null)
                {
                    query = query.OrderBy(payload.FirstSort.Field, payload.FirstSort.Direct == "DESC");
                }

                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query.Count();
                var data = list.Select(_mapper.Map<TmpItembarHead, BarhJsonModel>);

                //foreach (BarhJsonModel item in data)
                //{
                    //string ls_a = item.creuser;
                    //item.fmono = "test";
                //}

                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(data, totalCount);
                return Ok(response);
            }
        }

        /// <summary>
        /// 上传导入数据
        /// </summary>
        /// <returns>object</returns>
        [HttpPost]
        public IActionResult SaveValueBatch(IFormCollection form)
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

                            string ls_ret = Update(allPath, creUser, orgId); //写入数据库服务器
                            if(ls_ret != "OK")
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
                    return Ok(response);
                }
            }
        }

        private ActionResult UserJsonModel(object p, string v)
        {
            throw new NotImplementedException();
        }

        #region 私有方法
        /// <summary>
        /// 上传数据到数据库
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        //private async System.Threading.Tasks.Task<string> UpdateAsync(IFormFile file)
        private string Update(string path, string creUser, string orgId)
        {
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
                        string ls_value;
                        int li_boardnum = 0;
                        int li_wnum = 0;
                        string ls_lcode = "";

                        if (ws == null)
                        {
                            return "没找到Excel的‘Sheet1’";
                        }
                        li_wnum = getMaxnum(_dbContext, orgId, ref ls_lcode);

                        while (!lb_isRowEnd)
                        {
                            li_curRow++;
                            li_boardnum++;
                            li_wnum++;

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
                                return "制令不可为空!";
                            }
                            else if (ls_curgoodscode == "")
                            {
                                return "品号不可为空!";
                            }

                            if (ls_prmono != ls_curmono || ls_prgoodscode != ls_curgoodscode)
                            {
                                SfMOItem moItem = getSfMoItem(_dbContext, ls_curmono, ls_curgoodscode);
                                if (moItem == null)
                                {
                                    return "制令：" + ls_curmono + " 品号:" + ls_curgoodscode + "在数夫制令里不存在或不正确!";
                                }

                                li_boardnum = 1;
                                hid = Guid.NewGuid();
                                var entity = new TmpItembarHead();
                                entity.Guid = hid;
                                entity.Org = 1; // AuthContextService.CurrentUser.OrgID;
                                entity.fmono = ls_curmono;
                                entity.fordno = moItem.fOrdNo;
                                entity.fgoodscode = ls_curgoodscode;
                                entity.fgoodsname = moItem.fGoodsName;

                                ls_value = getColValue(ref ls_code, ws, li_beginRow, li_curRow, li_colCount, "fqty");
                                if(ls_value == "")
                                {
                                    ls_value = "1";
                                }
                                entity.fqty = Convert.ToInt32(ls_value);
                                entity.creuser = creUser;
                                entity.credate = DateTime.Now;
                                entity.status = tmpEnum.Status.Open;
                                entity.spStatus = tmpEnum.SpStatus.Not;
                                _dbContext.TmpItembarHead.Add(entity);
                            }

                            string ls_boardcode = "00000" + li_wnum.ToString();
                            ls_boardcode = ls_lcode + ls_boardcode.Substring(ls_boardcode.Length - 5);

                            var entityLine = new TmpItembarLine();
                            entityLine.ItembarHead = new TmpItembarHead();
                            entityLine.ItembarHead.Guid = hid;
                            ls_value = getColValue(ref ls_code, ws, li_beginRow, li_curRow, li_colCount, "fspcode");
                            entityLine.fspcode = ls_value;
                            ls_value = getColValue(ref ls_code, ws, li_beginRow, li_curRow, li_colCount, "fspname");
                            entityLine.fspname = ls_value;
                            entityLine.boardnum = li_boardnum;
                            entityLine.boardcode = ls_boardcode;
                            entityLine.credate = DateTime.Now;
                            entityLine.creuser = creUser;
                            _dbContext.TmpItembarLine.Add(entityLine);

                            ls_prmono = ls_curmono;
                            ls_prgoodscode = ls_curgoodscode;
                        }
                        _dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                string msg = e.Message;
                return msg;
            }
            return "OK";
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
                var sql = @"select t1.fMONo,t1.fOrdNo, t3.fGoodsCode, t3.fGoodsName,t3.fGoodsID
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

        private int getMaxnum(DncZeusDbContext dbContext, string Orgid, ref string rs_lcode)
        {
            string ls_year = DateTime.Now.Year.ToString().Substring(2);
            string ls_month = "00" + DateTime.Now.Month.ToString();
            string ls_day = "00" + DateTime.Now.Day.ToString();
            ls_month = ls_month.Substring(ls_month.Length - 2);
            ls_day = ls_day.Substring(ls_day.Length - 2);
            rs_lcode = "A" + ls_year + ls_month + ls_day;

            var response = ResponseModelFactory.CreateInstance;
            var sql = string.Format(@"select max(t1.boardcode) as val
                        from TmpItembarLine t1
                        inner join TmpItembarHead t2 on t1.ItembarHeadGuid = t2.Guid
                        where t2.Org = {0} and t1.boardcode like '{1}%'", Orgid, rs_lcode);
            var queryData = dbContext.retValue.FromSql(sql).ToList();
            foreach (retValue item in queryData)
            {
                if(item.val == null)
                {
                    return 0;
                }
                return Convert.ToInt32(item.val.Substring(item.val.Length - 5));
            }
            return 0;
        }

        //public class retValue
        //{
        //    public string code { get; set; }
        //    public string msg { get; set; }
        //}
        #endregion



    }
}