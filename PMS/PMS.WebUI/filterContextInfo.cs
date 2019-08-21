using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PMS.WebUI
{
   public class filterContextInfo
    {
        public filterContextInfo(ActionExecutingContext filterContext)
        {
            //获取域名
            domainName = filterContext.HttpContext.Request.Url.Authority;
            //获取 controllerName 名称
            controllerName = filterContext.RouteData.Values["controller"].ToString();


            //获取ACTION 名称
            actionName = filterContext.RouteData.Values["action"].ToString();
        }
        public string domainName { get; set; }
        /// <summary>
        /// 获取模块名称
        /// </summary>
        public string module { get; set; }
        /// <summary>
        /// 获取 controllerName 名称
        /// </summary>
        public string controllerName { get; set; }
        /// <summary>
        /// 获取ACTION 名称
        /// </summary>
        public string actionName { get; set; }


    }
}
