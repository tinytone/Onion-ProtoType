using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

using Company.Module.Domain;
using Company.Module.Repositories.EntityFramework;

namespace Company.Module.Repositories
{
    /// <summary>
    /// Repository Pattern:  Mediates between the domain and data mapping layers using a collection-like interface for accessing domain objects.  
    /// http://martinfowler.com/eaaCatalog/repository.html
    /// </summary>
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        //// ----------------------------------------------------------------------------------------------------------

        //public PatientRepository(IObjectContextAdapter contextAdapter)
        //    : base(contextAdapter)
        //{
        //    this.disposed = false;
        //}

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
            return Get(id);
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
