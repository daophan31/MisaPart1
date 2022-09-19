using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MISA.WEB07.Common.Entities;
using MISA.WEB07.Common.Entities.DTO;
using MISA.WEB07.DL;

namespace MISA.WEB07.DL
{
    public interface IEmployeeDL : IBaseDL<Employee>
    {




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


        public Task<PagingData<Employee>> FilterEmployee(string keyword, int pageLimit, int pageIndex, string sortField, string sortOder, List<string> statementList);
    }
}
