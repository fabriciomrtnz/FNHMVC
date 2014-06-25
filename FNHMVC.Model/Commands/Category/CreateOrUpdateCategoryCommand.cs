using System;
using FNHMVC.CommandProcessor.Command;
using FNHMVC.Model;
using System.Collections.Generic;

namespace FNHMVC.Model.Commands
{
    public class CreateOrUpdateCategoryCommand : ICommand
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
