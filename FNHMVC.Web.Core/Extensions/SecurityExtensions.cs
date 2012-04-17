using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using FNHMVC.Web.Core.Models;

namespace FNHMVC.Web.Core.Extensions
{
    public static class SecurityExtensions
    {
        public static string Name(this IPrincipal user)
        {
            return user.Identity.Name;
        }

        public static bool InAnyRole(this IPrincipal user, params string[] roles)
        {
            foreach (string role in roles)
            {
                if (user.IsInRole(role)) return true;
            }
            return false;
        }

        public static FNHMVCUser GetFNHMVCUser(this IPrincipal principal)
        {
            if (principal.Identity is FNHMVCUser)
                return (FNHMVCUser)principal.Identity;
            else
                return new FNHMVCUser(string.Empty, new UserInfo());
        }
    }
}
