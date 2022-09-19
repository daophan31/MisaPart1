import customeJS from "./custome.js";
import commonJS from "./common.js";

var dataHandleJS = {
  /**
   * * Thực hiện load dữ liệu
   * * AUTHOR: pddao (02/08/2022)
   */
  loadData() {
    try {
      // Gọi API lấy dữ liệu:
      $.ajax({
        type: "GET",
        url: "https://cukcuk.manhnv.net/api/v1/Employees",

        success: function (response) {
          // Xử lý dữ liệu:
          $("#tblEmployee #tbBody").empty();
          for (const obj of response) {
            const employeeCode = obj.EmployeeCode;
            const dateOfBirth = commonJS.formatDate(obj.DateOfBirth);

            const fullName = obj.FullName;
            const gender = obj.GenderName;
            const indentityNumber = obj.indentityNumber;
            const indentityDate = obj.IndentityDate;
            const indentityPlace = obj.IndentityPlace;
            const positionName = obj.PositionName;
            const departmentName = obj.DepartmentName;
            const departmentCode = obj.departmentCode;
            const phoneNumber = obj.phoneNumber;
            const email = obj.Email;
            const address = obj.Address;
            const salary = new Intl.NumberFormat("vi-vn", {
              style: "currency",
              currency: "VND",
            }).format(obj.Salary);
            const createdDate = commonJS.formatDate(obj.CreatedDate);

            const createdBy = commonJS.formatDate(obj.CreatedBy);
            const modifiedDate = commonJS.formatDate(obj.ModifiedDate);
            const modifiedBy = commonJS.formatDate(obj.ModifiedBy);
            //Build chuỗi html với 1 dòng dữ liệu:

            let rowHTML = $(
              `<tr class="tr-viewer revune-status">
                <td class="out-left-white-16"></td>

                <td class="td-viewer td-multi">
                  <label class="lb-checkbox">
                    <input type="checkbox" class="checkbox--input" />
                    <span class="checkbox">
                      <span class="checkbox-inner"></span>
                    </span>
                  </label>
                </td>
                <td
                  class="td-viewer static-col justify-left"
                  style="left: 55px"
                >
                  ${employeeCode || ""}
                </td>
                <td
                  class="td-viewer static-col justify-center"
                  style="left: 234px"
                >
                  ${dateOfBirth || ""}
                </td>
                <td class="td-viewer justify-left">${fullName || ""}</td>
                <td class="td-viewer justify-left">${gender || ""}</td>
                <td class="td-viewer justify-left">${indentityNumber || ""}</td>
                <td class="td-viewer justify-left">${indentityDate || ""}</td>
                <td class="td-viewer justify-left">${indentityPlace || ""}</td>
                <td class="td-viewer justify-left">${positionName || ""}</td>
                <td class="td-viewer justify-left">${departmentCode || ""}</td>
                <td class="td-viewer justify-left">${departmentName || ""}</td>
                <td class="td-viewer justify-left">${phoneNumber || ""}</td>
                <td class="td-viewer justify-left">${email || ""}</td>
                <td class="td-viewer justify-left">${address || ""}</td>

                <td class="td-viewer justify-center">${createdDate || ""}</td>
                <td class="td-viewer justify-center">${createdBy || ""}</td>
                <td class="td-viewer justify-center">${modifiedDate || ""}</td>
                <td class="td-viewer justify-center">${modifiedBy || ""}</td>
                <td class="td-viewer justify-right">${salary || ""}</td>
                <td class="td-viewer td-widget" style="right: 30px">
                  <div class="btn-edit-data">Sửa</div>
                </td>
                <td class="out-right-white-30"></td>
              </tr>
            `
            );
            rowHTML.data("data", obj);
            $("#tblEmployee #tbBody").append(rowHTML);
          }

          // Đặt lại width thanh phân trang phục vụ thuộc tính stiky:
          customeJS.setWidthPagination();

          // Đếm bản ghi:
          $(
            ".pagination .pagination-bar .left-pagination .total-record b"
          ).text(commonJS.rowCount("#tblEmployee #tbBody tr"));
        },
        error: function (response) {
          console.log(response);
        },
      });
    } catch (error) {}
  },

  /**
   * * Load các dữ liệu lựa chọn lên combox
   *  * AUTHOR: pddao (02/08/2022)
   */
  loadDataCombobox() {
    try {
      // Gọi API lấy dữ liệu
      $.ajax({
        type: "GET",
        url: "https://cukcuk.manhnv.net/api/v1/Departments",
        success: function (response) {
          console.log(response);
          $("#combo-dropdown").empty();
          // Xử lý dữ liệu
          for (const obj of response) {
            const departmentId = obj.DepartmentId;
            const departmentName = obj.DepartmentName;
            const departmentCode = obj.DepartmentCode;

            // Build chuỗi html với 1 dòng dữ liệu
            let rowHTML = `<div class="combo-data__item" subvalue="${departmentId}"}>
                            <div class="w-30 item-code">${departmentCode}</div>
                            
                            <div class="w-70 item-name">${departmentName}</div>
                        </div>`;

            $(".row-input #conCombo #combo-dropdown").append(rowHTML);
          }
        },
        error: function (response) {
          console.log(response);
        },
      });
    } catch (error) {}
  },
};

export default dataHandleJS;
