using System;

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
        
        [Test]
        public void Constructor_AllDependanciesAreValid_ExpectInstanceWithDefaultState()
        {
            // Arrange
            var context = this.mocks.StrictMock<IContext>();
            var unitOfWork = PrepareUnitOfWork(context);

            this.mocks.ReplayAll();

            // Act
            var repository = GetRepository(unitOfWork);

            // Assert
            Assert.IsNotNull(repository);
        }
        /*
        //// ----------------------------------------------------------------------------------------------------------

        [Test]
        public void Get_WithValidNumber_ExpectPatientInstance()
        {
            // Arrange
            const string NhsNumber = "123 123 1234";

            //Patient[] patients = new[]
            //                   {
            //                       new Patient { Id = 1, NHSNumber = "111 111 1111" },
            //                       new Patient { Id = 2, NHSNumber = "222 222 2222" },
            //                       new Patient { Id = 3, NHSNumber = "333 333 3333" },
            //                       new Patient { Id = 4, NHSNumber = "123 123 1234" }
            //                   };

            var patients = new FakeDbSet<Patient>();

            //var dbset = this.mocks.StrictMock<DbSet<Patient>>();

            var stuff = this.mocks.StrictMock<FakeDbSet<Patient>>();
//            Expect.Call(stuff.All(null)).IgnoreArguments().Return(patient);
            
            var context = this.mocks.StrictMock<IMyContext>();
            Expect.Call(context.Patients).Return(patients);
            
            var unitOfWork = PrepareUnitOfWork(context);

            this.mocks.ReplayAll();

            var repository = GetRepository(unitOfWork);

            // Act
            var result = repository.Get(NhsNumber);

            // Assert
            Assert.That(result, Is.NotNull());
        }
        */
        //// ----------------------------------------------------------------------------------------------------------
		 
        private IUnitOfWork PrepareUnitOfWork(IContext context)
        {
            var unitOfWork = this.mocks.StrictMock<IUnitOfWork>();
            Expect.Call(unitOfWork.Context).Return(context).Repeat.AtLeastOnce();
            return unitOfWork;
        }
        //// ----------------------------------------------------------------------------------------------------------

        private IPatientRepository GetRepository(IUnitOfWork unitOfWork)
        {
            return new PatientRepository(unitOfWork);
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
