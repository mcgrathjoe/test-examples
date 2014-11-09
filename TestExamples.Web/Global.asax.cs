using System;
using System.Web.Routing;

namespace TestExamples
{
  public class MvcApplication : System.Web.HttpApplication
  {
    protected void Application_Start(object sender, EventArgs e)
    {
      RouteConfig.RegisterRoutes(RouteTable.Routes);

      AutoMapperConfiguration.Configure();
    }
  }
}