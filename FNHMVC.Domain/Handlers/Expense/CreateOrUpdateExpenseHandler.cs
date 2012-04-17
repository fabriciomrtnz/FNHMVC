using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FNHMVC.CommandProcessor.Command;
using FNHMVC.Domain.Commands;
using FNHMVC.Data.Repositories;
using FNHMVC.Data.Infrastructure;
using FNHMVC.Model;

namespace FNHMVC.Domain.Handlers
{
    public class CreateOrUpdateExpenseHandler : ICommandHandler<CreateOrUpdateExpenseCommand>
    {
        private readonly IExpenseRepository expenseRepository;
        private readonly IUnitOfWork unitOfWork;

        public CreateOrUpdateExpenseHandler(IExpenseRepository expenseRepository, IUnitOfWork unitOfWork)
        {
            this.expenseRepository = expenseRepository;
            this.unitOfWork = unitOfWork;
        }

        public ICommandResult Execute(CreateOrUpdateExpenseCommand command)
        {
            var expense = new Expense
            {
                ExpenseId = command.ExpenseId,
                Category = command.Category,
                Amount = command.Amount,
                Date = command.Date,
                TransactionDesc = command.Transaction
            };

            if (expense.ExpenseId == 0)
                expenseRepository.Add(expense);
            else
                expenseRepository.Update(expense);
            unitOfWork.Commit();
            return new CommandResult(true);
        }
    }
}
