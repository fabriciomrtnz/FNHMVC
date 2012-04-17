using FNHMVC.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using Autofac;
using Autofac.Configuration;
using FNHMVC.CommandProcessor.Dispatcher;
using FNHMVC.Data.Repositories;
using FNHMVC.Data.Infrastructure;
using FNHMVC.Domain.Commands;
using FNHMVC.Core.Common;
using FNHMVC.CommandProcessor.Command;
using System.Reflection;
using NHibernate;
using FluentNHibernate.Cfg;

namespace FNHMVC.Test
{
    [TestClass()]
    public class UserTest
    {
        private TestContext testContextInstance;
        private IContainer container;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        public UserTest()
        {
            var builder = new ContainerBuilder();

            builder.Register(c => FNHMVC.Data.Infrastructure.ConnectionHelper.BuildSessionFactory()).As<ISessionFactory>().SingleInstance();
            builder.Register(c => c.Resolve<ISessionFactory>().OpenSession()).InstancePerLifetimeScope();

            builder.RegisterType<DefaultCommandBus>().SingleInstance();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(IRepository<User>).Assembly).Where(t => t.Name.EndsWith("UserRepository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            var services = Assembly.Load("FNHMVC.Domain");
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(IValidationHandler<>)).InstancePerLifetimeScope();

            container = builder.Build();
        }

        [TestMethod()]
        public void UserCreateTest()
        {
            using (var lifetime = container.BeginLifetimeScope())
            {
                DefaultCommandBus commandBus = lifetime.Resolve<DefaultCommandBus>();
                commandBus.Container = this.container;

                User user = new User()
                {
                    FirstName = "Test",
                    LastName = "User",
                    Email = "testuser@gmail.com",
                    DateCreated = DateTime.Now,
                    RoleId = 1,
                    Activated = true
                };

                UserRegisterCommand command = new UserRegisterCommand(user, "TEST");
                IEnumerable<ValidationResult> validations = commandBus.Validate(command);
                foreach (var val in validations)
                {
                    Assert.IsNull(val, "Error: User creation did not validate " + val.Message);
                }
                ICommandResult result = commandBus.Submit(command);
                Assert.IsNotNull(result, "Error: User was not created by CommandBus");
                Assert.IsTrue(result.Success, "Error: User was not created by CommandBus");
            }
        }

        [TestMethod()]
        public void UserGetTest()
        {
            using (var lifetime = container.BeginLifetimeScope())
            {
                IUserRepository userRepository = lifetime.Resolve<IUserRepository>();
                DefaultCommandBus commandBus = lifetime.Resolve<DefaultCommandBus>();
                commandBus.Container = this.container;

                User user = userRepository.Get(c => c.Email == "testuser@gmail.com");
                Assert.IsNotNull(user, "Error: User was not found");
            }
        }

        [TestMethod()]
        public void UserChangePasswordTest()
        {
            using (var lifetime = container.BeginLifetimeScope())
            {
                IUserRepository userRepository = lifetime.Resolve<IUserRepository>();
                DefaultCommandBus commandBus = lifetime.Resolve<DefaultCommandBus>();
                commandBus.Container = this.container;

                User user = userRepository.Get(c => c.Email == "testuser@gmail.com");
                Assert.IsNotNull(user, "Error: User was not found");

                ChangePasswordCommand command = new ChangePasswordCommand();
                command.UserId = user.UserId;
                command.OldPassword = "TEST";
                command.NewPassword = "TEST2";

                IEnumerable<ValidationResult> validations = commandBus.Validate(command);
                foreach (var val in validations)
                {
                    Assert.IsNull(val, "Error: User password change did not validate " + val.Message);
                }
                ICommandResult result = commandBus.Submit(command);
                Assert.IsNotNull(result, "Error: User password change did not work");
                Assert.IsTrue(result.Success, "Error: User password change did not work");
            }
        }
    }
}
