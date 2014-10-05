using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using Company.Module.Domain;
using Company.Module.Repositories.EntityFramework;

namespace Company.Module.Repositories.Configuration
{
    //public class PatientDataInitializer : DropCreateDatabaseAlways<Context>
    //{
    //    //// ----------------------------------------------------------------------------------------------------------

    //    protected override void Seed(Context context)
    //    {
    //        var patients = SeedPatients(context);

    //        SeedTestResults(context, patients);
    //    }

    //    //// ----------------------------------------------------------------------------------------------------------
		 
    //    private static List<Patient> SeedPatients(Context context)
    //    {
    //        var patients = new List<Patient>
    //                           {
    //                               new Patient { FirstName = "Tony", Surname = "Harding", DateOfBirth = DateTime.Parse("1978-01-06"), NHSNumber = "123 123 1234" },
    //                               new Patient { FirstName = "Nicola", Surname = "Spurdens", DateOfBirth = DateTime.Parse("1984-04-22"), NHSNumber = "222 333 4444" }
    //                           };

    //        patients.ForEach(p => context.Patients.Add(p));
    //        context.SaveChanges();
    //        return patients;
    //    }

    //    //// ----------------------------------------------------------------------------------------------------------

    //    private static void SeedTestResults(Context context, List<Patient> patients)
    //    {
    //        var testResults = new List<TestResult>
    //                              {
    //                                  new TestResult { Patient = patients.First(), Outcome = true, TestDate = DateTime.Now },
    //                                  new TestResult { Patient = patients.Last(), Outcome = false, TestDate = DateTime.Now.Subtract(new TimeSpan(3, 0, 0)) }
    //                              };

    //        testResults.ForEach(tr => context.TestResults.Add(tr));
    //        context.SaveChanges();
    //    }

    //    //// ----------------------------------------------------------------------------------------------------------
    //}
}
