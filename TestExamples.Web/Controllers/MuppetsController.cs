namespace TestExamples.Controllers
{
  using AutoMapper;
  using System.Web.Mvc;
  using Dal;
  using ViewModels;

  public class MuppetsController : Controller
  {
    [Route("muppets/{muppetName}")]
    public ActionResult GetMuppet(string muppetName)
    {
      var muppetApiClient = new MuppetApiClient();

      var response = muppetApiClient.GetMuppet(muppetName);

      if (response.NotFound)
      {
        return HttpNotFound();
      }

      if (response.Failed)
      {
        return View("GetMuppetError");
      }

      var muppetDto = response.DeserialisedContent;

      var muppetViewModel = Mapper.Map<MuppetViewModel>(muppetDto);

      return View(muppetViewModel);
    }
  }
}