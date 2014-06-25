using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FNHMVC.CommandProcessor.Command;

namespace FNHMVC.Model.Commands
{
    public class ChangePasswordCommand : ICommand
    {
        public long UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
