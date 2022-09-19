using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MISA.WEB07.Common.Entities.DTO;

namespace MISA.WEB07.DL
{
    public interface IBaseDL<T>
    {
        /// <summary>
        /// Lấy thông tin của tất cả bản ghi 
        /// </summary>
        /// <returns>Data của bản ghi</returns>
        /// AUTHOR: PDDAO (21/08/2022)
        public Task<IEnumerable<dynamic>> GetAllRecords();

        /// <summary>
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <returns></returns>
        /// AUTHOR: PDDAO (21/08/2022)
        public Task<Guid> InsertRecord(T record);


        /// <summary>
        /// Lấy thông tin của bản ghi theo ID
        /// </summary>
        /// <param name="employeeID">ID của bản ghi cần lấy</param>
        /// <returns>Data của bản ghi</returns>
        /// AUTHOR: PDDAO (21/08/2022)
        public Task<T> GetRecordByID(Guid id);

        /// <summary>
        /// Xóa bản ghi theo ID
        /// </summary>
        /// <param name="employeeID">ID của bản ghi muốn xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// AUTHOR: PDDAO (21/08/2022)
        public Task<int> DeleteRecordByID(Guid id);

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="recordListID"></param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// AUTHOR: PDDAO (21/08/2022)
        public Task<int> DeleteMultipleRecord(List<Guid> recordListID);

    }
}
