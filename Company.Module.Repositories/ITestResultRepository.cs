using Company.Module.Domain;

namespace Company.Module.Repositories
{
    public interface ITestResultRepository
    {
        //// ----------------------------------------------------------------------------------------------------------
		 
        TestResult GetById(int id);
        
        //// ----------------------------------------------------------------------------------------------------------

        TestResult Insert(TestResult testResult);

        //// ----------------------------------------------------------------------------------------------------------
    }
}