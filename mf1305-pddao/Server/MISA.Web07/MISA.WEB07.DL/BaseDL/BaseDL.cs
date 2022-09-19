using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;

using MISA.WEB07.Common.Entities;
using MISA.WEB07.Common.Entities.DTO;

using MySqlConnector;

namespace MISA.WEB07.DL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        /// <summary>
        /// Lấy thông tin của tất cả bản ghi 
        /// </summary>
        /// <returns>Data của bản ghi</returns>
        /// AUTHOR: PDDAO (21/08/2022)
        public async Task<IEnumerable<dynamic>> GetAllRecords()
        {
            // Chuẩn bị câu lệnh SELECT 
            string className = typeof(T).Name;
            string getAllRecordsCommand = $"SELECT * FROM {className} ORDER BY ModifiedDate DESC";
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                // Thực hiện gọi vào DB để chạy câu lệnh SELECT 
                var records = await mySqlConnection.QueryAsync(getAllRecordsCommand);

                return records;
            }
        }

        public async Task<Guid> InsertRecord(T record)
        {
            string className = typeof(T).Name;
            string insertStoredProcedureName = $"Proc_{className}_Insert{className}";
            string localDate = DateTime.UtcNow.ToString("ddTHH-MM-yyyy\\:mm\\:ss.fffffffzzz");
            record.GetType().GetProperty($"{className}ID").SetValue(record, Guid.NewGuid(), null);
            record.GetType().GetProperty("CreatedDate").SetValue(record, DateTime.Now, null);
            record.GetType().GetProperty("ModifiedDate").SetValue(record, DateTime.Now, null);

            // Chuẩn bị tham số đầu vào stored procedure 
            var properties = typeof(T).GetProperties();
            var parameters = new DynamicParameters();
            foreach (var property in properties)
            {
                string propertyName = $"v_{property.Name}";
                var propertyValue = property.GetValue(record);
                parameters.Add(propertyName, propertyValue);
            }

            // Thực hiện gọi vào DB để chạy câu lệnh stored procedure với tham số đầu vào ở trên
            int numberOfAffectedRow = 0;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                numberOfAffectedRow = mySqlConnection.Execute(insertStoredProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                var result = Guid.Empty;
                if (numberOfAffectedRow > 0)
                {
                    var primaryKeyProperty = typeof(T).GetProperties().FirstOrDefault(prop => prop.GetCustomAttributes(typeof(KeyAttribute), true).Count() > 0);
                    var newId = primaryKeyProperty?.GetValue(record);
                    if (newId != null)
                    {
                        result = (Guid)newId;
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// Lấy thông tin của bản ghi theo ID
        /// </summary>
        /// <param name="employeeID">ID của bản ghi cần lấy</param>
        /// <returns>Data của bản ghi</returns>
        /// AUTHOR: PDDAO (21/08/2022)
        public async Task<T> GetRecordByID(Guid id)
        {
            string className = typeof(T).Name;
            // Chuẩn bị tên Stored Procedure
            string storedProcedureName = $"Proc_{className}_Get{className}ByID";

            // Chuẩn bị tham số đầu vào cho stored Procedure
            var parameters = new DynamicParameters();
            parameters.Add($"v_{className}ID", id);

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                // Thực hiện gọi vào DB để chạy stored procedure với tham số đầu vào ở trên
                var result = await mySqlConnection.QueryFirstOrDefaultAsync<T>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                return result;

            }
        }

        /// <summary>
        /// Xóa bản ghi theo ID
        /// </summary>
        /// <param name="employeeID">ID của bản ghi muốn xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// AUTHOR: PDDAO (21/08/2022)
        public async Task<int> DeleteRecordByID(Guid id)
        {
            string className = typeof(T).Name;
            // Chuẩn bị tên Stored Procedure
            string storedProcedureName = $"Proc_{className}_Delete{className}ByID";

            // Chuẩn bị tham số đầu vào cho stored Procedure
            var parameters = new DynamicParameters();
            parameters.Add($"v_{className}ID", id);

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                int numberOfAffectedRows = await mySqlConnection.ExecuteAsync(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                return numberOfAffectedRows;
            }
        }

        public async Task<int> DeleteMultipleRecord(List<Guid> recordListID)
        {
            // Khai báo biến lưu trữ số bản ghi bị xoá bỏ
            int numberRowEffected = 0;

            await Parallel.ForEachAsync(recordListID, async (recordId, cancellationToken) =>
            {
                // Khai báo store Produre
                string className = typeof(T).Name;
                string storedProcedureName = $"Proc_{className}_Delete{className}ById";

                using var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);
                var transaction = await mySqlConnection.BeginTransactionAsync();

                mySqlConnection.Open();
                // Chuẩn bị tham số truyền vào Store Produre
                var parameters = new DynamicParameters();
                parameters.Add($"v_{className}Id", recordId);
                // Truy vấn vào database
                var numberAffectedRow = await mySqlConnection.ExecuteAsync(storedProcedureName, parameters, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);

                transaction.Commit();
                // Tăng biến đếm thêm 1
                numberRowEffected += numberAffectedRow;

            });

            return numberRowEffected;

        }

    }
}
