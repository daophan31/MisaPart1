import axiosClient from "./axiosClient";

const departmentApi = {
  getDepartment: () => {
    const url = "/api/v1/Departments";
    return axiosClient.get(url);
  },
};

export default departmentApi;
