using Company.Module.Domain;

namespace Company.Module.Repositories
{
    public class TestResultGenericRepository : GenericRepository<TestResult>
    {
        //// ----------------------------------------------------------------------------------------------------------

        public TestResultGenericRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
