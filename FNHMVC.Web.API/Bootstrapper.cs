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
using NHibernate;
using AutoMapper;
using Microsoft.Owin.Security;
using FNHMVC.Web.Core.Models;
using Microsoft.AspNet.Identity;

namespace FNHMVC.Web.API
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacWebAPIServices();
            AutoMapperConfiguration.Configure();
        }

        private static void SetAutofacWebAPIServices()
        {
            var builder = new ContainerBuilder();

            //NHibernate objects
            builder.Register(c => FNHMVC.Data.Infrastructure.ConnectionHelper.BuildSessionFactory("FNHMVCContainer")).As<ISessionFactory>().SingleInstance();
            builder.Register(c => c.Resolve<ISessionFactory>().OpenSession()).InstancePerRequest();

            //Infrastructure objects
            builder.RegisterType<DefaultCommandBus>().As<ICommandBus>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(IRepository<Expense>).Assembly).Where(t => t.Name.EndsWith("ExpenseRepository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(IRepository<Category>).Assembly).Where(t => t.Name.EndsWith("CategoryRepository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(IRepository<User>).Assembly).Where(t => t.Name.EndsWith("UserRepository")).AsImplementedInterfaces().InstancePerRequest();

            //Command Query Responsibility Separation objects
            var services = Assembly.Load("FNHMVC.Domain");
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerRequest();
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(IValidationHandler<>)).InstancePerRequest();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //Automapper objects
            builder.Register(c => new ConfigurationStore(new TypeMapFactory(), AutoMapper.Mappers.MapperRegistry.Mappers)).AsImplementedInterfaces().SingleInstance();
            builder.Register(c => Mapper.Engine).As<IMappingEngine>().SingleInstance();
            builder.RegisterType<TypeMapFactory>().As<ITypeMapFactory>().SingleInstance();

            //Microsoft Identity objects
            builder.RegisterType<FNHMVCUser>().InstancePerRequest();
            builder.RegisterType<DefaultUserRoleStore>().As<IUserRoleStore<FNHMVCUser, int>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager<FNHMVCUser, int>>().As<UserManager<FNHMVCUser, int>>().InstancePerRequest();
            builder.RegisterType<EmailService>().As<IIdentityMessageService>().InstancePerRequest();
            builder.RegisterType<SmsService>().As<IIdentityMessageService>().InstancePerRequest();

            IContainer container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);

            GlobalConfiguration.Configuration.DependencyResolver = resolver;            
        }
    }
}
