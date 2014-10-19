using System.Web.Mvc;

namespace TestExamples.Tests.Unit.HittingControllers.nUnit
{
  using Controllers;
  using ViewModels;
  using NUnit.Framework;
  using stubby;

  class ViewAMuppet
  {
    private readonly Stubby _stubby = new Stubby(new Arguments
    {
      Data = "../../endpoints.yaml"
    });

    [TestFixtureSetUp]
    public void TestFixtureSetUp()
    {
      _stubby.Start();
    }

    [TestFixtureTearDown]
    public void TestFixtureTearDown()
    {
      _stubby.Stop();
    }

    [Test]
    public void When_you_view_a_muppet_profile_then_you_see_the_muppet_name()
    {
      var actionResult = GetMuppet("gonzo");

      var viewModel = 
        (MuppetViewModel)((ViewResult)actionResult).Model;

      Assert.That(
        viewModel.Name,
        Is.EqualTo("Gonzo"));
    }

    [Test]
    public void When_you_view_a_muppet_profile_then_you_see_the_muppet_gender()
    {
      var actionResult = GetMuppet("gonzo");

      var viewModel =
        (MuppetViewModel)((ViewResult)actionResult).Model;

      Assert.That(
        viewModel.Gender,
        Is.EqualTo("Male"));
    }

    [Test]
    public void When_you_view_a_muppet_profile_then_you_see_when_the_muppet_first_appeared()
    {
      var actionResult = GetMuppet("gonzo");

      var viewModel =
        (MuppetViewModel)((ViewResult)actionResult).Model;

      Assert.That(
        viewModel.FirstAppearance, 
        Is.EqualTo("1976"));
    }

    [Test]
    public void When_you_try_to_view_a_non_existent_muppet_then_you_get_a_404()
    {
      var actionResult = GetMuppet("bungle");

      Assert.That(
        actionResult, 
        Is.InstanceOf<HttpNotFoundResult>());
    }

    private static ActionResult GetMuppet(string muppetName)
    {
      var controller = new MuppetsController();

      return controller.GetMuppet(muppetName);
    }
  }
}
