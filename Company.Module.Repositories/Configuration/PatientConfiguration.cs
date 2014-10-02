using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using Company.Module.Domain;

namespace Company.Module.Repositories.Configuration
{
    public class PatientConfiguration : EntityTypeConfiguration<Patient>
    {
        //// ----------------------------------------------------------------------------------------------------------

        public PatientConfiguration()
        {
            HasKey(k => k.Id);

            Property(p => p.Id).HasColumnName("PatientId").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.NHSNumber).HasColumnName("NHSNumber");
            Property(p => p.FirstName).HasColumnName("FirstName");
            Property(p => p.Surname).HasColumnName("Surname");
            Property(p => p.DateOfBirth).HasColumnName("DateOfBirth");
            Property(p => p.RowVersion).HasColumnName("RowVersion").IsConcurrencyToken();
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
