using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Company.Module.Web.Host.IoC;

namespace Company.Module.Web.Host
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        //// ----------------------------------------------------------------------------------------------------------
		 
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ObjectMappingConfig.Configure();

            var container = IocConfig.RegisterDependencyResolver(GlobalConfiguration.Configuration);
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory(container));
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
