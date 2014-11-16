namespace TestExamples.Muppets.Api.Tests.Unit
{
  using NUnit.Framework;
  using Models;
  using Raven.Client.Embedded;
  using System.Web.Http;
  using System.Web.Http.Results;
  using Controllers;

  [TestFixture]
  class GetAMuppet
  {
    private EmbeddableDocumentStore _store;

    [TestFixtureSetUp]
    public void TestFixtureSetup()
    {
      _store = new EmbeddableDocumentStore
      {
        RunInMemory = true
      };

      _store.Initialize();

      var gonzo = new Muppet
      {
        FirstAppearance = "1976",
        Gender = "Male",
        Name = "Gonzo"
      };

      using (var session = _store.OpenSession())
      {
        session.Store(gonzo);
        session.SaveChanges();
      }
    }

    [TestFixtureTearDown]
    public void TestFixtureTearDown()
    {
      _store.Dispose();
    }

    [Test]
    public void When_you_get_a_muppet_then_you_get_the_muppet_name()
    {
      var muppet = GetMuppetFromController("Gonzo");

      Assert.That(
        muppet.Name,
        Is.EqualTo("Gonzo"));
    }

    [Test]
    public void When_you_get_a_muppet_then_you_get_the_muppet_gender()
    {
      var muppet = GetMuppetFromController("Gonzo");

      Assert.That(
        muppet.Gender,
        Is.EqualTo("Male"));
    }

    [Test]
    public void When_you_get_a_muppet_then_you_get_when_the_muppet_first_appeared()
    {
      var muppet = GetMuppetFromController("Gonzo");

      Assert.That(
        muppet.FirstAppearance,
        Is.EqualTo("1976"));
    }

    [Test]
    public void When_you_get_a_muppet_then_the_result_is_OK()
    {
      var result = GetMuppetResultFromController("Gonzo");

      Assert.That(
        result,
        Is.InstanceOf<OkNegotiatedContentResult<Muppet>>());
    }

    [Test]
    public void When_you_try_to_get_a_non_existent_muppet_then_the_result_is_NotFound()
    {
      var result = GetMuppetResultFromController("bungle");

      Assert.That(
        result, 
        Is.InstanceOf<NotFoundResult>());
    }

    private Muppet GetMuppetFromController(string muppetName)
    {
      var result = GetMuppetResultFromController(muppetName) 
        as OkNegotiatedContentResult<Muppet>;

      return result.Content;
    }

    private IHttpActionResult GetMuppetResultFromController(
      string muppetName)
    {
      var controller = new MuppetsController(_store);
      
      return controller.GetMuppet(muppetName);
    }
  }
}