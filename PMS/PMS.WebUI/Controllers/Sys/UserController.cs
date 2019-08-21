using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMS.Entity;
using PMS.UIModel;
using PMS.Util;
using PMS.Logic;

namespace PMS.WebUI.Controllers.Sys
{
    [RequestAuthorize]
    public class UserController : Controller
    {
        private UserService userservice = new UserService();

        // GET: User
        public ActionResult Index()
        {
            //DataTableJsonResult<UserUIModel> datatableresult = userservice.GetUserList();

            // return Json(datatableresult, JsonRequestBehavior.AllowGet);

            return View();
        }

        /// <summary>
        /// 判断权限
        /// </summary>
        /// <param name="actionid">动作id (0代表新增，1新增保存，2代表修改，3代表修改保存，4代表删除，5代表禁用)</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult IsHaveAuthor(int actionid)
        {
            ICollection<Author> authorlist = (ICollection<Author>)Session["authorlist"];
            Author author = new Author();
            switch (actionid)
            {
                case 0:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "User" && x.ActionName == "Create");
                    break;
                case 1:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "User" && x.ActionName == "AddUser");
                    break;
                case 2:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "User" && x.ActionName == "Update");
                    break;
                case 3:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "User" && x.ActionName == "UpdateSave");
                    break;
                case 4:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "User" && x.ActionName == "Delete");
                    break;
                case 5:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "User" && x.ActionName == "DisableUser");
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
        /// 添加窗体
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <returns></returns>
        public ActionResult AddUser(User user)
        {

            JsonResultData<string> resultdata = new JsonResultData<string>();


            try
            {
                int result = userservice.AddUser(user);

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
        /// 编辑窗体
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public ActionResult Update(int userid)
        {
            ViewBag.userid = userid;
            return View();
        }

        /// <summary>
        /// 初始化用户信息编辑时
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult UpdateInit(int userid)
        {
            JsonResultData<UserUIModel> resultdata = new JsonResultData<UserUIModel>();

            try
            {
                UserUIModel user = userservice.GetUserById(userid);

                if (user == null)
                {
                    resultdata.Code = 0;
                    resultdata.Data = user;
                }
                else
                {
                    resultdata.Code = 1;
                    resultdata.Data = user;
                }
            }
            catch (Exception ex)
            {
                resultdata.Msg = ex.Message;
            }
            return Json(resultdata, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 更新保存
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ActionResult UpdateSave(User user)
        {


            JsonResultData<string> resultdata = new JsonResultData<string>();

            try
            {
                int result = userservice.Update(user);

                if (result == 0)
                {
                    resultdata.Code = 0;
                    resultdata.Data = "编辑失败";
                }
                else
                {
                    resultdata.Code = 1;
                    resultdata.Data = "编辑成功";
                }
            }
            catch (Exception ex)
            {
                resultdata.Msg = ex.Message;
            }

            return Json(resultdata, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public ActionResult Delete(int userid)
        {
            JsonResultData<string> resultdata = new JsonResultData<string>();

            try
            {
                int result = userservice.Delete(userid);

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

        [AllowAnonymous]
        public ActionResult GetSelectLsit()
        {
            List<RoleUIModel> rolelsit = userservice.GetRoleList();
            return Json(rolelsit, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult DisableUser(int userid)
        {
            JsonResultData<string> resultdata = new JsonResultData<string>();

            try
            {
                int result = userservice.DiableUser(userid);

                if (result == 0)
                {
                    resultdata.Code = 0;
                    resultdata.Data = "禁用失败！";
                }
                else if (result == 1)
                {
                    resultdata.Code = 1;
                    resultdata.Data = "禁用成功！";
                }
                else if (result == 2)
                {
                    resultdata.Code = 0;
                    resultdata.Data = "禁用失败！";
                }
            }
            catch (Exception ex)
            {
                resultdata.Msg = ex.Message;
            }
            return Json(resultdata, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult GetUserList()
        {
            DataTableJsonResult<UserUIModel> datatableresult = userservice.GetUserList();

            return Json(datatableresult, JsonRequestBehavior.AllowGet);
        }
    }
}