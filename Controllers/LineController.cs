using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using LineWebHook.Models;
using System.Net;
using System.Text;
using System.IO;

namespace LineWebHook.Controllers
{
    [System.Web.Http.RoutePrefix("Hook")]
    [System.Web.Mvc.RoutePrefix("Hook2")]
    public class LineController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route]
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route]
        public IHttpActionResult webhook([FromBody] LineWebhookModels data)
        {
            if (data == null) return BadRequest();
            if (data.events == null) return BadRequest();
            string senderID = "";
            string replyToken = "";
            foreach (Event e in data.events)
            {
                if (e.type == EventType.message)
                {
                    
                    switch (e.source.type)
                    {
                        case SourceType.user:
                            senderID = e.source.userId;
                            break;
                        case SourceType.room:
                            senderID = e.source.roomId;
                            break;
                        case SourceType.group:
                            senderID = e.source.groupId;
                            break;
                    }
                    replyToken = e.replyToken;
                    System.IO.File.WriteAllText(@"D:\List.txt", replyToken);

                }
            }
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.line.me/v2/bot/message/reply");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add("Authorization", "Bearer Token...");
            httpWebRequest.Method = "POST";

            //將測試訊息預先寫死
            var json = Encoding.UTF8.GetBytes("{\"replyToken\":\""+ replyToken + "\",\"messages\":[{\"type\":\"text\",\"text\":\"你有Free Style嗎\"}]}");

            Stream stream = httpWebRequest.GetRequestStream();
            stream.Write(json, 0, json.Length);
            stream.Close();

            WebResponse response = httpWebRequest.GetResponse();
            return Ok(senderID);
        }
    }
}