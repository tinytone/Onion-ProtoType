using System;
using System.ComponentModel.DataAnnotations;

using Company.Module.Domain.Interfaces;

namespace Company.Module.Domain
{
    public class Patient : IPatient
    {
        //// ----------------------------------------------------------------------------------------------------------

        [Key]
        public int Id { get; set; }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public string NHSNumber { get; set; }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public string FirstName { get; set; }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public string Surname { get; set; }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public DateTime DateOfBirth { get; set; }

        //// ----------------------------------------------------------------------------------------------------------

        [Timestamp]
        public byte[] RowVersion { get; set; }

        //// ----------------------------------------------------------------------------------------------------------

        public ITestResult PerformTest(ITestSpecifications testSpecifications)
        {
            foreach (var testCondition in testSpecifications.TestConditions)
            {
                Console.WriteLine("Processing '{0}' of '{1}' for Patient '{2}'.", testCondition.Key, testCondition.Value, this.NHSNumber);
            }

            // Assume the tests pass based on the fake testConditions
            var outcome = true;

            return new TestResult(this, outcome);
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
