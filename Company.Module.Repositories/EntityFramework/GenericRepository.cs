using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Company.Module.Domain.Interfaces;

namespace Company.Module.Repositories.EntityFramework
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>, IDisposable
        where TEntity : class, IIdentifiable
    {
        //// ----------------------------------------------------------------------------------------------------------

        private readonly Context context;

        private bool disposed;

        //// ----------------------------------------------------------------------------------------------------------

        public GenericRepository(IUnitOfWork unitOfWork)
        {
            Contract.Requires<ArgumentNullException>(unitOfWork != null);

            this.context = unitOfWork.Context as Context;
            this.disposed = false;
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public ICollection<TEntity> All()
        {
            return this.context.Set<TEntity>().ToList();
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await this.context.Set<TEntity>().ToListAsync();
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public TEntity Get(int id)
        {
            return this.context.Set<TEntity>().Find(id);
        }

        //// ----------------------------------------------------------------------------------------------------------

        public TEntity GetEager(int id, Expression<Func<TEntity, object>> include)
        {
            var query = this.context.Set<TEntity>().Include(include);

            return query.SingleOrDefault(x => x.Id == id);
        }

        //// ----------------------------------------------------------------------------------------------------------

        public TEntity GetEager(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = this.context.Set<TEntity>();

            foreach (var include in includes)
                query.Include(include);

            //return query.SingleOrDefault(p => p.Id == id);
            return query.Find(id);
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public async Task<TEntity> GetAsync(int id)
        {
            return await this.context.Set<TEntity>().FindAsync(id);
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public TEntity Find(Expression<Func<TEntity, bool>> match)
        {
            return this.context.Set<TEntity>().SingleOrDefault(match);
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
        {
            return await this.context.Set<TEntity>().SingleOrDefaultAsync(match);
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match)
        {
            return this.context.Set<TEntity>().Where(match).ToList();
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
        {
            return await this.context.Set<TEntity>().Where(match).ToListAsync();
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public TEntity Add(TEntity t)
        {
            this.context.Set<TEntity>().Add(t);

            // commented out as this is performed by the Unit of Work
            // this.context.SaveChanges();
            return t;
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public async Task<TEntity> AddAsync(TEntity t)
        {
            this.context.Set<TEntity>().Add(t);
            await this.context.SaveChangesAsync();
            return t;
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public TEntity Update(TEntity updated, int key)
        {
            if (updated == null)
                return null;

            var existing = this.context.Set<TEntity>().Find(key);
            if (existing != null)
            {
                this.context.Entry(existing).CurrentValues.SetValues(updated);

                // commented out as this is performed by the Unit of Work
                // this.context.SaveChanges();
            }

            return existing;
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public async Task<TEntity> UpdateAsync(TEntity updated, int key)
        {
            if (updated == null)
                return null;

            var existing = await this.context.Set<TEntity>().FindAsync(key);
            if (existing != null)
            {
                this.context.Entry(existing).CurrentValues.SetValues(updated);
                await this.context.SaveChangesAsync();
            }

            return existing;
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public void Delete(TEntity t)
        {
            this.context.Set<TEntity>().Remove(t);

            // commented out as this is performed by the Unit of Work
            // this.context.SaveChanges();
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public async Task<int> DeleteAsync(TEntity t)
        {
            this.context.Set<TEntity>().Remove(t);
            return await this.context.SaveChangesAsync();
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public int Count()
        {
            return this.context.Set<TEntity>().Count();
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public async Task<int> CountAsync()
        {
            return await this.context.Set<TEntity>().CountAsync();
        }

        //// ----------------------------------------------------------------------------------------------------------

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (this.context != null)
                        this.context.Dispose();
                }
            }

            this.disposed = true;
        }
        //// ----------------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}