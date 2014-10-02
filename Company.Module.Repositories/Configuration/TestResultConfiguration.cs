using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Company.Module.Domain;

namespace Company.Module.Repositories.Configuration
{
    public class TestResultConfiguration : EntityTypeConfiguration<TestResult>
    {
        //// ----------------------------------------------------------------------------------------------------------

        public TestResultConfiguration()
        {
            HasKey(k => k.Id);

            Property(p => p.Id).HasColumnName("TestResultId").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.PatientId).HasColumnName("PatientId");
            Property(p => p.Outcome).HasColumnName("Outcome");
            Property(p => p.TestDate).HasColumnName("TestDate");
            Property(p => p.RowVersion).HasColumnName("RowVersion").IsConcurrencyToken();

            HasRequired(x => x.Patient);
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
