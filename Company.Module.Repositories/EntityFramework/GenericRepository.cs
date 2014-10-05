using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Company.Module.Domain.Interfaces;

namespace Company.Module.Repositories.EntityFramework
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IIdentifiable
    {
        //// ----------------------------------------------------------------------------------------------------------

        private readonly IObjectSet<TEntity> objectSet;

        //// ----------------------------------------------------------------------------------------------------------

        public GenericRepository(IObjectContextAdapter contextAdapter)
        {
            Contract.Requires<ArgumentNullException>(contextAdapter != null);

            this.objectSet = contextAdapter.ObjectContext.CreateObjectSet<TEntity>();
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public ICollection<TEntity> All()
        {
            return objectSet.ToList();
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await objectSet.ToListAsync();
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public TEntity Get(int id)
        {
            return Find(e => e.Id == id);
        }

        //// ----------------------------------------------------------------------------------------------------------

        //public TEntity GetEager(int id, Expression<Func<TEntity, object>> include)
        //{
        //    var query = this.context.Set<TEntity>().Include(include);

        //    return query.SingleOrDefault(x => x.Id == id);
        //}

        ////// ----------------------------------------------------------------------------------------------------------

        //public TEntity GetEager(int id, params Expression<Func<TEntity, object>>[] includes)
        //{
        //    var query = this.context.Set<TEntity>();

        //    foreach (var include in includes)
        //        query.Include(include);

        //    //return query.SingleOrDefault(p => p.Id == id);
        //    return query.Find(id);
        //}

        //// ----------------------------------------------------------------------------------------------------------
		 
        public async Task<TEntity> GetAsync(int id)
        {
            return await FindAsync(e => e.Id == id);
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public TEntity Find(Expression<Func<TEntity, bool>> match)
        {
            return objectSet.SingleOrDefault(match);
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
        {
            return await objectSet.SingleOrDefaultAsync(match);
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> match)
        {
            return objectSet.Where(match);
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public TEntity Add(TEntity t)
        {
            objectSet.AddObject(t);
            return t;
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public void Delete(TEntity t)
        {
            objectSet.DeleteObject(t);
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public int Count()
        {
            return objectSet.Count();
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public async Task<int> CountAsync()
        {
            return await objectSet.CountAsync();
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}