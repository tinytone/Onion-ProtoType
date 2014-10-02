using System;
using System.Diagnostics.Contracts;

using Company.Module.Domain.Interfaces;
using Company.Module.Shared.DTO;

namespace Company.Module.Application.AggregateRootServices
{
    [ContractClassFor(typeof(ITestResultService))]
    internal abstract class TestResultServiceContract : ITestResultService
    {
        //// ----------------------------------------------------------------------------------------------------------
		 
        public TestResultDTO GetId(int id)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id > 0);

            throw new NotImplementedException("This class is only used to provide contract requirements for ITestResultService.");
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public TestResultDTO ProcessTest(ITestSpecifications testSpecifications)
        {
            Contract.Requires<ArgumentNullException>(testSpecifications != null);

            throw new NotImplementedException("This class is only used to provide contract requirements for ITestResultService.");
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
