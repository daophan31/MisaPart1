using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.Common.Enum
{
    public enum MISAErrorCode
    {
        /// <summary>
        /// Lỗi do exception chưa xác định được
        /// </summary>
        Exception = 1,

        /// <summary>
        /// Lỗi khi gọi vào database
        /// </summary>
        Uneffected = 2,

        /// <summary>
        /// Lỗi do trùng mã
        /// </summary>
        DuplicateCode = 3,

        Validate = 4
    }
}

