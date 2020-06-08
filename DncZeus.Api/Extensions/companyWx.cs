using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ZDClass;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace DncZeus.Api.Extensions
{
    public class CompanyWx
    {
        private string Corpid = "wwb88b795d4ec8d9b4";
        private string Secret = "WldjZLL_r_rWeBCcPY-lWHD8CQYry9o8bSC0nhPrAtI";
        private string Token = "";
        public string Get_Token(String corpid,String secret)
        {
            try
            {
                
                var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}", corpid, secret);

                var response = new HttpClient().GetAsync(url).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;
                JObject jo = (JObject)JsonConvert.DeserializeObject(responseContent);
                string ls_expires_in = jo["expires_in"].ToString();
                string ls_access_token = jo["access_token"].ToString();
                return ls_access_token;
               

            }
            catch (Exception ex)
            {
                string ls_msg = ex.Message;
            }

            
            return "";
        }
        //批量获取审批单号
        public JArray GetApprovalInfo(String starttime,String endtime,int cursor,int size,String tempid)
        {
            JArray arr = new JArray();
            try
            {
                
                var token = Get_Token(Corpid,Secret);
                Token = token;
                var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/oa/getapprovalinfo?access_token={0}", Token);


                //var data = Encoding.UTF8.GetBytes("{ \"starttime\": \""+starttime+ "\", \"endtime\": \"" + endtime + "\",\"cursor\":"+ cursor + ",\"size\":"+ size + "\",\"filters\":[{\"key\":\"template_id\",\"value\":" + tempid + "}] }");
                string param = "{\"starttime\":\"" + starttime + "\",\"endtime\": \"" + endtime + "\",\"cursor\":\"" + cursor + "\",\"size\":" + size + ",\"filters\":[{\"key\":\"template_id\",\"value\":\"" + tempid + "\"}] }";
                var data = Encoding.UTF8.GetBytes(param);

                var content = new ByteArrayContent(data);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                var response = new HttpClient().PostAsync(url, content).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;
                JObject jo = (JObject)JsonConvert.DeserializeObject(responseContent);
                JArray spList = (JArray)jo["sp_no_list"];
               
                return spList;

            }
            catch (Exception ex)
            {
                string ls_msg = ex.Message;
            }
            return arr;
        }

        //获取审批详情
        public JObject GetApprovalDetail(String ordernum)
        {
            JObject json = new JObject();
            try
            {
                var token = Get_Token(Corpid, Secret);
                Token = token;
                var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/oa/getapprovaldetail?access_token={0}&sp_no={1}", Token, ordernum);
                var data = Encoding.UTF8.GetBytes("{ \"sp_no\":" + ordernum + "}");

                var content = new ByteArrayContent(data);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                var response = new HttpClient().PostAsync(url, content).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;
                JObject jo = (JObject)JsonConvert.DeserializeObject(responseContent);
                JObject info = (JObject)jo["info"];
                //response.SetData(result);
                return info;

            }
            catch (Exception ex)
            {
                string ls_msg = ex.Message;
            }
            return json;
        }

        //获取成员信息
        public JObject GetUser(String userid)
        {
            JObject json = new JObject();
            try
            {
                var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/get?access_token={0}&userid={1}", Token, userid);
                
                var response = new HttpClient().GetAsync(url).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;
                JObject jo = (JObject)JsonConvert.DeserializeObject(responseContent);
                //response.SetData(result);
                return jo;

            }
            catch (Exception ex)
            {
                string ls_msg = ex.Message;
            }
            return json;
        }

        //获取部门
        public String GetDepartment(String partyid)
        {
            
            try
            {
                var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/department/list?access_token={0}", Token);

                var response = new HttpClient().GetAsync(url).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;
                JObject jo = (JObject)JsonConvert.DeserializeObject(responseContent);
                JArray deps =(JArray)jo["department"];

                foreach (JObject dep in deps) {
                    if ((int)dep["id"]== Convert.ToInt32(partyid)) {
                        return (string)dep["name"];
                    }
                }
                    //response.SetData(result); 

            }
            catch (Exception ex)
            {
                string ls_msg = ex.Message;
            }
            return "无部门";
        }

        //获取素材
        public void DwonloadFile(String fileId)
        {
            try
            {
                /*var token = Get_Token();
                Token = token;
                var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}", Token, fileId);

                var resp = new HttpClient().GetAsync(url).Result;
                var con_dis = resp.Content.Headers.Where(o => o.Key == "Content-Disposition").Select(m => m.Value).FirstOrDefault().FirstOrDefault();
                Regex regex = new Regex("filename=(.+);");
                var filename = regex.Match(con_dis).Groups[1].Value;
                var ext = filename.Split('.').Reverse().First();
                new FileExtensionContentTypeProvider().Mappings.TryGetValue("." + ext, out var contenttype);
                var buffer = resp.Content.ReadAsByteArrayAsync();

                return File(buffer, contenttype, filename);*/

                /*var response = new HttpClient().GetAsync(url).Result;
                var responseContent = response.Content.read().Result;*/
               // var responseContent = response.Content.ReadAsStringAsync().Result;

                
                //return true;
            }
            catch (System.Exception e)
            {
                //return false;
            }


        }

        //时间戳转日期
        public DateTime GetDateTime(string strLongTime)
        {
            string ls_Old = "", ls_New = "";
            int li_Index = 0; //变量声明
            ls_Old = strLongTime;//赋原始字符串值给变量
            li_Index = ls_Old.LastIndexOf(".");//获得.的位置
            if (li_Index != -1)
            {
                ls_New = ls_Old.Substring(0, li_Index);//获得目标字符串
            }
            else {
                ls_New = ls_Old;
            }
            

            Int64 begtime = Convert.ToInt64(ls_New) * 10000000;//100毫微秒为单位,textBox1.text需要转化的int日期
            DateTime dt_1970 = new DateTime(1970, 1, 1, 8, 0, 0);
            long tricks_1970 = dt_1970.Ticks;//1970年1月1日刻度
            long time_tricks = tricks_1970 + begtime;//日志日期刻度
            DateTime dt = new DateTime(time_tricks);//转化为DateTim
            return dt;
        }

        //日期转时间戳
        public long GetTimeStamp(DateTime strLongTime)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            long timeStamp = (long)(strLongTime - startTime).TotalSeconds; // 相差秒数
            return timeStamp;
        }

        //审批节点状态：1-审批中；2-已通过；3-已驳回；4-已撤销；6-通过后撤销；7-已删除；10-已支付
        public string RedenerStatus(string status)
        {
            string stat = "";
            switch(status){
                case "1":
                    stat = "审批中";
                    break;
                case "2":
                    stat = "已通过";
                    break;
                case "3":
                    stat = "已驳回";
                    break;
                case "4":
                    stat = "已撤销";
                    break;
                case "6":
                    stat = "通过后撤销";
                    break;
                

            }
            return stat;
        }

        //批量获取已删除审批单号
        public IList<JToken> GetApprovalInfoByDelete(String starttime, String endtime, int cursor, int size, String tempid)
        {
            IList<JToken> arr = new List<JToken>();
            try
            {

                var token = Get_Token(Corpid, Secret);
                Token = token;
                var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/oa/getapprovalinfo?access_token={0}", Token);


                //var data = Encoding.UTF8.GetBytes("{ \"starttime\": \""+starttime+ "\", \"endtime\": \"" + endtime + "\",\"cursor\":"+ cursor + ",\"size\":"+ size + "\",\"filters\":[{\"key\":\"template_id\",\"value\":" + tempid + "}] }");
                string param = "{\"starttime\":\"" + starttime + "\",\"endtime\": \"" + endtime + "\",\"cursor\":\"" + cursor + "\",\"size\":" + size + ",\"filters\":[{\"key\":\"template_id\",\"value\":\"" + tempid + "\"},{\"key\":\"sp_status\",\"value\":\"7\"}] }";
                var data = Encoding.UTF8.GetBytes(param);

                var content = new ByteArrayContent(data);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                var response = new HttpClient().PostAsync(url, content).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;
                JObject jo = (JObject)JsonConvert.DeserializeObject(responseContent);
                IList<JToken> spList = (IList<JToken>)jo["sp_no_list"];

                return spList;

            }
            catch (Exception ex)
            {
                string ls_msg = ex.Message;
            }
            return arr;
        }

        //推送消息
        public void SendWxMsg(string token,string touser,string msgContent)
        {
            JObject json = new JObject();


            //HttpContext.Session.SetString("VERFIY_CODE_TOKEN", 123);
            
            try
            {
                //Secret = "9sZRSUF4hj866IZ7Pf-kCHjz7nCFuEbzp0LPCd-7x14";//美盈HR 密匙
                Secret = "WldjZLL_r_rWeBCcPY-lWHD8CQYry9o8bSC0nhPrAtI";//审批 密匙
                var agentId = 3010040;

                //var token = Get_Token();
                Token = token;
                var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={0}", Token);
                string param = "{\"touser\":\"" + touser + "\",\"msgtype\": \"text\",\"agentid\":"+ agentId + ",\"text\":{\"content\":\"" + msgContent + "\"}}";
                var data = Encoding.UTF8.GetBytes(param);

                var content = new ByteArrayContent(data);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                var response = new HttpClient().PostAsync(url, content).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;
                JObject jo = (JObject)JsonConvert.DeserializeObject(responseContent);
                JObject info = (JObject)jo["info"];
                //response.SetData(result);
               // return info;

            }
            catch (Exception ex)
            {
                string ls_msg = ex.Message;
            }
            //return json;
        }





    }


}
