using System.Collections.Generic;

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

        public PatientRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
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
            return GetEager(id);
        }

        //// ----------------------------------------------------------------------------------------------------------

        public Patient GetByNhsNumber(string nhsNumber)
        {
            return Find(patient => patient.NHSNumber == nhsNumber);
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public Patient Insert(Patient patient)
        {
            return Add(patient);
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
