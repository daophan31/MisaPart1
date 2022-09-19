<template>
  <div
    v-if="isShow"
    @keydown.esc="btnCloseOnClick"
    @keydown.ctrl.s.exact="btnSubmit"
    @keydown.ctrl.shift.s="btnSubmitAndAdd"
    ref="dlgEmployee"
    id="dlgEmployee"
    class="popup"
  >
    <div class="add-employee-container">
      <div class="popup-content">
        <div class="popup--header">
          <div class="popup--title">
            <div style="display: flex">
              <div class="hd-pop-title">Thông tin nhân viên</div>
            </div>
          </div>
          <div class="popup--close">
            <div
              class="mi mi-24 icon-help"
              data-title="Giúp (F1)"
              style="padding-right: 6px"
            ></div>
            <div
              data-title="Đóng (ESC)"
              id="btn-close"
              class="mi mi-24 icon-close"
              @mousedown="btnCloseOnClick"
            ></div>
            <!-- data-title="Giúp (?)"
          data-title="Đóng (ESC)" -->
          </div>
        </div>
        <div class="popup--content">
          <div class="popup--content__employee">
            <div class="employee-general-info">
              <div class="w-1/2 pr-26">
                <span>
                  <div class="row-input">
                    <div class="w-2/5 pr-6">
                      <div class="ms-input">
                        <div class="flex">
                          <div class="input-title">Mã</div>
                          <div class="title-required">&nbsp;*</div>
                          <!-- <div
                            :class="{ shake: shake }"
                            class="error-message"
                            v-if="employeeCodeNotValid"
                          >
                            Trường này là bắt buộc
                          </div> -->
                        </div>
                        <div error-title="">
                          <div class="input-style-label">
                            <input
                              ref="inputEmpCode"
                              v-model="employee.EmployeeCode"
                              id="txtEmployeeCode"
                              type="text"
                              class="input-normal"
                              @blur.prevent="required('inputEmpCode')"
                              @focus="inputFocus('inputEmpCode')"
                              :class="{ error: employeeCodeNotValid }"
                              @mouseover="showEmployeeCodeToolTip = true"
                              @mouseout="showEmployeeCodeToolTip = false"
                            />
                          </div>

                          <ToolTip
                            v-show="
                              showEmployeeCodeToolTip && employeeCodeNotValid
                            "
                            :message="'Mã nhân viên không được để trống'"
                          />
                        </div>
                      </div>
                    </div>
                    <div class="w-3/5">
                      <div class="ms-input">
                        <div class="flex">
                          <div class="input-title">Tên</div>
                          <div class="title-required">&nbsp;*</div>
                          <!-- <div
                            :class="{ shake: shake }"
                            class="error-message"
                            v-if="employeeFullNameNotValid"
                          >
                            Trường này là bắt buộc
                          </div> -->
                        </div>
                        <div>
                          <div class="input-style-label">
                            <input
                              ref="inputEmployeeName"
                              type="text"
                              class="input-normal"
                              prop-name="FullName"
                              v-model="employee.FullName"
                              @blur="required('inputEmployeeName')"
                              @focus="inputFocus('inputEmployeeName')"
                              @mouseover="showFullNameToolTip = true"
                              @mouseout="showFullNameToolTip = false"
                              :class="{ error: employeeFullNameNotValid }"
                            />
                            <ToolTip
                              v-show="
                                showFullNameToolTip && employeeFullNameNotValid
                              "
                              :message="'Tên không được để trống'"
                            />
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="row-input">
                    <div class="w-full">
                      <div class="flex">
                        <div class="input-title">Đơn vị</div>
                        <div class="title-required">&nbsp;*</div>
                        <!-- <div
                          :class="{ shake: shake }"
                          class="error-message"
                          v-if="departmentNotValid"
                        >
                          Trường này là bắt buộc
                        </div> -->
                      </div>
                      <div class="combobox-style">
                        <BaseCombobox
                          @mouseover="showDepartmentNotValid = true"
                          @mouseout="showDepartmentNotValid = false"
                          ref="departmentCombobox"
                          :dataList="departmentData"
                          :size="'large'"
                          :selectingValue="this.employeeSelected.DepartmentID"
                          :selectingText="this.employeeSelected.DepartmentName"
                          @passValue="getComboboxValue"
                        /><ToolTip
                          v-show="departmentNotValid && showDepartmentNotValid"
                          :message="'Đơn vị không được để trống'"
                        />
                      </div>

                      <!-- <DepartmentCombobox /> -->
                    </div>
                  </div>
                  <div class="row-input">
                    <div class="w-full">
                      <div class="flex">
                        <div class="input-title">Chức danh</div>
                      </div>
                      <div>
                        <div class="input-style-label">
                          <input
                            v-model="employee.Position"
                            type="text"
                            class="input-normal"
                            prop-name="PositionName"
                          />
                        </div>
                      </div>
                    </div>
                  </div>
                </span>
              </div>
              <div class="w-1/2">
                <div class="row-input">
                  <div class="w-2/5 pr-6">
                    <div class="date-picker-container">
                      <div class="flex">
                        <div class="input-title">Ngày sinh</div>
                      </div>
                      <div>
                        <div
                          class="input-style-label"
                          @mouseover="showDateOfBirthNotValid = true"
                          @mouseout="showDateOfBirthNotValid = false"
                        >
                          <!-- <input
                            ref="dateOfBirthInput"
                            type="date"
                            class="input-normal"
                            prop-name="DateOfBirth"
                            :value="
                              formatDateToBind(employee.DateOfBirth) &&
                              formatDateToBind(employee.DateOfBirth)
                                .toISOString()
                                .split('T')[0]
                            "
                          /> -->

                          <el-date-picker
                            ref="inputEmployeeDateOfBirth"
                            v-model="dateOfBirthValue"
                            type="date"
                            placeholder="DD /MM /YYYY"
                            style="width: 100%"
                            format="DD/MM/YYYY"
                            :shortcuts="shortcuts"
                            @blur="pastOrPresentBobValidate(dateOfBirthValue)"
                            :class="{ 'input--error': dateOfBirthNotValid }"
                          />
                          <ToolTip
                            v-show="
                              dateOfBirthNotValid && showDateOfBirthNotValid
                            "
                            :message="'Ngày sinh không được lớn hơn ngày hiện tại'"
                          />
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="w-3/5">
                    <div class="flex">
                      <div class="input-title">Giới tính</div>
                    </div>
                    <div>
                      <div class="row-input">
                        <div class="w-30">
                          <span style="display: flex; align-items: center">
                            <input
                              tabindex="0"
                              v-model="genderCheck"
                              class="custome-checkbox"
                              type="radio"
                              value="1"
                            />

                            <div>Nam</div>
                          </span>
                        </div>
                        <div class="w-30">
                          <span style="display: flex; align-items: center">
                            <input
                              tabindex="1"
                              v-model="genderCheck"
                              class="custome-checkbox"
                              type="radio"
                              value="2"
                            />
                            Nữ
                          </span>
                        </div>
                        <div class="w-30">
                          <span style="display: flex; align-items: center">
                            <input
                              tabindex="1"
                              v-model="genderCheck"
                              class="custome-checkbox"
                              type="radio"
                              value="0"
                            />
                            Khác
                          </span>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="row-input">
                  <div class="w-3/5 pr-6">
                    <div class="flex">
                      <div
                        class="input-title"
                        data-title="Số chứng minh nhân dân"
                      >
                        Số CMND
                      </div>
                    </div>
                    <div>
                      <div class="input-style-label">
                        <input
                          type="text"
                          class="input-normal"
                          prop-name="IndentityNumber"
                          v-model="employee.IndentityNumber"
                        />
                      </div>
                    </div>
                  </div>
                  <div class="w-2/5">
                    <div class="date-picker-container">
                      <div class="flex">
                        <div class="input-title">Ngày cấp</div>
                      </div>
                      <div>
                        <div
                          class="input-style-label"
                          @mouseover="showIndentityDateNotValid = true"
                          @mouseout="showIndentityDateNotValid = false"
                        >
                          <el-date-picker
                            ref="inputEmployeeIndentityDate"
                            v-model="indentityDateValue"
                            type="date"
                            placeholder="DD/MM/YYYY"
                            style="width: 100%"
                            :shortcuts="shortcuts"
                            format="DD/MM/YYYY"
                            @blur="
                              pastOrPresentIndentityValidate(indentityDateValue)
                            "
                            :class="{ 'input--error': indentityDateNotValid }"
                          />
                          <ToolTip
                            v-show="
                              indentityDateNotValid && showIndentityDateNotValid
                            "
                            :message="'Ngày cấp không được lớn hơn ngày hiện tại'"
                          />
                          <!-- <input
                            ref="indentityDateInput"
                            type="date"
                            class="input-normal"
                            prop-name="IndentityDate"
                            :value="
                              formatDateToBind(employee.IndentityDate) &&
                              formatDateToBind(employee.IndentityDate)
                                .toISOString()
                                .split('T')[0]
                            "
                          /> -->
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="row-input">
                  <div class="w-full">
                    <div class="flex">
                      <div class="input-title">Nơi cấp</div>
                    </div>
                    <div>
                      <div class="input-style-label">
                        <input
                          type="text"
                          class="input-normal"
                          prop-name="IndentityPlace"
                          v-model="employee.IndentityPlace"
                        />
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="employee-advanced-info">
              <div class="row-input">
                <div class="w-full">
                  <div class="flex">
                    <div class="input-title">Địa chỉ</div>
                  </div>
                  <div>
                    <div class="input-style-label">
                      <input
                        type="text"
                        class="input-normal"
                        v-model="employee.Address"
                      />
                    </div>
                  </div>
                </div>
              </div>
              <div class="w-75">
                <div class="row-input">
                  <div class="w-30 pr-6">
                    <div class="flex">
                      <div class="input-title">Điện thoại di động</div>
                    </div>
                    <div>
                      <div class="input-style-label">
                        <input
                          type="text"
                          class="input-normal"
                          v-model="employee.MobileNumber"
                        />
                      </div>
                    </div>
                  </div>
                  <div class="w-30 pr-6">
                    <div class="flex">
                      <div class="input-title">Điện thoại cố định</div>
                    </div>
                    <div>
                      <div class="input-style-label">
                        <input
                          type="text"
                          class="input-normal"
                          v-model="employee.Hotline"
                        />
                      </div>
                    </div>
                  </div>
                  <div class="w-30">
                    <div class="flex">
                      <div class="input-title">Email</div>
                    </div>
                    <div>
                      <div class="input-style-label">
                        <input
                          ref="inputEmployeeEmail"
                          type="text"
                          class="input-normal"
                          v-model="employee.Email"
                          @blur="checkEmailValid(employee.Email)"
                          @focus="inputFocus('inputEmployeeEmail')"
                          @mouseover="showEmailNotValid = true"
                          @mouseout="showEmailNotValid = false"
                        />
                        <ToolTip
                          v-show="showEmailNotValid && emailNotValid"
                          :message="'Email sai định dạng'"
                        />
                      </div>
                    </div>
                  </div>
                </div>
                <div class="row-input">
                  <div class="w-30 pr-6">
                    <div class="flex">
                      <div class="input-title">Tài khoản ngân hàng</div>
                    </div>
                    <div>
                      <div class="input-style-label">
                        <input
                          type="text"
                          class="input-normal"
                          v-model="employee.BankAccount"
                        />
                      </div>
                    </div>
                  </div>
                  <div class="w-30 pr-6">
                    <div class="flex">
                      <div class="input-title">Tên ngân hàng</div>
                    </div>
                    <div>
                      <div class="input-style-label">
                        <input
                          type="text"
                          class="input-normal"
                          v-model="employee.BankName"
                        />
                      </div>
                    </div>
                  </div>
                  <div class="w-30">
                    <div class="flex">
                      <div class="input-title">Chi nhánh</div>
                    </div>
                    <div>
                      <div class="input-style-label">
                        <input
                          type="text"
                          class="input-normal"
                          v-model="employee.BankBranch"
                        />
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="flex">
            <div class="popup-tab w-full"></div>
          </div>
          <div class="footer-container">
            <div class="divide"></div>
            <div class="popup--footer">
              <div class="flex">
                <div class="w-full flex align-right">
                  <div class="right-group-button-popup flex">
                    <div
                      data-title="Cất (Ctrl + S)"
                      class="first-right-button px-3"
                    >
                      <!-- data-title="Cất và thêm (Ctrl + Shift + S)"
  data-title="Cất (Ctrl + S)" -->
                      <button class="btn btn-normal" @click="btnSubmit">
                        Cất
                      </button>
                    </div>
                    <div
                      data-title="Cất (Ctrl + Shift + S)"
                      class="second-right-button"
                    >
                      <button @click="btnSubmitAndAdd" class="btn btn-add">
                        Cất và thêm
                      </button>
                    </div>
                  </div>
                </div>
                <div class="w-1/2 align-left" style="position: absolute">
                  <div class="left-group-button">
                    <button class="btn btn-normal" @click="btnCancel">
                      Hủy
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <DialogWarning
    @submit="submit"
    @submitAndAdd="submitAndAdd"
    @cancel="btnCancel"
    @showWarningDialog="showWarningDialog"
    :isShow="showWarning"
    :submitOrAdd="submitOrAdd"
    :message="dialogMessage"
    v-if="showWarning"
  />
  <BaseToastMessage
    :isShow="isShowToastMessage"
    :texts="toastMessage"
    @actionClose="showToastMassage"
  />
