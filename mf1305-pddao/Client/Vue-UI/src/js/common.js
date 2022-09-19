export default {
  methods: {
    /**
     * * Định dạng ngày tháng
     * * AUTHOR: pddao (06/06/2022)
     */
    formatDate(result) {
      try {
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
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * * ĐỊnh dạng tiền VND
     * * AUTHOR: pddao (06/08/2022)
     */
    formatMoney(value) {
      try {
        let newVal = new Intl.NumberFormat("vi-vn", {
          style: "currency",
          currency: "VND",
        }).format(value);
        return newVal;
      } catch (error) {
        console.log(error);
      }
    },
  },
};
