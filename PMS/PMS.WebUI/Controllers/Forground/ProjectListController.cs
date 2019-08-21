using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMS.Logic;
using PMS.Entity;
using PMS.UIModel;
using PMS.Util;
using PMS.UIModel.Forground;

namespace PMS.WebUI.Controllers.Forground
{
    [Authorize]
    public class ProjectListController : Controller
    {
        // GET: ProjectList
        public ActionResult Index()
        {
            User user = (User)Session["currentuserinfo"];

            List<ForProjectUIModel> forprolist = new ForProjectService().GetProjectList();
            ViewBag.users = user;
            return View(forprolist);
            
        }


        //public ActionResult GetProjectList()
        //{



        //    return View("Index",)
        //}
    }
}