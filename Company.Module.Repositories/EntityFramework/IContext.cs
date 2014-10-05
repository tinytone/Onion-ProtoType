using System.Data.Entity;

using Company.Module.Domain;

namespace Company.Module.Repositories.EntityFramework
{
    public interface IContext
    {
        //// ----------------------------------------------------------------------------------------------------------

        int SaveChanges();

        //// ----------------------------------------------------------------------------------------------------------
    }
}
