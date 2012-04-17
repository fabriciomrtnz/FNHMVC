using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FNHMVC.Data.Infrastructure;
using FNHMVC.Model;
using NHibernate;

namespace FNHMVC.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ISession session)
            : base(session)
        {
        }
    }

    public interface IUserRepository : IRepository<User>
    {
    }
}
