namespace TestExamples.Tests.Unit.HittingControllers.nUnit
{
  using Controllers;
  using ViewModels;
  using NUnit.Framework;
  using stubby;

  class ViewAMuppet
  {
    private MuppetViewModel _viewModel;

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

    [SetUp]
    public void SetUp()
    {
      var controller = new MuppetsController();

      var viewResult = controller.GetMuppet("Gonzo");

      _viewModel = (MuppetViewModel)viewResult.Model;
    }

    [Test]
    public void When_you_view_a_muppet_profile_then_you_see_the_muppet_name()
    {
      Assert.That(
        _viewModel.Name,
        Is.EqualTo("Gonzo"));
    }

    [Test]
    public void When_you_view_a_muppet_profile_then_you_see_the_muppet_gender()
    {
      Assert.That(
        _viewModel.Gender,
        Is.EqualTo("Male"));
    }

    [Test]
    public void When_you_view_a_muppet_profile_then_you_see_when_the_muppet_first_appeared()
    {
      Assert.That(
        _viewModel.FirstAppearance, 
        Is.EqualTo("1976"));
    }
  }
}
