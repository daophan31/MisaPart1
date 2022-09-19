using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MISA.WEB07.Common.Entities.DTO;
using MISA.WEB07.DL;

namespace MISA.WEB07.BL.BaseBL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field

        private readonly IBaseDL<T> _baseDL;

        #endregion

        #region Constructor

        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi của một bảng</returns>
        /// Created by:  TMSANG (23/08/2022)
        public virtual async Task<IEnumerable<dynamic>> GetAllRecords()
        {
            return await _baseDL.GetAllRecords();
        }

        /// <summary>
        /// Thêm mới 1  bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi của một bảng</returns>
        /// Created by:  TMSANG (23/08/2022)%
        public virtual async Task<Guid> InsertRecord(T record)
        {
            return await _baseDL.InsertRecord(record);
        }

        /// <summary>
        /// Lấy thông tin của bản ghi theo ID
        /// </summary>
        /// <param name="employeeID">ID của bản ghi cần lấy</param>
        /// <returns>Data của bản ghi</returns>
        /// AUTHOR: PDDAO (21/08/2022)
        public virtual async Task<T> GetRecordByID(Guid id)
        {
            return await _baseDL.GetRecordByID(id);
        }

        /// <summary>
        /// Xóa bản ghi theo ID
        /// </summary>
        /// <param name="employeeID">ID của bản ghi muốn xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// AUTHOR: PDDAO (21/08/2022)
        public virtual async Task<int> DeleteRecordByID(Guid id)
        {
            return await _baseDL.DeleteRecordByID(id);
        }

        public virtual async Task<int> DeleteMultipleRecord(List<Guid> recordListID)
        {
            return await _baseDL.DeleteMultipleRecord(recordListID);
        }

        #endregion
    }
}
