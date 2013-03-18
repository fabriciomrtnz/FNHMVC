using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Security.Principal;

namespace FNHMVC.Web.Core.Models
{ 
    [Serializable]
    public class FNHMVCUser : IIdentity
    {
        public FNHMVCUser(){}

        public FNHMVCUser(string name, string displayName, int userId)
        {
            this.Name = name;
            this.DisplayName = displayName;
            this.UserId = userId;
        }

        public FNHMVCUser(string name, string displayName, int userId,string roleName)
        {
            this.Name = name;
            this.DisplayName = displayName;
            this.UserId = userId;
            this.RoleName = roleName;
        }

        public FNHMVCUser(string name, UserInfo userInfo)
            : this(name, userInfo.DisplayName, userInfo.UserId,userInfo.RoleName)
        {
            if (userInfo == null) throw new ArgumentNullException("userInfo");
            this.UserId = userInfo.UserId;
        }

        public FNHMVCUser(FormsAuthenticationTicket ticket)
            : this(ticket.Name, UserInfo.FromString(ticket.UserData))
        {
            if (ticket == null) throw new ArgumentNullException("ticket");
        }

        public string Name { get; private set; }

        public string AuthenticationType
        {
            get { return "FNHMVCForms"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        public string DisplayName { get; private set; }
        public string RoleName { get; private set; }
        public int UserId { get; private set; }
    }
}
