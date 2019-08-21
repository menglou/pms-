using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Security;

namespace PMS.WebUI.Controllers
{
    
    public class SysExitsController : Controller
    {
        // GET: SysExits
        public ActionResult Index()
        {
            FormsAuthentication.SignOut();//消除身份票据
            Session.Abandon();
            return RedirectToAction("Index","AdminLogin");
        }
    }
}