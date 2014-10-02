using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using AutoMapper;

using Company.Module.Domain;
using Company.Module.Domain.Interfaces;
using Company.Module.Repositories;
using Company.Module.Shared.DTO;

namespace Company.Module.Application.AggregateRootServices
{
    public class PatientService : IPatientService
    {
        //// ----------------------------------------------------------------------------------------------------------

        private readonly IUnitOfWork unitOfWork; 
        private readonly IPatientRepository patientRepository;
        private readonly IMappingEngine mapper;

        //// ----------------------------------------------------------------------------------------------------------

        public PatientService(
            IUnitOfWork unitOfWork, 
            IPatientRepository patientRepository, 
            IMappingEngine mapper)
        {
            Contract.Requires<ArgumentNullException>(unitOfWork != null);
            Contract.Requires<ArgumentNullException>(patientRepository != null);
            Contract.Requires<ArgumentNullException>(mapper != null);

            this.unitOfWork = unitOfWork; 
            this.patientRepository = patientRepository;
            this.mapper = mapper;
        }

        //// ----------------------------------------------------------------------------------------------------------

        public IEnumerable<PatientDTO> GetAll()
        {
            var patients = this.patientRepository.GetAll();

            return this.mapper.Map<IEnumerable<IPatient>, IEnumerable<PatientDTO>>(patients);
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public PatientDTO GetByPatientId(int id)
        {
            PatientDTO patientDTO = null;

            var patient = this.patientRepository.GetById(id);

            if (IsValid(patient))
                patientDTO = this.mapper.Map<Patient, PatientDTO>(patient);

            return patientDTO;
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        private bool IsValid(IPatient patient)
        {
            return patient != null;
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public PatientDTO GetByNhsNumber(string nhsNumber)
        {
            PatientDTO patientDTO = null;

            var patient = this.patientRepository.GetByNhsNumber(nhsNumber);

            if (IsValid(patient))
                patientDTO = this.mapper.Map<Patient, PatientDTO>(patient);

            return patientDTO;
        }

        //// ----------------------------------------------------------------------------------------------------------

        public Patient CreatePatient(PatientDTO patientDTO)
        {
            var patient = this.mapper.Map<PatientDTO, Patient>(patientDTO);

            var insertedPatient = this.patientRepository.Insert(patient);
            this.unitOfWork.Save();

            return insertedPatient;
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
