using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using Company.Module.Domain;
using Company.Module.Repositories.Configuration;

namespace Company.Module.Repositories.EntityFramework
{
    public class Context : DbContext, IContext
    {
        //// ----------------------------------------------------------------------------------------------------------

        static Context()
        {
            // Database.SetInitializer(new PatientDataInitializer());
            // Database.SetInitializer(new CreateDatabaseIfNotExists<Context>());

            // Disables Database Initialization
            Database.SetInitializer<Context>(null);    
        }

        //// ----------------------------------------------------------------------------------------------------------

        public Context() : base("DefaultConnectionString")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        //// ----------------------------------------------------------------------------------------------------------

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new PatientConfiguration())
                                       .Add(new TestResultConfiguration());
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
