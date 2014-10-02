using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

using StructureMap;

namespace Company.Module.Web.Host.IoC
{
    public class StructureMapDependencyScope : IDependencyScope
    {
        //// ----------------------------------------------------------------------------------------------------------

        protected readonly IContainer Container;

        //// ----------------------------------------------------------------------------------------------------------

        public StructureMapDependencyScope(IContainer container)
        {
            this.Container = container;
        }

        //// ----------------------------------------------------------------------------------------------------------

        public object GetService(Type serviceType)
        {
            var isConcrete = !serviceType.IsAbstract && !serviceType.IsInterface;

            return isConcrete ? this.Container.GetInstance(serviceType) : this.Container.TryGetInstance(serviceType);
        }

        //// ----------------------------------------------------------------------------------------------------------

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.Container.GetAllInstances<object>().Where(s => s.GetType() == serviceType);
        }

        //// ----------------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            if (this.Container != null)
                this.Container.Dispose();
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}