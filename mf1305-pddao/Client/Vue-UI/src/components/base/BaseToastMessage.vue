<template>
  <div
    v-if="isShow"
    class="warning-dialog"
    @keydown.esc="btnCancel"
    @keydown.enter="btnAccept"
  >
    <div class="dialog-container">
      <div class="warning-content">
        <div class="warning-content_detail">
          <div class="icon"></div>
          <div class="text">
            <div v-for="text in texts" :key="text">{{ text }}</div>
          </div>
        </div>
      </div>
      <div class="warning-footer">
        <div class="right-footer">
          <button @click="btnClose" ref="btnAccept" class="btn btn-add">
            Đóng
          </button>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "DeleteDialog",
  data() {
    return {};
  },
  mounted() {
    // this.$refs.btnAccept.focus();
  },
  methods: {
    btnClose() {
      this.$emit("actionClose", false);
    },
  },
  props: {
    isShow: Boolean,
    texts: Array,
  },
  emits: ["actionClose"],
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
  justify-content: center;
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
.warning-content_detail .icon {
  background: url(../../assets/img/Sprites.64af8f61.svg) no-repeat -593px -145px;
  width: 22px;
  height: 23px;
  margin-bottom: 20px;
}
</style>
