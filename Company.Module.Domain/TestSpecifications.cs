using System.Collections.Generic;

using Company.Module.Domain.Interfaces;

namespace Company.Module.Domain
{
    public class TestSpecifications : ITestSpecifications
    {
        //// ----------------------------------------------------------------------------------------------------------

        public string NhsNumber { get; set; }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public IDictionary<string, string> TestConditions { get; set; }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
