using FNHMVC.Data.Repositories;
using FNHMVC.Data.Infrastructure;
using FNHMVC.Model.Commands;
using FNHMVC.CommandProcessor.Command;
using FNHMVC.Model;
using AutoMapper;

namespace FNHMVC.Domain.Handlers
{
    public class CreateOrUpdateCategoryHandler : ICommandHandler<CreateOrUpdateCategoryCommand>
    {
        private readonly IMappingEngine mapper;
        private readonly ICategoryRepository categoryRepository;
        private readonly IUnitOfWork unitOfWork;

        public CreateOrUpdateCategoryHandler(IMappingEngine mapper, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.categoryRepository = categoryRepository;
            this.unitOfWork = unitOfWork;
        }

        public ICommandResult Execute(CreateOrUpdateCategoryCommand command)
        {
            var category = this.mapper.Map<Category>(command);

            if (category.CategoryId == 0)
                categoryRepository.Add(category);
            else
                categoryRepository.Update(category);

            unitOfWork.Commit();
            return new CommandResult(true);
        }
    }
}
