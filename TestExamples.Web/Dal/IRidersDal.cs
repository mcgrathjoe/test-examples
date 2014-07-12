using System.Collections.Generic;

namespace TestExamples.Dal
{
  public interface IRidersDal
  {
    IEnumerable<RiderDto> GetRidersByTeam(string teamName);
  }
}