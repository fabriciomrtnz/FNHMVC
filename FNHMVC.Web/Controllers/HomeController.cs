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
    public class HomeController : Controller
    {
        private readonly ICommandBus commandBus;

        public HomeController(ICommandBus commandBus)
        {
            this.commandBus = commandBus;
        }

        public ActionResult AsyncCall()
        {
            //Async Runner Example
            //commandBus.AsyncRun<FNHMVC.Web.Core.Common.WCComunicator>(c => c.DoSomething());
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to FNHMVC!";
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
