using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

using AutoMapper;

using Company.Module.Application.AggregateRootServices;
using Company.Module.Domain;
using Company.Module.Shared.DTO;

namespace Company.Module.Web.Host.Controllers.WebApi
{
    public class PatientController : ApiController
    {
        //// ----------------------------------------------------------------------------------------------------------

        private readonly IPatientService patientService;
        private readonly IMappingEngine mapper;

        //// ----------------------------------------------------------------------------------------------------------

        public PatientController(
            IPatientService patientService, 
            IMappingEngine mapper)
        {
            Contract.Requires<ArgumentNullException>(patientService != null);
            Contract.Requires<ArgumentNullException>(mapper != null);

            this.patientService = patientService;
            this.mapper = mapper;
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        // GET: api/Patient
        [ResponseType(typeof(IEnumerable<PatientDTO>))]
        public IHttpActionResult Get()
        {
            var patients = this.patientService.GetAll();

            var patientDtos = this.mapper.Map<IEnumerable<Patient>, IEnumerable<PatientDTO>>(patients);

            return Ok(patientDtos);
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        // GET: api/Patient/5
        [ResponseType(typeof(PatientDTO))]
        [Route("api/patient/{id}", Order = 1)]
        public IHttpActionResult GetByPatientId(int id)
        {
            var patient = this.patientService.GetByPatientId(id);

            if (NotFound(patient))
                PatientNotFoundException();

            var patientDTO = this.mapper.Map<Patient, PatientDTO>(patient);

            return Ok(patientDTO);
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        private bool NotFound(Patient patient)
        {
            return patient == null;
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
            var patient = this.patientService.GetByNhsNumber(nhsNumber);

            if (NotFound(patient))
                PatientNotFoundException();

            var patientDTO = this.mapper.Map<Patient, PatientDTO>(patient);

            return Ok(patientDTO);
        }

        //// ----------------------------------------------------------------------------------------------------------
		 
        // POST: api/Patient
        public HttpResponseMessage Post([FromBody]PatientDTO patientDTO)
        {
            var patient = this.mapper.Map<PatientDTO, Patient>(patientDTO);

            var insertedPatient = this.patientService.CreatePatient(patient);

            if (insertedPatient.Id > 0)
            {
                var response = Request.CreateResponse(HttpStatusCode.Created, insertedPatient);

                var uri = Url.Link("DefaultApi", new { id = insertedPatient.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }

            var errorResponse = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Patient creation was unsuccessful!");
            throw new HttpResponseException(errorResponse);
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
