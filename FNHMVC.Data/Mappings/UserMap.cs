using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using FNHMVC.Model;
using FluentNHibernate.Mapping;

namespace FNHMVC.Data.Mappings
{
    public partial class UserMap: ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.UserId).GeneratedBy.Identity();
            Map(x => x.Activated).Not.Nullable();
            Map(x => x.DateCreated).Not.Nullable();
            Map(x => x.Email).Length(256).Unique().Not.Nullable();
            Map(x => x.FirstName).Length(100).Not.Nullable();
            Map(x => x.LastLoginTime).Nullable();
            Map(x => x.LastName).Length(100).Not.Nullable();
            Map(x => x.PasswordHash).Length(256).Nullable();
            Map(x => x.RoleId).Not.Nullable();
        }
    }
}
