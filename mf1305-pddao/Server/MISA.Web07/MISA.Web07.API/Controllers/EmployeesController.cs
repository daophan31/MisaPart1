using System;
using System.Reflection.Metadata;

using Dapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MISA.Web07.API.Helpers;
using MISA.WEB07.BL;
using MISA.WEB07.Common.Entities;
using MISA.WEB07.Common.Entities.DTO;

using MySqlConnector;

using Swashbuckle.AspNetCore.Annotations;

namespace MISA.Web07.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        #region Field

        private IEmployeeBL _employeeBL;

        #endregion


        #region Contructor

        public EmployeesController(IEmployeeBL employeeBL)
        {
            _employeeBL = employeeBL;
        }

        #endregion
        /// <summary>
        /// Lấy tất cả danh sách nhân viên
        /// </summary>
        /// <returns>Danh sách nhân viên</returns>
        /// AUTHOR: PDDAO (19/08/2022)
        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            try
            {
                var employee = await _employeeBL.GetAllRecords();
                return StatusCode(StatusCodes.Status200OK, employee);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, "e001");
            }

        }

        /// <summary>
        /// API Thêm mới nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>StatusCode</returns>
        /// AUTHOR: PDDAO (15/08/2022)
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(Guid))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> InsertEmployee([FromBody] Employee employee)
        {
            try
            {
                var validateResult = HandleError.ValidateEntity(ModelState, HttpContext);
                if (validateResult != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, validateResult);
                }

                var recordID = await _employeeBL.InsertRecord(employee);

                if (recordID != Guid.Empty)
                {
                    return StatusCode(StatusCodes.Status201Created, recordID);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "e004");
                }
            }
            catch (MySqlException mySqlException)
            {
                return StatusCode(StatusCodes.Status400BadRequest, HandleError.GenerateDuplicateCodeErrorResult(mySqlException, HttpContext));
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, HandleError.GenerateExceptionResult(exception, HttpContext));
            }
        }

        /// <summary>
        /// API Sửa nhân viên
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut("{employeeID}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(Guid))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid employeeID, [FromBody] Employee employee)
        {
            try
            {
                int numberOfAffectedRows = await _employeeBL.UpdateEmployee(employeeID, employee);

                // Xử lý kết quả trả về database
                if (numberOfAffectedRows > 0)
                {
                    // Trả về dữ liệu cho Client
                    return StatusCode(StatusCodes.Status200OK, employeeID);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, HandleError.GenerateUneffectedResult(HttpContext));
                }
            }
            catch (MySqlException mySqlException)
            {
                return StatusCode(StatusCodes.Status400BadRequest, HandleError.GenerateDuplicateCodeErrorResult(mySqlException, HttpContext));
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, HandleError.GenerateExceptionResult(exception, HttpContext));
            }

        }

        /// <summary>
        /// API Xóa nhân viên
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        [HttpDelete("{employeeID}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(Guid))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid employeeID)
        {
            try
            {

                int numberOfAffectedRows = await _employeeBL.DeleteRecordByID(employeeID);

                // Xử lý kết quả trả về từ DB
                if (numberOfAffectedRows > 0)
                {
                    //trả về dữ liệu cho client
                    return StatusCode(StatusCodes.Status200OK, employeeID);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, HandleError.GenerateUneffectedResult(HttpContext));
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return StatusCode(StatusCodes.Status400BadRequest, HandleError.GenerateExceptionResult(exception, HttpContext));
            }
        }

        /// <summary>
        /// API xóa nhiều nhân viên
        /// </summary>
        /// <param name="recordListID"></param>
        /// <returns></returns>
        [HttpPost("delete-multiple")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(List<Guid>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMultipleRecord([FromBody] List<Guid> recordListID)
        {
            try
            {
                var numberAffectedRow = await _employeeBL.DeleteMultipleRecord(recordListID);
                if (numberAffectedRow > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, numberAffectedRow);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, HandleError.GenerateUneffectedResult(HttpContext));
                }

            }
            catch (Exception exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, HandleError.GenerateExceptionResult(exception, HttpContext));
            }
        }

        /// <summary>
        /// API Lấy thông tin nhân viên theo ID
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        [HttpGet("{employeeID}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(Employee))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployeeByID([FromRoute] Guid employeeID)
        {
            try
            {
                var employee = await _employeeBL.GetRecordByID(employeeID);

                // Xử lý kết quả trả về từ DB
                if (employee != null)
                {
                    return StatusCode(StatusCodes.Status200OK, employee);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return StatusCode(StatusCodes.Status400BadRequest, HandleError.GenerateExceptionResult(exception, HttpContext));
            }
        }

        /// <summary>
        /// Tìm kiếm và phân trang
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageLimit"></param>
        /// <param name="pageIndex"></param>
        /// <param name="sortField"></param>
        /// <param name="sortOder"></param>
        /// <returns></returns>
        /// AUTHOR: PDDAO (19/08/2022)
        [HttpGet("filter")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(PagingData<Employee>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> FilterEmployee([FromQuery] string? keyword, [FromQuery] int pageLimit = 10, [FromQuery] int pageIndex = 1, [FromQuery] string? sortField = "ModifiedDate", [FromQuery] string? sortOder = "DESC")
        {
            try
            {
                var statementList = new List<string> { $"EmployeeCode", "FullName" };
                var filterResult = await _employeeBL.FilterEmployee(keyword, pageLimit, pageIndex, sortField, sortOder, statementList);

                return StatusCode(StatusCodes.Status200OK, filterResult);
            }
            catch (Exception exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, HandleError.GenerateExceptionResult(exception, HttpContext));
            }

        }

        /// <summary>
        /// API Lấy mã nhân viên tự động tăng
        /// </summary>
        /// <returns>Mã nhân viên tự động tăng</returns>
        /// AUTHOR: PDDAO (19/08/2022)
        [HttpGet("new-code")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNewEmployeeCode()
        {
            try
            {

                string newEmployeeCode = await _employeeBL.GetNewEmployeeCode();

                //Trả về kiểu dữ liệu cho client
                return StatusCode(StatusCodes.Status200OK, newEmployeeCode);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return StatusCode(StatusCodes.Status400BadRequest, HandleError.GenerateExceptionResult(exception, HttpContext));
            }
        }


    }
}
