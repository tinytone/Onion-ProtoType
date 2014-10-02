using System;

using AutoMapper;

using Company.Module.Domain;
using Company.Module.Shared.DTO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Company.Module.Web.Host.Tests
{
    [TestClass]
    public class ObjectMappingConfigTest
    {
        //// ----------------------------------------------------------------------------------------------------------

        [TestInitialize]
        public void TestInitialize()
        {
            ObjectMappingConfig.Configure();
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestCleanup]
        public void TestCleanup()
        {
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        [TestMethod]
        public void ConfigureObjectMapper_ConfigurationShouldBeValid_ExpectValidConfiguration()
        {
            // Arrange

            // Act

            // Assert
            Mapper.AssertConfigurationIsValid();
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        public void Map_PatientToPatientDTO_ExpectValidPatientDTO()
        {
            // Arrange
            var patient = new Patient
                              {
                                  Id = 1,
                                  FirstName = "Tony",
                                  Surname = "Harding",
                                  DateOfBirth = DateTime.Parse("1978-01-06"),
                                  NHSNumber = "111 111 1111",
                                  RowVersion = new byte[] { 01, 02, 03, 04, 05, 06, 07 }
                              };

            // Act
            var patientDTO = Mapper.Map<Patient, PatientDTO>(patient);

            // Assert
            Assert.AreEqual(patientDTO.Id, patient.Id);
            Assert.AreEqual(patientDTO.FirstName, patient.FirstName);
            Assert.AreEqual(patientDTO.Surname, patient.Surname);
            Assert.AreEqual(patientDTO.DateOfBirth, patient.DateOfBirth);
            Assert.AreEqual(patientDTO.FullName, string.Format("{0} {1}", patient.FirstName, patient.Surname));
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        public void Map_PatientDTOToPatient_ExpectValidPatient()
        {
            // Arrange
            var patientDTO = new PatientDTO
            {
                Id = 1,
                FirstName = "Tony",
                Surname = "Harding",
                FullName = "Tony Harding",
                DateOfBirth = DateTime.Parse("1978-01-06"),
                NHSNumber = "111 111 1111",
            };

            // Act
            var patient = Mapper.Map<PatientDTO, Patient>(patientDTO);

            // Assert
            Assert.AreEqual(patient.Id, patientDTO.Id);
            Assert.AreEqual(patient.FirstName, patientDTO.FirstName);
            Assert.AreEqual(patient.Surname, patientDTO.Surname);
            Assert.AreEqual(patient.DateOfBirth, patientDTO.DateOfBirth);
            Assert.AreEqual(patient.RowVersion, default(byte[]));
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        public void Map_TestResultToTestResultDTO_ExpectValidTestResultDTO()
        {
            // Arrange
            var testResult = new TestResult
            {
                Id = 1,
                PatientId = 2,
                Outcome = true,
                TestDate = DateTime.Now,
                RowVersion = new byte[] { 01, 02, 03, 04, 05, 06, 07 }
            };

            // Act
            var testResultDTO = Mapper.Map<TestResult, TestResultDTO>(testResult);

            // Assert
            Assert.AreEqual(testResultDTO.Id, testResult.Id);
            Assert.AreEqual(testResultDTO.PatientId, testResult.PatientId);
            Assert.AreEqual(testResultDTO.Outcome, testResult.Outcome);
            Assert.AreEqual(testResultDTO.TestDate, testResult.TestDate);
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TestMethod]
        public void Map_TestResultDTOToTestResult_ExpectValidTestResult()
        {
            // Arrange
            var testResultDTO = new TestResultDTO
            {
                Id = 1,
                PatientId = 2,
                Outcome = true,
                TestDate = DateTime.Now
            };

            // Act
            var testResult = Mapper.Map<TestResultDTO, TestResult>(testResultDTO);

            // Assert
            Assert.AreEqual(testResult.Id, testResultDTO.Id);
            Assert.AreEqual(testResult.PatientId, testResultDTO.PatientId);
            Assert.AreEqual(testResult.Outcome, testResultDTO.Outcome);
            Assert.AreEqual(testResult.TestDate, testResultDTO.TestDate);
            Assert.AreEqual(testResult.RowVersion, default(byte[]));
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
