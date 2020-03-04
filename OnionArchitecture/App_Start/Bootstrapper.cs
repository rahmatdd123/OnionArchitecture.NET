using Autofac;
using Autofac.Integration.Mvc;
using OnionArchitecture.Core.Interfaces.DomainServices.Repositories;
using OnionArchitecture.Core.Services;
using OnionArchitecture.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac.Integration.WebApi;
using System.Web.Http;

namespace OnionArchitecture.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(TodoRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(TodoService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest();

            //builder.RegisterType<Authentication>().As<IAuthentication>().InstancePerRequest();
            //builder.RegisterType<EmailEngineExchange>().As<IEmailEngine>().InstancePerRequest();

            //builder.RegisterAssemblyTypes(typeof(BasicAuthentication).Assembly)
            //    .Where(t => t.Name.EndsWith("Authentication"))
            //    .AsImplementedInterfaces().InstancePerRequest();

            IContainer container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

    }
}