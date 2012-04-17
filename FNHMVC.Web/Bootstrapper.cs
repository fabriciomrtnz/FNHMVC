using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using FNHMVC.Model;
using FNHMVC.CommandProcessor.Command;
using FNHMVC.CommandProcessor.Dispatcher;
using FNHMVC.Data.Infrastructure;
using FNHMVC.Data.Repositories;
using FNHMVC.Web.Core.Authentication;
using System.Web.Http;
using FNHMVC.Web.Core.Autofac;
using NHibernate;

namespace FNHMVC.Web
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();          
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            
            builder.Register(c => FNHMVC.Data.Infrastructure.ConnectionHelper.BuildSessionFactory()).As<ISessionFactory>().SingleInstance();
            builder.Register(c => c.Resolve<ISessionFactory>().OpenSession()).InstancePerLifetimeScope();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<DefaultCommandBus>().As<ICommandBus>().InstancePerHttpRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerHttpRequest();

            builder.RegisterAssemblyTypes(typeof(IRepository<Expense>).Assembly).Where(t => t.Name.EndsWith("ExpenseRepository")).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(IRepository<Category>).Assembly).Where(t => t.Name.EndsWith("CategoryRepository")).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(IRepository<User>).Assembly).Where(t => t.Name.EndsWith("UserRepository")).AsImplementedInterfaces().InstancePerLifetimeScope();
            
            var services = Assembly.Load("FNHMVC.Domain");
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerHttpRequest();
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(IValidationHandler<>)).InstancePerHttpRequest();
            builder.RegisterType<DefaultFormsAuthentication>().As<IFormsAuthentication>().InstancePerHttpRequest();
            builder.RegisterFilterProvider();

            builder.Register(c => FNHMVC.Data.Infrastructure.ConnectionHelper.BuildSessionFactory()).As<ISessionFactory>().SingleInstance();
            builder.Register(c => c.Resolve<ISessionFactory>().OpenSession()).InstancePerLifetimeScope();
            
            IContainer container = builder.Build();                  
            
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }        
    }
}
