using System;
using System.Diagnostics.Contracts;
using System.Web.Http;
using System.Web.Http.Description;

using Company.Module.Application.AggregateRootServices;
using Company.Module.Domain;

namespace Company.Module.Web.Host.Controllers
{
    public class TestResultsController : ApiController
    {
        //// ----------------------------------------------------------------------------------------------------------

        private readonly IPatientService patientService;

        //// ----------------------------------------------------------------------------------------------------------

        public TestResultsController(IPatientService patientService)
        {
            Contract.Requires<ArgumentNullException>(patientService != null);

            this.patientService = patientService;
        }

        //// ----------------------------------------------------------------------------------------------------------

        // POST: api/TestResults
        [ResponseType(typeof(TestResult))]
        public IHttpActionResult PostTestResult(TestSpecifications testSpecifications)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var testResult = patientService.ProcessTest(testSpecifications);

            return CreatedAtRoute("DefaultApi", new { id = testResult.Id }, testResult);
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}