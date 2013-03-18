using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FNHMVC.Web.ViewModels;
using FNHMVC.Domain.Commands;
using FNHMVC.Web.Core.Models;
using FNHMVC.CommandProcessor.Dispatcher;
using FNHMVC.Data.Repositories;
using FNHMVC.Core.Common;
using FNHMVC.Web.Core.Extensions;
using FNHMVC.Web.Core.Authentication;
using FNHMVC.Model;

namespace FNHMVC.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICommandBus commandBus;
        private readonly IUserRepository userRepository;
        private readonly IFormsAuthentication formAuthentication;

        public AccountController(ICommandBus commandBus, IUserRepository userRepository, IFormsAuthentication formAuthentication)
        {
            this.commandBus = commandBus;
            this.userRepository = userRepository;
            this.formAuthentication = formAuthentication;
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            formAuthentication.Signout();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return ContextDependentView();
        }

        private bool ValidatePassword(User user, string password)
        {
            var encoded = Md5Encrypt.Md5EncryptPassword(password);
            return user.PasswordHash.Equals(encoded);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return ContextDependentView();
        }

        [HttpPost]
        public JsonResult JsonLogin(LogOnFormModel form, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = userRepository.Get(u => u.Email == form.UserName && u.Activated == true);
                if (user != null)
                {
                    if (ValidatePassword(user, form.Password))
                    {
                        formAuthentication.SetAuthCookie(this.HttpContext, UserAuthenticationTicketBuilder.CreateAuthenticationTicket(user));

                        return Json(new { success = true, redirect = returnUrl });
                    }
                    else
                    {
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    }
                }
            }

            // If we got this far, something failed
            return Json(new { errors = GetErrorsFromModelState() });
        }
        //
        // POST: /Account/JsonRegister

        [AllowAnonymous]
        [HttpPost]
        public ActionResult JsonRegister(UserFormModel form)
        {
            if (ModelState.IsValid)
            {
                var command = new UserRegisterCommand
                {
                    FirstName = form.FirstName,
                    LastName = form.LastName,
                    Email = form.Email,
                    Password = form.Password,
                    Activated = true,
                    RoleId = (Int32)UserRoles.User
                };
                IEnumerable<ValidationResult> errors = commandBus.Validate(command);
                ModelState.AddModelErrors(errors);
                if (ModelState.IsValid)
                {
                    var result = commandBus.Submit(command);
                    if (result.Success)
                    {
                        User user = userRepository.Get(u => u.Email == form.Email);
                        formAuthentication.SetAuthCookie(this.HttpContext,
                                                          UserAuthenticationTicketBuilder.CreateAuthenticationTicket(
                                                              user));
                        return Json(new { success = true });
                    }
                    else
                    {
                        ModelState.AddModelError("", "An unknown error occurred.");
                    }
                }
                // If we got this far, something failed
                return Json(new { errors = GetErrorsFromModelState() });
            }

            // If we got this far, something failed
            return Json(new { errors = GetErrorsFromModelState() });
        }

        // POST: /Account/Register

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(UserFormModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new UserRegisterCommand
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                    Activated = true,
                    RoleId = (Int32)UserRoles.User
                };

                IEnumerable<ValidationResult> errors = commandBus.Validate(command);
                ModelState.AddModelErrors(errors);
                if (ModelState.IsValid)
                {
                    var result = commandBus.Submit(command);
                    if (result.Success)
                    {
                        User user = userRepository.Get(u => u.Email == model.Email);
                        formAuthentication.SetAuthCookie(this.HttpContext, UserAuthenticationTicketBuilder.CreateAuthenticationTicket(user));
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "An unknown error occurred.");
                    }
                }
                // If we got this far, something failed, redisplay form
                return View(model);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private IEnumerable<string> GetErrorsFromModelState()
        {
            return ModelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
        }

        private ActionResult ContextDependentView()
        {
            string actionName = ControllerContext.RouteData.GetRequiredString("action");
            if (Request.QueryString["content"] != null)
            {
                ViewBag.FormAction = "Json" + actionName;
                return PartialView();
            }
            else
            {
                ViewBag.FormAction = actionName;
                return View();
            }
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordFormModel form)
        {
            if (ModelState.IsValid)
            {
                FNHMVCUser user = HttpContext.User.GetFNHMVCUser();
                var command = new ChangePasswordCommand
                {
                    UserId = user.UserId,
                    OldPassword = form.OldPassword,
                    NewPassword = form.NewPassword
                };
                IEnumerable<ValidationResult> errors = commandBus.Validate(command);
                ModelState.AddModelErrors(errors);
                if (ModelState.IsValid)
                {
                    var result = commandBus.Submit(command);
                    if (result.Success)
                    {
                        return RedirectToAction("ChangePasswordSuccess");
                    }
                    else
                    {
                        ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                    }
                }
            }
            // If we got this far, something failed, redisplay form
            return View(form);
        }

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }
    }
}
