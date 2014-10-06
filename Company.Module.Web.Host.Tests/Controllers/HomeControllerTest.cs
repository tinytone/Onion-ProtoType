using System;
using System.Collections.Generic;
using System.Web.Mvc;

using AutoMapper;

using Company.Module.Application.AggregateRootServices;
using Company.Module.Domain;
using Company.Module.Domain.Interfaces;
using Company.Module.Repositories;
using Company.Module.Web.Host.Controllers;
using Company.Module.Web.Host.Controllers.MVC;
using Company.Module.Web.Host.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Rhino.Mocks;

namespace Company.Module.Web.Host.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
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
        public void Constructor_PatientServiceIsNull_ExpectArgumentNullException()
        {
            // Arrange
            IPatientService patientService = null;
            
            this.mocks.ReplayAll();

            // Act
            GetHomeController(patientService);

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
            var controller = GetHomeController(patientService);

            // Assert
            Assert.IsNotNull(controller);
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        [TestMethod]
        public void Index_CallsRepository_ExpectCorrectModelData()
        {
            // Arrange
            var patient1 = new Patient { FirstName = "Joe", Surname = "Blogs", Id = 1 };
            var patient2 = new Patient { FirstName = "Sue", Surname = "White", Id = 2 };
            var patient3 = new Patient { FirstName = "Adam", Surname = "Black", Id = 3 };

            var patients = new List<Patient> { patient1, patient2, patient3 };

            var patientService = this.mocks.StrictMock<IPatientService>();
            Expect.Call(patientService.GetAll()).Return(patients);

            this.mocks.ReplayAll();

            var controller = GetHomeController(patientService);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);

            var viewModel = result.Model as PatientViewModel;
            Assert.IsNotNull(viewModel);
            Assert.AreEqual(viewModel.Patients, patients);
        }

        //// ----------------------------------------------------------------------------------------------------------

        private HomeController GetHomeController(IPatientService patientService)
        {
            return new HomeController(patientService);
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
