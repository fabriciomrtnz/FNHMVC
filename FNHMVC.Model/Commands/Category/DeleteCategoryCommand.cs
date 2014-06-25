using FNHMVC.CommandProcessor.Command;

namespace FNHMVC.Model.Commands
{
    public class DeleteCategoryCommand : ICommand
    {
        public int CategoryId { get; set; }
    }
}
