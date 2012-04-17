using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FNHMVC.Web.ViewModels
{
    public class LogOnFormModel
    {
            [Required]
            [DataType(DataType.EmailAddress)]
            [Display(Name = "EMail")]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
      
    }
}