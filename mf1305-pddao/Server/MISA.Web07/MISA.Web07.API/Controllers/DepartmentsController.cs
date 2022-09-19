using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MISA.WEB07.BL;
using MISA.WEB07.Common.Entities;

using Swashbuckle.AspNetCore.Annotations;

namespace MISA.Web07.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        #region Field

        private IDeparmentBL _departmentBL;

        #endregion

        #region Contructor

        public DepartmentsController(IDeparmentBL deparmentBL)
        {
            _departmentBL = deparmentBL;
        }

        #endregion

        /// <summary>
        /// API Lấy danh sách tất cả đơn vị
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(Guid))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDepartment()
        {
            try
            {
                var department = await _departmentBL.GetAllRecords();
                return StatusCode(StatusCodes.Status200OK, department);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, "e001");
            }
        }

        /// <summary>
        /// API Xóa đơn vị
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        [HttpDelete("{departmentID}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(Guid))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDepartment([FromRoute] Guid departmentID)
        {
            try
            {

                int numberOfAffectedRows = await _departmentBL.DeleteRecordByID(departmentID);

                // Xử lý kết quả trả về từ DB
                if (numberOfAffectedRows > 0)
                {
                    //trả về dữ liệu cho client
                    return StatusCode(StatusCodes.Status200OK, departmentID);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "e002");
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return StatusCode(StatusCodes.Status400BadRequest, "e001");
            }
        }

        /// <summary>
        /// API Lấy thông tin đơn vị theo ID
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        [HttpGet("{departmentID}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(Department))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDepartmentByID([FromRoute] Guid departmentID)
        {
            try
            {
                var department = await _departmentBL.GetRecordByID(departmentID);

                // Xử lý kết quả trả về từ DB
                if (department != null)
                {
                    return StatusCode(StatusCodes.Status200OK, department);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return StatusCode(StatusCodes.Status400BadRequest, "e001");
            }
        }
    }
}
