<template>
  <table id="tblEmployee" ref="tblEmp" class="emp-table-viewer">
    <thead class="thead-viewer">
      <tr class="tr-viewer">
        <th class="out-left-white-16"></th>

        <th class="th_viewer sticky" style="z-index: 25; left: 16px">
          <label class="lbcheckbox">
            <span class="checkbox">
              <el-checkbox
                v-model="isAllRowSelected"
                @change="onCheckAllChange"
              ></el-checkbox>
              <!-- <input type="checkbox" class="checkbox--input" /> -->
            </span>
          </label>
        </th>

        <!-- <div class="dis--content tbody--viewer"> -->
        <th
          class="th_viewer justify-left sticky"
          style="min-width: 179px; width: 179px; left: 55px; z-index: 20"
        >
          <span class="head--title">MÃ NHÂN VIÊN</span>
        </th>
        <th
          class="th_viewer justify-left sticky"
          style="min-width: 167px; width: 167px; left: 234px; z-index: 20"
        >
          <span class="head--title">TÊN NHÂN VIÊN</span>
        </th>
        <th
          class="th_viewer justify-center sticky index-20"
          style="min-width: 159px; width: 159px; left: 0px"
        >
          <span class="head--title">NGÀY SINH</span>
        </th>
        <th
          class="th_viewer justify-left sticky"
          style="min-width: 159px; width: 159px; left: 0px"
        >
          <span class="head--title">GIỚI TÍNH</span>
        </th>
        <th
          class="th_viewer justify-left sticky"
          style="min-width: 159px; width: 159px; left: 0px"
        >
          <span class="head--title">ĐƠN VỊ</span>
        </th>
        <th
          class="th_viewer justify-left sticky"
          style="min-width: 159px; width: 159px; left: 0px"
        >
          <span class="head--title">CHỨC VỤ</span>
        </th>
        <th
          class="th_viewer justify-left sticky"
          style="min-width: 159px; width: 159px; left: 0px"
        >
          <span class="head--title">SỐ CMND/CCCD</span>
        </th>
        <th
          class="th_viewer justify-center sticky"
          style="min-width: 159px; width: 159px; left: 0px"
        >
          <span class="head--title">NGÀY CẤP</span>
        </th>
        <th
          class="th_viewer justify-left sticky"
          style="min-width: 159px; width: 159px; left: 0px"
        >
          <span class="head--title">NƠI CẤP</span>
        </th>
        <th
          class="th_viewer justify-left sticky"
          style="min-width: 159px; width: 159px; left: 0px"
        >
          <span class="head--title">ĐỊA CHỈ</span>
        </th>

        <th
          class="th_viewer justify-left sticky"
          style="min-width: 159px; width: 159px; left: 0px"
        >
          <span class="head--title">ĐT DI ĐỘNG</span>
        </th>
        <th
          class="th_viewer justify-left sticky"
          style="min-width: 159px; width: 159px; left: 0px"
        >
          <span class="head--title">ĐIỆN THOẠI CỐ ĐỊNH</span>
        </th>

        <th
          class="th_viewer justify-left sticky"
          style="min-width: 159px; width: 159px; left: 0px"
        >
          <span class="head--title">EMAIL</span>
        </th>

        <th
          class="th_viewer justify-left sticky"
          style="min-width: 159px; width: 159px; left: 0px"
        >
          <span class="head--title">TÀI KHOẢN NGÂN HÀNG</span>
        </th>
        <th
          class="th_viewer justify-left sticky"
          style="min-width: 159px; width: 159px; left: 0px"
        >
          <span class="head--title sticky">TÊN NGÂN HÀNG</span>
        </th>

        <th
          class="th_viewer justify-left sticky"
          style="min-width: 159px; width: 159px; left: 0px"
        >
          <span class="head--title">CHI NHÁNH NGÂN HÀNG</span>
        </th>
        <th
          class="th_viewer justify-center sticky"
          style="min-width: 159px; width: 159px; left: 0px"
        >
          <span class="head--title">NGÀY TẠO</span>
        </th>
        <th
          class="th_viewer justify-left sticky"
          style="min-width: 159px; width: 159px; left: 0px"
        >
          <span class="head--title">NGƯỜI TẠO</span>
        </th>
        <th
          class="th_viewer justify-center sticky"
          style="min-width: 159px; width: 159px; left: 0px"
        >
          <span class="head--title">NGÀY SỬA</span>
        </th>
        <th
          class="th_viewer justify-left sticky"
          style="min-width: 159px; width: 159px; left: 0px"
        >
          <span class="head--title">NGƯỜI SỬA</span>
        </th>
        <!-- </div> -->
        <th
          class="th_viewer th-widget"
          style="right: 16px; width: 120px; min-width: 120px; border-right: 0px"
        >
          <span>CHỨC NĂNG </span>
        </th>
        <th class="out-right-white-30"></th>
      </tr>
    </thead>
    <tbody id="tbBody" class="dis-content tbody-viewer">
      <tr
        v-for="(employee, index) in employeeData"
        :key="index"
        class="tr-viewer revune-status"
        @dblclick="editAction(employee)"
      >
        <td class="out-left-white-16"></td>

        <td
          class="td-viewer td-multi"
          :class="{ rowSelected: compareRowList(employee.EmployeeID) }"
        >
          <label class="lb-checkbox">
            <!-- <input type="checkbox" class="checkbox--input" /> -->

            <span class="checkbox">
              <el-checkbox-group v-model="selectedRowList"
                ><el-checkbox
                  :label="employee.EmployeeID"
                  :checked="compareRowList(employee.EmployeeID)"
                  name="selectedRow"
                  @dblclick.stop=""
                ></el-checkbox
              ></el-checkbox-group>

              <!-- <input type="checkbox" class="checkbox--input" /> -->
            </span>
          </label>
        </td>
        <td
          class="td-viewer static-col justify-left"
          :class="{ rowSelected: compareRowList(employee.EmployeeID) }"
          style="left: 55px"
        >
          {{ employee.EmployeeCode }}
        </td>
        <td
          class="td-viewer static-col justify-left"
          :class="{ rowSelected: compareRowList(employee.EmployeeID) }"
          style="left: 234px"
        >
          {{ employee.FullName }}
        </td>
        <td
          class="td-viewer justify-center"
          :class="{ rowSelected: compareRowList(employee.EmployeeID) }"
        >
          {{ formatDate(employee.DateOfBirth) }}
        </td>
        <td
          class="td-viewer justify-left"
          :class="{ rowSelected: compareRowList(employee.EmployeeID) }"
        >
          {{ gender[employee.Gender] }}
        </td>
        <td
          class="td-viewer justify-left"
          :class="{ rowSelected: compareRowList(employee.EmployeeID) }"
        >
          {{ employee.DepartmentName }}
        </td>
        <td
          class="td-viewer justify-left"
          :class="{ rowSelected: compareRowList(employee.EmployeeID) }"
        >
          {{ employee.Position }}
        </td>
        <td
          class="td-viewer justify-left"
          :class="{ rowSelected: compareRowList(employee.EmployeeID) }"
        >
          {{ employee.IndentityNumber }}
        </td>
        <td
          class="td-viewer justify-center"
          :class="{ rowSelected: compareRowList(employee.EmployeeID) }"
        >
          {{ formatDate(employee.IndentityDate) }}
        </td>
        <td
          class="td-viewer justify-left"
          :class="{ rowSelected: compareRowList(employee.EmployeeID) }"
        >
          {{ employee.IndentityPlace }}
        </td>
        <td
          class="td-viewer justify-left"
          :class="{ rowSelected: compareRowList(employee.EmployeeID) }"
        >
          {{ employee.Address }}
        </td>
        <td
          class="td-viewer justify-left"
          :class="{ rowSelected: compareRowList(employee.EmployeeID) }"
        >
          {{ employee.MobileNumber }}
        </td>
        <td
          class="td-viewer justify-left"
          :class="{ rowSelected: compareRowList(employee.EmployeeID) }"
        >
          {{ employee.Hotline }}
        </td>
        <td
          class="td-viewer justify-left"
          :class="{ rowSelected: compareRowList(employee.EmployeeID) }"
        >
          {{ employee.Email }}
        </td>
        <td
          class="td-viewer justify-left"
          :class="{ rowSelected: compareRowList(employee.EmployeeID) }"
        >
          {{ employee.BankAccount }}
        </td>
        <td
          class="td-viewer justify-left"
          :class="{ rowSelected: compareRowList(employee.EmployeeID) }"
        >
          {{ employee.BankName }}
        </td>
        <td
          class="td-viewer justify-left"
          :class="{ rowSelected: compareRowList(employee.EmployeeID) }"
        >
          {{ employee.BankBranch }}
        </td>

        <td
          class="td-viewer justify-center"
          :class="{ rowSelected: compareRowList(employee.EmployeeID) }"
        >
          {{ formatDate(employee.CreatedDate) }}
        </td>
        <td
          class="td-viewer justify-center"
          :class="{ rowSelected: compareRowList(employee.EmployeeID) }"
        >
          {{ employee.CreatedBy }}
        </td>
        <td
          class="td-viewer justify-center"
          :class="{ rowSelected: compareRowList(employee.EmployeeID) }"
        >
          {{ formatDate(employee.ModifiedDate) }}
        </td>
        <td
          class="td-viewer justify-left"
          :class="{ rowSelected: compareRowList(employee.EmployeeID) }"
        >
          {{ employee.ModifiedBy }}
        </td>

        <td
          class="td-viewer td-widget"
          :class="{
            hightIndex: this.expandIndex == index,
            rowSelected: compareRowList(employee.EmployeeID),
          }"
          style="right: 16px"
        >
          <a
            tabindex="0"
            class="btn-edit-data pr-3"
            @click="editAction(employee)"
            >Sửa
          </a>

          <!-- <a
            class="btn-edit-data pl-3"
            @click="setDeleteId(employee.EmployeeId)"
          >
            Xóa</a
          > -->

          <div
            class="btn-option"
            tabindex="0"
            @click="setExpandIndex(index)"
            @keydown.enter="setExpandIndex(index)"
            @dblclick.stop=""
          >
            <div class="option-icon"></div>
            <div class="option-expand" v-show="this.expandIndex == index">
              <div
                class="option-expand__item"
                @click="copyEmployeeData(employee)"
              >
                Nhân bản
              </div>
              <div
                class="option-expand__item"
                @click="setDeleteId(employee.EmployeeID, employee.EmployeeCode)"
              >
                Xóa
              </div>
              <div class="option-expand__item">Ngưng sử dụng</div>
            </div>
          </div>
        </td>

        <td class="out-right-white-30"></td>
      </tr>
    </tbody>
  </table>

  <DeleteDialog
    :message="deleteMessage"
    :isShow="isShowDeleteDialog"
    @deleteAction="deleteAction"
    @showDeleteDialog="showDeleteDialog"
  />
  <!-- <DeleteDialog
    :isShow="isShowDeleteDialog"
    @deleteAction="deleteAction"
    @showDeleteDialog="showDeleteDialog"
  /> -->
