using FNHMVC.CommandProcessor.Command;

namespace FNHMVC.Domain.Commands
{
    public class DeleteCategoryCommand : ICommand
    {
        public int CategoryId { get; set; }
    }
}
