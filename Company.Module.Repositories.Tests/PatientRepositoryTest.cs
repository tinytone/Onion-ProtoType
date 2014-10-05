using System;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using Company.Module.Domain;
using Company.Module.Repositories.EntityFramework;

using NUnit.Framework;

using Rhino.Mocks;

namespace Company.Module.Repositories.Tests
{
    [TestFixture]
    public class PatientRepositoryTest
    {
        //// ----------------------------------------------------------------------------------------------------------
		 
        private MockRepository mocks;

        //// ----------------------------------------------------------------------------------------------------------

        [SetUp]
        public void TestInitialize()
        {
            this.mocks = new MockRepository();            
        }

        //// ----------------------------------------------------------------------------------------------------------

        [TearDown]
        public void TestCleanup()
        {
            this.mocks.VerifyAll();
        }

        //// ----------------------------------------------------------------------------------------------------------

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_UnitOfWorkIsNull_ExpectArgumentNullException()
        {
            // Arrange
            IUnitOfWork unitOfWork = null;

            this.mocks.ReplayAll();

            // Act
            GetRepository(unitOfWork);

            // Assert
        }

        //// ----------------------------------------------------------------------------------------------------------
        
        //[Test]
        //public void Constructor_AllDependanciesAreValid_ExpectInstanceWithDefaultState()
        //{
        //    // Arrange
        //    var context = this.mocks.StrictMock<IContext>();
        //    var unitOfWork = PrepareUnitOfWork(context);

        //    this.mocks.ReplayAll();

        //    // Act
        //    var repository = GetRepository(unitOfWork);

        //    // Assert
        //    Assert.IsNotNull(repository);
        //}
        
        //// ----------------------------------------------------------------------------------------------------------

        [Test]
        public void GetByNhsNumber_WithValidNumber_ExpectPatientInstance()
        {
            // Arrange
            const string NhsNumber = "123 123 1234";

            var patients = new[]
                               {
                                   new Patient { Id = 1, NHSNumber = "111 111 1111" },
                                   new Patient { Id = 2, NHSNumber = "222 222 2222" },
                                   new Patient { Id = 3, NHSNumber = "333 333 3333" },
                                   new Patient { Id = 4, NHSNumber = "123 123 1234" }
                               };

            var objectSet = new FakeObjectSet<Patient>(patients);

            var objectContext = this.mocks.StrictMock<ObjectContext>();
            Expect.Call(objectContext.CreateObjectSet<Patient>()).Return(objectSet);

            var contextAdapter = this.mocks.StrictMock<IObjectContextAdapter>();
            Expect.Call(contextAdapter.ObjectContext).Return(objectContext);

            this.mocks.ReplayAll();

            var repository = GetRepository(contextAdapter);

            // Act
            var result = repository.GetByNhsNumber(NhsNumber);

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        //// ----------------------------------------------------------------------------------------------------------

        private IPatientRepository GetRepository(IObjectContextAdapter contextAdapter)
        {
            return new PatientRepository(contextAdapter);
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