</template>
<script>
import commonJS from "@/js/common.js";

import employeeApi from "@/services/api/employeeApi";
import DeleteDialog from "@/components/base/DeleteDialog.vue";

export default {
  name: "EmployeeList",
  comments: { DeleteDialog },
  props: { employeeData: {} },
  emits: [
    "reloadData",
    "showLoading",
    "showEmployeeDialog",
    "setAddOrUpdate",
    "setEmployeeSelected",
    "onCopyEmployee",
  ],
  data() {
    return {
      deleteId: "",
      deleteCode: "",
      isDeleteMultiple: false,
      isShowDeleteDialog: false,
      expandIndex: null,
      isAllRowSelected: false,
      checkedAll: false,
      selectedRowList: [],
      deleteMessage: "",
      gender: {
        0: "Nam",
        1: "Nữ",
        2: "Khác",
      },
    };
  },
  methods: {
    /**
     * * Hiện dialog xóa nhân viên
     * * AUTHOR: pddao(05/09/2022)
     */
    showDeleteDialog(isShow) {
      if (this.isDeleteMultiple) {
        this.deleteMessage = "Bạn có muốn xóa nhân viên đã chọn?";
      } else {
        this.deleteMessage = `Bạn có muốn xóa nhân viên <<${this.deleteCode}>>  không?`;
      }
      this.isShowDeleteDialog = isShow;
    },

    /**
     * * Gán giá trị id để xóa
     * * AUTHOR: pddao(05/09/2022)
     */
    setDeleteId(id, code) {
      this.isDeleteMultiple = false;
      this.deleteId = id;
      this.deleteCode = code;
      this.showDeleteDialog(true);
    },

    /**
     * * Hàm gọi xóa nhiều bản ghi
     * * AUTHOR: pddao(05/09/2022)
     */
    deleteMultiple() {
      if (this.selectedRowList != "") {
        this.isDeleteMultiple = true;
        this.showDeleteDialog(true);
      }
    },

    /**
     * * Thực thi xóa nhân viên
     * * AUTHOR: pddao(05/09/2022)
     */
    async deleteAction() {
      if (this.isDeleteMultiple) {
        await this.deleteMultipleRecord(this.selectedRowList);
      } else {
        await this.deleteRecord(this.deleteId);
      }
      this.showDeleteDialog(false);
      await this.$emit("reloadData");
    },

    /**
     * * API xóa nhân viên
     * * AUTHOR: pddao(05/09/2022)
     */
    async deleteRecord(id) {
      try {
        await employeeApi.deleteEmployee(id);
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * * API xóa nhiều nhân viên
     * * AUTHOR: pddao(05/09/2022)
     */
    async deleteMultipleRecord(listId) {
      try {
        await employeeApi.deleteMultipleEmployee(listId);
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * * Hiện phần lựa chọn
     * * AUTHOR: pddao(05/09/2022)
     */
    setExpandIndex(index) {
      if (this.expandIndex == index) {
        this.expandIndex = null;
      } else {
        this.expandIndex = index;
      }
    },

    /**
     * * Hành động sửa thông tin nhân viên
     * * AUTHOR: pddao(05/09/2022)
     */
    editAction(employee) {
      this.$emit("setEmployeeSelected", employee);
      this.$emit("showEmployeeDialog", true);
      this.$emit("setAddOrUpdate", false);
    },

    copyEmployeeData(employee) {
      this.$emit("onCopyEmployee", employee);
    },

    /**
     * * Kiểm tra checkbox chọn tất cả
     * * AUTHOR: pddao(05/09/2022)
     */
    onCheckAllChange(value) {
      try {
        if (value) {
          this.employeeData.forEach((employee) => {
            if (!this.selectedRowList.includes(employee.EmployeeID)) {
              this.selectedRowList.push(employee.EmployeeID);
            }
          });
        } else {
          this.employeeData.forEach((employee) => {
            if (this.selectedRowList.includes(employee.EmployeeID)) {
              this.selectedRowList.splice(
                this.selectedRowList.indexOf(employee.EmployeeID),
                1
              );
            }
          });
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * * Kiểm tra uncheck checkbox chọn tất
     * * AUTHOR: pddao(05/09/2022)
     */
    checkSelectedAll() {
      let checked = true;

      this.employeeData.forEach((employee) => {
        if (this.selectedRowList.includes(employee.EmployeeID)) {
          checked == checked;
        } else {
          checked = false;
        }
      });
      this.isAllRowSelected = checked;
    },
    compareRowList(employeeID) {
      return this.selectedRowList.includes(employeeID);
    },
  },
  watch: {
    employeeData: function () {
      this.checkSelectedAll();
    },
  },
  mixins: [commonJS],
  components: { DeleteDialog },
};
</script>
<style scoped>
.check-all {
  position: abolute;
  right: 20px;
}

.btn-option {
  position: relative;
  display: flex;
  width: 16px;
  height: 16px;
  background-color: transparent;
  justify-content: center;
  align-items: center;
  border: 1px solid transparent;
  cursor: pointer;
}
.btn-option:hover {
  border: 1px solid #c7c7c7;
}

.btn-option:focus-visible {
  outline: auto blue;
}

.btn-option:focus-within {
  border: 2px solid #c7c7c7;
}

.btn-option:active {
  border: 1px solid blue;
}

.option-icon {
  background: url("../../../assets/img/Sprites.64af8f61.svg") no-repeat -900.5px -365px;
  width: 8px;
  height: 5px;
}

.option-expand {
  position: absolute;
  background-color: #fff;
  width: 120px;
  border: 1px solid #c7c7c7;
  top: 18px;
  right: -20px;
}

.option-expand__item {
  width: 100%;
  height: 32px;
  padding-left: 12px;
  padding-right: 16px;
  box-sizing: border-box;
  line-height: 32px;
}

.option-expand__item:hover {
  background-color: #babec5;
}
.option-expand__item:active {
  background-color: blue;
  font-family: notosans-bold;
  color: #fff;
}

.hightIndex {
  z-index: 999;
}

.rowSelected {
  background: #e5f3ff !important;
}
</style>
