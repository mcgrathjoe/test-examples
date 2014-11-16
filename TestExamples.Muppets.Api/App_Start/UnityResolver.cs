namespace TestExamples.Muppets.Api
{
  using System;
  using System.Collections.Generic;
  using System.Web.Http.Dependencies;
  using Microsoft.Practices.Unity;

  public class UnityResolver : IDependencyResolver
  {
    public UnityResolver(IUnityContainer container)
    {
      if (container == null)
      {
        throw new ArgumentNullException("container");
      }
      
      Container = container;
    }

    public IUnityContainer Container { get; private set; }

    public object GetService(Type serviceType)
    {
      try
      {
        return Container.Resolve(serviceType);
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
        return Container.ResolveAll(serviceType);
      }
      catch (ResolutionFailedException)
      {
        return new List<object>();
      }
    }

    public IDependencyScope BeginScope()
    {
      var child = Container.CreateChildContainer();
      return new UnityResolver(child);
    }

    public void Dispose()
    {
      Container.Dispose();
    }
  }
}