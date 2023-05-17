using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Repository.DTOs.ResponseDTOs
{
    public class ApiResponseDto<T>
    {
        public ApiResponseStatus Status { get; set; }
        public string? ResultMessage { get; set; }
        public int ErrorCode { get; set; }
        public List<string>? Errors { get; set; }
        public T? Data { get; set; }

        public static ApiResponseDto<T> Success(T data)
        {
            return new ApiResponseDto<T> { Status = ApiResponseStatus.Success, Data = data };
        }

        public static ApiResponseDto<T> Success(T data, string resultMessage)
        {
            return new ApiResponseDto<T> { Status = ApiResponseStatus.Success, Data = data, ResultMessage = resultMessage };
        }

        public static ApiResponseDto<T> Fail(int errorCode, List<string> error)
        {
            return new ApiResponseDto<T> { Status = ApiResponseStatus.Failed, ErrorCode = errorCode, Data = default, ResultMessage = "Failed", Errors = error };
        }

        public static ApiResponseDto<T> Fail(int errorCode, string error)
        {
            return new ApiResponseDto<T> { Status = ApiResponseStatus.Failed, ErrorCode = errorCode, Data = default, ResultMessage = "Failed", Errors = new List<string> { error } };
        }
    }

    public enum ApiResponseStatus
    {
        Success,
        Failed
    }

}
