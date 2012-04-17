using System;
using FNHMVC.CommandProcessor.Command;
using FNHMVC.Core.Common;
using System.Collections.Generic;
using Autofac;

namespace FNHMVC.CommandProcessor.Dispatcher
{
    public interface ICommandBus
    {
        ICommandResult Submit<TCommand>(TCommand command) where TCommand: ICommand;
        IEnumerable<ValidationResult> Validate<TCommand>(TCommand command) where TCommand : ICommand;
        void AsyncRun<T>(Action<T> action);
    }
}

