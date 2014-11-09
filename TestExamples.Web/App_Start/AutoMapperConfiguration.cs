using AutoMapper;
using TestExamples.Dal;
using TestExamples.ViewModels;

namespace TestExamples
{
  public class AutoMapperConfiguration
  {
    public static void Configure()
    {
      Mapper.CreateMap<MuppetDto, MuppetViewModel>();
    }
  }
}