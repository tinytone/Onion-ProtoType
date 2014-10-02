using System;
using System.Diagnostics.Contracts;
using System.Web.Mvc;

using Company.Module.Repositories;
using Company.Module.Repositories.EntityFramework;

namespace Company.Module.Web.Host.Controllers
{
    public class HomeController : Controller
    {
        //// ----------------------------------------------------------------------------------------------------------

        private readonly IPatientRepository patientRepository;

        //// ----------------------------------------------------------------------------------------------------------

        public HomeController()
        {
            IContext context = new Context();
            IUnitOfWork unitOfWork = new UnitOfWork(context);

            this.patientRepository = new PatientRepository(unitOfWork);
        }

        //// ----------------------------------------------------------------------------------------------------------

        public HomeController(IPatientRepository patientRepository)
        {
            Contract.Requires<ArgumentNullException>(patientRepository != null);

            this.patientRepository = patientRepository;
        }


        //// ----------------------------------------------------------------------------------------------------------
		 
        public ActionResult Index()
        {
            var patients = this.patientRepository.GetAll();

            return View(patients);
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
