using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capsule.App_Start
{
    using System.Web.Http.Dependencies;

    using Unity;

    public class DependencyResolver : IDependencyResolver
    {
        private IUnityContainer container;
        public DependencyResolver(IUnityContainer _container)
        {
            if (_container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.container = _container;
        }
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            container.Dispose();
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new DependencyResolver(child);
        }
    }
}