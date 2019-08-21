using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMS.Logic;
using PMS.Entity;
using PMS.UIModel;

namespace PMS.WebUI.Controllers.Sys
{
    [RequestAuthorize]
    public class MenuController : Controller
    {
        private MenuService menuservice = new MenuService();

        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>

        public ActionResult Create()
        {
            string parentguid = Request.QueryString["menuguid"];

            ViewBag.parentguid = parentguid;
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
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Menu" && x.ActionName == "Create");
                    break;
                case 1:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Menu" && x.ActionName == "AddChildOrParentMenu");
                    break;
                case 2:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Menu" && x.ActionName == "Update");
                    break;
                case 3:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Menu" && x.ActionName == "UpdateMenu");
                    break;
                case 4:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Menu" && x.ActionName == "Delete");
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
        /// 更新
        /// </summary>
        /// <returns></returns>
        public ActionResult Update(int menuid)
        {
            ViewBag.menuid = menuid;
            return View();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="menuid"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult UpdateInit(int menuid)
        {
            JsonResultData<Menu> resultdata = new JsonResultData<Menu>();

            try
            {
                Menu menu = menuservice.GetMenuInfoByMenuId(menuid);

                if (menu == null)
                {
                    resultdata.Code = 0;
                    resultdata.Data = menu;
                }
                else
                {
                    resultdata.Code = 1;
                    resultdata.Data = menu;
                }
            }
            catch (Exception ex)
            {
                resultdata.Msg = ex.Message;
            }
            ViewBag.menuid = menuid;
            return Json(resultdata, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 添加子菜单或一级菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public ActionResult AddChildOrParentMenu(Menu menu)
        {

            JsonResultData<string> resultdata = new JsonResultData<string>();

            try
            {
                int result = menuservice.AddChildOrParentMenu(menu);

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

        public ActionResult Delete(int menuid)
        {

            JsonResultData<string> resultdata = new JsonResultData<string>();

            try
            {
                int result = menuservice.DeleteMenu(menuid);

                if (result == 0)
                {
                    resultdata.Code = 0;
                    resultdata.Data = "删除失败！";
                }
                else
                {
                    resultdata.Code = 1;
                    resultdata.Data = "删除成功！";
                }
            }
            catch (Exception ex)
            {
                resultdata.Msg = ex.Message;
            }

            return Json(resultdata, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 更新菜单信息
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public ActionResult UpdateMenu(Menu menu)
        {
            JsonResultData<string> resultdata = new JsonResultData<string>();

            try
            {
                int result = menuservice.UpdateMenu(menu);

                if (result == 0)
                {
                    resultdata.Code = 0;
                    resultdata.Data = "更新失败！";
                }
                else
                {
                    resultdata.Code = 1;
                    resultdata.Data = "更新成功！";
                }
            }
            catch (Exception ex)
            {
                resultdata.Msg = ex.Message;
            }

            return Json(resultdata, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 菜单列表
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult GetMenuExpand()
        {
            TreeTable<MenUIModel> TreeTableMenUIModel = menuservice.GetMenuExpand();

            return Json(TreeTableMenUIModel, JsonRequestBehavior.AllowGet);
        }
    }
}