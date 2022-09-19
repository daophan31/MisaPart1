using Microsoft.AspNetCore.Http;
using System.Diagnostics;

using Microsoft.AspNetCore.Mvc.ModelBinding;

using MISA.WEB07.Common.Entities.DTO;
using MySqlConnector;

namespace MISA.Web07.API.Helpers
{
    public class HandleError
    {
        public static ErrorResult? ValidateEntity(ModelStateDictionary modelState, HttpContext httpContext)
        {
            if (!modelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var state in modelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                var errorResult = new ErrorResult(
                    WEB07.Common.Enum.MISAErrorCode.Validate,
                    "Dữ liệu không hợp lệ",
                errors,
                    "https://openapi.misa.com.vn/errorcode/e002",
                    Activity.Current?.Id ?? httpContext?.TraceIdentifier);

                return errorResult;
            }

            return null;
        }

        /// <summary>
        /// Sinh ra đối tượng lỗi trả về khi gặp exception
        /// </summary>
        /// <param name="exception">Đối tượng exception gặp phải</param>
        /// <param name="httpContext">Context khi gọi API sử dụng để lấy được traceId</param>
        /// <returns>Đối tượng chứa thông tin lỗi trả về cho client</returns>
        /// Created by: TMSANG (25/08/2022)
        public static ErrorResult? GenerateExceptionResult(Exception exception, HttpContext httpContext)
        {
            Console.WriteLine(exception.Message);
            return new ErrorResult(
                WEB07.Common.Enum.MISAErrorCode.Exception,
                "Có lỗi xảy ra. Vui lòng liên hệ MISA!",
                "Catched an exception",
                "https://openapi.misa.com.vn/errorcode/e002",
                Activity.Current?.Id ?? httpContext?.TraceIdentifier);
        }

        public static ErrorResult? GenerateUneffectedResult(HttpContext httpContext)
        {
            return new ErrorResult(
               WEB07.Common.Enum.MISAErrorCode.Uneffected,
                "Không thể thực hiện !!!",
                "Request no effect",
                 "https://openapi.misa.com.vn/errorcode/e002",
                Activity.Current?.Id ?? httpContext?.TraceIdentifier);
        }



        /// <summary>
        /// Sinh ra đối tượng lỗi trả về khi gặp lỗi trùng mã
        /// </summary>
        /// <param name="exception">Đối tượng exception gặp phải</param>
        /// <param name="httpContext">Context khi gọi API sử dụng để lấy được traceId</param>
        /// <returns>Đối tượng chứa thông tin lỗi trả về cho client</returns>
        /// Created by: TMSANG (25/08/2022)
        public static ErrorResult? GenerateDuplicateCodeErrorResult(MySqlException mySqlException, HttpContext httpContext)
        {
            if (mySqlException.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
            {
                var errorResult = new ErrorResult(
                    WEB07.Common.Enum.MISAErrorCode.DuplicateCode,
                    "Trùng mã",
                    $"{mySqlException.Message}",
                    "https://openapi.misa.com.vn/errorcode/e003",
                    Activity.Current?.Id ?? httpContext?.TraceIdentifier);
                return errorResult;
            }

            Console.WriteLine(mySqlException.Message);
            return new ErrorResult(
                WEB07.Common.Enum.MISAErrorCode.Exception,
                "Có lỗi xảy ra. Vui lòng liên hệ MISA!",
                "Catched an exception",
                "https://openapi.misa.com.vn/errorcode/e002",
                Activity.Current?.Id ?? httpContext?.TraceIdentifier);
        }
    }
}
