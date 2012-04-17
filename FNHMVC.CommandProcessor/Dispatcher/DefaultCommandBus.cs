using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Web.Mvc;
using FNHMVC.CommandProcessor.Command;
using FNHMVC.Core.Common;
using Autofac;
using Autofac.Integration.Mvc;

namespace FNHMVC.CommandProcessor.Dispatcher
{
    public class DefaultCommandBus : ICommandBus
    {
        public IContainer Container { get; set; }

        public ICommandResult Submit<TCommand>(TCommand command) where TCommand: ICommand
        {    
            ICommandHandler<TCommand> handler = null;
    
            if (Container == null) handler = DependencyResolver.Current.GetService<ICommandHandler<TCommand>>();
            else handler = this.Container.Resolve<ICommandHandler<TCommand>>();

            if (!((handler != null) && handler is ICommandHandler<TCommand>))
            {
                throw new CommandHandlerNotFoundException(typeof(TCommand));
            }

            return handler.Execute(command);
        }

        public IEnumerable<ValidationResult> Validate<TCommand>(TCommand command) where TCommand : ICommand
        {
            IValidationHandler<TCommand> handler = null;

            if (Container == null) handler = DependencyResolver.Current.GetService<IValidationHandler<TCommand>>();
            else handler = this.Container.Resolve<IValidationHandler<TCommand>>();

            if (!((handler != null) && handler is IValidationHandler<TCommand>))
            {
                throw new ValidationHandlerNotFoundException(typeof(TCommand));
            }

            return handler.Validate(command);
        }

        public void AsyncRun<T>(Action<T> action)
        {
            Task.Factory.StartNew(delegate
            {
                using (var container = AutofacDependencyResolver.Current.ApplicationContainer.BeginLifetimeScope("httpRequest"))
                {
                    var service = container.Resolve<T>();
                    action(service);
                }
            });
        }
    }
}

