using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MISA.WEB07.BL.BaseBL;
using MISA.WEB07.Common.Entities;
using MISA.WEB07.Common.Entities.DTO;
using MISA.WEB07.DL;

namespace MISA.WEB07.BL
{
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {
        #region Field

        private readonly IEmployeeDL _employeeDL;

        #endregion


        #region Contructor

        public EmployeeBL(IEmployeeDL employeeDL) : base(employeeDL)
        {
            _employeeDL = employeeDL;
        }


        #endregion


        /// <summary>
        /// Lấy mã nhân viên tự động tăng
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetNewEmployeeCode()
        {
            return await _employeeDL.GetNewEmployeeCode();
        }

        /// <summary>
        /// Thêm mới nhân viên
        /// </summary>
        /// <param name="employee">Thông tin của nhân viên mới</param>
        /// <returns>Số bản ghi được tạo</returns>
        /// AUTHOR: PDDAO (21/08/2022)
        // public int InsertEmployee(Employee employee)
        //{
        //  return _employeeDL.InsertEmployee(employee).Result;
        //}

        /// <summary>
        /// Sửa nhân viên
        /// </summary>
        /// <param name="employeeID">ID của nhân viên cần sửa</param>
        /// <param name="employee">Thông tin truyền vào</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// AUTHOR: PDDAO (21/08/2022)
        public async Task<int> UpdateEmployee(Guid employeeID, Employee employee)
        {
            return await _employeeDL.UpdateEmployee(employeeID, employee);
        }

        /// <summary>
        /// Tìm kiếm theo tên, mã nhân viên
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageLimit"></param>
        /// <param name="pageIndex"></param>
        /// <param name="sortField"></param>
        /// <param name="sortOder"></param>
        /// <param name="statementList"></param>
        /// <returns></returns>
        /// AUTHOR: PDDAO (19/08/2022)
        public async Task<PagingData<Employee>> FilterEmployee(string keyword, int pageLimit, int pageIndex, string sortField, string sortOder, List<string> statementList)
        {
            return await _employeeDL.FilterEmployee(keyword, pageLimit, pageIndex, sortField, sortOder, statementList);
        }
    }
}
