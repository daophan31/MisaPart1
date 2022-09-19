import axiosClient from "./axiosClient";

const employeeApi = {
  /**
   * * API Lấy tất cả danh sách nhân viên
   * * AUTHOR: pddao (20/08/2022)
   */
  getAllEmployee: () => {
    const url = "/api/v1/Employees";
    return axiosClient.get(url);
  },

  /**
   * * API Lấy thông tin nhân viên theo Id
   * * AUTHOR: pddao (20/08/2022)
   * @param {*employeeId Mã nhân viên} employeeId
   */
  getEmployeeById: (employeeId) => {
    const url = `/api/v1/Employees/${employeeId}`;
    return axiosClient.get(url);
  },

  /**
   * * API Thêm mới nhân viên
   * * AUTHOR: pddao (20/08/2022)
   * @param {*employee Thông tin nhân viên} employee
   */
  addNewEmployee: (employee) => {
    const data = JSON.stringify(employee);
    const url = `/api/v1/Employees`;
    console.log(data);
    console.log("employee");
    return axiosClient.post(url, data);
  },

  /**
   * * API Xóa nhân viên
   * * AUTHOR: pddao (20/08/2022)
   * @param {*Mã nhân viên} employeeId
   */
  deleteEmployee: (employeeId) => {
    const url = `api/v1/Employees/${employeeId}/`;
    return axiosClient.delete(url);
  },

  deleteMultipleEmployee: (employeeListId) => {
    const data = JSON.stringify(employeeListId);
    const url = `api/v1/Employees/delete-multiple`;
    return axiosClient.post(url, data);
  },

  /**
   * * API SỬa thông tin nhân viên
   * * AUTHOR: pddao (20/08/2022)
   * @param {*employeeId Id nhân viên} employeeId
   * @param {*employee Nội dung thay đổi} employee
   */
  updateEmployee: (employeeId, employee) => {
    const url = `api/v1/Employees/${employeeId}`;
    return axiosClient.put(url, employee);
  },

  filterEmployee: (params) => {
    const url = "api/v1/Employees/filter";
    return axiosClient.get(url, { params });
  },

  /**
   * * API Lấy mã nhân viên mới
   * * AUTHOR: pddao (20/08/2022)
   */
  getNewEmployeeCode: () => {
    const url = "api/v1/Employees/new-code";
    return axiosClient.get(url);
  },
};

export default employeeApi;
