using FNHMVC.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using Autofac;
using FNHMVC.CommandProcessor.Dispatcher;
using FNHMVC.Data.Repositories;
using FNHMVC.Data.Infrastructure;
using FNHMVC.Model.Commands;
using FNHMVC.Core.Common;
using FNHMVC.CommandProcessor.Command;
using System.Reflection;
using NHibernate;
using FluentNHibernate.Cfg;
using AutoMapper;

namespace FNHMVC.Test
{

    [TestClass()]
    public class CategoryTest
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

        public CategoryTest()
        {
            var builder = new ContainerBuilder();

            builder.Register(c => FNHMVC.Data.Infrastructure.ConnectionHelper.BuildSessionFactory("FNHMVCContainer")).As<ISessionFactory>().SingleInstance();
            builder.Register(c => c.Resolve<ISessionFactory>().OpenSession()).InstancePerLifetimeScope();

            builder.RegisterType<DefaultCommandBus>().SingleInstance();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(IRepository<Category>).Assembly).Where(t => t.Name.EndsWith("CategoryRepository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            var services = Assembly.Load("FNHMVC.Domain");
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(IValidationHandler<>)).InstancePerLifetimeScope();

            builder.Register(c => new ConfigurationStore(new TypeMapFactory(), AutoMapper.Mappers.MapperRegistry.Mappers)).AsImplementedInterfaces().SingleInstance();
            builder.Register(c => Mapper.Engine).As<IMappingEngine>().SingleInstance();
            builder.RegisterType<TypeMapFactory>().As<ITypeMapFactory>().SingleInstance();

            container = builder.Build();

            AutoMapperConfiguration.Configure();
        }

        [TestMethod()]
        public void CategoryCreateTest()
        {
            using (var lifetime = container.BeginLifetimeScope())
            {
                ICategoryRepository categoryRepository = lifetime.Resolve<ICategoryRepository>();
                DefaultCommandBus commandBus = lifetime.Resolve<DefaultCommandBus>();
                IMappingEngine mapper = lifetime.Resolve<IMappingEngine>();

                Category category = new Category()
                {
                    Name = "Test Category",
                    Description = "This is a test category"
                };

                CreateOrUpdateCategoryCommand command = mapper.Map<CreateOrUpdateCategoryCommand>(category);
                IValidationHandler<CreateOrUpdateCategoryCommand> validationHandler = lifetime.Resolve<IValidationHandler<CreateOrUpdateCategoryCommand>>();
                IEnumerable<ValidationResult> validations = commandBus.Validate(command, validationHandler);
                foreach (var val in validations)
                {
                    Assert.IsNull(val, "Error: Category creation did not validate " + val.Message);
                }
                ICommandHandler<CreateOrUpdateCategoryCommand> commnadHandler = lifetime.Resolve<ICommandHandler<CreateOrUpdateCategoryCommand>>();
                ICommandResult result = commandBus.Submit(command, commnadHandler);
                Assert.IsNotNull(result, "Error: Category was not created by commandBus");
                Assert.IsTrue(result.Success, "Error: Category was not created by commandBus");
            }
        }

        [TestMethod()]
        public void CategoryGetTest()
        {
            using (var lifetime = container.BeginLifetimeScope())
            {
                ICategoryRepository categoryRepository = lifetime.Resolve<ICategoryRepository>();

                Category category = categoryRepository.Get(c => c.Name == "Test Category");
                Assert.IsNotNull(category, "Error: Category was now found.");
            }
        }

        [TestMethod()]
        public void CategoryUpdateTest()
        {
            Category category;

            using (var lifetime = container.BeginLifetimeScope())
            {
                ICategoryRepository categoryRepository = lifetime.Resolve<ICategoryRepository>();
                DefaultCommandBus commandBus = lifetime.Resolve<DefaultCommandBus>();
                IMappingEngine mapper = lifetime.Resolve<IMappingEngine>();

                category = categoryRepository.Get(c => c.Name == "Test Category");
                Assert.IsNotNull(category, "Error: Category was now found.");

                category.Name = "Updated Test Category";

                CreateOrUpdateCategoryCommand command = mapper.Map<CreateOrUpdateCategoryCommand>(category);
                IValidationHandler<CreateOrUpdateCategoryCommand> validationHandler = lifetime.Resolve<IValidationHandler<CreateOrUpdateCategoryCommand>>();
                IEnumerable<ValidationResult> validations = commandBus.Validate(command, validationHandler);

                foreach (var val in validations)
                {
                    Assert.IsNull(val, "Error: Category creation did not validate " + val.Message);
                }
            }

            using (var lifetime = container.BeginLifetimeScope())
            {
                DefaultCommandBus commandBus = lifetime.Resolve<DefaultCommandBus>();
                IMappingEngine mapper = lifetime.Resolve<IMappingEngine>();

                CreateOrUpdateCategoryCommand command = mapper.Map<CreateOrUpdateCategoryCommand>(category);

                ICommandHandler<CreateOrUpdateCategoryCommand> commnadHandler = lifetime.Resolve<ICommandHandler<CreateOrUpdateCategoryCommand>>();
                ICommandResult result = commandBus.Submit(command, commnadHandler);
                Assert.IsNotNull(result, "Error: Category was not updated by CommandBus");
                Assert.IsTrue(result.Success, "Error: Provincia was not updated by CommandBus");
            }
        }

        [TestMethod()]
        public void CategoryDeleteTest()
        {
            using (var lifetime = container.BeginLifetimeScope())
            {
                ICategoryRepository categoryRepository = lifetime.Resolve<ICategoryRepository>();
                DefaultCommandBus commandBus = lifetime.Resolve<DefaultCommandBus>();
                IMappingEngine mapper = lifetime.Resolve<IMappingEngine>();

                Category category = categoryRepository.Get(c => c.Name == "Updated Test Category");
                Assert.IsNotNull(category, "Error: Category was now found.");

                DeleteCategoryCommand command = mapper.Map<DeleteCategoryCommand>(category);
                ICommandHandler<DeleteCategoryCommand> commnadHandler = lifetime.Resolve<ICommandHandler<DeleteCategoryCommand>>();
                ICommandResult result = commandBus.Submit(command, commnadHandler);
                Assert.IsNotNull(result, "Error: Category was not deleted by CommandBus");
                Assert.IsTrue(result.Success, "Error: Category was not deleted by CommandBus");
            }
        }
    }
}
