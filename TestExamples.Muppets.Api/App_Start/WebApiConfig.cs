using System.Web.Http;
using Microsoft.Practices.Unity;
using Raven.Client;
using Raven.Client.Document;

namespace TestExamples.Muppets.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
          config.MapHttpAttributeRoutes();

          var unityContainer = new UnityContainer();
          unityContainer.RegisterInstance<IDocumentStore>(
            new DocumentStore
            {
              Url = "http://localhost:8080/",
              DefaultDatabase = "Muppets"
            });

          config.DependencyResolver = 
            new UnityResolver(unityContainer);
        }
    }
}
