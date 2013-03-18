using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using FNHMVC.Model;

namespace FNHMVC.Web.ViewModels
{
    public class CategoryFormModel
    {
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryFormModel() {}
        public CategoryFormModel(Category category)
        {
            this.CategoryId = category.CategoryId;
            this.Name = category.Name;
            this.Description = category.Description;
        }
    }
}