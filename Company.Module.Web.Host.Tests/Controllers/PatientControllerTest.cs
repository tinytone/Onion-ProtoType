using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.UI;

using Company.Module.Application.AggregateRootServices;
using Company.Module.Domain;
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

            // Act
            GetPatientController(patientService);

            // Assert
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        public void Constructor_AllDependanciesAreValid_ExpectInstance()
        {
            // Arrange
            var patientService = this.mocks.StrictMock<IPatientService>();

            this.mocks.ReplayAll();

            // Act
            var controller = GetPatientController(patientService);

            // Assert
            Assert.IsNotNull(controller);
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        public void GetByPatientId_PatientSuccessfullyRetrieved_ExpectPatientDTO()
        {
            // Arrange
            const int PatientId = 1;

            var patientDTO = new PatientDTO { FirstName = "Joe", Surname = "Blogs", Id = PatientId };

            var patientService = this.mocks.StrictMock<IPatientService>();
            Expect.Call(patientService.GetByPatientId(PatientId)).Return(patientDTO);

            this.mocks.ReplayAll();

            var controller = GetPatientController(patientService);

            // Act
            var result = controller.GetByPatientId(PatientId) as OkNegotiatedContentResult<PatientDTO>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(patientDTO, result.Content);
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void GetByPatientId_PatientUnsuccessfullyRetrieved_ExpectPatientDTO()
        {
            // Arrange
            const int PatientId = 1;

            PatientDTO patientDTO = null;

            var patientService = this.mocks.StrictMock<IPatientService>();
            Expect.Call(patientService.GetByPatientId(PatientId)).Return(patientDTO);

            this.mocks.ReplayAll();

            var controller = GetPatientController(patientService);
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

            var patientDTO = new PatientDTO { FirstName = "Joe", Surname = "Blogs", Id = 1, NHSNumber = NhsNumber };

            var patientService = this.mocks.StrictMock<IPatientService>();
            Expect.Call(patientService.GetByNhsNumber(NhsNumber)).Return(patientDTO);

            this.mocks.ReplayAll();

            var controller = GetPatientController(patientService);

            // Act
            var result = controller.GetByNhsNumber(NhsNumber) as OkNegotiatedContentResult<PatientDTO>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(patientDTO, result.Content);
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void GetByNhsNumber_PatientUnsuccessfullyRetrieved_ExpectPatientDTO()
        {
            // Arrange
            const string NhsNumber = "123 456 7890";

            PatientDTO patientDTO = null;

            var patientService = this.mocks.StrictMock<IPatientService>();
            Expect.Call(patientService.GetByNhsNumber(NhsNumber)).Return(patientDTO);

            this.mocks.ReplayAll();

            var controller = GetPatientController(patientService);
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
        public void Post_PatientSuccessfullyCreated_ExpectPatientDTO()
        {
            // Arrange
            const int Id = 10;
            const string NhsNumber = "123 456 7890";
            const string RequestUri = "http://localhost:8086/api/patient/";

            var patientDTO = new PatientDTO { FirstName = "Joe", Surname = "Blogs", NHSNumber = NhsNumber, DateOfBirth = DateTime.Parse("2000-01-01") };
            var patient = new Patient { Id = Id, FirstName = "Joe", Surname = "Blogs", NHSNumber = NhsNumber, DateOfBirth = DateTime.Parse("2000-01-01") }; 

            var patientService = this.mocks.StrictMock<IPatientService>();
            Expect.Call(patientService.CreatePatient(patientDTO)).Return(patient);

            this.mocks.ReplayAll();

            var controller = GetPatientController(patientService);
            controller.SetRequest("Patient", HttpMethod.Post, RequestUri);

            // Act
            var response = controller.Post(patientDTO);

            // Assert
            var uriForNewPatient = new Uri(new Uri(RequestUri), Id.ToString(CultureInfo.InvariantCulture));

            Assert.IsNotNull(response);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Assert.AreEqual(response.Headers.Location, uriForNewPatient);
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

        private PatientController GetPatientController(IPatientService patientService)
        {
            return new PatientController(patientService);
        }

        //// ----------------------------------------------------------------------------------------------------------
    }

}
