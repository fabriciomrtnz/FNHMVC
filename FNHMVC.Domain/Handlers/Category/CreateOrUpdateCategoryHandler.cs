using FNHMVC.Data.Repositories;
using FNHMVC.Data.Infrastructure;
using FNHMVC.Domain.Commands;
using FNHMVC.CommandProcessor.Command;
using FNHMVC.Model;

namespace FNHMVC.Domain.Handlers
{
    public class CreateOrUpdateCategoryHandler : ICommandHandler<CreateOrUpdateCategoryCommand>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IUnitOfWork unitOfWork;

        public CreateOrUpdateCategoryHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            this.categoryRepository = categoryRepository;
            this.unitOfWork = unitOfWork;
        }

        public ICommandResult Execute(CreateOrUpdateCategoryCommand command)
        {
            var category = new Category
            {
                CategoryId = command.CategoryId,
                Name = command.Name,
                Description = command.Description
            };

            if (category.CategoryId == 0)
                categoryRepository.Add(category);
            else
                categoryRepository.Update(category);

            unitOfWork.Commit();
            return new CommandResult(true);
        }
    }
}
