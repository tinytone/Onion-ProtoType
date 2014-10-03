using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

using AutoMapper;

using Company.Module.Application.AggregateRootServices;
using Company.Module.Domain;
using Company.Module.Domain.Interfaces;
using Company.Module.Shared.DTO;
using Company.Module.Web.Host.Controllers;
using Company.Module.Web.Host.Tests.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Rhino.Mocks;

namespace Company.Module.Web.Host.Tests.Controllers
{
    [TestClass]
    public class PatientControllerTest
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
        public void Constructor_IPatientServiceIsNull_ExpectArgumentNullException()
        {
            // Arrange
            IPatientService patientService = null;
            var mappingEngine = this.mocks.StrictMock<IMappingEngine>();

            this.mocks.ReplayAll();

            // Act
            GetPatientController(patientService, mappingEngine);

            // Assert
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_MappingEngineIsNull_ExpectArgumentNullException()
        {
            // Arrange
            var patientService = this.mocks.StrictMock<IPatientService>();
            IMappingEngine mappingEngine = null;

            this.mocks.ReplayAll();

            // Act
            GetPatientController(patientService, mappingEngine);

            // Assert
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        public void Constructor_AllDependanciesAreValid_ExpectInstance()
        {
            // Arrange
            var patientService = this.mocks.StrictMock<IPatientService>();
            var mappingEngine = this.mocks.StrictMock<IMappingEngine>();

            this.mocks.ReplayAll();

            // Act
            var controller = GetPatientController(patientService, mappingEngine);

            // Assert
            Assert.IsNotNull(controller);
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        public void Get_PatientsSuccessfullyRetrieved_ExpectPatientDTOs()
        {
            // Arrange
            var patient1 = new Patient { FirstName = "Joe", Surname = "Blogs", Id = 1 };
            var patient2 = new Patient { FirstName = "Sue", Surname = "White", Id = 2 };
            var patient3 = new Patient { FirstName = "Adam", Surname = "Black", Id = 3 };

            var patients = new List<Patient> { patient1, patient2, patient3 };

            var patientService = this.mocks.StrictMock<IPatientService>();
            Expect.Call(patientService.GetAll()).Return(patients);

            var mappedPatients = new List<PatientDTO>
                                     {
                                         CreatePatientDTO(patient1),
                                         CreatePatientDTO(patient2),
                                         CreatePatientDTO(patient3),
                                     };

            var mappingEngine = this.mocks.StrictMock<IMappingEngine>();
            Expect.Call(mappingEngine.Map<IEnumerable<Patient>, IEnumerable<PatientDTO>>(patients)).Return(mappedPatients);

            this.mocks.ReplayAll();

            var controller = GetPatientController(patientService, mappingEngine);

            // Act
            var result = controller.Get() as OkNegotiatedContentResult<IEnumerable<PatientDTO>>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(mappedPatients, result.Content);
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        public void GetByPatientId_PatientSuccessfullyRetrieved_ExpectPatientDTO()
        {
            // Arrange
            const int PatientId = 1;

            var patient = new Patient { FirstName = "Joe", Surname = "Blogs", Id = PatientId };

            var patientService = this.mocks.StrictMock<IPatientService>();
            Expect.Call(patientService.GetByPatientId(PatientId)).Return(patient);

            var mappedPatient = CreatePatientDTO(patient);

            var mappingEngine = this.mocks.StrictMock<IMappingEngine>();
            Expect.Call(mappingEngine.Map<Patient, PatientDTO>(patient)).Return(mappedPatient);

            this.mocks.ReplayAll();

            var controller = GetPatientController(patientService, mappingEngine);

            // Act
            var result = controller.GetByPatientId(PatientId) as OkNegotiatedContentResult<PatientDTO>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(mappedPatient, result.Content);
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void GetByPatientId_PatientUnsuccessfullyRetrieved_ExpectNotFoundHttpStatusCode()
        {
            // Arrange
            const int PatientId = 1;

            Patient patient = null;

            var patientService = this.mocks.StrictMock<IPatientService>();
            Expect.Call(patientService.GetByPatientId(PatientId)).Return(patient);

            var mappingEngine = this.mocks.StrictMock<IMappingEngine>();

            this.mocks.ReplayAll();

            var controller = GetPatientController(patientService, mappingEngine);
            controller.EnsureNotNull();

            try
            {
                // Act
                controller.GetByPatientId(PatientId);
            }
            catch (HttpResponseException ex)
            {
                // Assert
                Assert.AreEqual(HttpStatusCode.NotFound, ex.Response.StatusCode);

                throw;
            }
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        public void GetByNhsNumber_PatientSuccessfullyRetrieved_ExpectPatientDTO()
        {
            // Arrange
            const string NhsNumber = "123 456 7890";

            var patient = new Patient { FirstName = "Joe", Surname = "Blogs", Id = 1, NHSNumber = NhsNumber };

            var patientService = this.mocks.StrictMock<IPatientService>();
            Expect.Call(patientService.GetByNhsNumber(NhsNumber)).Return(patient);

            var mappedPatient = CreatePatientDTO(patient);

            var mappingEngine = this.mocks.StrictMock<IMappingEngine>();
            Expect.Call(mappingEngine.Map<Patient, PatientDTO>(patient)).Return(mappedPatient);

            this.mocks.ReplayAll();

            var controller = GetPatientController(patientService, mappingEngine);

            // Act
            var result = controller.GetByNhsNumber(NhsNumber) as OkNegotiatedContentResult<PatientDTO>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(mappedPatient, result.Content);
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void GetByNhsNumber_PatientUnsuccessfullyRetrieved_ExpectNotFoundHttpStatusCode()
        {
            // Arrange
            const string NhsNumber = "123 456 7890";

            Patient patient = null;

            var patientService = this.mocks.StrictMock<IPatientService>();
            Expect.Call(patientService.GetByNhsNumber(NhsNumber)).Return(patient);

            var mappingEngine = this.mocks.StrictMock<IMappingEngine>();

            this.mocks.ReplayAll();

            var controller = GetPatientController(patientService, mappingEngine);
            controller.EnsureNotNull();

            try
            {
                // Act
                controller.GetByNhsNumber(NhsNumber);
            }
            catch (HttpResponseException ex)
            {
                // Assert
                Assert.AreEqual(HttpStatusCode.NotFound, ex.Response.StatusCode);

                throw;
            }
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        public void Post_PatientSuccessfullyCreated_ExpectCorrectLocationHeaderWithPatientId()
        {
            // Arrange
            const int Id = 10;
            const string FirstName = "Joe";
            const string Surname = "Blogs";
            const string NhsNumber = "123 456 7890";
            const string RequestUri = "http://localhost:8086/api/patient/";

            var dateOfBirth = DateTime.Parse("2000-01-01");

            var patientDTO = new PatientDTO { FirstName = FirstName, Surname = Surname, NHSNumber = NhsNumber, DateOfBirth = dateOfBirth };
            var patient = new Patient { Id = 0, FirstName = FirstName, Surname = Surname, NHSNumber = NhsNumber, DateOfBirth = dateOfBirth };
            var insertedPatient = new Patient { Id = Id, FirstName = FirstName, Surname = Surname, NHSNumber = NhsNumber, DateOfBirth = dateOfBirth };

            var patientService = this.mocks.StrictMock<IPatientService>();
            Expect.Call(patientService.CreatePatient(patient)).Return(insertedPatient);

            var mappingEngine = this.mocks.StrictMock<IMappingEngine>();
            Expect.Call(mappingEngine.Map<PatientDTO, Patient>(patientDTO)).Return(patient);

            this.mocks.ReplayAll();

            var controller = GetPatientController(patientService, mappingEngine);
            controller.SetRequest("patient", HttpMethod.Post, RequestUri);

            // Act
            var response = controller.Post(patientDTO);

            // Assert
            var uriForNewPatient = new Uri(new Uri(RequestUri), Id.ToString(CultureInfo.InvariantCulture));

            Assert.IsNotNull(response);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Assert.AreEqual(response.Headers.Location, uriForNewPatient);
        }

        //// ----------------------------------------------------------------------------------------------------------

        private PatientDTO CreatePatientDTO(Patient patient)
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

        ////// ----------------------------------------------------------------------------------------------------------

        //private PatientController GetPatientController(
        //    Lazy<IPatientLookupById> patientByIdLookup = null, 
        //    IPatientService patientService = null)
        //{
        //    if (patientByIdLookup == null)
        //        patientByIdLookup = this.mocks.GenerateLazyMock<IPatientLookupById>();
        //    if (patientService == null)
        //        patientService = MockRepository.GenerateMock<IPatientService>();

        //    return new PatientController(patientByIdLookup, patientService);
        //}

        //// ----------------------------------------------------------------------------------------------------------

        private PatientController GetPatientController(
            IPatientService patientService, 
            IMappingEngine mappingEngine)
        {
            return new PatientController(patientService, mappingEngine);
        }

        //// ----------------------------------------------------------------------------------------------------------
    }

}
