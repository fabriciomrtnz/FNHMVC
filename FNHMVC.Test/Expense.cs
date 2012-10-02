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
    public class ExpenseTest
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

        public ExpenseTest()
        {
            var builder = new ContainerBuilder();

            builder.Register(c => FNHMVC.Data.Infrastructure.ConnectionHelper.BuildSessionFactory()).As<ISessionFactory>().SingleInstance();
            builder.Register(c => c.Resolve<ISessionFactory>().OpenSession()).InstancePerLifetimeScope();

            builder.RegisterType<DefaultCommandBus>().SingleInstance();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(IRepository<Expense>).Assembly).Where(t => t.Name.EndsWith("ExpenseRepository")).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(IRepository<Category>).Assembly).Where(t => t.Name.EndsWith("CategoryRepository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            var services = Assembly.Load("FNHMVC.Domain");
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(IValidationHandler<>)).InstancePerLifetimeScope();

            container = builder.Build();
        }

        [TestMethod()]
        public void ExpenseCreateTest()
        {
            using (var lifetime = container.BeginLifetimeScope())
            {
                DefaultCommandBus commandBus = lifetime.Resolve<DefaultCommandBus>();
                ICategoryRepository categoryRepository = lifetime.Resolve<ICategoryRepository>();

                Category category = categoryRepository.Get(x => x.Name == "Test Category");
                Assert.IsNotNull(category, "Error: Category not found");

                CreateOrUpdateExpenseCommand command = new CreateOrUpdateExpenseCommand();
                command.Amount = 120;
                command.Date = DateTime.Now;
                command.Category = category;
                command.Transaction = "Test transaction.";

                ICommandHandler<CreateOrUpdateExpenseCommand> commnadHandler = lifetime.Resolve<ICommandHandler<CreateOrUpdateExpenseCommand>>();
                ICommandResult result = commandBus.Submit(command, commnadHandler);
                Assert.IsNotNull(result, "Error: Tipo Via Was Not Created by CommandBus");
                Assert.IsTrue(result.Success, "Error: Tipo Via Was Not Created by CommandBus");
            }
        }

        [TestMethod()]
        public void ExpenseGetTest()
        {
            using (var lifetime = container.BeginLifetimeScope())
            {
                IExpenseRepository expenseRepository = lifetime.Resolve<IExpenseRepository>();

                Expense expense = expenseRepository.Get(c => c.Amount == 120);
                Assert.IsNotNull(expense, "Error: Expense was not found");
            }
        }

        [TestMethod()]
        public void ExpenseUpdateTest()
        {
            using (var lifetime = container.BeginLifetimeScope())
            {
                IExpenseRepository expenseRepository = lifetime.Resolve<IExpenseRepository>();
                DefaultCommandBus commandBus = lifetime.Resolve<DefaultCommandBus>();

                Expense expense = expenseRepository.Get(c => c.Amount == 120);
                Assert.IsNotNull(expense, "Error: Expense was not found");

                CreateOrUpdateExpenseCommand command = new CreateOrUpdateExpenseCommand();
                command.Amount = 150;
                command.Date = expense.Date;
                command.Category = expense.Category;
                command.Transaction = expense.TransactionDesc;

                ICommandHandler<CreateOrUpdateExpenseCommand> commnadHandler = lifetime.Resolve<ICommandHandler<CreateOrUpdateExpenseCommand>>();
                ICommandResult result = commandBus.Submit(command, commnadHandler);
                Assert.IsNotNull(result, "Error: Expense was not updated");
                Assert.IsTrue(result.Success, "Error: Expense was not updated");
            }

        }

        [TestMethod()]
        public void ExpenseDeleteTest()
        {
            using (var lifetime = container.BeginLifetimeScope())
            {
                IExpenseRepository expenseRepository = lifetime.Resolve<IExpenseRepository>();
                DefaultCommandBus commandBus = lifetime.Resolve<DefaultCommandBus>();

                Expense expense = expenseRepository.Get(c => c.Amount == 150);
                Assert.IsNotNull(expense, "Error: Expense was not found");

                DeleteExpenseCommand command = new DeleteExpenseCommand() { ExpenseId = expense.ExpenseId };
                ICommandHandler<DeleteExpenseCommand> commnadHandler = lifetime.Resolve<ICommandHandler<DeleteExpenseCommand>>();
                ICommandResult result = commandBus.Submit(command, commnadHandler);
                Assert.IsNotNull(result, "Error: Expense was not deleted");
                Assert.IsTrue(result.Success, "Error: Expense was not deleted");
            }
        }
    }
}
