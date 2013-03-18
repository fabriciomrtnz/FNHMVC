using System.Web.Mvc;
using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using FNHMVC.Model;
using FNHMVC.CommandProcessor.Command;
using FNHMVC.CommandProcessor.Dispatcher;
using FNHMVC.Data.Infrastructure;
using FNHMVC.Data.Repositories;
using FNHMVC.Web.Core.Authentication;
using System.Web.Http;
//using FNHMVC.Web.Core.Autofac; //Using Autofact.Integration.WebApi instead
using NHibernate;

namespace FNHMVC.Web.API
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacWebAPIServices();
        }

        private static void SetAutofacWebAPIServices()
        {
            var builder = new ContainerBuilder();

            builder.Register(c => FNHMVC.Data.Infrastructure.ConnectionHelper.BuildSessionFactory("FNHMVCContainer")).As<ISessionFactory>().SingleInstance();
            builder.Register(c => c.Resolve<ISessionFactory>().OpenSession()).InstancePerLifetimeScope();

            builder.RegisterType<DefaultCommandBus>().As<ICommandBus>().InstancePerApiRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerApiRequest();

            builder.RegisterAssemblyTypes(typeof(IRepository<Expense>).Assembly).Where(t => t.Name.EndsWith("ExpenseRepository")).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(IRepository<Category>).Assembly).Where(t => t.Name.EndsWith("CategoryRepository")).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(IRepository<User>).Assembly).Where(t => t.Name.EndsWith("UserRepository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            var services = Assembly.Load("FNHMVC.Domain");
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerApiRequest();
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(IValidationHandler<>)).InstancePerApiRequest();
           
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            IContainer container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);

            GlobalConfiguration.Configuration.DependencyResolver = resolver;            
        }
    }
}
