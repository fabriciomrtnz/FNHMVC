using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FNHMVC.Data.Repositories;
using FNHMVC.Data.Infrastructure;
using FNHMVC.CommandProcessor.Command;
using FNHMVC.Model;
using FNHMVC.Domain.Commands;

namespace FNHMVC.Domain.Handlers.Security
{
    public class ChangePasswordHandler : ICommandHandler<ChangePasswordCommand>
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;

        public ChangePasswordHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        public ICommandResult Execute(ChangePasswordCommand command)
        {
            var user = userRepository.GetById(command.UserId);
            user.Password = command.NewPassword;
            userRepository.Update(user);
            unitOfWork.Commit();
            return new CommandResult(true);
        }
    }
}

