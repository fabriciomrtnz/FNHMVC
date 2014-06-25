using System.Collections.Generic;
using FNHMVC.CommandProcessor.Command;
using FNHMVC.Model.Commands;
using FNHMVC.Core.Common;
using FNHMVC.Data.Repositories;
using FNHMVC.Data.Infrastructure;
using FNHMVC.Model;

namespace FNHMVC.Domain.Handlers
{
    public class CanAddCategory : IValidationHandler<CreateOrUpdateCategoryCommand>
    {
        private readonly ICategoryRepository categoryRepository;

        public CanAddCategory(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            this.categoryRepository = categoryRepository;
        }

        public IEnumerable<ValidationResult> Validate(CreateOrUpdateCategoryCommand command)
        {
            Category isCategoryExists = null;
            if (command.CategoryId == 0)
                isCategoryExists = categoryRepository.Get(c => c.Name == command.Name);
            else
                isCategoryExists = categoryRepository.Get(c => c.Name == command.Name && c.CategoryId != command.CategoryId);

            if (isCategoryExists != null)
            {
                yield return new ValidationResult("Name", Resources.CategoryExists);
            }
        }
    }
}
