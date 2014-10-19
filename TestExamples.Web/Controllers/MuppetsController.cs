using System;
using System.Net.Http.Headers;

namespace TestExamples.Controllers
{
  using System.Net.Http;
  using System.Web.Mvc;
  using ViewModels;

  public class MuppetsController : Controller
  {
    [Route("muppets/{muppetName}")]
    public ViewResult GetMuppet(string muppetName)
    {
      HttpResponseMessage message;

      using (var client = new HttpClient())
      {
        client.BaseAddress = new Uri("http://localhost:8882/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
          new MediaTypeWithQualityHeaderValue("application/json"));

        message = 
          client.GetAsync("muppets/gonzo").Result;
      }

      var muppet = message.Content.ReadAsAsync<Muppet>().Result;

      var muppetViewModel = new MuppetViewModel
      {
        FirstAppearance = muppet.FirstAppearance,
        Gender = muppet.Gender,
        Name = muppet.Name
      };

      return View(muppetViewModel);
    }

  }

  public class Muppet
  {
    public string FirstAppearance { get; set; }
    public string Gender { get; set; }
    public string Name { get; set; }
  }
}
