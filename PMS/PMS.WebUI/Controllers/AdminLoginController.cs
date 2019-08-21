using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMS.Logic;
using PMS.Entity;
using PMS.UIModel;
using PMS.Util;
using System.Web.Security;
using System.Web.Script.Serialization;
 
namespace PMS.WebUI.Controllers
{
    public class AdminLoginController : Controller
    {
       [Authorize]
        // GET: AdminLogin
        public ActionResult Index()
        {
            EFContext ef = new EFContext();
            var data = ef.SysMenus.ToList();
            return View();
        }

        [HttpPost]
   
        public ActionResult AdminLogin(string loginphonenum, string loginpwd)
        {
            JsonResultData<string> resultdata = new JsonResultData<string>();

            string statuscode = string.Empty;
            User user = new Entity.User()
            {
                UserPhoneNumber = loginphonenum,
                UserPwd = loginpwd,
            };
            try
            {
                User getuser = new AdminLoginService().AdminLogin(user, out statuscode);

                if (getuser == null)
                {
                    resultdata.Code = 0;
                    resultdata.Data = statuscode;
                }
                else
                {
                    if (statuscode == "账户被禁用")
                    {
                        resultdata.Code = 0;
                        resultdata.Data = statuscode;
                    }
                    else if (statuscode == "用户名或密码错误")
                    {
                        resultdata.Code = 0;
                        resultdata.Data = statuscode;
                    }
                    else if (statuscode == "登录成功")
                    {

                        //如果登录成功//把信息保存到session中
                        // Response.Cookies["currentuserinfo"].Value = getuser;

                        //在获取此学员拥有的权限

                        int? roleid = getuser.RoleId;

                        ICollection<Author> authorlist = new RoleService().GetAllAuthor(roleid);//获取这个学员所有的权限

                        // List<AuthorUIModel> list = authorlist;


                        // ViewBag.authorlist = list;

                        //判断此人的角色
                        //   int roleid = (int)getuser.RoleId;
                        Role role = new AdminLoginService().GetRole(roleid);
                        Author author = null;

                        if(role!=null)
                        {
                            if (role.RoleName == "学员")
                            {
                                author = authorlist.FirstOrDefault(x => x.ActionName == "Index" && x.ControllerName == "ProjectList");
                                resultdata.IsStuent = 0;
                            }

                            else//不是学生的
                            {
                                author = authorlist.FirstOrDefault(x => x.ActionName == "Index" && x.ControllerName == "Home");
                                resultdata.IsStuent = 1;
                            }
                            if (author != null)
                            {
                                resultdata.Code = 1;
                                resultdata.Data = statuscode;
                                

                                FormsAuthentication.SetAuthCookie(getuser.UserId.ToString(), true);//创建一个身份凭证

                                //FormsAuthenticationTicket ticks=new FormsAuthenticationTicket(1,getuser.UserId,DateTime.Now,DateTime.Now.AddHours(1.0),true, Newtonsoft)

                                //用户的信息
                                Session["currentuserinfo"] = getuser;
                                ////权限的信息
                                Session["authorlist"] = authorlist;


                            }
                            else
                            {
                                resultdata.Code = 2;
                                resultdata.Data = "您没有权限";
                            }
                        }
                        else
                        {
                            resultdata.Code = 3;
                            resultdata.Data = "您还是未知的角色";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultdata.Msg = ex.Message;
            }
            return Json(resultdata, JsonRequestBehavior.AllowGet);
        }
    }
}