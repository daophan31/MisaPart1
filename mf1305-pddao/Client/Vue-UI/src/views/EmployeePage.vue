<template>
  <div class="layout-dictionary">
    <div class="title-distance">
      <div class="header-di">
        <div class="title-di">Nhân viên</div>
        <div class="button-di">
          <button
            id="add-employee-button"
            @click="btnAddOnClick()"
            class="btn btn-add"
          >
            <div class="button-text">Thêm mới nhân viên</div>
          </button>
        </div>
      </div>
    </div>
    <div class="layout-dictionary-body">
      <div class="top-option sticky">
        <div class="batch-option" @click="showMultipleAction">
          <div class="batch-option__text">Thực hiện hàng loạt</div>
          <div class="batch-option__icon"></div>
          <div v-show="isShowMultipleAction" class="batch-option__action">
            <div
              class="multiple-action"
              @click="$refs.employeeList.deleteMultiple()"
            >
              Xóa
            </div>
          </div>
        </div>

        <div class="search-option">
          <input
            id="InputSearch"
            class="search-input"
            placeholder="Tìm theo mã, tên nhân viên"
            v-model="searchQuery"
          />
          <div class="ultility-button">
            <div
              @click="btnRefesh"
              class="refesh-button"
              style="padding: 0 6px"
              data-title="Lấy lại dữ liệu"
            >
              <div class="mi mi-24 icon-refresh"></div>
            </div>
            <div
              class="excel-export__button"
              style="padding: 0 6px"
              data-title="Xuất ra excel"
            >
              <download-excel
                :data="export_field"
                :fields="json_field"
                worksheet="My Worksheet"
                name="employee.xls"
              >
                <div class="mi mi-24 icon__excel-export"></div>
              </download-excel>
            </div>
          </div>
        </div>
      </div>
      <div style="position: relative">
        <LoadingData :isShow="isShowloading" />
        <div
          ref="employeeTable"
          style="
            position: relative;
            overflow: auto;
            min-height: calc(100vh - 261px);
            max-height: calc(100vh - 261px);

            background-color: #fff;
          "
        >
          <div class="viewer-table">
            <div class="viewer-table-scroll">
              <EmployeeList
                ref="employeeList"
                :employeeData="dataPage"
                @reloadData="btnRefesh"
                @showLoading="showLoading"
                @showEmployeeDialog="showEmployeeDialog"
                @setAddOrUpdate="setAddOrUpdate"
                @setEmployeeSelected="setEmployeeSelected"
                @onCopyEmployee="btnAddOnClick"
              />
            </div>
          </div>
        </div>
      </div>
      <BasePagination
        ref="pagination"
        :data="resultQuery"
        :totalData="totalCount"
        :searchQuery="searchQuery"
        @getDataPaging="getDataPage"
      />
    </div>
  </div>
  <EmployeeDetail
    :isAddOrUpdate="isAddOrUpdate"
    :isShow="isShowDetail"
    v-if="isShowDetail"
    :employeeSelected="employeeSelected"
    @isShowDialog="showEmployeeDialog"
    @btnAddOnClick="btnAddOnClick"
    @loadData="loadData"
  />
</template>

<script>
import commonJS from "../js/common.js";
import EmployeeDetail from "@/components/page/employee/EmployeeDetail.vue";
import LoadingData from "@/components/base/LoadingData.vue";
import BasePagination from "@/components/base/BasePagination.vue";
import EmployeeList from "@/components/page/employee/EmployeeList.vue";
import employeeApi from "@/services/api/employeeApi";
import enums from "../resources/enums.js";
import _ from "lodash";

