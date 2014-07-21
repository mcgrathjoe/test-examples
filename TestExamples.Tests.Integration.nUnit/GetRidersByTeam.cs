using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using TestExamples.Dal;

namespace TestExamples.Tests.Integration.nUnit
{
  class GetRidersByTeam
  {
    [TestFixtureSetUp]
    public void TestFixtureSetUp()
    {
      using (var connection = new SqlConnection(
        ConfigurationManager.ConnectionStrings["dev"].ToString()))
      {
        using (var command = new SqlCommand
        {
          Connection = connection
        })
        {
          connection.Open();

          command.CommandText = 
            "DELETE FROM [Rider]; DELETE FROM [Team]";
          command.ExecuteNonQuery();

          command.CommandText = "INSERT INTO [Team] ([Name]) VALUES ('TeamWithNoRiders')";
          command.ExecuteNonQuery();

          command.CommandText = "INSERT INTO [Team] ([Name]) VALUES ('Team Sky'); SELECT SCOPE_IDENTITY()";
          var teamId = command.ExecuteScalar();

          command.CommandText = string.Format(
            "INSERT INTO [Rider] ([Name], [TeamId]) VALUES ('Chris Froome', '{0}'), ('Geraint Thomas', '{0}')",
            teamId);
          command.ExecuteNonQuery();

          command.CommandText = "INSERT INTO [Team] ([Name]) VALUES ('Garmin - Sharp'); SELECT SCOPE_IDENTITY()";
          teamId = command.ExecuteScalar();

          command.CommandText = string.Format(
            "INSERT INTO [Rider] ([Name], [TeamId]) VALUES ('Jack Bauer', '{0}')",
            teamId);
          command.ExecuteNonQuery();
        }
      }
    }

    [Test]
    public void given_some_riders_in_team_when_get_riders_then_team_riders_returned()
    {
      var dal = new RidersDal();
      var results = dal.GetRidersByTeam("Team Sky");
      var riderDtos = results as RiderDto[] ?? results.ToArray();

      Assert.That(riderDtos.Count(), Is.EqualTo(2));
      Assert.That(riderDtos.First().Name, Is.EqualTo("Chris Froome"));
      Assert.That(riderDtos.Last().Name, Is.EqualTo("Geraint Thomas"));
    }

    [Test]
    public void given_no_riders_in_a_team_when_get_riders_then_empty_list_returned()
    {
      var dal = new RidersDal();
      var results = dal.GetRidersByTeam("TeamWithNoRiders");

      Assert.That(results, Is.Empty);
    }

    [Test]
    public void given_no_team_when_get_riders_then_empty_list_returned()
    {
      var dal = new RidersDal();
      var results = dal.GetRidersByTeam("NonExistentTeam");
      
      Assert.That(results, Is.Empty);
    }
  }
}
