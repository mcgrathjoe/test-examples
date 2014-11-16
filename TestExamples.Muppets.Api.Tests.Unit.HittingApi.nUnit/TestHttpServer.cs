using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace TestExamples.Muppets.Api.Tests.Unit.HittingApi.nUnit
{
  class TestHttpServer
  {
    private readonly HttpMessageInvoker _invoker;

    internal TestHttpServer()
    {
      var httpConfiguration = new HttpConfiguration();
      WebApiConfig.Register(httpConfiguration);
      var httpServer = new HttpServer(httpConfiguration);
      _invoker = new HttpMessageInvoker(httpServer);
    }

    public HttpResponseMessage Send(HttpRequestMessage request)
    {
      return _invoker.SendAsync(request, new CancellationToken()).Result;
    }
  }
}
