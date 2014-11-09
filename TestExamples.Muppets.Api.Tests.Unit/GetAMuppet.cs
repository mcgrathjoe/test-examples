namespace TestExamples.Muppets.Api.Tests.Unit
{
  using System;
  using System.Net;
  using System.Net.Http;
  using NUnit.Framework;
  using Models;

  [TestFixture]
  class GetAMuppet
  {
    private TestHttpServer _testHttpServer;

    [TestFixtureSetUp]
    public void CreateTestHttpServer()
    {
      _testHttpServer = new TestHttpServer();
    }

    [Test]
    public void When_you_get_a_muppet_then_the_response_contains_the_muppet_name()
    {
      var muppet = GetMuppet("Gonzo");

      Assert.That(
        muppet.Name,
        Is.EqualTo("Gonzo"));
    }

    [Test]
    public void When_you_get_a_muppet_then_the_response_contains_the_muppet_gender()
    {
      var muppet = GetMuppet("Gonzo");

      Assert.That(
        muppet.Gender,
        Is.EqualTo("Male"));
    }

    [Test]
    public void When_you_get_a_muppet_then_the_response_contains_when_the_muppet_first_appeared()
    {
      var muppet = GetMuppet("Gonzo");

      Assert.That(
        muppet.FirstAppearance,
        Is.EqualTo("1976"));
    }

    [Test]
    public void When_you_get_a_muppet_then_the_response_status_is_OK()
    {
      var response = GetMuppetResponse("Gonzo");

      Assert.That(
        response.StatusCode,
        Is.EqualTo(HttpStatusCode.OK));
    }

    private Muppet GetMuppet(string muppetName)
    {
      var response = GetMuppetResponse(muppetName);

      return response.Content.ReadAsAsync<Muppet>().Result;
    }

    private HttpResponseMessage GetMuppetResponse(string muppetName)
    {
      var requestUri =
        new Uri("http://localhost/muppets/" + muppetName);

      var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

      return _testHttpServer.Send(request);
    }
  }
}
