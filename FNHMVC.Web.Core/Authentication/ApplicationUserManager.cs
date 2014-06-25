using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using FNHMVC.Web.Core.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Security.Claims;

namespace FNHMVC.Web.Core.Authentication
{
    public class ApplicationUserManager<TUser, TKey> : UserManager<TUser, TKey> where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
    {
        public ApplicationUserManager(IUserRoleStore<TUser, TKey> store): base(store)
        {
        }

        //public static ApplicationUserManager<TUser, TKey> Create(IdentityFactoryOptions<ApplicationUserManager<TUser, TKey>> options, IOwinContext context)
        //{
        //    var userStore = DependencyResolver.Current.GetService<DefaultUserRoleStore<TUser, TKey>>();

        //    ApplicationUserManager<TUser, TKey> manager = new ApplicationUserManager<TUser, TKey>(userStore);

        //    // Configure validation logic for usernames
        //    manager.UserValidator = new UserValidator<TUser, TKey>(manager)
        //    {
        //        AllowOnlyAlphanumericUserNames = false,
        //        RequireUniqueEmail = true
        //    };

        //    // Configure validation logic for passwords
        //    manager.PasswordValidator = new PasswordValidator
        //    {
        //        RequiredLength = 6,
        //        RequireNonLetterOrDigit = true,
        //        RequireDigit = true,
        //        RequireLowercase = true,
        //        RequireUppercase = true,
        //    };

        //    // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
        //    // You can write your own provider and plug in here.
        //    manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<TUser, TKey>
        //    {
        //        MessageFormat = "Your security code is: {0}"
        //    });

        //    manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<TUser, TKey>
        //    {
        //        Subject = "Security Code",
        //        BodyFormat = "Your security code is: {0}"
        //    });

        //    manager.EmailService = new EmailService();
        //    manager.SmsService = new SmsService();

        //    var dataProtectionProvider = options.DataProtectionProvider;
        //    if (dataProtectionProvider != null)
        //    {
        //        manager.UserTokenProvider = new DataProtectorTokenProvider<TUser, TKey>(dataProtectionProvider.Create("ASP.NET Identity"));
        //    }

        //    return manager;
        //}

  
    }

    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your sms service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
