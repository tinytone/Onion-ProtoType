using System.Collections.Generic;

using Company.Module.Domain;

namespace Company.Module.Web.Host.Models
{
    public class PatientViewModel
    {
        //// ----------------------------------------------------------------------------------------------------------

        public IEnumerable<Patient> Patients { get; set; }

        //// ----------------------------------------------------------------------------------------------------------
    }
}