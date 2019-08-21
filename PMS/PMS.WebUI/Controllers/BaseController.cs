using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using PMS.Entity;

namespace PMS.WebUI.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 保存用户登陆信息
        /// </summary>
        public void WriteUserInfoToCookie(PMS.Entity.User userinfo)
        {
            var jss = new JavaScriptSerializer();
            var logonInfo = jss.Serialize(userinfo);

            //设置Ticket信息
            var ticket = new FormsAuthenticationTicket(1, userinfo.UserNickName, DateTime.Now,
                                                       DateTime.Now.AddDays(1), false,
                                                       logonInfo);
            //加密验证票据
            var strTicket = FormsAuthentication.Encrypt(ticket);
            //保存cookie
            SetCookie(FormsAuthentication.FormsCookieName, strTicket, ticket.Expiration, true);
        }

        /// <summary>
        /// 写入Cooike
        /// </summary>
        /// <param name="cookiename"></param>
        /// <param name="value"></param>
        /// <param name="expires"></param>
        /// <param name="isSetExpires"></param>
        public static void SetCookie(string cookiename, string value, DateTime expires, bool isSetExpires)
        {
            var request = System.Web.HttpContext.Current.Request;
            var response = System.Web.HttpContext.Current.Response;
            var cookie = request.Cookies[cookiename] ?? new System.Web.HttpCookie(cookiename);
            cookie.Domain = FormsAuthentication.CookieDomain;
            if (value == null)
            {
                RemoveCookie(cookiename);
            }
            else
            {
                cookie.Value = value;

                //true代表客户端只能读，不能写。只有服务端可写，防止被篡改
                cookie.HttpOnly = true;

                if (isSetExpires)
                {
                    cookie.Expires = expires;
                }
            }
            response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 移除指定名称的cookie对象中的集合对
        /// </summary>
        /// <param name="cookieName">cookie名称</param>
        public static void RemoveCookie(string cookieName)
        {
            var cookie = System.Web.HttpContext.Current.Request.Cookies[cookieName];
            var response = System.Web.HttpContext.Current.Response;
            if (cookie == null) return;
            cookie.Values.Clear();
            cookie.Domain = FormsAuthentication.CookieDomain;
            cookie.Expires = DateTime.Now.AddDays(-10000d);
            response.Cookies.Add(cookie);
        }
    }
}