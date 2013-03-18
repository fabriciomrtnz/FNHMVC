using System;
using FNHMVC.CommandProcessor.Command;
using FNHMVC.Model;
using System.Collections.Generic;

namespace FNHMVC.Domain.Commands
{
    public class CreateOrUpdateCategoryCommand : ICommand
    {
        public CreateOrUpdateCategoryCommand(int CategoryId, string name, string description)
        {
            this.CategoryId = CategoryId;
            this.Name = name;
            this.Description = description;
        }

        public CreateOrUpdateCategoryCommand(Category category)
        {
            this.CategoryId = category.CategoryId;
            this.Name = category.Name;
            this.Description = category.Description;
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
