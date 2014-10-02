﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Company.Module.Domain.Interfaces;

namespace Company.Module.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class, IIdentifiable
    {
        //// ----------------------------------------------------------------------------------------------------------
		 
        ICollection<TEntity> All();

        //// ----------------------------------------------------------------------------------------------------------
		 
        Task<ICollection<TEntity>> GetAllAsync();

        //// ----------------------------------------------------------------------------------------------------------
		 
        TEntity Get(int id);

        //// ----------------------------------------------------------------------------------------------------------
		 
        Task<TEntity> GetAsync(int id);

        //// ----------------------------------------------------------------------------------------------------------
		 
        TEntity Find(Expression<Func<TEntity, bool>> match);

        //// ----------------------------------------------------------------------------------------------------------
		 
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match);

        //// ----------------------------------------------------------------------------------------------------------
		 
        ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match);

        //// ----------------------------------------------------------------------------------------------------------
		 
        Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match);

        //// ----------------------------------------------------------------------------------------------------------
		 
        TEntity Add(TEntity t);

        //// ----------------------------------------------------------------------------------------------------------
		 
        Task<TEntity> AddAsync(TEntity t);

        //// ----------------------------------------------------------------------------------------------------------
		 
        TEntity Update(TEntity updated, int key);

        //// ----------------------------------------------------------------------------------------------------------
		 
        Task<TEntity> UpdateAsync(TEntity updated, int key);

        //// ----------------------------------------------------------------------------------------------------------
		 
        void Delete(TEntity t);

        //// ----------------------------------------------------------------------------------------------------------
		 
        Task<int> DeleteAsync(TEntity t);

        //// ----------------------------------------------------------------------------------------------------------
		 
        int Count();

        //// ----------------------------------------------------------------------------------------------------------
		 
        Task<int> CountAsync();

        //// ----------------------------------------------------------------------------------------------------------
    }
}
