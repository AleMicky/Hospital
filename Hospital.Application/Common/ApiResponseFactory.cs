namespace Hospital.Application.Common;

public static class ApiResponseFactory
{
    public static ApiResponse<T> Success<T>(
        T data,
        string message = "Operación realizada correctamente.")
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data,
            Errors = null,
            Timestamp = DateTime.UtcNow
        };
    }

    public static ApiResponse<object> Fail(
        string message,
        object? errors = null)
    {
        return new ApiResponse<object>
        {
            Success = false,
            Message = message,
            Data = null,
            Errors = errors,
            Timestamp = DateTime.UtcNow
        };
    }
}