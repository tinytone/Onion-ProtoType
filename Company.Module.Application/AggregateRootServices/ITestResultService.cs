using Company.Module.Domain.Interfaces;
using Company.Module.Shared.DTO;

namespace Company.Module.Application.AggregateRootServices
{
    public interface ITestResultService
    {
        //// ----------------------------------------------------------------------------------------------------------
		 
        TestResultDTO GetId(int id);

        //// ----------------------------------------------------------------------------------------------------------

        TestResultDTO ProcessTest(ITestSpecifications testSpecifications);

        //// ----------------------------------------------------------------------------------------------------------
    }
}