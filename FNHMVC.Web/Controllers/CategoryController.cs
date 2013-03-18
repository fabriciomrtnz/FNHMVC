using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FNHMVC.Web.ViewModels;
using FNHMVC.Domain.Commands;
using FNHMVC.Core.Common;
using FNHMVC.Web.Core.Extensions;
using FNHMVC.CommandProcessor.Dispatcher;
using FNHMVC.Data.Repositories;
using FNHMVC.Web.Core.ActionFilters;

namespace FNHMVC.Web.Controllers
{
    [CompressResponse]
    public class CategoryController : Controller
    {
        private readonly ICommandBus commandBus;
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICommandBus commandBus, ICategoryRepository categoryRepository)
        {
            this.commandBus = commandBus;
            this.categoryRepository = categoryRepository;
        }

        public ActionResult Index()
        {
            var categories = categoryRepository.GetAll();
            return View(categories);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {

            var category = categoryRepository.GetById(id);
            var viewModel = new CategoryFormModel(category);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CategoryFormModel form)
        {
            if (ModelState.IsValid)
            {
                var command = new CreateOrUpdateCategoryCommand(form.CategoryId, form.Name, form.Description);
                IEnumerable<ValidationResult> errors = commandBus.Validate(command);
                ModelState.AddModelErrors(errors);
                if (ModelState.IsValid)
                {
                    var result = commandBus.Submit(command);
                    if (result.Success) return RedirectToAction("Index");
                }
            }

            if (form.CategoryId == 0)
                return View("Create", form);
            else
                return View("Edit", form);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var command = new DeleteCategoryCommand { CategoryId = id };
            var result = commandBus.Submit(command);
            var categories = categoryRepository.GetAll();
            return PartialView("_CategoryList", categories);
        }
    }
}
