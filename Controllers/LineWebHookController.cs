using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace LineWebHook.Controllers
{
    [System.Web.Http.RoutePrefix("Hook")]
    public class LineWebHookController : ApiController
    {

        // GET: LineWebHook
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route]
        public IHttpActionResult webhook()
        {
            return Ok("OK");
        }
    }
}