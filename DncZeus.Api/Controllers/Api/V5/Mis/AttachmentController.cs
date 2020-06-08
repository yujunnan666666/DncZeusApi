using AutoMapper;
using DncZeus.Api.Entities;
using DncZeus.Api.Entities.SF;
using DncZeus.Api.Entities.Enums;
using DncZeus.Api.Extensions;
using DncZeus.Api.Extensions.AuthContext;
using DncZeus.Api.Extensions.CustomException;
using DncZeus.Api.Extensions.DataAccess;
using DncZeus.Api.Models.Response;
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
using DncZeus.Api.RequestPayload.Sec.Org;
using DncZeus.Api.ViewModels.Sec.Org;
using DncZeus.Api.Entities.Sec;
using static DncZeus.Api.Entities.Tmp.tmpEnum;
using DncZeus.Api.RequestPayload.Sec.User;
using DncZeus.Api.ViewModels.Sec.User;
using System.Collections.Generic;
using DncZeus.Api.ViewModels.Sec.Group;
using File = DncZeus.Api.Entities.File;
using DncZeus.Api.RequestPayload.Pmc.Projectattachment;
using DncZeus.Api.ViewModels.Pmc.Projectattachment;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using DncZeus.Api.Entities.Pmc;
using ImgUtil;

namespace DncZeus.Api.Controllers.Api.Mis
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomAuthorize]
    [ApiController]
    [ApiVersion("5.0")] //V5
    [Route("api/v5/Mis/[controller]/[action]")]
    public class AttachmentController : ControllerBase
    {
        private readonly DncZeusDbContext _dbContext;
        private readonly IMapper _mapper;
        private IHostingEnvironment _host;

        public AttachmentController(DncZeusDbContext dbContext, IMapper mapper, IHostingEnvironment host)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _host = host;
        }

        //统计字符
      /*  private static int SubstringCount(string str, string substring) {
            if (str.Contains(substring)) {
                string strReplaced = str.Replace(substring, "");
                return (str.Length - strReplaced)
            }
            return 0;
        }*/

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [DisableRequestSizeLimit]
        public IActionResult UploadFile()
        {
            //var response = ResponseModelFactory.CreateInstance;
            var response = ResponseModelFactory.CreateResultInstance;

            var oFile = Request.Form.Files["file"];
            string fn = oFile.FileName.ToString();

            var dian = fn.IndexOf(".");


            string ext=fn.Substring(fn.IndexOf("."),fn.Length-fn.IndexOf("."));
            string filename = fn.Substring(0,fn.IndexOf("."));

            Guid guid = Guid.NewGuid();
            string uuid = guid.ToString("N");
            string folderName = "MisAttachment";
            string yyyy = DateTime.Now.ToString("yyyy");
            string mmdd = DateTime.Now.ToString("MMdd");
            string pathUrl= folderName+ "/" + yyyy + "/" + mmdd + "/" + uuid ;

            var path = Path.Combine(_host.ContentRootPath + "\\Upload\\file\\"+ pathUrl);
            var path_com= Path.Combine(_host.ContentRootPath + "\\Upload\\file\\" + pathUrl+"\\thumb\\");
            var allPath = Path.Combine(path, fn);

            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string stamp= Convert.ToInt64(ts.TotalMilliseconds).ToString();
            var allPath_com= Path.Combine(path_com, fn);

            if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                Directory.CreateDirectory(path_com);
            }
            using (var stream = System.IO.File.Create(allPath))
                 {
                      oFile.CopyTo(stream);
                     stream.Close();
                     stream.Dispose();
                if (ext == "jpg" || ext == "png"|| ext == "jpeg" || ext == "gif" || ext == "bmp" || ext == "pic" ) {
                    var ls_ret = Copy_Image(allPath, allPath_com);
                }
                
            }

           
            

            MisAttachment Att = new MisAttachment();
            Att.Guid = guid;
            Att.mainUrl = "/Upload/file/";
            Att.folderName = folderName;
            Att.yyyy = yyyy;
            Att.mmdd = mmdd;
            Att.pathUrl = pathUrl+"/"+fn;
            Att.thumbUrl = pathUrl + "/thumb/" + fn;
            Att.fillName = filename;
            Att.fillType = ext;
            Att.fillSize = int.Parse(oFile.Length.ToString());
            Att.credate = DateTime.Now;
            Att.creuser = AuthContextService.CurrentUser.DisplayName;

            _dbContext.MisAttachment.Add(Att);
            _dbContext.SaveChanges();

           
            

            var filePath = "/Upload/file/" + pathUrl + "/" + fn;
            var filePath_com = "/Upload/file/" + pathUrl + "/thumb/" + fn;

            JObject info = new JObject(){
                        new JProperty("attGuid",guid),//
                        new JProperty("path",filePath),//
                        new JProperty("path_com",filePath_com),//
                        new JProperty("fileName",fn),//
                       
                    };

            var returnJson = (JObject)JsonConvert.DeserializeObject("{'data':" + info + "}");
            return Ok(returnJson);
        }

        //文件压缩
        private string Copy_Image(string vs_toUrl,string vs_compathUrl)
        {
            /*//创建文件夹
            if (!Directory.Exists(vs_toPath))
            {
                Directory.CreateDirectory(vs_toPath);
            }
            System.IO.File.Copy(vs_fromUrl, vs_toUrl, true); //复制文件*/

            //图片压缩
            ImageCompress img = new ImageCompress();
            /// <param name="vs_toUrl">原图片地址</param>
            /// <param name="vs_compathUrl">压缩后保存图片地址</param>
            /// <param name="flag">压缩质量（数字越小压缩率越高）1-100</param>
            /// <param name="size">压缩后图片的最大大小</param>
            bool lb_ret = img.CompressImage(vs_toUrl, vs_compathUrl, 80, 100);
            if (!lb_ret) return "图片压缩失败";

            return "OK";
        }

        public class uploadResult
        {
            public string fileName { get; set; }
            public string error { get; set; }
        }
    }
}