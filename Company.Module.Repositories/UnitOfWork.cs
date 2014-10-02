using System.Data.Entity.Infrastructure;

using Company.Module.Repositories.EntityFramework;

namespace Company.Module.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        //// ----------------------------------------------------------------------------------------------------------

        private readonly IContext context;

        //// ----------------------------------------------------------------------------------------------------------

        public IContext Context
        {
            get
            {
                return this.context;
            }
        }

        //// ----------------------------------------------------------------------------------------------------------

        //private readonly IObjectContextAdapter contextAdapter;
        //public UnitOfWork(IObjectContextAdapter contextAdapter)
        //{
        //    this.contextAdapter = contextAdapter;
        //}

        public UnitOfWork(IContext context)
        {
            this.context = context;
        }

        //// ----------------------------------------------------------------------------------------------------------

        public int Save()
        {
            return this.Context.SaveChanges();
            //return this.contextAdapter.ObjectContext.SaveChanges();
        }

        //// ----------------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            //if (this.contextAdapter != null)
            //{
            //    if (this.contextAdapter.ObjectContext != null)
            //        this.contextAdapter.ObjectContext.Dispose();
            //}
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
