using System.Collections.Generic;

using Company.Module.Domain;
using Company.Module.Domain.Interfaces;

namespace Company.Module.Repositories
{
    public interface IPatientRepository
    {
        //// ----------------------------------------------------------------------------------------------------------

        IEnumerable<Patient> GetAll();

        //// ----------------------------------------------------------------------------------------------------------

        Patient GetById(int id);

        //// ----------------------------------------------------------------------------------------------------------

        Patient GetByNhsNumber(string nhsNumber);

        //// ----------------------------------------------------------------------------------------------------------

        Patient Insert(Patient patient);

        //// ----------------------------------------------------------------------------------------------------------
    }
}
