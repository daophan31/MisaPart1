using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MISA.WEB07.BL.BaseBL;
using MISA.WEB07.Common.Entities;
using MISA.WEB07.DL;


namespace MISA.WEB07.BL
{
    public class DepartmentBL : BaseBL<Department>, IDeparmentBL
    {
        #region Field

        private IDepartmentDL _departmentDL;


        #endregion


        #region Contructor

        public DepartmentBL(IDepartmentDL departmentDL) : base(departmentDL)
        {
            _departmentDL = departmentDL;
        }

        #endregion
    }
}
