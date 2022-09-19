import axios from "axios";
import queryString from "query-string";

const baseUrl = "http://localhost:25312/";
const axiosClient = axios.create({
  baseURL: baseUrl,
  headers: { Accept: "application/json", "Content-Type": "application/json" },
  maxContentLength: Infinity,
  maxBodyLength: Infinity,

  paramsSerializer: (params) => queryString.stringify({ ...params }),
});

axiosClient.interceptors.request.use(async (config) => {
  return config;
});

axiosClient.interceptors.response.use(
  (response) => {
    if (response && response.data) {
      return response.data;
    }
    return response;
  },
  (error) => {
    console.log(error);
  }
);

export default axiosClient;
