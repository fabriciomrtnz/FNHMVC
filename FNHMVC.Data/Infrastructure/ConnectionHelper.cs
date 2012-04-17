using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FNHMVC.Data.Mappings;

namespace FNHMVC.Data.Infrastructure
{
    static public class ConnectionHelper
    {
        public static ISessionFactory BuildSessionFactory()
        {
            return GetConfiguration().BuildSessionFactory();
        }

        public static FluentConfiguration GetConfiguration()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("FNHMVCContainer")))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>());
        }
    }
}
