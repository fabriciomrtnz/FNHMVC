using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FNHMVC.Data.Infrastructure;
using FNHMVC.Model;
using NHibernate;

namespace FNHMVC.Data.Repositories
{
    public class ExpenseRepository : RepositoryBase<Expense>, IExpenseRepository
    {
        public ExpenseRepository(ISession session)
            : base(session)
        {
        }
    }

    public interface IExpenseRepository : IRepository<Expense>
    {
    }
}
