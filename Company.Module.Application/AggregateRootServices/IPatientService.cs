﻿using System.Collections.Generic;
using System.Diagnostics.Contracts;

using Company.Module.Domain;

namespace Company.Module.Application.AggregateRootServices
{
    [ContractClass(typeof(PatientServiceContract))]
    public interface IPatientService
    {
        //// ----------------------------------------------------------------------------------------------------------

        IEnumerable<Patient> GetAll();
            
        //// ----------------------------------------------------------------------------------------------------------

        Patient GetByPatientId(int id);

        //// ----------------------------------------------------------------------------------------------------------

        Patient GetByNhsNumber(string nhsNumber);

        //// ----------------------------------------------------------------------------------------------------------

        Patient CreatePatient(Patient patient);

        //// ----------------------------------------------------------------------------------------------------------
    }

    /*
    public interface ICommand<in TKey, out TResult>
        where TResult : class
    {
        TResult Execute(TKey key);
    }

    public interface IPatientLookupById : ICommand<int, PatientDTO>
    {
    }

    public class PatientLookupById : IPatientLookupById
    {
        private readonly IPatientRepository repository;
        private readonly IMappingEngine mapper;

        public PatientLookupById(IPatientRepository repository, IMappingEngine mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public PatientDTO Execute(int key)
        {
            var patient = repository.GetById(key);

            if (patient == null)
                return null;

            return mapper.Map<PatientDTO>(patient);
        }
    }

    public class PatientLookupByNHSNumber : ICommand<string, PatientDTO>
    {
        public PatientDTO Execute(string key)
        {
            throw new NotImplementedException();
        }
    }

    public interface ISetCommand<out TResult>
        where TResult : class
    {
        IEnumerable<TResult> Execute();
    }

    public class PatientLookup : ISetCommand<PatientDTO>
    {
        public IEnumerable<PatientDTO> Execute()
        {
            throw new NotImplementedException();
        }
    }

    public interface ISetCommand<in TKey, out TResult>
        where TResult : class
    {
        IEnumerable<TResult> Execute(TKey key);
    }

    public class PatientLookupByAddress : ISetCommand<object, PatientDTO>
    {
        public IEnumerable<PatientDTO> Execute(object address)
        {
            throw new NotImplementedException();
        }
    }
     * */
}
