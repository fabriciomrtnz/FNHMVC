using FNHMVC.CommandProcessor.Command;

namespace FNHMVC.Model.Commands
{
    public class DeleteExpenseCommand : ICommand
    {
        public int ExpenseId { get; set; }
    }
}
