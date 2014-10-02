using System.Diagnostics.Contracts;

using Company.Module.Domain.Interfaces;
using Company.Module.Shared.DTO;

namespace Company.Module.Application.AggregateRootServices
{
    [ContractClass(typeof(TestResultServiceContract))]
    public interface ITestResultService
    {
        //// ----------------------------------------------------------------------------------------------------------
		 
        TestResultDTO GetId(int id);

        //// ----------------------------------------------------------------------------------------------------------

        TestResultDTO ProcessTest(ITestSpecifications testSpecifications);

        //// ----------------------------------------------------------------------------------------------------------
    }
}