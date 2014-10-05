using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

using Company.Module.Domain;

namespace Company.Module.Repositories.EntityFramework
{
    /// <summary>
    /// Repository Pattern:  Mediates between the domain and data mapping layers using a collection-like interface for accessing domain objects.  
    /// http://martinfowler.com/eaaCatalog/repository.html
    /// </summary>
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        //// ----------------------------------------------------------------------------------------------------------

        public PatientRepository(IObjectContextAdapter contextAdapter)
            : base(contextAdapter)
        {
        }

        //// ----------------------------------------------------------------------------------------------------------

        public IEnumerable<Patient> GetAll()
        {
            return All();
        }

        //// ----------------------------------------------------------------------------------------------------------

        public Patient GetById(int id)
        {
            // TODO : Eagerly load the test results.
            return Get(id);
        }

        //// ----------------------------------------------------------------------------------------------------------

        public Patient GetByNhsNumber(string nhsNumber)
        {
            return Find(patient => patient.NHSNumber == nhsNumber);
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public void Insert(Patient patient)
        {
            // NOTE : Not sure that we need to return the patient
            Add(patient);
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
