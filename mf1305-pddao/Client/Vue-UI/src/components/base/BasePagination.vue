<template>
  <div class="pagination">
    <div class="pagination-bar">
      <div class="left-pagination">
        <div class="total-record">
          <p>
            Tổng số: <b> {{ totalData }} </b> bản ghi
          </p>
        </div>
        <!--  -->
        <!--  -->
      </div>
      <div class="right-pagination">
        <div class="record-in-page">
          <div class="record-in-page__value">
            <div class="con-tooltip">
              <div class="tooltip-parent">
                <div
                  class="tooltip"
                  style="left: 0px; top: 0px; position: fixed; display: none"
                ></div>
              </div>
              <div class="tooltip-content"></div>
              <!--  -->
            </div>
            <span>
              <div class="con-tooltip">
                <div class="tooltip-parent">
                  <div
                    class="tooltip"
                    style="left: 0px; top: 0px; position: fixed; display: none"
                  ></div>
                </div>
                <div class="tooltip-content">
                  <!-- <div class="combo-main-content">
                                    <div class="selected-option">
                                      <input
                                        type="text"
                                        size="1"
                                        placeholder="100 bản ghi"
                                        readonly="true"
                                        autofocus="autofocus"
                                        error="Error"
                                        class="combo-input"
                                        style="text-align: left"
                                      />
                                      
                                      
                                    </div>
                                    <div
                                      class="combo-action"
                                      style="display: flex"
                                    >
                                      <div class="btn-dropdown">
                                        <div
                                          class="mi mi-16 icon-arrow-dropdown"
                                        ></div>
                                      </div>
                                    </div>
                                  </div> -->
                  <div class="con-combo-editor" isShow="true">
                    <!-- <div class="combo-editor" required="true">
                      <div class="selected-input-option">
                        <input
                          ref="inputChangeRecord"
                          :value="pageItemText"
                          type="text"
                          class="combo-input"
                          size="1"
                          @blur="hideChangeRecord"
                        />
                      </div>
                      <div class="combo-action">
                        <div
                          @click="showChangeRecord"
                          class="btn-dropdown active"
                        >
                          <div
                            id="arrow-down"
                            class="mi mi-16 icon-arrow-dropdown"
                          ></div>
                        </div>
                      </div>
                    </div> -->
                    <BaseCombobox
                      ref="comboboxChangeRecord"
                      :dataList="recordPerPage"
                      :position="'top'"
                      :selectingValue="pageItemTotal"
                      :selectingText="pageItemText"
                      :size="'small'"
                      @passValue="changeRecord"
                    />
                    <!-- <div
                      class="combo-data"
                      style="
                        min-width: 30px;
                        width: 100%;
                        top: -105px;
                        z-index: 9999;
                      "
                      v-show="isShowChangeRecord"
                    >
                      <div
                        v-for="record in recordPerPage"
                        :key="record.value"
                        class="combo-data__item"
                        @click="changeRecord(record.value, record.text)"
                      >
                        {{ record.text }}
                      </div>
                    </div> -->
                  </div>
                </div>
                <!--  -->
              </div>
            </span>
          </div>
        </div>
        <div class="total-record" style="display: flex; align-items: center">
          <div
            class="prev-text"
            @click="getPreviousPage()"
            v-bind:class="isFistPage()"
          >
            Trước
          </div>

          <div
            class="page-index"
            v-for="data in totalPage()"
            :key="data"
            @click="getDataPage(data)"
          >
            <div v-if="isLast(data)">...</div>
            <!-- <div class="none" :class="isLeftMore(data)">...</div> -->
            <div class="pd-2" v-bind:class="isSelected(data)">
              {{ data }}
            </div>
            <div v-if="isFirst(data)">...</div>
            <!-- <div class="none" :class="isRightMore(data)">...</div> -->
          </div>

          <div
            class="next-text"
            @click="getNextPage()"
            v-bind:class="isLastPage()"
          >
            Sau
          </div>
        </div>
      </div>
    </div>
    <!-- <th class="out-righ-white-30"></th>
    <th class="out-right-grey-30"></th> -->
  </div>
</template>
<script>
import BaseCombobox from "./BaseCombobox.vue";

