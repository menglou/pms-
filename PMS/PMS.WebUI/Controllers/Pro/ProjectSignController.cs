using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMS.Entity;
using PMS.Logic;
using PMS.UIModel;
using PMS.Util;


namespace PMS.WebUI.Controllers.Pro
{
    [Authorize]
    public class ProjectSignController : Controller
    {
        private ProjectSignService projectsignservice = new ProjectSignService();

        // GET: ProjectSign
        public ActionResult Index()
        {
            ICollection<Author> authorlist = (ICollection<Author>)Session["authorlist"];

            Author author = authorlist.FirstOrDefault(x => x.ControllerName == "ProjectSign" && x.ActionName == "Index");

            if(author!=null)
            {
                return View();
            }
            else
            {
                return Redirect("NoAuthority.html");
            }
           
        }

        /// <summary>
        /// 判断权限
        /// </summary>
        /// <param name="actionid">动作id (0查看学员信息，1审核)</param>
        /// <returns></returns>
        public ActionResult IsHaveAuthor(int actionid)
        {
            ICollection<Author> authorlist = (ICollection<Author>)Session["authorlist"];
            Author author = new Author();
            switch (actionid)
            {
                case 0:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "ProjectSign" && x.ActionName == "PreviewStudentInfo");
                    break;
                case 1:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "ProjectSign" && x.ActionName == "Audit");
                    break;
               
            }
            JsonResultData<string> result = new JsonResultData<string>();

            if (author == null)
            {
                result.Code = 0;
                result.Data = "没有权限";
            }
            else
            {
                result.Code = 1;
                result.Data = "有权限";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 项目报名列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetProjecSigntrForList()
        {
            DataTableJsonResult<ProjectSignUIModel> tableprosign = projectsignservice.GetProjecSigntrList();
            return Json(tableprosign,JsonRequestBehavior.AllowGet);
        }

        public ActionResult PreviewStudentInfo(int studentid)
        {
            StudentUIModel stu = new StudentService().GetStudentById(studentid);
            return View(stu);
        }

        public ActionResult Audit()
        {
            return View();
        }
    }
}