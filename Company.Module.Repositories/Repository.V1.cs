//using System;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.Diagnostics.Contracts;
//using System.Linq;
//using System.Linq.Expressions;

//using Company.Module.Domain.Interfaces;

//namespace Company.Module.Repositories
//{
//    public class Repository<T> : IRepository<T> where T : class, IIdentifiable
//    {
//        //// ----------------------------------------------------------------------------------------------------------

//        private readonly IDbSet<T> dbSet;

//        //// ----------------------------------------------------------------------------------------------------------

//        public Repository(IObjectContextAdapter objectContextAdapter)
//        {
//            Contract.Requires<ArgumentNullException>(objectContextAdapter != null);

//            this.dbSet = ((DbContext)objectContextAdapter).Set<T>();
//        }

//        //// ----------------------------------------------------------------------------------------------------------

//        protected IQueryable<T> Query
//        {
//            get
//            {
//                return this.dbSet;
//            }
//        }

//        //// ----------------------------------------------------------------------------------------------------------

//        public IQueryable<T> AllEager(params Expression<Func<T, object>>[] includes)
//        {
//            var query = this.Query;

//            foreach (var include in includes)
//                query.Include(include);

//            return query;
//        }

//        //// ----------------------------------------------------------------------------------------------------------

//        public T Find(int id)
//        {
//            return this.dbSet.Find(id);
//        }

//        //// ----------------------------------------------------------------------------------------------------------

//        public void Insert(T item)
//        {
//            this.dbSet.Add(item);
//        }

//        //// ----------------------------------------------------------------------------------------------------------

//        public void Update(T item)
//        {
//            this.dbSet.Attach(item);
//            this.context.Entry(item).State = EntityState.Modified;
//        }

//        //// ----------------------------------------------------------------------------------------------------------

//        public void Delete(int id)
//        {
//            var item = this.Find(id);
//            this.dbSet.Remove(item);
//        }

//        //// ----------------------------------------------------------------------------------------------------------
//    }

//}