export default {
  name: "EmplopyeePage",
  components: {
    EmployeeDetail,

    LoadingData,
    BasePagination,
    EmployeeList,
  },
  data: () => {
    return {
      json_field: {
        STT: "Count",
        "Mã nhân viên": "EmployeeCode",
        "Tên nhân viên": "FullName",
        "Ngày sinh": "DateOfBirth",
        "Giới tính": "Gender",
        "Đơn vị": "DepartmentName",
        "Vị trí": "Position",
        "Số CMND/CCCD": "IndentityNumber",
        "Ngày cấp CMND/CCCD": "IndentityDate",
        "Nơi cấp CMND/CCCD": "IndentityPlace",
        "Địa chỉ": "Address",
        "Số di động": "MobileNumber",
        "Điện thoại cố định": "Hotline",
        Email: "Email",
        "Tài khoản ngân hàng": "BankAccount",
        "Tên ngân hàng": "BankName",
        "Chi nhánh ngân hàng": "BankBranch",
      },
      export_field: [],
      isAddOrUpdate: true, // Thêm là true, sửa là false
      isShowDeleteDialog: false,
      deleteId: "",
      searchQuery: "",
      resultQuery: Array,
      employees: [],
      eployeeSelected: {},
      isShowDetail: false,
      dataPage: [],
      focusOrBlur: true,
      isShowloading: false,
      isShowMultipleAction: false,
      totalCount: Number,
    };
  },
  watch: {
    dataPage: function () {
      this.$refs.employeeTable.scrollTo({ top: 0, behavior: "smooth" });
      let count = 1;
      this.resultQuery.forEach((data) => {
        this.export_field.push({
          Count: count,
          EmployeeCode: data.EmployeeCode,
          FullName: data.FullName,
          DateOfBirth: commonJS.methods.formatDate(data.DateOfBirth),
          Gender: enums.gender[data.Gender],
          DepartmentName: data.DepartmentName,
          Position: data.Position,
          IndentityNumber: `'${data.IndentityNumber}`,
          IndentityDate: commonJS.methods.formatDate(data.IndentityDate),
          IndentityPlace: data.IndentityPlace,
          Address: data.Address,
          MobileNumber: data.MobileNumber,
          Hotline: data.Hotline,
          Email: data.Email,
          BankAccount: data.BankAccount,
          BankName: data.BankName,
          BankBranch: data.BankBranch,
        });
        count++;
      });
      console.log(this.export_field);
    },
    searchQuery: _.debounce(function () {
      this.filterEmployee();
    }, 500),
  },
  methods: {
    /**
     * * Hiển thị form thêm mới
     * * AUTHOR: pddao (06/08/2022)
     */
    btnAddOnClick(employee) {
      if (employee) {
        this.employeeSelected = employee;
      } else {
        this.employeeSelected = {};
      }

      this.isAddOrUpdate = true;
      this.showEmployeeDialog(true);
    },
    /**
     * * Đặt biến isShow là true hoặc false để gọi cả từ component cha và con
     * * AUTHOR: pddao (06/08/2022)
     */
    showEmployeeDialog(isShow) {
      this.isShowDetail = isShow;
    },

    /**
     * * Hiện Dialog
     * * AUTHOR: pddao (06/08/2022)
     */
    showDeleteDialog(isShow) {
      this.isShowDeleteDialog = isShow;
    },

    /**
     * * Gán gía trị id xóa
     * * AUTHOR: pddao(05/09/2022)
     */
    setDeleteId(id) {
      this.deleteId = id;
      this.showDeleteDialog(true);
    },

    /**
     * * Sửa nhân viên tương ứng
     * * AUTHOR: pddao (06/08/2022)
     * @param {} employee
     */
    editAction(employee) {
      this.employeeSelected = employee;
      this.showEmployeeDialog(true);
      this.isAddOrUpdate = false;
    },

    /**
     * * Xóa nhân viên tương ứng
     * * AUTHOR: pddao (06/08/2022)
     */
    async deleteAction() {
      try {
        await employeeApi.deleteEmployee(this.deleteId);
        this.showDeleteDialog(false);

        await this.loadData();
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Làm mới lại trang
     */
    btnRefesh() {
      this.loadData();
    },

    /**
     * * Lấy dữ liệu
     * * AUTHOR: pddao(05/09/2022)
     */
    async loadData() {
      /**
       * * Lấy dữ liệu từ API và truyền vào mảng employees
       * * AUTHOR: pddao (06/08/2022)
       */
      try {
        this.showLoading(true);
        await this.filterEmployee();
        // const response = await employeeApi.getAllEmployee();
        // this.employees = response;
        this.showLoading(false);
      } catch (error) {
        console.log(error.message);
      }
    },

    /**
     * * Lấy dữ liệu theo phân trang
     * * AUTHOR: pddao(06/08/2022)
     * @param {} data
     */
    async getDataPage(data) {
      this.dataPage = await data;
      console.log(this.dataPage);
    },

    /**
     * * Hiện icon loading
     * * AUTHOR: pddao(05/09/2022)
     */
    showLoading(show) {
      this.isShowloading = show;
    },

    /**
     * * Gán hành động là thêm mới hay cập nhật
     * * AUTHOR: pddao(05/09/2022)
     */
    setAddOrUpdate(bool) {
      this.isAddOrUpdate = bool;
    },

    /**
     * * Gán dữ liệu cho nhân viên được chọn
     * * AUTHOR: pddao(05/09/2022)
     */
    setEmployeeSelected(employee) {
      this.employeeSelected = employee;
    },

    showMultipleAction() {
      this.isShowMultipleAction = !this.isShowMultipleAction;
    },

    /**
     * * API tìm kiếm
     * * AUTHOR: pddao(05/09/2022)
     */
    async filterEmployee() {
      try {
        this.showLoading(true);
        const params = {
          keyword: this.searchQuery,
          pageLimit: -1,
        };
        const response = await employeeApi.filterEmployee(params);

        if (response && response.Data) {
          this.resultQuery = response.Data;
          this.totalCount = response.TotalCount;
        }
        this.showLoading(false);
      } catch (error) {
        console.log(error);
      }
    },
  },
  created() {
    this.loadData();
  },
  computed: {
    /**
     * * Lấy giá trị của bảng sau khi lọc bằng tìm kiếm
     * * AUTHOR: pddao (06/08/2022)
     */
  },
  mixins: [commonJS],
};
</script>
<style>
.ultility-button {
  display: flex;
}

.batch-option {
  position: relative;
  display: flex;
  border: 2px solid #000;
  border-radius: 50ch;
  box-sizing: border-box;
  padding: 0 16px 0 16px;
  align-items: center;
  cursor: pointer;
  height: 36px;
}

.batch-option:hover {
  background-color: #e0e0e0;
}

.batch-option:active {
  background-color: #dad3d6;
}

.batch-option__text {
  font-family: notosans-bold;
  font-size: 12px;
  font-weight: 600;
  padding-right: 4px;
}

.batch-option__icon {
  background: url(../assets/img/Sprites.64af8f61.svg) no-repeat -564px -365px;
  width: 8px;
  height: 5px;
}

.batch-option__action {
  position: absolute;
  height: 32px;
  /* padding-left: 12px;
  padding-right: 16px; */
  padding: 2px 0 2px 0;
  box-sizing: border-box;
  line-height: 26px;
  background: #fff;
  border: 1px solid #dad3d6;
  top: 40px;
  width: 100px;
  left: 60px;
  border-radius: 2px;
}

.multiple-action {
  padding: 0 16px;
}

.multiple-action:hover {
  color: #2ca01c;
  background: #e5e8ec;
}
</style>
