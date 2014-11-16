using System;
using System.Linq;

namespace TestExamples.Muppets.Api.Controllers
{
  using System.Web.Http;
  using Models;
  using Raven.Client;
  using Raven.Client.Document;

  public class MuppetsController : ApiController
  {
    private readonly IDocumentStore _store;

    public MuppetsController() : this(
      new DocumentStore
      {
        Url = "http://localhost:8080/",
        DefaultDatabase = "Muppets"
      }) { }

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