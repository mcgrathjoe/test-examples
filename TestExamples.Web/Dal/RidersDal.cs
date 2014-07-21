using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace TestExamples.Dal
{
  public class RidersDal : IRidersDal
  {
    public IEnumerable<RiderDto> GetRidersByTeam(string teamName)
    {
      var dtos = new List<RiderDto>();

      using (var connection = new SqlConnection(
        ConfigurationManager.ConnectionStrings["dev"].ToString()))
      {
        var query = string.Format(
          "Select [Rider].[Name] From [Rider] inner join [Team] on [Rider].[TeamId] = [Team].[Id] Where [Team].[Name] = '{0}'",
          teamName);
        var command = new SqlCommand(query, connection);
        connection.Open();

        using (var dataReader = command.ExecuteReader())
        {
          while (dataReader.Read())
          {
            dtos.Add(new RiderDto { Name = dataReader["Name"].ToString() });
          }
        }
      }

      return dtos;
    }
  }
}