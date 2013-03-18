using FNHMVC.CommandProcessor.Command;

namespace FNHMVC.Domain.Commands
{
    public class DeleteExpenseCommand : ICommand
    {
        public int ExpenseId { get; set; }
    }
}
