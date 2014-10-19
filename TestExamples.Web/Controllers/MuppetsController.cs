using System.Net;

namespace TestExamples.Controllers
{
  using System;
  using System.Configuration;
  using System.Net.Http.Headers;
  using System.Net.Http;
  using System.Web.Mvc;
  using ViewModels;

  public class MuppetsController : Controller
  {
    [Route("muppets/{muppetName}")]
    public ActionResult GetMuppet(string muppetName)
    {
      HttpResponseMessage response;

      using (var client = new HttpClient())
      {
        var apiBaseAddress =
          ConfigurationManager.AppSettings["apiBaseAddress"];
        client.BaseAddress = new Uri(apiBaseAddress);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
          new MediaTypeWithQualityHeaderValue("application/json"));

        response = 
          client.GetAsync("muppets/" + muppetName).Result;
      }

      if (response.StatusCode == HttpStatusCode.NotFound)
      {
        return HttpNotFound();
      }

      var muppet = response.Content.ReadAsAsync<Muppet>().Result;

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