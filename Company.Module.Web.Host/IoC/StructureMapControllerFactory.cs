using System;
using System.Web.Mvc;
using System.Web.Routing;

using StructureMap;

namespace Company.Module.Web.Host.IoC
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        //// ----------------------------------------------------------------------------------------------------------
		 
        private readonly IContainer container;

        //// ----------------------------------------------------------------------------------------------------------

        public StructureMapControllerFactory(IContainer container)
        {
            this.container = container;
        }

        //// ----------------------------------------------------------------------------------------------------------

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            try
            {
                return (IController)container.GetInstance(controllerType);
            }
            catch (StructureMapException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}