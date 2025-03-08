namespace WebApi.Controllers;

public class Response<T>
{
    public T Data { get; set; }
    public string Message { get; set; }
    
    public Response(T data, string message = "")
    {
        Data = data;
        Message = message;
    }
}
