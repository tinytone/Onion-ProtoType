using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using Company.Module.Domain;
using Company.Module.Domain.Interfaces;
using Company.Module.Repositories;

namespace Company.Module.Application.AggregateRootServices
{
    public class PatientService : IPatientService
    {
        //// ----------------------------------------------------------------------------------------------------------

        private readonly IUnitOfWork unitOfWork; 
        private readonly IPatientRepository patientRepository;

        //// ----------------------------------------------------------------------------------------------------------

        public PatientService(
            IUnitOfWork unitOfWork, 
            IPatientRepository patientRepository)
        {
            Contract.Requires<ArgumentNullException>(unitOfWork != null);
            Contract.Requires<ArgumentNullException>(patientRepository != null);

            this.unitOfWork = unitOfWork; 
            this.patientRepository = patientRepository;
        }

        //// ----------------------------------------------------------------------------------------------------------

        public IEnumerable<Patient> GetAll()
        {
            return this.patientRepository.GetAll();
        }

        //// ----------------------------------------------------------------------------------------------------------

        public Patient GetByPatientId(int id)
        {
            return this.patientRepository.GetById(id);
        }

        //// ----------------------------------------------------------------------------------------------------------

        public Patient GetByNhsNumber(string nhsNumber)
        {
            return this.patientRepository.GetByNhsNumber(nhsNumber);
        }

        //// ----------------------------------------------------------------------------------------------------------

        public Patient CreatePatient(Patient patient)
        {
            var insertedPatient = this.patientRepository.Insert(patient);

            this.unitOfWork.Save();

            return insertedPatient;
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
