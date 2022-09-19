var customeJS = {
  /**
   * * Ẩn hiện combobox
   * * AUTHOR: pddao (02/08/2022)
   */
  showHideComboData(element) {
    const target = $(element).parents(".con-combo-editor");

    const isActive = $(element).hasClass("active");

    if (isActive) {
      $(target).children("#combo-dropdown").show();
      $(target).children().css({
        border: "1px solid #2ca01c",
      });
      $(target).find("#arrow-down").css({
        transform: "rotate( -90deg )",
        transition: "transform 300ms ease",
      });

      $(element).removeClass("active");
    } else {
      $(target).children("#combo-dropdown").hide();
      $(target).find("#arrow-down").css({
        transform: "rotate( 0 )",
        transition: "transform 300ms ease",
      });

      $(target).children().css({
        border: "1px solid #babec5",
      });
      $(element).addClass("active");
    }
  },

  /**
   * * Chỉnh sửa width của thanh phân trang
   * * AUTHOR: pddao (02/08/2022)
   */
  setWidthPagination() {
    let width = $("#tblEmployee").width();

    $(".pagination").css({
      width: width,
    });
  },
};

export default customeJS;