export default {
  name: "BasePagination",
  components: { BaseCombobox },
  data() {
    return {
      recordPerPage: [
        {
          text: "10 bản ghi/trang",
          value: 10,
        },
        {
          text: "20 bản ghi/trang",
          value: 20,
        },
        {
          text: "30 bản ghi/trang",
          value: 30,
        },
        {
          text: "50 bản ghi/trang",
          value: 50,
        },
        {
          text: "100 bản ghi/trang",
          value: 100,
        },
      ],
      pageItemTotal: "10",
      pageItemText: "10 bản ghi/trang",
      paginActual: 1,
      isShowChangeRecord: false,
      dataPaging: [],
      dataQuery: [],
    };
  },
  props: {
    data: {
      type: Array,
      default() {
        return [];
      },
    },
    totalData: {
      type: Array,
      default() {
        return [];
      },
    },
    searchQuery: String,
  },

  watch: {
    data: function (value) {
      this.dataQuery = value;
      this.getDataPage(1);
    },
    // searchQuery: function () {
    //   this.getDataPage(1);
    // },
  },
  methods: {
    /**
     * * Đếm só lượng phân trang
     * * AUTHOR: pddao (06/08/2022)
     */
    totalPage() {
      return Math.ceil(this.data.length / this.pageItemTotal); // return Math.ceil(this.employees.length / this.pageItemTotal);
    },

    /**
     * * Lấy dữ liệu cho từng trang
     * * * AUTHOR: pddao (06/08/2022)
     */
    getDataPage(numberOfPage) {
      this.paginActual = numberOfPage;

      this.dataPaging = [];
      let ini = numberOfPage * this.pageItemTotal - this.pageItemTotal;
      let fin = numberOfPage * this.pageItemTotal;
      this.dataPaging = this.data.slice(ini, fin);
      console.log(this.data);
      console.log(this.dataPaging);
      this.$emit("getDataPaging", this.dataPaging);
    },

    /**
     * * Lấy trang trước đó
     * * AUTHOR: pddao (06/08/2022)
     */
    getPreviousPage() {
      if (this.paginActual > 1) {
        this.paginActual--;
      }
      this.getDataPage(this.paginActual);
    },

    /**
     * * Lấy trang tiếp theo
     * * AUTHOR: pddao (06/08/2022)
     */
    getNextPage() {
      if (this.paginActual < this.totalPage()) {
        this.paginActual++;
      }
      this.getDataPage(this.paginActual);
    },

    /**
     * * Thêm class vào các nút khi click
     * * AUTHOR: pddao (06/08/2022)
     */
    isSelected(numberOfPage) {
      if (this.totalPage() > 5) {
        if (numberOfPage > 1 && numberOfPage < this.paginActual - 1) {
          return "hide-button";
        } else if (
          numberOfPage > this.paginActual + 1 &&
          numberOfPage < this.totalPage()
        ) {
          return "hide-button";
        }

        if (numberOfPage == this.paginActual) {
          return "page-selected";
        } else {
          return "";
        }
      } else {
        return numberOfPage == this.paginActual ? "page-selected" : "";
      }
    },

    /**
     * * kiểm tra nếu hiện tại là trang đầu thì nút "trước" sẽ đổi style
     * * AUTHOR: pddao (06/08/2022)
     */
    isFistPage() {
      if (this.paginActual == 1) {
        return "disable-text";
      } else {
        return "enabled-text";
      }
    },

    /**
     * * kiểm tra nếu hiện tại là trang cuối thì nút "sau" sẽ đổi style
     * * AUTHOR: pddao (06/08/2022)
     */
    isLastPage() {
      if (this.paginActual == this.totalPage()) {
        return "disable-text";
      } else {
        return "enabled-text";
      }
    },

    changeRecord(value, text) {
      this.pageItemTotal = value;
      this.pageItemText = text;
      this.getDataPage(1);
      // this.hideChangeRecord();
    },

    focusAndBlur() {
      if (this.focusOrBlur == false) {
        this.$refs.inputChangeRecord.blur();
      } else {
        this.$refs.inputChangeRecord.focus();
      }
    },
    showChangeRecord() {
      this.isShowChangeRecord = !this.isShowChangeRecord;

      // this.focusOrBlur = false;
    },
    hideChangeRecord() {
      if (this.isShowChangeRecord === true) {
        this.isShowChangeRecord = false;
        // this.focusOrBlur = true;
      }
    },
    isLast(numberOfPage) {
      if (
        this.paginActual <= this.totalPage() - 4 &&
        numberOfPage == this.totalPage()
      ) {
        return true;
      } else {
        return false;
      }
    },
    isFirst(numberOfPage) {
      if (this.paginActual >= 4 && numberOfPage == 1) {
        return true;
      } else {
        return false;
      }
    },
    // isRightMore(numberOfPage) {
    //   if (this.totalPages > 5) {
    //     if (numberOfPage == 1 && this.paginActual >= 4) {
    //       return "show-more";
    //     }
    //     return "";
    //   }
    // },
    // isLeftMore(numberOfPage) {
    //   if (this.totalPage() > 5) {
    //     if (
    //       numberOfPage == this.totalPage() &&
    //       this.paginActual <= this.totalPage() - 3
    //     ) {
    //       return "show-more";
    //     }
    //     return "";
    //   }
    // },
  },
};
</script>
<style scoped>
.combo-dropdown {
  width: 100%;
  display: absolute;
  bottom: 36px;
}
.total-record {
  display: flex;

  justify-content: end;
}

.hide-button {
  display: none;
}

.show-more {
  display: block;
}
</style>
