using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using FNHMVC.Web.ViewModels;
using FNHMVC.Model.Commands;
using FNHMVC.Web.Core.Models;
using FNHMVC.CommandProcessor.Dispatcher;
using FNHMVC.Data.Repositories;
using FNHMVC.Core.Common;
using FNHMVC.Web.Core.Extensions;
using FNHMVC.Web.Core.Authentication;
using FNHMVC.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using System.Threading.Tasks;

namespace FNHMVC.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICommandBus commandBus;
        private readonly IUserRepository userRepository;
        private readonly ApplicationUserManager<FNHMVCUser, int> userManager;

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public AccountController(ICommandBus commandBus, IUserRepository userRepository, ApplicationUserManager<FNHMVCUser, int> userManager)
        {
            this.commandBus = commandBus;
            this.userRepository = userRepository;
            this.userManager = userManager;
        }

        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LogOnFormModel form, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = this.userRepository.Get(x => x.Email.ToUpper() == form.Email.ToUpper() && Md5Encrypt.Md5EncryptPassword(form.Password) == x.PasswordHash);
                if (user != null)
                {
                    FNHMVCUser appUser = new FNHMVCUser(user);
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true, RedirectUri = returnUrl }, await appUser.GenerateUserIdentityAsync(userManager));
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            return View(form);
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Register(UserFormModel model)
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
                        var user = this.userRepository.Get(x => x.Email.ToUpper() == command.Email.ToUpper() && Md5Encrypt.Md5EncryptPassword(command.Password) == x.PasswordHash);
                        FNHMVCUser appUser = new FNHMVCUser()
                        {
                            Id = user.UserId,
                            RoleName = Enum.GetName(typeof(UserRoles), user.RoleId),
                            UserName = user.DisplayName
                        };
                        AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                        AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, await appUser.GenerateUserIdentityAsync(userManager));
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "An unknown error occurred.");
                    }
                }
                return View(model);
            }

            return View(model);
        }

        private IEnumerable<string> GetErrorsFromModelState()
        {
            return ModelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
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
                var user = HttpContext.User;
                var command = new ChangePasswordCommand
                {
                    UserId = int.Parse(user.Identity.GetUserId()),
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
             
            return View(form);
        }

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

    }
}
