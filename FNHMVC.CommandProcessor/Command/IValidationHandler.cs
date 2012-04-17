using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FNHMVC.CommandProcessor;
using FNHMVC.Core.Common;

namespace FNHMVC.CommandProcessor.Command
{
    public interface IValidationHandler<in TCommand> where TCommand : ICommand
    {
        IEnumerable<ValidationResult>  Validate(TCommand command);
    }
}
