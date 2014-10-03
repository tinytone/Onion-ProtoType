using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using Company.Module.Domain;
using Company.Module.Domain.Interfaces;

namespace Company.Module.Application.AggregateRootServices
{
    [ContractClassFor(typeof(IPatientService))]
    internal abstract class PatientServiceContract : IPatientService
    {
        //// ----------------------------------------------------------------------------------------------------------

        public IEnumerable<Patient> GetAll()
        {
            throw new NotImplementedException("This class is only used to provide contract requirements for IPatientService.");
        }

        //// ----------------------------------------------------------------------------------------------------------

        public Patient GetByPatientId(int id)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id > 0);

            throw new NotImplementedException("This class is only used to provide contract requirements for IPatientService.");
        }

        //// ----------------------------------------------------------------------------------------------------------

        public Patient GetByNhsNumber(string nhsNumber)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(nhsNumber));

            throw new NotImplementedException("This class is only used to provide contract requirements for IPatientService.");
        }

        //// ----------------------------------------------------------------------------------------------------------

        public Patient CreatePatient(Patient patient)
        {
            Contract.Requires<ArgumentNullException>(patient != null);

            throw new NotImplementedException("This class is only used to provide contract requirements for IPatientService.");
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
