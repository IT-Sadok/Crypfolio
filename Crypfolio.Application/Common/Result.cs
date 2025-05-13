namespace Crypfolio.Application.Common;

public class Result
{
    public bool IsSuccess { get; }
    public string? Error { get; }

    private Result(bool isSuccess, string? error = null)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new Result(true);
    public static Result Fail(string error) => new Result(false, error);
}