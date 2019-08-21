using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMS.Entity;
using PMS.Logic;
using PMS.UIModel;
using System.Web.Script.Serialization;

namespace PMS.WebUI.Controllers.Sys
{
    [RequestAuthorize]
    public class RoleController : Controller
    {
        private RoleService roleservice = new RoleService();

       
        // GET: Role
        public ActionResult Index()
        {
          return View();
        }

        /// <summary>
        /// 判断权限
        /// </summary>
        /// <param name="actionid">动作id (0代表新增，1新增保存，2代表修改，3代表修改保存，4代表删除,5选择权限，6选择权保存)</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult IsHaveAuthor(int actionid)
        {
            ICollection<Author> authorlist = (ICollection<Author>)Session["authorlist"];
            Author author = new Author();
            switch (actionid)
            {
                case 0:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Role" && x.ActionName == "Create");
                    break;
                case 1:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Role" && x.ActionName == "AddRole");
                    break;
                case 2:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Role" && x.ActionName == "Update");
                    break;
                case 3:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Role" && x.ActionName == "UpdateRole");
                    break;
                case 4:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Role" && x.ActionName == "Delete");
                    break;
                case 5:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Role" && x.ActionName == "ChoseAuthor");
                    break;
                case 6:
                    author = authorlist.FirstOrDefault(x => x.ControllerName == "Role" && x.ActionName == "UpateRoleAuthor");
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
            string roleguid = Request.QueryString["roleguid"];
            ViewBag.roleguid=roleguid;
            return View();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public ActionResult AddRole(Role role)
        {
            ICollection<Author> authorlist = (ICollection<Author>)Session["authorlist"];

            Author author = authorlist.FirstOrDefault(x => x.ControllerName == "Role" && x.ActionName == "AddRole");

            JsonResultData<string> resultdata = new JsonResultData<string>();

            if (author!=null)
            {
                try
                {
                    int result = roleservice.AddRole(role);

                    if (result == 0)
                    {
                        resultdata.Code = 0;
                        resultdata.Data = "添加失败";
                    }
                    else if (result == 1)
                    {
                        resultdata.Code = 1;
                        resultdata.Data = "添加成功";
                    }
                }
                catch (Exception ex)
                {
                    resultdata.Msg = ex.Message;
                }
            }
            else
            {
                resultdata.Code = 2;
                resultdata.Data = "没有权限";
            }
            return Json(resultdata, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public ActionResult Delete(int roleid)
        {
            JsonResultData<string> resultdata = new JsonResultData<string>();
            
            try
            {
                int result = roleservice.Delete(roleid);

                if(result==0)
                {
                    resultdata.Code = 0;
                    resultdata.Data = "删除失败";
                }
                else if(result==1)
                {
                    resultdata.Code = 1;
                    resultdata.Data = "删除成功";
                }
            }
            catch (Exception ex)
            {
                resultdata.Msg = ex.Message;
            }
            return Json(resultdata, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public ActionResult Update(int roleid)
        {
            ViewBag.roleid = roleid;
            return View();
        }


        public ActionResult GetAuthorByRoleId(int roleid)
        {
            TreeTable<AuthorUIModel> authorlist = roleservice.GetAuthorByRoleId(roleid);

            ViewBag.authorlist = authorlist.data;
            return Json(authorlist, JsonRequestBehavior.AllowGet);
            
        }


        /// <summary>
        /// 初始化信息
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns
        [AllowAnonymous]
        public ActionResult UpdateInit(int roleid)
        {
            JsonResultData<RoleUIModel> resultdata = new JsonResultData<RoleUIModel>();

            try
            {
                RoleUIModel role = roleservice.GetRoleById(roleid);

          
                if (role== null)
                {
                    resultdata.Code = 0;
                    resultdata.Data = role;
                }
                else
                {
                    resultdata.Code = 1;
                    resultdata.Data = role;
                }
            }
            catch (Exception ex)
            {
                resultdata.Msg = ex.Message;
            }

            return Json(resultdata,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public ActionResult UpdateRole(Role role)
        {
            ICollection<Author> authorlist = (ICollection<Author>)Session["authorlist"];

            Author author = authorlist.FirstOrDefault(x => x.ControllerName == "Role" && x.ActionName == "UpdateRole");

            JsonResultData<string> resultdata = new JsonResultData<string>();

            if(author!=null)
            {
                try
                {
                    int result = roleservice.Update(role);

                    if (result == 0)
                    {
                        resultdata.Code = 0;
                        resultdata.Data = "编辑失败！";
                    }
                    else
                    {
                        resultdata.Code = 1;
                        resultdata.Data = "编辑成功！";
                    }
                }
                catch (Exception ex)
                {
                    resultdata.Msg = ex.Message;
                }
            }
            else
            {
                resultdata.Code = 2;
                resultdata.Data = "没有权限！";
            }
            return Json(resultdata,JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult ChoseAuthor(int roleid)
        {
            ViewBag.roleid = roleid;
            return View();
        }
        [AllowAnonymous]
        public ActionResult GetRoleExpand()
        {
            TreeTable<RoleUIModel> treetablerole = roleservice.GetRoleList();
           
            return Json(treetablerole, JsonRequestBehavior.AllowGet);
        }


        public ActionResult UpateRoleAuthor(string datalist,int rolid)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();

            List<AuthorUIModel> authorlist =js.Deserialize<List< AuthorUIModel>>(datalist);//获取要添加的权限

            List<int> authorid = new List<int>();
            foreach (var item in authorlist)
            {
                authorid.Add(item.AuthorIdUIModel);
            }
            JsonResultData<string> resultdata = new JsonResultData<string>();

            ICollection<Author> authorlists = (ICollection<Author>)Session["authorlist"];

            Author author = authorlists.FirstOrDefault(x => x.ControllerName == "Role" && x.ActionName == "UpateRoleAuthor");
             
            if(author!=null)
            {
                try
                {
                    int result = roleservice.AddAuthorForRole(authorid, rolid);

                    if (result == 0)
                    {
                        resultdata.Code = 0;
                        resultdata.Data = "保存权限失败！";
                    }
                    else if (result == -1)
                    {
                        resultdata.Code = 0;
                        resultdata.Data = "保存权限失败！";
                    }
                    else
                    {
                        resultdata.Code = 1;
                        resultdata.Data = "保存权限成功！";
                    }
                }
                catch (Exception ex)
                {
                    resultdata.Msg = ex.Message;
                }
            }
            else
            {
                resultdata.Code = 2;
                resultdata.Data = "没有权限";
            }
            
            return Json(resultdata,JsonRequestBehavior.AllowGet);
        }
    }
}