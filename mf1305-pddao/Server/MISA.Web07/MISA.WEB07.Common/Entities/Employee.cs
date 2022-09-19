using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MISA.WEB07.Common.Enum;
using MISA.WEB07.Common.CustomeAttribute;

namespace MISA.WEB07.Common.Entities
{
    /// <summary>
    /// bảng nhân viên
    /// </summary>
    [Table("employee")]
    public class Employee
    {
        /// <summary>
        /// ID nhân viên
        /// </summary>
        [Key]
        public Guid EmployeeID { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [Required(ErrorMessage = "e004")]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Họ tên nhân viên
        /// </summary>
        [Required(ErrorMessage = "e005")]
        public string FullName { get; set; }

        /// <summary>
        /// Ngày sinh 
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// Số CCCD/CMND
        /// </summary>
        public string? IndentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp CCCD/CMND
        /// </summary>

        public DateTime? IndentityDate { get; set; }

        /// <summary>
        /// Nơi cấp CCCD/CMND
        /// </summary>
        public string? IndentityPlace { get; set; }

        /// <summary>
        /// Email
        /// </summary>

        public string? Email { get; set; }

        /// <summary>
        /// Mã dơn vị
        /// </summary>
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Vị trí nhân viên
        /// </summary>
        public string? Position { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Số điện thoại di động
        /// </summary>
        public string? MobileNumber { get; set; }

        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        public string? Hotline { get; set; }

        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        public string? BankAccount { get; set; }

        /// <summary>
        /// Tên ngân hàng 
        /// </summary>
        public string? BankName { get; set; }

        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        public string? BankBranch { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>

        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>

        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
}
