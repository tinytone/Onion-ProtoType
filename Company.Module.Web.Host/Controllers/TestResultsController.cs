using System;
using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

using Company.Module.Application.AggregateRootServices;
using Company.Module.Domain;
using Company.Module.Shared.DTO;

namespace Company.Module.Web.Host.Controllers
{
    public class TestResultsController : ApiController
    {
        //// ----------------------------------------------------------------------------------------------------------

        private readonly ITestResultService testResultService;

        //// ----------------------------------------------------------------------------------------------------------

        public TestResultsController(ITestResultService testResultService)
        {
            Contract.Requires<ArgumentNullException>(testResultService != null);

            this.testResultService = testResultService;
        }

        //// ----------------------------------------------------------------------------------------------------------

        // GET: api/TestResults/5
        [ResponseType(typeof(TestResultDTO))]
        public IHttpActionResult Get(int id)
        {
            var testResultDTO = this.testResultService.GetId(id);

            if (NotFound(testResultDTO))
                TestResultNotFoundException();

            return Ok(testResultDTO);
        }

        //// ----------------------------------------------------------------------------------------------------------

        private bool NotFound(TestResultDTO testResultDTO)
        {
            return testResultDTO == null;
        }

        //// ----------------------------------------------------------------------------------------------------------

        private void TestResultNotFoundException()
        {
            var response = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Test Result not found");
            throw new HttpResponseException(response);
        }

        //// ----------------------------------------------------------------------------------------------------------

        // POST: api/TestResults
        [ResponseType(typeof(TestResultDTO))]
        public IHttpActionResult PostTestResult(TestSpecifications testSpecifications)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var testResult = this.testResultService.ProcessTest(testSpecifications);

            return CreatedAtRoute("DefaultApi", new { id = testResult.Id }, testResult);
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}