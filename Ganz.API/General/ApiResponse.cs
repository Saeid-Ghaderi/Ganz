namespace Ganz.API.General
{
    public class ApiResponse<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ApiResponse(int statusCode, bool isSuccess, string? message = null, T? data = null)
        {
            StatusCode = statusCode;
            IsSuccess = isSuccess;
            Message = message!;
            Data = data!;
        }

        public static ApiResponse<T> Success(int statusCode, T? data = null, string? message = null)
        {
            return new ApiResponse<T>(statusCode, true, message!, data!);
        }

        public static ApiResponse<T> Failure(int statusCode, string message)
        {
            return new ApiResponse<T>(statusCode, false, message);
        }
    }


}
