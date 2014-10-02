using System.Collections.Generic;

namespace Company.Module.Domain.Interfaces
{
    public interface ITestSpecifications
    {
        //// ----------------------------------------------------------------------------------------------------------

        string NhsNumber { get; set; }

        //// ----------------------------------------------------------------------------------------------------------

        IDictionary<string, string> TestConditions { get; set; }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
