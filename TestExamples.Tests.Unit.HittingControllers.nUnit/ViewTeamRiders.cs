using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using TestExamples.Controllers;
using TestExamples.Dal;
using TestExamples.ViewModels;

namespace TestExamples.Tests.Unit.HittingControllers.nUnit
{
  class ViewTeamRiders
  {
    [Test]
    public void given_a_team_of_riders_when_I_search_for_the_team_name_then_I_see_a_list_of_riders()
    {
      const string teamName = "Sky";
      var stubRiders = new List<RiderDto>{ 
        new RiderDto{ Name = "Chris Froome"}, 
        new RiderDto{ Name = "Richie Porte"}};

      var dalStub = new Mock<IRidersDal>();
      dalStub.Setup(
        stub => stub.GetRidersByTeam(teamName))
        .Returns(stubRiders);

      // Can we replace this injection with application IOC?
      var controller = new RidersController(dalStub.Object);

      var result = controller.GetRiders(teamName);

      var ridersViewModel = (RidersViewModel)result.Model;

      Assert.That(
        ridersViewModel.Riders.First(), 
        Is.EqualTo("Chris Froome"));
      Assert.That(
        ridersViewModel.Riders.Last(),
        Is.EqualTo("Richie Porte"));
    }
  }
}