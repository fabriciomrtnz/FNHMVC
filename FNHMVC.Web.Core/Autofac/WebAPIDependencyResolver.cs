using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;

namespace FNHMVC.Web.Core.Autofac
{
    public class WebAPIDependencyResolver : System.Web.Mvc.IDependencyResolver
    {

        private readonly IContainer container;

        public WebAPIDependencyResolver(IContainer container)
        {

            this.container = container;
        }

        public object GetService(Type serviceType)
        {

            return
                container.IsRegistered(serviceType) ?
                container.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {

            Type enumerableServiceType =
                typeof(IEnumerable<>).MakeGenericType(serviceType);

            object instance =
                container.Resolve(enumerableServiceType);

            return ((IEnumerable)instance).Cast<object>();
        }

 }
}
