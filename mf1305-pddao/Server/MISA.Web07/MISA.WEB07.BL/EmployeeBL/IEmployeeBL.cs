using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MISA.WEB07.BL.BaseBL;
using MISA.WEB07.Common.Entities;
using MISA.WEB07.Common.Entities.DTO;

namespace MISA.WEB07.BL
{
    public interface IEmployeeBL : IBaseBL<Employee>
    {

        /// <summary>
        /// Thêm mới nhân viên
        /// </summary>
        /// <param name="employee">Thông tin của nhân viên mới</param>
        /// <returns>Số bản ghi được tạo</returns>
        /// AUTHOR: PDDAO (21/08/2022)
        //public int InsertEmployee(Employee employee);

        /// <summary>
        /// Sửa nhân viên
        /// </summary>
        /// <param name="employeeID">ID của nhân viên cần sửa</param>
        /// <param name="employee">Thông tin truyền vào</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// AUTHOR: PDDAO (21/08/2022)
        public Task<int> UpdateEmployee(Guid employeeID, Employee employee);

        /// <summary>
        /// Lấy mã nhân viên tự động tăng
        /// </summary>
        /// <returns></returns>
        public Task<string> GetNewEmployeeCode();

        /// <summary>
        /// Tìm kiếm theo mã, tên nhân viên
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageLimit"></param>
        /// <param name="pageIndex"></param>
        /// <param name="sortField"></param>
        /// <param name="sortOder"></param>
        /// <param name="statementList"></param>
        /// <returns></returns>
        /// AUTHOR: PDDAO (19/08/2022)
        public Task<PagingData<Employee>> FilterEmployee(string keyword, int pageLimit, int pageIndex, string sortField, string sortOder, List<string> statementList);
    }
}

