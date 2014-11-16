namespace TestExamples.Muppets.Api.Controllers
{
  using System.Web.Http;
  using System;
  using System.Linq;
  using Models;
  using Raven.Client;

  public class MuppetsController : ApiController
  {
    private readonly IDocumentStore _store;

    public MuppetsController(IDocumentStore store)
    {
      _store = store;
      _store.Initialize();
    }

    [Route("muppets/{muppetName}")]
    public IHttpActionResult GetMuppet(string muppetName)
    {
      Muppet muppet;

      using (var session = _store.OpenSession())
      {
        muppet = session.Query<Muppet>()
          .SingleOrDefault(m => m.Name.Equals(
            muppetName, 
            StringComparison.CurrentCultureIgnoreCase));
      }

      if (muppet == null)
      {
        return NotFound();
      }

      return Ok(muppet);
    }
  }
}