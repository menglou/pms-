using PMS.Entity;
using PMS.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace PMS.WebUI
{
   public class RequestAuthorizeAttribute:AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var name = filterContext.HttpContext.User.Identity.Name;

            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }
            if (!string.IsNullOrEmpty(name))
            {


                ICollection<Author> authorlist= SearchAuthorByRoleid(Convert.ToInt32(name));

                string controller = filterContext.RouteData.Values["controller"].ToString();
                string action = filterContext.RouteData.Values["action"].ToString();

                ICollection<Author> author = authorlist.Where(x => x.ControllerName == controller && x.ActionName == action).ToList();

                if(author.Count==0)
                {
                    //new JsonResult { Data = "", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    //new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "Home", Action = "Index" }));
                    //new ContentResult { Content = "写要返回的文本" };
                     filterContext.Result = new RedirectResult("NoAuthority.html");
                       
                } 
                else
                {

                }
            }
            else
            {
                  
                filterContext.Result = new  RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "AdminLogin", Action = "Index" }));
            }
        }


        public ICollection<Author> SearchAuthorByRoleid(int userid)
        {
            string statuscode = string.Empty;
            User user = new Entity.User()
            {
                UserId = userid
            };

            User getuser = new AdminLoginService().AdminLogin(user);

            int? roleid = getuser.RoleId;


            ICollection<Author> authorlist = new RoleService().GetAllAuthor(roleid);//获取这个学员所有的权限

            return authorlist;
        }
    }
}
