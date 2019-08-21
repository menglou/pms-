using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMS.UIModel;
using PMS.Logic;
using PMS.Entity;

namespace PMS.WebUI.Controllers.Sys
{
    [RequestAuthorize]
    public class AuthorController : Controller
    {
        private AuthorService authorservice = new AuthorService();

        // GET: Author
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 判断权限
        /// </summary>
        /// <param name="actionid">动作id (0代表新增，1新增保存，2代表修改，3代表修改保存，4代表删除)</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult IsHaveAuthor(int actionid)
        {
            ICollection<Author> authorlist = (ICollection<Author>)Session["authorlist"];
            Author author = new Author();
            switch (actionid)
            {
                case 0:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Author" && x.ActionName == "Create");
                    break;
                case 1:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Author" && x.ActionName == "AddParentOrChildAuthor");
                    break;
                case 2:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Author" && x.ActionName == "Update");
                    break;
                case 3:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Author" && x.ActionName == "UpdateSave");
                    break;
                case 4:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Author" && x.ActionName == "Delete");
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

        public ActionResult Create()
        {
            string authorguid = Request.QueryString["authorguid"];
            ViewBag.authorguid = authorguid;
            return View();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="authorid"></param>
        /// <returns></returns>

        public ActionResult Delete(int authorid)
        {
            JsonResultData<string> data = new JsonResultData<string>();

            try
            {
                int result = authorservice.Delete(authorid);

                if (result == 0)
                {
                    data.Code = 0;
                    data.Data = "删除失败";
                }
                else
                {
                    data.Code = 1;
                    data.Data = "删除成功";
                }
            }
            catch (Exception ex)
            {
                data.Msg = ex.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);

        }



        public ActionResult Update(int authorid)
        {

            ViewBag.authorid = authorid;

            return View();
        }

        /// <summary>
        /// 权限初始化
        /// </summary>
        /// <param name="authorid"></param>
        /// <returns></returns
        [AllowAnonymous]
        public ActionResult UpdateInit(int authorid)
        {
            JsonResultData<AuthorUIModel> resultdata = new JsonResultData<AuthorUIModel>();

            try
            {
                AuthorUIModel author = authorservice.GetAuthorById(authorid);
                resultdata.Data = author;
                if (author == null)
                {
                    resultdata.Code = 0;
                }
                else
                {
                    resultdata.Code = 1;
                }
            }
            catch (Exception ex)
            {
                resultdata.Msg = ex.Message;
            }

            return Json(resultdata, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 编辑权限保存
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateSave(Author author)
        {

            JsonResultData<string> resultdata = new JsonResultData<string>();


            try
            {
                int result = authorservice.UpdateAuthorSave(author);

                if (result == 0)
                {
                    resultdata.Code = 0;
                    resultdata.Data = "更新失败";
                }
                else
                {
                    resultdata.Code = 1;
                    resultdata.Data = "更新成功";
                }
            }
            catch (Exception ex)
            {
                resultdata.Msg = ex.Message;
            }

            return Json(resultdata, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult CheckSelf(string authorguid)
        {
            //string authorguid = Request.QueryString["authorguid"];

            JsonResultData<string> data = new JsonResultData<string>();

            try
            {
                int result = authorservice.CheckSelfIsHave(new Guid(authorguid));

                if (result == 0)
                {
                    data.Code = 0;
                }
                else
                {
                    data.Code = 1;
                }
            }
            catch (Exception ex)
            {
                data.Msg = ex.Message;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加子节点或父节点
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        public ActionResult AddParentOrChildAuthor(Author author)
        {

            JsonResultData<string> resultdata = new JsonResultData<string>();


            try
            {
                int result = authorservice.AddParentOrChildAuthor(author);

                if (result == 0)
                {
                    resultdata.Code = 0;
                    resultdata.Data = "添加失败！";
                }
                else
                {
                    resultdata.Code = 1;
                    resultdata.Data = "添加成功！";
                }
            }
            catch (Exception ex)
            {
                resultdata.Msg = ex.Message;
            }

            return Json(resultdata, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 权限列表
        /// </summary>
        [AllowAnonymous]
        public ActionResult GetAuthorExpand()
        {

            TreeTable<AuthorUIModel> treebaleAuthorUIModel = authorservice.GetAuthorList();

            return Json(treebaleAuthorUIModel, JsonRequestBehavior.AllowGet);
        }
    }
}