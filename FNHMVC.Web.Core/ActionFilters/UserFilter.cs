using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Security.Principal;
using FNHMVC.Web.Core.Models;
using FNHMVC.Web.Core.Extensions;

namespace FNHMVC.Web.Core.ActionFilters
{
    //Inject a ViewBag object to Views for getting information about an authenticated user
    public class UserFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            UserModel userModel;

            //if (filterContext.Controller.ViewBag.UserModel == null)
            //{
            //    userModel = new UserModel();
            //    filterContext.Controller.ViewBag.UserModel = userModel;
            //}
            //else
            //{
            //    userModel = filterContext.Controller.ViewBag.UserModel as UserModel;
            //}

            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //FNHMVCUser FNHMVCUser = filterContext.HttpContext.User.GetFNHMVCUser();
                //userModel.IsUserAuthenticated = FNHMVCUser.IsAuthenticated;
                //userModel.UserName = FNHMVCUser.DisplayName;
                //userModel.RoleName = FNHMVCUser.RoleName;
                IPrincipal user = filterContext.HttpContext.User;
                bool isInrole = user.IsInRole("Admin");
            }

            base.OnActionExecuted(filterContext);
        }
    }

    public class UserModel
    {
        public bool IsUserAuthenticated { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }
}
