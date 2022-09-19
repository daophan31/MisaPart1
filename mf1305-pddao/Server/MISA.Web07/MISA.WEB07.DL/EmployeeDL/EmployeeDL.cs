using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;

using MISA.WEB07.Common.Entities;
using MISA.WEB07.Common.Entities.DTO;
using MISA.WEB07.DL;

using MySqlConnector;

namespace MISA.WEB07.DL
{
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {

        /// <summary>
        /// Lấy mã nhân viên tự động tăng
        /// </summary>
        /// <returns>Mã nhân </returns>
        public async Task<string> GetNewEmployeeCode()
        {
            // Chuẩn bị tên Stored Procedure
            string storedProcedureName = "Proc_Employee_GetMaxCode";

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                //Thực hiện gọi vào DB thể thực hiện stored procedure trên
                string maxEmployeeCode = await mySqlConnection.QueryFirstOrDefaultAsync<string>(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý sinh mã nhân viên tự động tăng
                // Cắt chuỗi mã nhân viên lớn nhất trong hệ thống để lấy phần số
                // Mã nhân viên mới = "NV" + Giá trị cắt chuỗi ở  trên + 1
                string newEmployeeCode = "NV" + (Int64.Parse(maxEmployeeCode.Substring(2) + 1).ToString());

                return newEmployeeCode;

            }
        }




        /// <summary>
        /// Sửa nhân viên
        /// </summary>
        /// <param name="employeeID">ID của nhân viên cần sửa</param>
        /// <param name="employee">Thông tin truyền vào</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// AUTHOR: PDDAO (21/08/2022)
        public async Task<int> UpdateEmployee(Guid employeeID, Employee employee)
        {
            // Khởi tạo kết nối database 


            // Chuẩn bị câu lệnh UPDATE
            string updateEmployeeCommand = "UPDATE employee " +
                "SET  EmployeeCode = @EmployeeCode, " +
                "FullName = @FullName, " +
                "DateOfBirth = @DateOfBirth, " +
                "Gender = @Gender, " +
                "IndentityNumber = @IndentityNumber, " +
                "IndentityDate = @IndentityDate, " +
                "IndentityPlace = @IndentityPlace, " +
                "Email = @Email, " +
                "DepartmentID = @DepartmentID, " +
                "DepartmentName = @DepartmentName, " +
                "Position = @Position, " +
                "Address = @Address, " +
                "MobileNumber = @MobileNumber, " +
                "Hotline = @Hotline, " +
                "BankAccount = @BankAccount, " +
                "BankName = @BankName, " +
                "BankBranch = @BankBranch, " +
                "CreatedDate = @CreatedDate, " +
                "CreatedBy = @CreatedBy, " +
                "ModifiedDate = @ModifiedDate, " +
                "ModifiedBy = @ModifiedBy" +
                "WHERE EmployeeID = @EmployeeID;";
            // Chuẩn bị tham số đầu vào 
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeID", employeeID);
            parameters.Add("@EmployeeCode", employee.EmployeeCode);
            parameters.Add("@DateOfBirth", employee.DateOfBirth);
            parameters.Add("@Gender", employee.Gender);
            parameters.Add("@IndentityNumber", employee.IndentityNumber);
            parameters.Add("@IndentityDate", employee.IndentityDate);
            parameters.Add("@IndentityPlace", employee.IndentityPlace);
            parameters.Add("@Email", employee.Email);
            parameters.Add("@DepartmentID", employee.DepartmentID);
            parameters.Add("@DepartmentName", employee.DepartmentName);
            parameters.Add("@Position", employee.Position);
            parameters.Add("@Address", employee.Address);
            parameters.Add("@MobileNumber", employee.MobileNumber);
            parameters.Add("@Hotline", employee.Hotline);
            parameters.Add("@BankAccount", employee.BankAccount);
            parameters.Add("@BankName", employee.BankName);
            parameters.Add("@BankBranch", employee.BankBranch);
            parameters.Add("@CreatedDate", employee.CreatedDate);
            parameters.Add("@CreatedBy", employee.CreatedBy);
            parameters.Add("@ModifiedDate", DateTime.Now);
            parameters.Add("@ModifiedBy", employee.ModifiedBy);

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                int numberOfAffectedRows = await mySqlConnection.ExecuteAsync(updateEmployeeCommand, parameters);

                return numberOfAffectedRows;
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
        /// <param name="statementList"></param>
        /// <returns></returns>
        public async Task<PagingData<Employee>> FilterEmployee(string keyword, int pageLimit, int pageIndex, string sortField, string sortOder, List<string> statementList)
        {
            using var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

            // Khai báo store produre

            string storedProcedureName = $"Proc_Employee_GetPaging";

            // Chuẩn bị tham số đầu vào cho store produce
            var whereList = new List<string>();
            string whereClause = "";
            if (keyword != "" & keyword != null)
            {
                foreach (string statement in statementList)
                {
                    whereList.Add($"{statement} LIKE '%{keyword}%'");
                };
            }
            if (whereList.Count > 0)
            {
                whereClause = String.Join(" OR ", whereList);
            }
            else whereClause = "1 = 1";

            var parameters = new DynamicParameters();
            parameters.Add("v_Offset", (pageIndex - 1) * pageLimit);
            parameters.Add("v_Limit", pageLimit);
            parameters.Add("v_Sort", $"{sortField} {sortOder}");
            parameters.Add("v_Where", whereClause);

            // Truy vấn vào database
            var multipleResult = await mySqlConnection.QueryMultipleAsync(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            // Xử lý kết quả trả về
            var recordList = multipleResult.Read<Employee>().ToList();
            var totalCount = multipleResult.Read<long>().Single();


            var pagingResult = new PagingData<Employee>()
            {
                Data = recordList,
                TotalCount = totalCount,

            };

            Console.WriteLine("block");

            // Trả về kết quả
            return pagingResult;

        }

    }
}
