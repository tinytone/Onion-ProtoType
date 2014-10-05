using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;

namespace Company.Module.Repositories.Tests
{
    public class FakeObjectSet<TEntity> : IObjectSet<TEntity>
        where TEntity : class
    {
        private readonly ICollection<TEntity> data;
        private readonly IQueryable query;

        public FakeObjectSet()
            : this(Enumerable.Empty<TEntity>())
        {
        }

        public FakeObjectSet(IEnumerable<TEntity> collection)
        {
            this.data = new List<TEntity>(collection);
            this.query = this.data.AsQueryable();
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Expression Expression { get { return this.query.Expression; } }
        public Type ElementType { get { return this.query.ElementType; }}
        public IQueryProvider Provider { get { return this.query.Provider; } }
        
        public void AddObject(TEntity entity)
        {
            data.Add(entity);
        }

        public void Attach(TEntity entity)
        {
            AddObject(entity);
        }

        public void DeleteObject(TEntity entity)
        {
            data.Remove(entity);
        }

        public void Detach(TEntity entity)
        {
            DeleteObject(entity);
        }
    }
}