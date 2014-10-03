using System;
using System.Diagnostics.Contracts;
using System.Web.Mvc;

using Company.Module.Application.AggregateRootServices;
using Company.Module.Web.Host.Models;

namespace Company.Module.Web.Host.Controllers
{
    public class HomeController : Controller
    {
        //// ----------------------------------------------------------------------------------------------------------

        private readonly IPatientService patientService;

        //// ----------------------------------------------------------------------------------------------------------

        public HomeController(IPatientService patientService)
        {
            Contract.Requires<ArgumentNullException>(patientService != null);

            this.patientService = patientService;
        }


        //// ----------------------------------------------------------------------------------------------------------
		 
        public ActionResult Index()
        {
            var patients = this.patientService.GetAll();

            var viewModel = new PatientViewModel { Patients = patients };

            return View(viewModel);
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
