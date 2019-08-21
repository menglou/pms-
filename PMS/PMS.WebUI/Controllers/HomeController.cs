using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMS.Logic;
using PMS.Entity;
using PMS.UIModel;

namespace PMS.WebUI.Controllers
{
    [RequestAuthorize]
    public class HomeController :Controller
    {
        // GET: Home
     
        public ActionResult Index()
        {
            User user = (User)Session["currentuserinfo"];

            ViewBag.users = user;
            List<MenUIModel> MenUIModelList = new MenuService().GetMenList();
            return View(MenUIModelList);
        }
    }
}