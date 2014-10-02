using System;
using System.Collections.Generic;

using AutoMapper;

using Company.Module.Application.AggregateRootServices;
using Company.Module.Domain.Interfaces;
using Company.Module.Repositories;
using Company.Module.Shared.DTO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Rhino.Mocks;

namespace Company.Module.Application.Tests.AggregateRootServices
{
    [TestClass]
    public class TestResultServiceTest
    {
        //// ----------------------------------------------------------------------------------------------------------

        private MockRepository mocks;

        //// ----------------------------------------------------------------------------------------------------------

        [TestInitialize]
        public void TestInitialize()
        {
            this.mocks = new MockRepository();
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestCleanup]
        public void TestCleanup()
        {
            this.mocks.VerifyAll();
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_UnitOfWorkIsNull_ExpectArgumentNullException()
        {
            // Arrange
            IUnitOfWork unitOfWork = null;
            var patientRepository = this.mocks.StrictMock<IPatientRepository>();
            var testResultRepository = this.mocks.StrictMock<ITestResultRepository>();
            var mappingEngine = this.mocks.StrictMock<IMappingEngine>();

            this.mocks.ReplayAll();

            // Act
            GetTestResultService(unitOfWork, testResultRepository, patientRepository, mappingEngine);

            // Assert
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_PatientRepositoryIsNull_ExpectArgumentNullException()
        {
            // Arrange
            var unitOfWork = this.mocks.StrictMock<IUnitOfWork>();
            IPatientRepository patientRepository = null;
            var testResultRepository = this.mocks.StrictMock<ITestResultRepository>();
            var mappingEngine = this.mocks.StrictMock<IMappingEngine>();

            this.mocks.ReplayAll();

            // Act
            GetTestResultService(unitOfWork, testResultRepository, patientRepository, mappingEngine);

            // Assert
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_TestResultRepositoryIsNull_ExpectArgumentNullException()
        {
            // Arrange
            var unitOfWork = this.mocks.StrictMock<IUnitOfWork>();
            var patientRepository = this.mocks.StrictMock<IPatientRepository>();
            ITestResultRepository testResultRepository = null;
            var mappingEngine = this.mocks.StrictMock<IMappingEngine>();

            this.mocks.ReplayAll();

            // Act
            GetTestResultService(unitOfWork, testResultRepository, patientRepository, mappingEngine);

            // Assert
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_MappingEngineIsNull_ExpectArgumentNullException()
        {
            // Arrange
            var unitOfWork = this.mocks.StrictMock<IUnitOfWork>();
            var patientRepository = this.mocks.StrictMock<IPatientRepository>();
            var testResultRepository = this.mocks.StrictMock<ITestResultRepository>();
            IMappingEngine mappingEngine = null;

            this.mocks.ReplayAll();

            // Act
            GetTestResultService(unitOfWork, testResultRepository, patientRepository, mappingEngine);

            // Assert
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        public void Constructor_AllDependanciesAreValid_ExpectInstance()
        {
            // Arrange
            var unitOfWork = this.mocks.StrictMock<IUnitOfWork>();
            var patientRepository = this.mocks.StrictMock<IPatientRepository>();
            var testResultRepository = this.mocks.StrictMock<ITestResultRepository>();
            var mappingEngine = this.mocks.StrictMock<IMappingEngine>();

            this.mocks.ReplayAll();

            // Act
            var service = GetTestResultService(unitOfWork, testResultRepository, patientRepository, mappingEngine);

            // Assert
            Assert.IsNotNull(service);
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetId_IdIsZero_ExpectArgumentOutOfRangeException()
        {
            // Arrange
            const int Id = 0;

            var unitOfWork = this.mocks.StrictMock<IUnitOfWork>();
            var patientRepository = this.mocks.StrictMock<IPatientRepository>();
            var testResultRepository = this.mocks.StrictMock<ITestResultRepository>();
            var mappingEngine = this.mocks.StrictMock<IMappingEngine>();

            this.mocks.ReplayAll();

            var service = GetTestResultService(unitOfWork, testResultRepository, patientRepository, mappingEngine);

            // Act
            service.GetId(Id);

            // Assert
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetId_IdIsNegative_ExpectArgumentOutOfRangeException()
        {
            // Arrange
            const int Id = int.MinValue;

            var unitOfWork = this.mocks.StrictMock<IUnitOfWork>();
            var patientRepository = this.mocks.StrictMock<IPatientRepository>();
            var testResultRepository = this.mocks.StrictMock<ITestResultRepository>();
            var mappingEngine = this.mocks.StrictMock<IMappingEngine>();

            this.mocks.ReplayAll();

            var service = GetTestResultService(unitOfWork, testResultRepository, patientRepository, mappingEngine);

            // Act
            service.GetId(Id);

            // Assert
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ProcessTest_TestSpecificationsIsNull_ExpectArgumentNullException()
        {
            // Arrange
            ITestSpecifications testSpecifications = null;

            var unitOfWork = this.mocks.StrictMock<IUnitOfWork>();
            var patientRepository = this.mocks.StrictMock<IPatientRepository>();
            var testResultRepository = this.mocks.StrictMock<ITestResultRepository>();
            var mappingEngine = this.mocks.StrictMock<IMappingEngine>();

            this.mocks.ReplayAll();

            var service = GetTestResultService(unitOfWork, testResultRepository, patientRepository, mappingEngine);

            // Act
            service.ProcessTest(testSpecifications);

            // Assert
        }

        //// ----------------------------------------------------------------------------------------------------------

        private ITestResultService GetTestResultService(
            IUnitOfWork unitOfWork,
            ITestResultRepository testResultRepository,
            IPatientRepository patientRepository,
            IMappingEngine mapper)
        {
            return new TestResultService(unitOfWork, testResultRepository, patientRepository, mapper);
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
