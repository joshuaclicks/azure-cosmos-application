using Project.Application.Constants;
using Project.Application.Enums;
using Project.Application.Helpers;

namespace Project.Application.Formatters
{
    public static class ResponseHandler
    {
        public static ApiResponse FailureResponse(string message, string code)
        {
            return new ApiResponse(code, ResponseStatusEnum.Failed.ToDescription(), message);
        }
        public static ApiResponse SuccessResponse(string message, string code, object data)
        {
            return new ApiResponse(code, ResponseStatusEnum.Successful.ToDescription(), message, data);
        }
        public static ApiResponse SuccessResponse(string message, object data)
        {
            return new ApiResponse(SuccessCodes.DEFAULT_SUCCESS_CODE, ResponseStatusEnum.Successful.ToDescription(), message, data);
        }
        public static ApiResponse SuccessResponse(string message)
        {
            return new ApiResponse(SuccessCodes.DEFAULT_SUCCESS_CODE, ResponseStatusEnum.Successful.ToDescription(), message);
        }

        public static ApiResponse SuccessResponse(string message, string code)
        {
            return new ApiResponse(code, ResponseStatusEnum.Successful.ToDescription(), message);
        }
    }
}
