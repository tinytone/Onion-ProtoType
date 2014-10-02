using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

using Company.Module.Application.AggregateRootServices;
using Company.Module.Shared.DTO;

namespace Company.Module.Web.Host.Controllers
{
    public class PatientController : ApiController
    {
        //// ----------------------------------------------------------------------------------------------------------

        private readonly IPatientService patientService;

        //// ----------------------------------------------------------------------------------------------------------

        public PatientController(IPatientService patientService)
        {
            Contract.Requires<ArgumentNullException>(patientService != null);

            this.patientService = patientService;
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        // GET: api/Patient
        public IEnumerable<PatientDTO> Get()
        {
            return this.patientService.GetAll();
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        // GET: api/Patient/5
        [ResponseType(typeof(PatientDTO))]
        [Route("api/patient/{id}", Order = 1)]
        public IHttpActionResult GetByPatientId(int id)
        {
            var patientDTO = this.patientService.GetByPatientId(id);

            if (NotFound(patientDTO))
                PatientNotFoundException();

            return Ok(patientDTO);
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        private bool NotFound(PatientDTO patientDTO)
        {
            return patientDTO == null;
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        private void PatientNotFoundException()
        {
            var response = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Patient not found");
            throw new HttpResponseException(response);
        }

        //// ----------------------------------------------------------------------------------------------------------

        [ResponseType(typeof(PatientDTO))]
        [Route("api/patient/NHSNumber/{nhsNumber}", Order = 2)]
        public IHttpActionResult GetByNhsNumber(string nhsNumber)
        {
            var patientDTO = this.patientService.GetByNhsNumber(nhsNumber);

            if (NotFound(patientDTO))
                PatientNotFoundException();

            return Ok(patientDTO);
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        // POST: api/Patient
        public HttpResponseMessage Post([FromBody]PatientDTO patientDTO)
        {
            var patient = this.patientService.CreatePatient(patientDTO);

            if (patient.Id > 0)
            {
                var response = Request.CreateResponse(HttpStatusCode.Created, patient);

                var uri = Url.Link("DefaultApi", new { id = patient.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }

            var errorResponse = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Patient creation was unsuccessful!");
            throw new HttpResponseException(errorResponse);
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
