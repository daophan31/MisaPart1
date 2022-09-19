<template>
  <div>
    <div class="con-combo-editor">
      <div
        class="combo-editor"
        ref="comboEditor"
        v-bind:class="{ active: isActive, error: hasError }"
      >
        <div class="selected-input-option">
          <input
            @bind="selected.text"
            ref="comboBoxInput"
            type="text"
            class="combo-input"
            size="1"
            @focus="setCbxFocus"
            @blur="
              () => {
                isShowData = false;
                checkNullWhenBlur();
              }
            "
            @keydown.down="inputKeyDown"
            @keydown.up="inputKeyUp"
            @keydown.enter="inputEnter"
          />
        </div>
        <div class="combo-action">
          <div
            id="btnDropDown"
            @mousedown.prevent="btnShowData"
            class="btn-dropdown active"
          >
            <div class="mi mi-16 icon-arrow-dropdown"></div>
          </div>
        </div>
      </div>
      <div
        id="combo-dropdown"
        class="combo-data"
        v-show="isShowData"
        :class="[this.size, this.position]"
      >
        <div
          v-for="(data, index) in dataMatch"
          :key="data.value"
          @mousedown.prevent="selectData(data)"
          @keydown.enter="selectData(data)"
          :ref="`comboboxItem${index}`"
          :class="{
            selected: selected.value == data.value,
            focus: this.indexItemFocus === index,
          }"
          class="combo-data__item"
          @keydown.down="test(index)"
          tabindex="1"
        >
          {{ data.text }}
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "BaseCombobox ",
  data() {
    return {
      isShowData: false,
      isDataEmpty: false,
      isActive: false,
      hasError: false,
      indexItemFocus: -1,
      selected: {
        text: "",
        value: "",
      },
      filterText: "",
      dataMatch: [],
      inputKeyDown: () => {
        if (this.indexItemFocus != this.dataMatch.length - 1) {
          if (this.indexItemFocus == -1) {
            this.isShowData = true;
            if (this.dataMatch.length != 0) {
              this.indexItemFocus = 0;
            }
          } else {
            if (this.indexItemFocus.length === this.dataMatch.length - 1) {
              this.dataMatch.length - 1;
            } else {
              this.indexItemFocus += 1;
            }
            this.$refs.comboBoxInput.value =
              this.dataMatch[this.indexItemFocus].text;
          }
        }
      },
      inputKeyUp: () => {
        if (this.indexItemFocus != -1) {
          if (this.indexItemFocus == 0) {
            this.isShowData = false;
            this.indexItemFocus = -1;
            if (this.dataMatch.length != 0) {
              this.indexItemFocus = -1;
            }
          } else {
            if (this.indexItemFocus.length === this.dataMatch.length - 1) {
              this.dataMatch.length + 1;
            } else {
              this.indexItemFocus -= 1;
            }
            this.$refs.comboBoxInput.value =
              this.dataMatch[this.indexItemFocus].text;
          }
        }
      },
    };
  },
  props: {
    dataList: {
      type: Array,
      default() {
        return [];
      },
    },
    selectingValue: String,
    selectingText: String,
    position: String,
    size: String,
  },
  emits: ["passValue"],
  beforeMount() {
    this.dataMatch = this.dataList;
    if (this.selectingValue) {
      this.selected.value = this.selectingValue;
      this.selected.text = this.selectingText;
    }
  },
  mounted() {
    this.$refs.comboBoxInput.value = this.selected.text;
  },
  watch: {
    // filterText: function () {
    //   this.selected.value = "";
    //   this.selected.text = "";
    //   this.indexItemFocus = -1;
    //   this.dataMatch = this.dataList.filter((item) =>
    //     isMatch(item.text.trim(), this.filterText.trim())
    //   );
    //   if (!this.filterText) {
    //     this.dataMatch = this.dataList;
    //     this.selected.value = "";
    //     this.selected.text = "";
    //   }
    //   if (this.dataMatch.length === 0) {
    //     this.isDataEmpty = true;
    //     this.selected.value = "";
    //     this.selected.text = "";
    //     this.comboBoxInput.classList.add("input-error");
    //   } else {
    //     this.isDataEmpty = false;
    //     this.comboBoxInput.classList.remove("input-error");
    //   }
    //   this.filterText ? (this.isShowData = true) : (this.isShowData = false);
    // },
  },
  methods: {
    setCbxFocus() {
      this.hasError = false;
      this.isActive = true;
    },
    checkNullWhenBlur() {
      this.isActive = false;
      this.indexItemFocus = -1;
      this.$refs.comboBoxInput.value = this.selected.text;
      if (this.selected.text == "") {
        this.hasError = true;
      } else {
        this.hasError = false;
      }
    },
    btnShowData() {
      this.isShowData = !this.isShowData;
      this.$refs.comboBoxInput.focus();
      if (this.isShowData == false) {
        this.checkNullWhenBlur();
      } else if (this.isShowData == true) {
        this.setCbxFocus();
      }
    },
    // bindDataToInput(id, name) {
    //   this.comboboxValId = id;
    //   this.comboboxValText = name;
    //   this.isShowData = false;
    //   this.checkNullWhenBlur();
    // },
    selectData(select) {
      this.selected.value = select.value;
      this.selected.text = select.text;
      this.indexItemFocus = -1;
      this.$refs.comboBoxInput.value = this.selected.text;
      this.isShowData = false;
      this.$emit("passValue", this.selected.value, this.selected.text);
    },
    blurInput() {
      this.$refs.comboBoxInput.focus();
      this.$refs.comboBoxInput.blur();
    },
    inputEnter() {
      try {
        if (this.indexItemFocus != -1) {
          this.selected.text = this.dataMatch[this.indexItemFocus].text;

          this.selected.value = this.dataMatch[this.indexItemFocus].value;

          this.isShowData = false;
          this.indexItemFocus = -1;
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>
<style scoped>
.active {
  border-color: #2ca01c;
}

.selected {
  background-color: #2ca01c;
  color: #fff;
}

.focus {
  background-color: #babec5;
  color: #2ca01c;
}
.error {
  border-color: red;
}

.small {
  width: 200px;
}

.large {
  width: 405px;
}

.top {
  position: absolute;
  top: -209px;
  z-index: 9999999;
  width: 100%;
}
</style>
