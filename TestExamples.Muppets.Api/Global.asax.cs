using System.Web.Http;

namespace TestExamples.Muppets.Api
{
  public class WebApiApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      GlobalConfiguration.Configure(WebApiConfig.Register);
    }
  }
}