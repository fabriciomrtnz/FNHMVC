using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.Controllers;
using Autofac.Builder;
using Autofac.Features.Scanning;
using Autofac;
using System.Reflection;

namespace FNHMVC.Web.Core.Autofac
{
   public static class ApiControllerExtension
    {
       public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> RegisterApiControllers(this ContainerBuilder builder, params Assembly[] controllerAssemblies)
       {
           return from t in builder.RegisterAssemblyTypes(controllerAssemblies) where typeof(IHttpController).IsAssignableFrom(t) && t.Name.EndsWith("Controller") select t;
       }
    }
}
