using System;
using System.Collections.Generic;
using System.Linq;

using Company.Module.Application.AggregateRootServices;
using Company.Module.Domain;
using Company.Module.Repositories;
using Company.Module.Shared.DTO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Rhino.Mocks;

namespace Company.Module.Application.Tests.AggregateRootServices
{
    [TestClass]
    public class PatientServiceTest
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

            this.mocks.ReplayAll();

            // Act
            GetPatientService(unitOfWork, patientRepository);

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
            
            this.mocks.ReplayAll();

            // Act
            GetPatientService(unitOfWork, patientRepository);

            // Assert
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        public void Constructor_AllDependanciesAreValid_ExpectInstanceWithDefaultState()
        {
            // Arrange
            var unitOfWork = this.mocks.StrictMock<IUnitOfWork>();
            var patientRepository = this.mocks.StrictMock<IPatientRepository>();

            this.mocks.ReplayAll();

            // Act
            var service = GetPatientService(unitOfWork, patientRepository);

            // Assert
            Assert.IsNotNull(service);
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        public void GetAll_RetrievesAllPatientsFromRepository_ExpectMappedPatientsToBeReturned()
        {
            // Arrange
            var patients = new List<Patient>
                            {
                                new Patient { Id = 1, FirstName = "Tony", Surname = "Harding", DateOfBirth = DateTime.Parse("1978-01-06"), NHSNumber = "111 111 1111", RowVersion = new byte[] { 01, 02, 03, 04, 05, 06, 07 } },
                                new Patient { Id = 2, FirstName = "Joe", Surname = "Blogs", DateOfBirth = DateTime.Parse("1992-05-22"), NHSNumber = "222 222 2222", RowVersion = new byte[] { 08, 09, 10, 11, 12, 13, 14 } },
                                new Patient { Id = 3, FirstName = "Robin", Surname = "Smith", DateOfBirth = DateTime.Parse("1984-11-25"), NHSNumber = "333 333 3333", RowVersion = new byte[] { 15, 16, 17, 18, 19, 20, 21 } }
                            };

            var unitOfWork = this.mocks.StrictMock<IUnitOfWork>();

            var patientRepository = this.mocks.StrictMock<IPatientRepository>();
            Expect.Call(patientRepository.GetAll()).Return(patients);

            this.mocks.ReplayAll();

            var service = GetPatientService(unitOfWork, patientRepository);

            // Act
            var result = service.GetAll().ToList();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(patients.Count(), result.Count());

            for (var i = 0; i < patients.Count; i++)
                Assert.AreEqual(patients[i], result[i]);
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetByPatientId_PatientIdIsZero_ExpectArgumentOutOfRangeException()
        {
            // Arrange
            const int PatientId = 0;

            var patientRepository = this.mocks.StrictMock<IPatientRepository>();
            var unitOfWork = this.mocks.StrictMock<IUnitOfWork>();

            this.mocks.ReplayAll();

            var service = GetPatientService(unitOfWork, patientRepository);

            // Act
            service.GetByPatientId(PatientId);

            // Assert
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetByPatientId_PatientIdIsNegative_ExpectArgumentOutOfRangeException()
        {
            // Arrange
            const int PatientId = -1;

            var patientRepository = this.mocks.StrictMock<IPatientRepository>();
            var unitOfWork = this.mocks.StrictMock<IUnitOfWork>();

            this.mocks.ReplayAll();

            var service = GetPatientService(unitOfWork, patientRepository);

            // Act
            service.GetByPatientId(PatientId);

            // Assert
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        public void GetByPatientId_RetrievesPatientFromRepository_ExpectMappedPatientToBeReturned()
        {
            // Arrange
            const int PatientId = 1;

            var patient = new Patient
                              {
                                  Id = 1,
                                  FirstName = "Tony",
                                  Surname = "Harding",
                                  DateOfBirth = DateTime.Parse("1978-01-06"),
                                  NHSNumber = "111 111 1111",
                                  RowVersion = new byte[] { 01, 02, 03, 04, 05, 06, 07 }
                              };

            var patientRepository = this.mocks.StrictMock<IPatientRepository>();
            Expect.Call(patientRepository.GetById(PatientId)).Return(patient);

            var unitOfWork = this.mocks.StrictMock<IUnitOfWork>();

            this.mocks.ReplayAll();

            var service = GetPatientService(unitOfWork, patientRepository);

            // Act
            var result = service.GetByPatientId(PatientId);

            // Assert
            Assert.AreEqual(patient, result);
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetByNhsNumber_NhsNumberIsNull_ExpectArgumentNullException()
        {
            // Arrange
            const string NhsNumber = null;

            var patientRepository = this.mocks.StrictMock<IPatientRepository>();
            var unitOfWork = this.mocks.StrictMock<IUnitOfWork>();

            this.mocks.ReplayAll();

            var service = GetPatientService(unitOfWork, patientRepository);

            // Act
            service.GetByNhsNumber(NhsNumber);

            // Assert
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetByNhsNumber_NhsNumberIsEmptyString_ExpectArgumentNullException()
        {
            // Arrange
            var nhsNumber = string.Empty;

            var patientRepository = this.mocks.StrictMock<IPatientRepository>();
            var unitOfWork = this.mocks.StrictMock<IUnitOfWork>();

            this.mocks.ReplayAll();

            var service = GetPatientService(unitOfWork, patientRepository);

            // Act
            service.GetByNhsNumber(nhsNumber);

            // Assert
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        public void GetByNhsNumber_RetrievesPatientFromRepository_ExpectMappedPatientToBeReturned()
        {
            // Arrange
            const string NhsNumber = "123 123 1234";

            var patient = new Patient
            {
                Id = 1,
                FirstName = "Tony",
                Surname = "Harding",
                DateOfBirth = DateTime.Parse("1978-01-06"),
                NHSNumber = NhsNumber,
                RowVersion = new byte[] { 01, 02, 03, 04, 05, 06, 07 }
            };

            var patientRepository = this.mocks.StrictMock<IPatientRepository>();
            Expect.Call(patientRepository.GetByNhsNumber(NhsNumber)).Return(patient);

            var unitOfWork = this.mocks.StrictMock<IUnitOfWork>();

            this.mocks.ReplayAll();

            var service = GetPatientService(unitOfWork, patientRepository);

            // Act
            var result = service.GetByNhsNumber(NhsNumber);

            // Assert
            Assert.AreEqual(patient, result);
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatePatient_PatientIsNull_ExpectArgumentNullException()
        {
            // Arrange
            Patient patient = null;

            var patientRepository = this.mocks.StrictMock<IPatientRepository>();
            var unitOfWork = this.mocks.StrictMock<IUnitOfWork>();

            this.mocks.ReplayAll();

            var service = GetPatientService(unitOfWork, patientRepository);

            // Act
            service.CreatePatient(patient);

            // Assert
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        public void CreatePatient_InsertsPatientIntoRepository_ExpectInsertedPatientIdToBeReturned()
        {
            // Arrange
            const int PatientId = 50;

            var patientDTO = new PatientDTO
            {
                FirstName = "Tony",
                Surname = "Harding",
                DateOfBirth = DateTime.Parse("1978-01-06"),
                NHSNumber = "123 123 1234",
            };

            var patient = CreatePatient(patientDTO);

            var insertedPatient = CreatePatient(patientDTO);
            insertedPatient.Id = PatientId;

            var patientRepository = this.mocks.StrictMock<IPatientRepository>();
            Expect.Call(patientRepository.Insert(patient)).Return(insertedPatient);

            var unitOfWork = this.mocks.StrictMock<IUnitOfWork>();
            Expect.Call(unitOfWork.Save()).Return(1);
            this.mocks.ReplayAll();

            var service = GetPatientService(unitOfWork, patientRepository);

            // Act
            var result = service.CreatePatient(patient);

            // Assert
            Assert.AreEqual(insertedPatient, result);
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        private Patient CreatePatient(PatientDTO patientDTO)
        {
            return new Patient
            {
                Id = patientDTO.Id,
                FirstName = patientDTO.FirstName,
                Surname = patientDTO.Surname,
                DateOfBirth = patientDTO.DateOfBirth,
                NHSNumber = patientDTO.NHSNumber
            };
        }

        //// ----------------------------------------------------------------------------------------------------------
        
        private IPatientService GetPatientService(
            IUnitOfWork unitOfWork, 
            IPatientRepository patientRepository)
        {
            return new PatientService(unitOfWork, patientRepository);
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
