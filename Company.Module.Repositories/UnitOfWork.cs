using System.Data.Entity.Infrastructure;

namespace Company.Module.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        //// ----------------------------------------------------------------------------------------------------------

        private readonly IObjectContextAdapter contextAdapter;

        //// ----------------------------------------------------------------------------------------------------------

        public UnitOfWork(IObjectContextAdapter contextAdapter)
        {
            this.contextAdapter = contextAdapter;
        }

        //// ----------------------------------------------------------------------------------------------------------

        public void SaveChanges()
        {
            contextAdapter.ObjectContext.SaveChanges();
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
