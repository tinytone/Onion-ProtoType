using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using Company.Module.Application.AggregateRootServices;
using Company.Module.Domain;
using Company.Module.Domain.Interfaces;
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
            var mappingEngine = this.mocks.StrictMock<IMappingEngine>();

            this.mocks.ReplayAll();

            // Act
            GetPatientService(unitOfWork, patientRepository, mappingEngine);

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
            var mappingEngine = this.mocks.StrictMock<IMappingEngine>();
            
            this.mocks.ReplayAll();

            // Act
            GetPatientService(unitOfWork, patientRepository, mappingEngine);

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
            IMappingEngine mappingEngine = null;

            this.mocks.ReplayAll();

            // Act
            GetPatientService(unitOfWork, patientRepository, mappingEngine);

            // Assert
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        public void Constructor_AllDepedanciesAreValid_ExpectInstanceWithDefaultState()
        {
            // Arrange
            var unitOfWork = this.mocks.StrictMock<IUnitOfWork>();
            var patientRepository = this.mocks.StrictMock<IPatientRepository>();
            var mappingEngine = this.mocks.StrictMock<IMappingEngine>();

            this.mocks.ReplayAll();

            // Act
            var service = GetPatientService(unitOfWork, patientRepository, mappingEngine);

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

            var mappedPatients = new List<PatientDTO>
                            {
                                CreatePatientDTO(patients.First(p => p.Id == 1)),
                                CreatePatientDTO(patients.First(p => p.Id == 2)),
                                CreatePatientDTO(patients.First(p => p.Id == 3)),
                            };

            var mappingEngine = this.mocks.StrictMock<IMappingEngine>();
            Expect.Call(mappingEngine.Map<IEnumerable<IPatient>, IEnumerable<PatientDTO>>(patients)).Return(mappedPatients);

            this.mocks.ReplayAll();

            var service = GetPatientService(unitOfWork, patientRepository, mappingEngine);

            // Act
            var result = service.GetAll().ToList();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(patients.Count(), result.Count());

            for (var i = 0; i < patients.Count; i++)
                Assert.IsTrue(PatientDataMatches(patients[i], result[i]));
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

            var mappedPatient = CreatePatientDTO(patient);

            var mappingEngine = this.mocks.StrictMock<IMappingEngine>();
            Expect.Call(mappingEngine.Map<Patient, PatientDTO>(patient)).Return(mappedPatient);

            var unitOfWork = this.mocks.StrictMock<IUnitOfWork>();

            this.mocks.ReplayAll();

            var service = GetPatientService(unitOfWork, patientRepository, mappingEngine);

            // Act
            var result = service.GetByPatientId(PatientId);

            // Assert
            Assert.IsTrue(PatientDataMatches(patient, result));
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

            var mappedPatient = CreatePatientDTO(patient);

            var mappingEngine = this.mocks.StrictMock<IMappingEngine>();
            Expect.Call(mappingEngine.Map<Patient, PatientDTO>(patient)).Return(mappedPatient);

            var unitOfWork = this.mocks.StrictMock<IUnitOfWork>();

            this.mocks.ReplayAll();

            var service = GetPatientService(unitOfWork, patientRepository, mappingEngine);

            // Act
            var result = service.GetByNhsNumber(NhsNumber);

            // Assert
            Assert.IsTrue(PatientDataMatches(patient, result));
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
            
            var mappingEngine = this.mocks.StrictMock<IMappingEngine>();
            Expect.Call(mappingEngine.Map<PatientDTO, Patient>(patientDTO)).Return(patient);

            var patientRepository = this.mocks.StrictMock<IPatientRepository>();
            Expect.Call(patientRepository.Insert(patient)).Return(insertedPatient);

            var unitOfWork = this.mocks.StrictMock<IUnitOfWork>();
            Expect.Call(unitOfWork.Save()).Return(1);
            this.mocks.ReplayAll();

            var service = GetPatientService(unitOfWork, patientRepository, mappingEngine);

            // Act
            var result = service.CreatePatient(patientDTO);

            // Assert
            Assert.AreEqual(insertedPatient, result);
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        private bool PatientDataMatches(IPatient patient, PatientDTO patientDTO)
        {
            Assert.IsNotNull(patient);
            Assert.IsNotNull(patientDTO);

            Assert.AreEqual(patient.Id, patientDTO.Id);
            Assert.AreEqual(patient.FirstName, patientDTO.FirstName);
            Assert.AreEqual(patient.Surname, patientDTO.Surname);
            Assert.AreEqual(patient.DateOfBirth, patientDTO.DateOfBirth);
            Assert.AreEqual(patient.NHSNumber, patientDTO.NHSNumber);

            return true;
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        private PatientDTO CreatePatientDTO(IPatient patient)
        {
            return new PatientDTO
                       {
                           Id = patient.Id, 
                           FirstName = patient.FirstName, 
                           Surname = patient.Surname, 
                           DateOfBirth = patient.DateOfBirth, 
                           NHSNumber = patient.NHSNumber
                       };
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
            IPatientRepository patientRepository, 
            IMappingEngine mapper)
        {
            return new PatientService(unitOfWork, patientRepository, mapper);
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
