﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

using StructureMap;

namespace Company.Module.Web.Host.IoC
{
    public class StructureMapContainer : StructureMapDependencyScope, IDependencyResolver
    {
        //// ----------------------------------------------------------------------------------------------------------

        public StructureMapContainer(IContainer container)
            : base(container)
        {
        }

        //// ----------------------------------------------------------------------------------------------------------

        public IDependencyScope BeginScope()
        {
            return new StructureMapDependencyScope(Container.GetNestedContainer());
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}