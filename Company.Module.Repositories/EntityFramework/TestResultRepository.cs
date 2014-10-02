using Company.Module.Domain;

namespace Company.Module.Repositories.EntityFramework
{
    public class TestResultRepository : GenericRepository<TestResult>, ITestResultRepository
    {
        //// ----------------------------------------------------------------------------------------------------------

        public TestResultRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public TestResult GetById(int id)
        {
            return Get(id);
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public TestResult Insert(TestResult testResult)
        {
            return Add(testResult);
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
