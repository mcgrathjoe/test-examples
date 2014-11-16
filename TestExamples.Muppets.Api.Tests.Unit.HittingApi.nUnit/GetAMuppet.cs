using System;
using System.Net;
using System.Net.Http;
using NUnit.Framework;
using Raven.Client.Embedded;
using TestExamples.Muppets.Api.Models;

namespace TestExamples.Muppets.Api.Tests.Unit.HittingApi.nUnit
{
  [TestFixture, Ignore]
  class GetAMuppet
  {
    private TestHttpServer _testHttpServer;
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

      // TODO: Need to inject the store via the TestHttpServer - IOC?

      _testHttpServer = new TestHttpServer();
    }

    [TestFixtureTearDown]
    public void TestFixtureTearDown()
    {
      _store.Dispose();
    }

    [Test]
    public void When_you_get_a_muppet_then_the_response_contains_the_muppet_name()
    {
      var muppet = GetMuppetFromApi("Gonzo");

      Assert.That(
        muppet.Name,
        Is.EqualTo("Gonzo"));
    }

    [Test]
    public void When_you_get_a_muppet_then_the_response_contains_the_muppet_gender()
    {
      var muppet = GetMuppetFromApi("Gonzo");

      Assert.That(
        muppet.Gender,
        Is.EqualTo("Male"));
    }

    [Test]
    public void When_you_get_a_muppet_then_the_response_contains_when_the_muppet_first_appeared()
    {
      var muppet = GetMuppetFromApi("Gonzo");

      Assert.That(
        muppet.FirstAppearance,
        Is.EqualTo("1976"));
    }

    [Test]
    public void When_you_get_a_muppet_then_the_response_status_is_OK()
    {
      var response = GetMuppetResponseFromApi("Gonzo");

      Assert.That(
        response.StatusCode, 
        Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public void When_you_try_to_get_a_non_existent_muppet_then_the_response_status_is_NotFound()
    {
      var response = GetMuppetResponseFromApi("bungle");

      Assert.That(
        response.StatusCode,
        Is.EqualTo(HttpStatusCode.NotFound));
    }

    private Muppet GetMuppetFromApi(string muppetName)
    {
      var response = GetMuppetResponseFromApi(muppetName);

      return response.Content.ReadAsAsync<Muppet>().Result;
    }

    private HttpResponseMessage GetMuppetResponseFromApi(string muppetName)
    {
      var requestUri =
        new Uri("http://localhost/muppets/" + muppetName);

      var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

      return _testHttpServer.Send(request);
    }
  }
}
