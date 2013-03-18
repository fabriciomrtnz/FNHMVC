using System.Web;
using System.Web.Mvc;
using FNHMVC.Web.Core.ActionFilters;

namespace FNHMVC.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new UserFilter());
        }
    }
}