using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using System.Data;

namespace FNHMVC.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISession Session;
        private readonly ITransaction transaction;

        public UnitOfWork(ISession session)
        {
            this.Session = session;
            this.Session.FlushMode = FlushMode.Auto;
            this.transaction = Session.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        //~UnitOfWork()
        //{
        //    this.Dispose();
        //}

        public void Dispose()
        {
            this.Session.Close();
        }

        public void Commit()
        {
            if (!this.transaction.IsActive)
            {
                throw new InvalidOperationException("No active transation");
            }
            this.transaction.Commit();
        }

        public void Rollback()
        {
            if (this.transaction.IsActive)
            {
                this.transaction.Rollback();
            }
        }
    }
}
