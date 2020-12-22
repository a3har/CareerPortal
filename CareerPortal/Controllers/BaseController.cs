using CareerPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareerPortal.Controllers
{
    public class BaseController : Controller
    {
        protected static SessionInfo UserInfo;
        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            base.OnActionExecuting(ctx);
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SessionUser")))
            {
                UserInfo = new SessionInfo();
                HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(UserInfo));
            }
            else
            {
                UserInfo = JsonConvert.DeserializeObject<SessionInfo>(HttpContext.Session.GetString("SessionUser"));
            }
        }
    }
}
