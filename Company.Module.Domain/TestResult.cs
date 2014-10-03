using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Company.Module.Domain.Interfaces;

namespace Company.Module.Domain
{
    public sealed class TestResult : IIdentifiable
    {
        //// ----------------------------------------------------------------------------------------------------------

        [Key]
        public int Id { get; set; }

        //// ----------------------------------------------------------------------------------------------------------

        [ForeignKey("Patient")]
        public int PatientId { get; set; }

        //// ----------------------------------------------------------------------------------------------------------

        public Patient Patient { get; set; }

        //// ----------------------------------------------------------------------------------------------------------

        public bool Outcome { get; set; }

        //// ----------------------------------------------------------------------------------------------------------

        public DateTime TestDate { get; set; }

        //// ----------------------------------------------------------------------------------------------------------

        [Timestamp]
        public byte[] RowVersion { get; set; }

        //// ----------------------------------------------------------------------------------------------------------

        public TestResult()
        {
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        public TestResult(Patient patient, bool outcome)
        {
            this.PatientId = patient.Id;
            this.Patient = patient;
            this.Outcome = outcome;
            this.TestDate = DateTime.Now;
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
