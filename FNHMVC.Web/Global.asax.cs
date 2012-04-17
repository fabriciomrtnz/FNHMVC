using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using FNHMVC.Web.Core.Models;
using System.Security.Principal;
using FNHMVC.Web.Core.Authentication;
using FNHMVC.Web.Core.ActionFilters;

namespace FNHMVC.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new UserFilter());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
        public override void Init()
        {
            this.PostAuthenticateRequest += this.PostAuthenticateRequestHandler;
            base.Init();
        }
        private void PostAuthenticateRequestHandler(object sender, EventArgs e)
        {
            HttpCookie authCookie = this.Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (IsValidAuthCookie(authCookie))
            {
                var formsAuthentication = DependencyResolver.Current.GetService<IFormsAuthentication>();

                var ticket = formsAuthentication.Decrypt(authCookie.Value);
                var FNHMVCUser = new FNHMVCUser(ticket);
                string[] userRoles = { FNHMVCUser.RoleName };
                this.Context.User = new GenericPrincipal(FNHMVCUser, userRoles);
                formsAuthentication.SetAuthCookie(this.Context, ticket);
            }
        }
        private static bool IsValidAuthCookie(HttpCookie authCookie)
        {
            return authCookie != null && !String.IsNullOrEmpty(authCookie.Value);
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            BundleTable.Bundles.RegisterTemplateBundles();
            Bootstrapper.Run();
        }
    }
}