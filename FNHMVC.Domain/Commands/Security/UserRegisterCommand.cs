using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FNHMVC.CommandProcessor.Command;
using FNHMVC.Model;

namespace FNHMVC.Domain.Commands
{
    public class UserRegisterCommand : ICommand
    {
        public UserRegisterCommand() { }

        public UserRegisterCommand(User user, string password)
        {
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Email = user.Email;
            this.Password = password;
            this.RoleId = user.RoleId;
            this.Activated = user.Activated;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public bool Activated { get; set; }
    }
}