</template>
<script>
//import DepartmentCombobox from "../../base/DepartmentCombobox.vue";
import DialogWarning from "../../base/DialogWarning.vue";
import departmentApi from "../../../services/api/departmentApi.js";
import BaseCombobox from "../../base/BaseCombobox.vue";
import employeeApi from "@/services/api/employeeApi.js";
import BaseToastMessage from "@/components/base/BaseToastMessage.vue";
import ToolTip from "@/components/base/ToolTip.vue";

export default {
  name: "EmployeeDetail",

  data() {
    return {
      isShowToastMessage: false,
      employee: {},
      genderCheck: 1,
      showWarning: false,
      submitOrAdd: true,
      departmentData: [],
      dateOfBirthValue: Date,
      indentityDateValue: Date,
      shortcuts: [
        {
          text: "Hôm nay",
          value: new Date(),
        },
      ],
      toastMessage: [],
      employeeCodeNotValid: false,
      employeeFullNameNotValid: false,
      departmentNotValid: false,
      emailNotValid: false,
      dateOfBirthNotValid: false,
      indentityDateNotValid: false,
      shake: false,
      isDataChanged: false,
      dialogMessage: "",
      showFullNameToolTip: false,
      showEmployeeCodeToolTip: false,
      showDepartmentNotValid: false,
      showEmailNotValid: false,
      showDateOfBirthNotValid: false,
      showIndentityDateNotValid: false,
    };
  },
  props: {
    isShow: Boolean,
    isAddOrUpdate: Boolean,
    employeeSelected: Object,
  },
  emits: ["isShowDialog", "btnAddOnClick", "loadData"],
  watch: {
    /**
     * * Theo dõi thay đổi của giá trị isAddOrUpdate được gửi qua props
     * * AUTHOR: pddao (10/08/2022)
     */
    isAddOrUpdate: function (value) {
      this.addOrUpdate = value;
    },
    /**
     * * Gán giá trị genderCheck
     * * AUTHOR: pddao (10/08/2022)
     */
    employee: function () {
      this.genderCheck = this.employee.Gender;
      this.isDataChanged = true;
    },
    employeeCodeNotValid: function () {
      if (this.employeeCodeNotValid) {
        this.toastMessage.push("Mã nhân viên không được để trống.");
      } else {
        this.toastMessage.splice("Mã nhân viên không được để trống.", 1);
      }
    },
    employeeFullNameNotValid: function () {
      if (this.employeeFullNameNotValid) {
        this.toastMessage.push("Tên nhân viên không được để trống.");
      } else {
        this.toastMessage.splice("Tên nhân viên không được để trống.", 1);
      }
    },
    departmentNotValid: function () {
      if (this.departmentNotValid) {
        this.toastMessage.push("Đơn vị không được để trống.");
      } else {
        this.toastMessage.splice("Đơn vị không được để trống.", 1);
      }
    },
    dateOfBirthNotValid: function () {
      if (this.dateOfBirthNotValid) {
        this.toastMessage.push("Ngày sinh không được lớn hơn ngày hiên tại.");
      } else {
        this.toastMessage.splice(
          "Ngày sinh không được lớn hơn ngày hiên tại.",
          1
        );
      }
    },
    indentityDateNotValid: function () {
      if (this.indentityDateNotValid) {
        this.toastMessage.push("Ngày cấp không được lớn hơn ngày hiên tại.");
      } else {
        this.toastMessage.splice(
          "Ngày cấp không được lớn hơn ngày hiên tại.",
          1
        );
      }
    },
    emailNotValid: function () {
      if (this.emailNotValid) {
        this.toastMessage.push("Email sai định dạng.");
      } else {
        this.toastMessage.splice("Email sai định dạng.", 1);
      }
    },
  },
  mounted() {
    //focus vào ô input mã nhân viên
    this.$refs.inputEmpCode.focus();
    this.employee = this.employeeSelected;
    this.dateOfBirthValue = this.employeeSelected.DateOfBirth;
    this.indentityDateValue = this.employeeSelected.IndentityDate;

    if (this.isAddOrUpdate == true) {
      this.getNewEmployeeCode();
    }
    // console.log(this.employeeSelected);
    // console.log(this.employee);
    this.isDataChanged = false;
  },
  created() {
    document.addEventListener("keyup", this.btnCloseOnClickKeyUpESC);
    document.addEventListener("keydown", this.btnSubmitHandleKeyPress);
    this.getDepartmentData();
  },
  methods: {
    /**
     * * API lấy dữ liệu đơn vị
     * * AUTHOR: pddao (26/08/2022)
     */
    async getDepartmentData() {
      try {
        const response = await departmentApi.getDepartment();
        response.forEach((department) => {
          this.departmentData.push({
            value: department.DepartmentID,
            text: department.DepartmentName,
          });
        });
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * * Dóng dialog chi tiết nhân viên
     * * AUTHOR: pddao (10/08/2022)
     */
    btnCloseOnClick() {
      if (this.isDataChanged) {
        this.dialogMessage = "Dữ liệu đã bị thay đổi. Bạn có muốn lưu không?";
        this.showWarningDialog(true);
        this.submitOrAdd = true;
      } else {
        this.$refs.inputEmpCode.blur();
        this.$refs.inputEmployeeName.blur();
        this.$emit("isShowDialog", false);
      }
    },

    /**
     * * Đóng dialog chi tiết nhân viên
     * * AUTHOR: pddao(30/08/2022)
     */
    btnCancel() {
      this.$refs.inputEmpCode.blur();
      this.$refs.inputEmployeeName.blur();
      this.$emit("isShowDialog", false);
    },

    /**
     * * Đóng dialog chi tiết nhân viên bằng phím tắt
     * * AUTHOR: pddao(30/08/2022)
     */
    btnCloseOnClickKeyUpESC(e) {
      if (e.key == "Escape") {
        this.$emit("isShowDialog", false);
      }
    },
    /**
     * * Nút Cất trong form chi tiết
     * * AUTHOR: pddao (10/08/2022)
     */
    btnSubmit() {
      this.dialogMessage = "Bạn có muốn cất không";
      if (this.validate()) {
        this.showWarningDialog(true);
        this.submitOrAdd = true;
        document.removeEventListener("keyup", this.btnCloseOnClickKeyUpESC);
      } else {
        this.showToastMassage(true);
      }
      console.log(this.toastMessage);
    },

    /**
     * * Lưu chi tiết nhân viên
     * * AUTHOR: pddao(30/08/2022)
     */
    btnSubmitHandleKeyPress(e) {
      if (e.key == "Control" && e.key == "S") {
        this.btnSubmit();
      }
    },

    /**
     * * Nút Cất và Thêm trong form chi tiết
     * * AUTHOR: pddao (10/08/2022)
     */
    btnSubmitAndAdd() {
      this.dialogMessage = "Bạn có muốn cất không";
      if (this.validate()) {
        this.showWarningDialog(true);
        this.submitOrAdd = false;
      } else {
        this.showToastMassage(true);
      }
    },

    /**
     * * Function thực thi cất sẽ được gửi qua props
     * * AUTHOR: pddao (10/08/2022)
     */
    submit() {
      this.pullEmployeeData();
      if (this.isAddOrUpdate) {
        this.addNewData(this.employee);
      } else {
        this.updateData(this.employee.EmployeeID);
      }
      this.$emit("isShowDialog", false);
      this.showWarningDialog(false);
    },

    /**
     * * Function thực thi cất vả thêm sẽ được gửi qua props
     * * AUTHOR: pddao (10/08/2022)
     */
    submitAndAdd() {
      this.submit();
      this.$emit("btnAddOnClick");
    },

    /**
     * * Gọi API thêm mới
     * * Author: pddao (10/08/2022)
     */
    async addNewData(employee) {
      try {
        await employeeApi.addNewEmployee(employee);
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * * Gọi API Cập nhật (sửa)
     * * AUTHOR: pddao (10/08/2022)
     */
    async updateData(employeeID) {
      try {
        const response = await employeeApi.updateEmployee(
          employeeID,
          this.employee
        );
        console.log(response);
      } catch (error) {
        console.log(error);
      }
      // fetch("https://cukcuk.manhnv.net/api/v1/Employees", {
      //   method: "PUT",
      //   headers: {
      //     "Content-Type": "application/json",
      //   },
      //   body: JSON.stringify(this.employee),
      // })
      //   .then((res) => {
      //     if (res.ok) {
      //       console.log("Put successful");
      //       this.$emit("loadData");
      //     } else {
      //       console.log("Put unsuccessful");
      //     }
      //     return res;
      //   })
      //   .then((res) => res.json())
      //   .then((data) => console.log(data))
      //   .catch((err) => console.log(err));
    },
    /**
     * * Tự động lấy mã nhân viên mới
     * * AUTHOR: pddao(30/08/2022)
     */
    async getNewEmployeeCode() {
      try {
        const res = await employeeApi.getNewEmployeeCode();
        this.employee.EmployeeCode = res;
      } catch (err) {
        console.log(err);
      }
    },

    /**
     * * Cập nhật lại object employee sau khi giá trị các biến trung gian thay đổi
     * * AUTHOR: pddao (10/08/2022)
     */
    pullEmployeeData() {
      this.employee.Gender = Number(this.genderCheck);

      this.employee.DateOfBirth = this.dateOfBirthValue;
      this.employee.IndentityDate = this.indentityDateValue;
    },

    /**
     * * Hiện dialog nhắc nhở
     * * AUTHOR: pddao (10/08/2022)
     * @param {*} isShow
     */
    showWarningDialog(isShow) {
      this.showWarning = isShow;
    },

    /**
     * * Hiện toast massage thông báo
     * * AUTHOR: pddao(30/08/2022)
     */
    showToastMassage(showToastMessage) {
      this.isShowToastMessage = showToastMessage;
    },

    /**
     * * Định dạng lại ngày khi binding lên form chi tiết
     * * Author: pddao (10/08/2022)
     * @param {} result
     */
    formatDateToBind(result) {
      if (result) {
        result = new Date(result);
        let date = new Date(
          result.getTime() + Math.abs(result.getTimezoneOffset() * 60000)
        );
        return date;
      }
    },

    /**
     * * Regex định dạng email
     * * AUTHOR: pddao(05/09/2022)
     */
    validateEmail(email) {
      return email.match(
        /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
      );
    },

    /**
     * * Định dạng email
     * * AUTHOR: pddao(05/09/2022)
     */
    checkEmailValid(email) {
      if (email) {
        if (this.validateEmail(email)) {
          this.$refs.inputEmployeeEmail.classList.remove("error");
          this.emailNotValid = false;
        } else {
          this.$refs.inputEmployeeEmail.classList.add("error");
          this.emailNotValid = true;
        }
      }
    },

    /**
     * * Validate ngày sinh không được lớn hơn ngày hiện tại
     * * AUTHOR: pddao(30/08/2022)
     */
    pastOrPresentBobValidate(dateValue) {
      if (dateValue) {
        let date = new Date(dateValue);

        let dateNow = new Date();

        if (date > dateNow) {
          this.dateOfBirthNotValid = true;
        } else {
          this.dateOfBirthNotValid = false;
        }
      }
    },

    /**
     * * Validate ngày cấp CMND được lớn hơn ngày hiện tại
     * * AUTHOR: pddao(30/08/2022)
     */
    pastOrPresentIndentityValidate(dateValue) {
      if (dateValue) {
        let date = new Date(dateValue);
        let dateNow = new Date();

        if (date > dateNow) {
          this.indentityDateNotValid = true;
        } else {
          this.indentityDateNotValid = false;
        }
      }
    },

    /**
     * * Validate dữ liệu đầu vào các trường bắt buộc
     * * AUTHOR: pddao (25/08/2022)
     */
    validate() {
      // Validate trường mã nhân viên bắt buộc
      if (!this.$refs.inputEmpCode.value) {
        this.employeeCodeNotValid = true;
      } else {
        this.employeeCodeNotValid = false;
      }

      // Validate trường Tên nhân viên bắt buộc
      if (this.$refs.inputEmployeeName.value == "") {
        this.employeeFullNameNotValid = true;
      } else {
        this.employeeFullNameNotValid = false;
      }

      // validate trường Đơn vị bắt buộc
      if (this.employee.DepartmentID == null) {
        this.departmentNotValid = true;
        console.log("aaa");
        this.$refs.departmentCombobox.blurInput();
      } else {
        this.departmentNotValid = false;
      }

      this.shake = true;
      setTimeout(() => {
        this.shake = false;
      }, 1500);
      if (
        this.employeeCodeNotValid == false &&
        this.employeeFullNameNotValid == false &&
        this.departmentNotValid == false
      ) {
        return true;
      } else {
        return false;
      }
    },

    /**
     * * Set style cho các trường bắt buộc
     * * AUTHOR: pddao (26/08/2022)
     * @param {} ref
     */
    required(ref) {
      if (this.$refs[ref].value == "") {
        this.$refs[ref].classList.add("error");
      } else {
        this.$refs[ref].classList.remove("error");
      }
    },

    /**
     * * Focus vào và xóa báo lỗi
     * * AUTHOR: pddao(30/08/2022)
     */
    inputFocus(ref) {
      this.$refs[ref].classList.remove("error");
    },

    /**
     * * Lấy giá trị từ combobox
     * * AUTHOR: pddao (26/08/2022)
     * @param {*} value
     * @param {*} text
     */
    getComboboxValue(value, text) {
      this.employee.DepartmentID = value;
      this.employee.DepartmentName = text;
    },
    /**
     * * Hiện, ẩn tooltip
     * * AUTHOR: pddao(30/08/2022)
     */
    fullNameToolTip() {
      return this.employeeFullNameNotValid == false ? true : false;
    },
  },

  unmounted() {
    this.employeeCodeNotValid = false;
    this.employeeFullNameNotValid = false;
    this.departmentNotValid = false;
  },

  components: { DialogWarning, BaseCombobox, BaseToastMessage, ToolTip },
};
</script>
<style scoped>
.el-date-picker__inner.el-date-picker__inner {
  width: 100% !important;
}

.remove-width {
  width: 100%;
}

.error {
  border: 1px solid red;
}

.error-message {
  color: red;
  font-family: notosans-bold;
  font-size: 11px;
}

.shake {
  animation: shake 0.82s cubic-bezier(0.36, 0.07, 0.19, 0.97) both;
  transform: translate3d(0, 0, 0);
}

@keyframes shake {
  10%,
  90% {
    transform: translate3d(-1px, 0, 0);
  }

  20%,
  80% {
    transform: translate3d(2px, 0, 0);
  }

  30%,
  50%,
  70% {
    transform: translate3d(-4px, 0, 0);
  }

  40%,
  60% {
    transform: translate3d(4px, 0, 0);
  }
}

.combobox-style {
  position: relative;
}

.input--error {
  border: 1px solid red;
}
</style>
