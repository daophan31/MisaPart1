var commonJS = {
  /**
   *  * Định dạng ngày tháng năm
   *  * AUTHOR: pddao (02/08/2022)
   */
  formatDate(result) {
    if (result) {
      result = new Date(result);
      // Lấy ra ngày;
      let date = result.getDate();
      date = date < 10 ? `0${date}` : date;
      // Lấy ra tháng:
      let month = result.getMonth() + 1;
      month = month < 10 ? `0${month}` : month;
      //Lấy ra năm:
      let year = result.getFullYear();
      result = `${date}/${month}/${year}`;
    }
    return result;
  },

  /**
   * * Đếm số lượng bản ghi của bảng
   * * AUTHOR: pddao (03/08/2022)
   */
  rowCount(element) {
    let count = $(element).length;

    return count;
  },
};

export default commonJS;
