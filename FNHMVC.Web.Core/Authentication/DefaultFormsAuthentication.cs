
using System.Web;
using System.Web.Security;
using System;

namespace FNHMVC.Web.Core.Authentication
{
    public class DefaultFormsAuthentication : IFormsAuthentication
    {
        public void SetAuthCookie(string userName, bool persistent)
        {
            FormsAuthentication.SetAuthCookie(userName, persistent);
        }

        public void Signout()
        {
            FormsAuthentication.SignOut();
        }
       
        public void SetAuthCookie(HttpContextBase httpContext, FormsAuthenticationTicket authenticationTicket)
        {
            var encryptedTicket = FormsAuthentication.Encrypt(authenticationTicket);
            httpContext.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket) { Expires = CalculateCookieExpirationDate() });
        }

        public void SetAuthCookie(HttpContext httpContext, FormsAuthenticationTicket authenticationTicket)
        {
            var encryptedTicket = FormsAuthentication.Encrypt(authenticationTicket);
            httpContext.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket) { Expires = CalculateCookieExpirationDate()});
        }

        private static DateTime CalculateCookieExpirationDate()
        {
            return DateTime.Now.Add(FormsAuthentication.Timeout);
        }

        public FormsAuthenticationTicket Decrypt(string encryptedTicket)
        {
            return FormsAuthentication.Decrypt(encryptedTicket);
        }
    }
}