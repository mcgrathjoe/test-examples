using System.Web.Http;
using TestExamples.Muppets.Api.Models;

namespace TestExamples.Muppets.Api.Controllers
{
    public class MuppetsController : ApiController
    {
      [Route("muppets/{muppetName}")]
      public IHttpActionResult GetMuppet(string muppetName)
      {
        var muppet = new Muppet
        {
          FirstAppearance = "1976",
          Gender = "Male",
          Name = "Gonzo"
        };

        return Ok(muppet);
      }
    }
}