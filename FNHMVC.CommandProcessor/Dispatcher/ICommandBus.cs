using System;
using FNHMVC.CommandProcessor.Command;
using FNHMVC.Core.Common;
using System.Collections.Generic;
using Autofac;

namespace FNHMVC.CommandProcessor.Dispatcher
{
    public interface ICommandBus
    {
        ICommandResult Submit<TCommand>(TCommand command) where TCommand : ICommand;
        IEnumerable<ValidationResult> Validate<TCommand>(TCommand command) where TCommand : ICommand;
        ICommandResult Submit<TCommand, TCommandHandler>(TCommand command, TCommandHandler commandHandler)
            where TCommand : ICommand
            where TCommandHandler : ICommandHandler<TCommand>;
        IEnumerable<ValidationResult> Validate<TCommand, TValidationHandler>(TCommand command, TValidationHandler validationHandler)
            where TCommand : ICommand
            where TValidationHandler : IValidationHandler<TCommand>;
        void AsyncRun<T>(Action<T> action);
    }
}

