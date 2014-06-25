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

namespace FNHMVC.Domain.Handlers
{
    public class CreateOrUpdateExpenseHandler : ICommandHandler<CreateOrUpdateExpenseCommand>
    {
        private readonly IMappingEngine mapper;
        private readonly IExpenseRepository expenseRepository;
        private readonly IUnitOfWork unitOfWork;

        public CreateOrUpdateExpenseHandler(IMappingEngine mapper, IExpenseRepository expenseRepository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.expenseRepository = expenseRepository;
            this.unitOfWork = unitOfWork;
        }

        public ICommandResult Execute(CreateOrUpdateExpenseCommand command)
        {
            var expense = this.mapper.Map<Expense>(command);

            if (expense.ExpenseId == 0)
                expenseRepository.Add(expense);
            else
                expenseRepository.Update(expense);

            unitOfWork.Commit();
            return new CommandResult(true);
        }
    }
}
