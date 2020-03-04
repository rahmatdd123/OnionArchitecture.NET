using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace OnionArchitecture.Auth
{
    public class Auth
    {

        private readonly HttpContextBase httpContextBase;
        public void SignOn(string username, double expiration, bool isPersistent)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddMinutes(expiration), isPersistent,username);
            string encToken = FormsAuthentication.Encrypt(ticket);
            HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, encToken);
            //httpResponseBase.Cookies.Add(authCookies);
            httpContextBase.Response.Cookies.Add(authCookies);
        }
    }
}