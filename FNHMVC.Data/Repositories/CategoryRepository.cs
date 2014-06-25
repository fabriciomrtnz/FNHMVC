using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FNHMVC.Data.Infrastructure;
using FNHMVC.Model;
using NHibernate;

namespace FNHMVC.Data.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(ISession session)
            : base(session)
        {
        }
    }

    public interface ICategoryRepository : IRepository<Category>
    {
    }
}
