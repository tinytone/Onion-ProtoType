using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;

using AutoMapper;
using Company.Module.Repositories.EntityFramework;
using Company.Module.Web.Host.IoC;

using StructureMap;
using StructureMap.Web.Pipeline;

namespace Company.Module.Web.Host
{
    public class IocConfig
    {
        //// ----------------------------------------------------------------------------------------------------------

        public static IContainer RegisterDependencyResolver(HttpConfiguration config)
        {
            var container = new Container(x => x.Scan(scan =>
                                                      {
                                                          scan.WithDefaultConventions();

                                                          AppDomain.CurrentDomain.GetAssemblies()
                                                              .Where(a => a.GetName().Name.StartsWith("Company.Module"))
                                                              .ToList()
                                                              .ForEach(scan.Assembly);

                                                          x.For<IMappingEngine>().Use(Mapper.Engine);
                                                          x.For<IObjectContextAdapter>()
                                                              .Use<Context>()
                                                              .LifecycleIs<HttpContextLifecycle>();
                                                      }));

            // container.Configure(x => x.For<IRepository>().Use<Repository>().Ctor<DbContext>().Is(dataContext));
            // container.Configure(x => x.For<IUnitOfWork>().Use<UnitOfWork>());

            config.DependencyResolver = new StructureMapContainer(container);

            return container;
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}