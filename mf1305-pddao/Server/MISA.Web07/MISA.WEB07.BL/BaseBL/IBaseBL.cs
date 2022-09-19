using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MISA.WEB07.Common.Entities.DTO;

namespace MISA.WEB07.BL.BaseBL
{
    public interface IBaseBL<T>
    {
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi của một bảng</returns>
        /// Created by:  PPDAO (23/08/2022)
        public Task<IEnumerable<dynamic>> GetAllRecords();

        /// <summary>
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="record"></param>
        /// <returns>Bản ghi được thêm mới</returns>
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


        public Task<int> DeleteMultipleRecord(List<Guid> recordListID);
    }
}
