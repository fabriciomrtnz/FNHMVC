using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Linq.Expressions;
using FNHMVC.Data;
using NHibernate;
using NHibernate.Linq;


namespace FNHMVC.Data.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class
    {
        private readonly ISession session;

        public RepositoryBase(ISession session)
        {
            this.session = session;
            this.session.FlushMode = FlushMode.Auto;
        }

        public bool Add(T entity)
        {
            this.session.Save(entity);
            return true;
        }

        public bool Add(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                this.session.Save(item);
            }
            return true;
        }

        public bool Update(T entity)
        {
            this.session.Update(entity);
            return true;
        }

        public bool Delete(T entity)
        {
            this.session.Delete(entity);
            return true;
        }

        public bool Delete(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                this.Delete(entity);
            }
            return true;
        }

        public T GetById(int id)
        {
            return this.session.Get<T>(id);
        }

        public T GetById(string id)
        {
            return this.session.Get<T>(id);
        }

        public T GetById(long id)
        {
            return this.session.Get<T>(id);
        }

        public IQueryable<T> GetAll()
        {
            return this.session.Query<T>();
        }

        public T Get(Expression<System.Func<T, bool>> expression)
        {
            return GetMany(expression).SingleOrDefault();
        }

        public IQueryable<T> GetMany(Expression<System.Func<T, bool>> expression)
        {
            return GetAll().Where(expression).AsQueryable();
        }

    }
}
