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
using FNHMVC.Web.Core.Models;
using Microsoft.AspNet.Identity;
using NHibernate;
using AutoMapper;
using Microsoft.Owin.Security;

namespace FNHMVC.Web
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
            AutoMapperConfiguration.Configure();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();

            //Infrastructure objects
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterAssemblyTypes(typeof(MvcApplication).Assembly).AsImplementedInterfaces();
            builder.RegisterType<DefaultCommandBus>().As<ICommandBus>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterModule(new AutofacWebTypesModule());

            //Command Query Responsibility Separation objects
            var services = Assembly.Load("FNHMVC.Domain");
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerRequest();
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(IValidationHandler<>)).InstancePerRequest();

            //Repositories objects
            builder.RegisterAssemblyTypes(typeof(IRepository<Expense>).Assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(IRepository<Category>).Assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(IRepository<User>).Assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();

            //NHibernate objects
            builder.Register(c => FNHMVC.Data.Infrastructure.ConnectionHelper.BuildSessionFactory("FNHMVCContainer")).As<ISessionFactory>().SingleInstance();
            builder.Register(c => c.Resolve<ISessionFactory>().OpenSession()).InstancePerRequest();

            //Automapper objects
            builder.Register(c => new ConfigurationStore(new TypeMapFactory(), AutoMapper.Mappers.MapperRegistry.Mappers)).AsImplementedInterfaces().SingleInstance();
            builder.Register(c => Mapper.Engine).As<IMappingEngine>().SingleInstance();
            builder.RegisterType<TypeMapFactory>().As<ITypeMapFactory>().SingleInstance();

            //Microsoft Identity objects
            builder.RegisterType<FNHMVCUser>().InstancePerRequest();
            builder.RegisterType<DefaultUserRoleStore>().As<IUserRoleStore<FNHMVCUser, int>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager<FNHMVCUser, int>>().InstancePerRequest();
            builder.RegisterType<EmailService>().As<IIdentityMessageService>().InstancePerRequest();
            builder.RegisterType<SmsService>().As<IIdentityMessageService>().InstancePerRequest();

            builder.RegisterModelBinderProvider();
            builder.RegisterFilterProvider();

            IContainer container = builder.Build();
 
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }        
    }
}
