using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Company.Module.Domain;
using Company.Module.Shared.DTO;

namespace Company.Module.Application.AggregateRootServices
{
    [ContractClassFor(typeof(IPatientService))]
    internal abstract class PatientServiceContract : IPatientService
    {
        //// ----------------------------------------------------------------------------------------------------------
		 
        public IEnumerable<PatientDTO> GetAll()
        {
            throw new NotImplementedException("This class is only used to provide contract requirements for IPatientService.");
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public PatientDTO GetByPatientId(int id)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id > 0);

            throw new NotImplementedException("This class is only used to provide contract requirements for IPatientService.");
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public PatientDTO GetByNhsNumber(string nhsNumber)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(nhsNumber));

            throw new NotImplementedException("This class is only used to provide contract requirements for IPatientService.");
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public Patient CreatePatient(PatientDTO patientDTO)
        {
            Contract.Requires<ArgumentNullException>(patientDTO != null);

            throw new NotImplementedException("This class is only used to provide contract requirements for IPatientService.");
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
