namespace TestExamples.Dal
{
  public class ApiResponse<T>
  {
    public T DeserialisedContent { get; set; }
    public bool Failed { get; set; }
    public bool NotFound { get; set; }
  }
}