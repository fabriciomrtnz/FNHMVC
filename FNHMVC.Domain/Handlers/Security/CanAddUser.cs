using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FNHMVC.Model.Commands;
using FNHMVC.CommandProcessor.Command;
using FNHMVC.Data.Infrastructure;
using FNHMVC.Data.Repositories;
using FNHMVC.Core.Common;
using FNHMVC.Model;

namespace FNHMVC.Domain.Handlers
{
    public class CanAddUser : IValidationHandler<UserRegisterCommand>
    {
        private readonly IUserRepository userRepository;

        public CanAddUser(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IEnumerable<ValidationResult> Validate(UserRegisterCommand command)
        {
            User isUserExists = null;
            isUserExists = userRepository.Get(c => c.Email == command.Email);

            if (isUserExists != null)
            {
                yield return new ValidationResult("EMail", Resources.UserExists);
            }
        }
    }
}
