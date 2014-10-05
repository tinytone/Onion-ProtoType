using System.Data.Entity.Infrastructure;
using Company.Module.Domain;

namespace Company.Module.Repositories.EntityFramework
{
    public class TestResultRepository : GenericRepository<TestResult>, ITestResultRepository
    {
        //// ----------------------------------------------------------------------------------------------------------

        public TestResultRepository(IObjectContextAdapter contextAdapter)
            : base(contextAdapter)
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
