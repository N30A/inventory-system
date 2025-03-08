namespace Services;

public class Result<T>
{
    public bool Status { get; private set; }
    public string Message { get; private set; }
    public T? Data { get; private set; }

    private Result(bool status, string message, T? data)
    {
        Status = status;
        Message = message;
        Data = data;
    }

    public static Result<T> Success(T? data = default)
    {
        return Success(string.Empty, data);
    }

    public static Result<T> Success(string message, T? data = default)
    {
        return new Result<T>(true, message, data);
    }

    public static Result<T> Failure(string message, T? data = default)
    {
        return new Result<T>(false, message, data);
    } 
}
