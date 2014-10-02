using System;

using Company.Module.Repositories.EntityFramework;

namespace Company.Module.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        //// ----------------------------------------------------------------------------------------------------------

        int Save();

        //// ----------------------------------------------------------------------------------------------------------

        IContext Context { get; }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
