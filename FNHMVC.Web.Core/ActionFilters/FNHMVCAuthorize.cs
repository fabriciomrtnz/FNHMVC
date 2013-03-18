using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace FNHMVC.Web.Core.ActionFilters
{
    public class FNHMVCAuthorize : AuthorizeAttribute
    {
        public FNHMVCAuthorize(params string[] roles)
        {
            this.Roles = String.Join(", ", roles);
        }
    }
}
