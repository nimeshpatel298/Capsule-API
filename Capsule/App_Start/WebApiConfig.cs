using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Capsule
{
    using Capsule.App_Start;
    using Capsule.Repository;

    using Unity;
    using Unity.Lifetime;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.EnableCors();
            var container = new UnityContainer();
            container.RegisterType<ITaskRepository, TaskRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new DependencyResolver(container);
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
