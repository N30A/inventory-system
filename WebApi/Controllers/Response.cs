namespace WebApi.Controllers;

public class Response<T>
{
    public required T Data { get; set; }
    public string Message { get; set; } = string.Empty;
}
