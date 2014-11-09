using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TestExamples.Dal
{
  public class MuppetApiClient
  {
    public ApiResponse<MuppetDto> GetMuppet(string muppetName)
    {
      var apiResponse = new ApiResponse<MuppetDto>();
      var httpResponse = new HttpResponseMessage();

      using (var client = new HttpClient())
      {
        var apiBaseAddress =
          ConfigurationManager.AppSettings["apiBaseAddress"];
        client.BaseAddress = new Uri(apiBaseAddress);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
          new MediaTypeWithQualityHeaderValue("application/json"));

        try
        {
          httpResponse =
            client.GetAsync("muppets/" + muppetName).Result;

          apiResponse.DeserialisedContent =
            httpResponse.Content.ReadAsAsync<MuppetDto>().Result;
        }
        catch (Exception)
        {
          apiResponse.Failed = true;
        }
      }

      if (httpResponse.StatusCode == HttpStatusCode.NotFound)
      {
        apiResponse.NotFound = true;
      }

      if (!httpResponse.IsSuccessStatusCode)
      {
        apiResponse.Failed = true;
      }

      return apiResponse;
    }
  }
}