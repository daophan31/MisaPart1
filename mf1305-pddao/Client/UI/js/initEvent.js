import customeJS from "./custome.js";
import dataHandleJS from "./dataHandle.js";
import commonJS from "./common.js";
/**
 * * Thực hiện gắn sự kiện cho element
 * * AUTHOR: pddao (02/08/2022)
 */
const initEvent = () => {
  //  Bấm nút thêm mới nhân viên
  $("#add-employee-button").click(btnAddOnClick);

  // Đóng dialog thêm mới nhân viên
  $("#btn-close").click(btnCloseDialog);

  //Validate dữ liệu khi nhập vào
  $("[required]").blur(inputRequireOnBlur);

  //click vào button của combobox
  $("body #btnDropDown").click(function () {
    dataHandleJS.loadDataCombobox();
    customeJS.showHideComboData($(this));
  });

  //Bấm nút thu gọn menu
  $("#collapse-nav").click(collapseOnClick);
  //Bấm nút hiện đầy đủ menu

  $("#expand-nav").click(expandOnClick);

  //Lọc dữ liệu:
  $("#InputSearch").keyup(filterTable);

  //Nút sửa trên bảng dữ liệu:
  $("#tblEmployee").on("click", ".btn-edit-data", btnEditOnClick);
};

/**
 * * Thêm mới nhân viên
 * * AUTHOR: pddao (02/08/2022)
 */
function btnAddOnClick() {
  $("[prop-name]").value = "";
  try {
    $("#dlgEmployee").show();

    // Focus vào ô nhập liệu đầu tiên
    $("#txtEmployeeCode").focus();

    // Tự động lấy mã nhân viên mới
    $.ajax({
      type: "GET",
      url: "https://cukcuk.manhnv.net/api/v1/Employees/NewEmployeeCode",
      success: function (response) {
        $("#txtEmployeeCode").val(response);
      },
      error: function (response) {
        switch (response.status) {
          case 500:
            alert(response.responseJSON.userMsg);
            break;
          default:
            break;
        }
      },
    });
  } catch (error) {
    console.log(error);
  }
}

/**
 * * Đóng dialog thêm nhân viên
 * * AUTHOR: pddao (02/08/2022)
 */
function btnCloseDialog() {
  try {
    $(this).parents(".popup").hide();
  } catch (error) {
    console.log(error);
  }
}

/**
 *  * Validate
 *  * AUTHOR: pddao (02/08/2022)
 */
function inputRequireOnBlur() {
  try {
    const value = $(this).val();
    if (!value) {
      this.classList.add("border-red");
    } else {
      this.classList.remove("border-red");
    }
  } catch (error) {}
}

/**
 * * Lọc dữ liệu của bảng
 * * AUTHOR: pddao (02/08/2022)
 */
function filterTable() {
  var value = $(this).val().toLowerCase();
  $("#tblEmployee #tbBody tr").filter(function () {
    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
  });

  // Đặt lại width của thanh phân trang phục vụ thuộc tính sticky:
  customeJS.setWidthPagination();

  // // Đếm lại bản ghi:
  $(".pagination .pagination-bar .left-pagination .total-record b").text(
    commonJS.rowCount("#tblEmployee #tbBody tr:visible")
  );
}

/**
 * * Thu gọn menu
 * * AUTHOR: pddao (03/08/2022)
 */
function collapseOnClick() {
  $(".navbar-item__list .navbar-item .navbar-item__title").hide();
  $(".navbar__top .icon").hide();
  $(".navbar__top .logo").hide();
  $(".navbar__top .none").show();
  $("#collapse-nav").hide();
  try {
  } catch (error) {
    console.log(error);
  }
}

function expandOnClick() {
  try {
    $(".navbar-item__list .navbar-item .navbar-item__title").show();
    $(".navbar__top .icon").show();
    $(".navbar__top .logo").show();
    $(".navbar__top .none").hide();
    $("#collapse-nav").show();
  } catch (error) {
    console.log(error);
  }
}

function btnEditOnClick() {
  try {
    $("#dlgEmployee").show();
    //Lấy dữ liệu tương ứng vừa chọn
    let emp = $(this).parents("#tblEmployee #tbBody tr").data("data");

    //Lấy toàn bộ các input, select có thông tin prop-name
    let props = $("[prop-name]");

    // duyệt từng phần tử:
    for (const prop of props) {
      //Lấy ra tên của thuộc tính pop-name
      const propName = $(prop).attr("prop-name");

      //Lấy ra các giá trị tương ứng với prop-name có tên tương ứng
      let value = emp[propName];
      prop.value = !value ? "" : value;
    }
  } catch (error) {
    console.log(error);
  }
}

export default initEvent;
