using System.Linq;
using System.Web.Mvc;
using TestExamples.Dal;
using TestExamples.ViewModels;

namespace TestExamples.Controllers
{
  public class RidersController : Controller
  {
    private readonly IRidersDal _ridersDal;

    public RidersController(IRidersDal ridersDal)
    {
      _ridersDal = ridersDal;
    }

    public ViewResult GetRiders(string teamName)
    {
      var riderDtos =
        _ridersDal.GetRidersByTeam(teamName);

      var riderNames = 
        riderDtos.Select(riderDto => riderDto.Name).ToList();

      var viewModel = new RidersViewModel{Riders = riderNames};

      return View(viewModel);
    }
  }
}
