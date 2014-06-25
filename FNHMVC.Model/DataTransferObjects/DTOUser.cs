using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using FNHMVC.Core.Common;

namespace FNHMVC.Model
{
    [DataContract(IsReference = true)]
    public class DTOUser
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string PasswordHash { get; set; }
        [DataMember]
        public DateTime DateCreated { get; set; }
        [DataMember]
        public DateTime? LastLoginTime { get; set; }
        [DataMember]
        public bool Activated { get; set; }
        [DataMember]
        public int RoleId { get; set; }
    }
}
