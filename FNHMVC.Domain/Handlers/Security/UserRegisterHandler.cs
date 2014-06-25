using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FNHMVC.CommandProcessor.Command;
using FNHMVC.Model.Commands;
using FNHMVC.Data.Repositories;
using FNHMVC.Data.Infrastructure;
using FNHMVC.Model;
using AutoMapper;

namespace FNHMVC.Domain.Handlers.Security
{
    public class UserRegisterHandler : ICommandHandler<UserRegisterCommand>
    {
        private readonly IMappingEngine mapper;
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserRegisterHandler(IMappingEngine mapper, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        public ICommandResult Execute(UserRegisterCommand command)
        {
            var user = this.mapper.Map<User>(command);
            if (!string.IsNullOrEmpty(command.Password)) user.Password = command.Password;

            userRepository.Add(user);
            unitOfWork.Commit();
            return new CommandResult(true);
        }
    }
}
