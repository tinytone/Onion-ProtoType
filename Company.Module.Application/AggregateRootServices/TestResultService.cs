using System;
using System.Diagnostics.Contracts;

using AutoMapper;

using Company.Module.Domain;
using Company.Module.Domain.Interfaces;
using Company.Module.Repositories;
using Company.Module.Shared.DTO;

namespace Company.Module.Application.AggregateRootServices
{
    public class TestResultService : ITestResultService
    {
        //// ----------------------------------------------------------------------------------------------------------

        private readonly IUnitOfWork unitOfWork; 
        private readonly ITestResultRepository testResultRepository;
        private readonly IPatientRepository patientRepository; 
        private readonly IMappingEngine mapper;

        //// ----------------------------------------------------------------------------------------------------------

        public TestResultService(
            IUnitOfWork unitOfWork,
            ITestResultRepository testResultRepository,
            IPatientRepository patientRepository,
            IMappingEngine mapper)
        {
            Contract.Requires<ArgumentNullException>(unitOfWork != null);
            Contract.Requires<ArgumentNullException>(testResultRepository != null);
            Contract.Requires<ArgumentNullException>(patientRepository != null);
            Contract.Requires<ArgumentNullException>(mapper != null);

            this.unitOfWork = unitOfWork;
            this.testResultRepository = testResultRepository;
            this.patientRepository = patientRepository;
            this.mapper = mapper;
        }

        //// ----------------------------------------------------------------------------------------------------------

        public TestResultDTO GetId(int id)
        {
            TestResultDTO testResultDTO = null;

            var testResult = this.testResultRepository.GetById(id);

            if (IsValid(testResult))
                testResultDTO = this.mapper.Map<TestResult, TestResultDTO>(testResult);

            return testResultDTO;
        }

        //// ----------------------------------------------------------------------------------------------------------

        private bool IsValid(TestResult testResult)
        {
            return testResult != null;
        }
        
        //// ----------------------------------------------------------------------------------------------------------

        public TestResultDTO ProcessTest(ITestSpecifications testSpecifications)
        {
            var patient = this.patientRepository.GetByNhsNumber(testSpecifications.NhsNumber);

            var testResult = patient.PerformTest(testSpecifications) as TestResult;

            var insertedTestResult = this.testResultRepository.Insert(testResult);
            this.unitOfWork.Save();

            var testResultDTO = this.mapper.Map<TestResult, TestResultDTO>(insertedTestResult);

            return testResultDTO;
        }
        //// ----------------------------------------------------------------------------------------------------------
    }
}
