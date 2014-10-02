﻿using System;
using System.Linq;
using System.Web.Http;

using AutoMapper;

using Company.Module.Repositories.EntityFramework;
using Company.Module.Web.Host.IoC;

using StructureMap;

namespace Company.Module.Web.Host
{
    public class IocConfig
    {
        //// ----------------------------------------------------------------------------------------------------------

        public static void RegisterDependencyResolver(HttpConfiguration config)
        {
            var container = new Container(x =>
            {
                x.Scan(scan =>
                {
                    scan.WithDefaultConventions();

                    AppDomain.CurrentDomain.GetAssemblies()
                                           .Where(a => a.GetName().Name.StartsWith("Company.Module")).ToList()
                                           .ForEach(scan.Assembly);

                    x.For<IMappingEngine>().Use(Mapper.Engine);
                });
            });

            config.DependencyResolver = new StructureMapContainer(container);
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}