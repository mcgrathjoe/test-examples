using System.Net.Http;
using System.Threading;
using System.Web.Http;
using Microsoft.Practices.Unity;
using Raven.Client;

namespace TestExamples.Muppets.Api.Tests.Unit.HittingApi.nUnit
{
  class TestHttpServer
  {
    private readonly HttpMessageInvoker _invoker;
    private readonly HttpServer _server;

    internal TestHttpServer()
    {
      var httpConfiguration = new HttpConfiguration();
      WebApiConfig.Register(httpConfiguration);
      _server = new HttpServer(httpConfiguration);
      _invoker = new HttpMessageInvoker(_server);
    }

    public void SetDocumentStoreDependency(IDocumentStore store)
    {
      var unityResolver = 
        (UnityResolver)_server.Configuration.DependencyResolver;
      var container = (UnityContainer)unityResolver.Container;
      container.RegisterInstance(store);
    }

    public HttpResponseMessage Send(HttpRequestMessage request)
    {
      return _invoker.SendAsync(request, new CancellationToken()).Result;
    }
  }
}
