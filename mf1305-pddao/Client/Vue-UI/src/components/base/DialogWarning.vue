<template>
  <div class="warning-dialog" @keyup.esc="closeDilog">
    <div class="dialog-container">
      <div class="warning-content">
        <div class="warning-content_detail">
          <div class="icon"></div>
          <div class="text">{{ message }}</div>
        </div>
      </div>
      <div class="warning-footer">
        <div class="left-footer">
          <button class="btn btn-normal" @click="cancelDialog">Hủy</button>
        </div>
        <div class="right-footer">
          <button
            ref="btnClose"
            class="btn btn-normal mr-6"
            @click="closeDilog"
          >
            Không
          </button>

          <button ref="btnAccept" @click="btnAccept" class="btn btn-add">
            Có
          </button>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "DialogWarning",
  components: {},
  data() {
    return {};
  },
  props: { isShow: Boolean, submitOrAdd: Boolean, message: String },
  emits: ["submit", "submitAndAdd", "showWarningDialog", "cancel"],
  methods: {
    btnAccept() {
      if (this.submitOrAdd) {
        this.$emit("submit");
      } else {
        this.$emit("submitAndAdd");
      }
    },
    closeDilog() {
      this.$emit("showWarningDialog", false);
    },
    cancelDialog() {
      this.closeDilog();
      this.$emit("cancel");
    },
  },
  mounted() {
    this.$refs.btnClose.focus();
  },
};
</script>
<style scoped>
.warning-dialog {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.4);
  z-index: 1000;
}
.dialog-container {
  border-radius: 3px;
  position: absolute;
  padding: 32px;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  background-color: #fff;
  min-width: 300px;
}
.warning-content {
  border-bottom: 1px solid #bbb;
  margin-bottom: 20px;
}
.warning-footer {
  display: flex;
  justify-content: space-between;
}
.right-footer {
  display: flex;
}
.warning-content_detail {
  display: flex;
  justify-content: center;
  align-items: center;
}
.warning-content_detail .text {
  padding: 12px 0 0 16px;
  margin-bottom: 32px;
}

.icon {
  margin-top: 6px;
  background: url(../../assets/img/Sprites.64af8f61.svg) no-repeat -832px -462px;
  width: 36px;
  height: 36px;
  margin-bottom: 20px;
}
</style>
