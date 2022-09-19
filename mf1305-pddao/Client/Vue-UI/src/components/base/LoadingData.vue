<template>
  <div v-if="isShow" class="warning-dialog">
    <div class="loading-container">
      <div class="loading-icon"></div>
      <div class="loading-text">Đang tải dữ liệu, vui lòng đợi</div>
    </div>
  </div>
</template>
<script>
export default {
  name: "LoadingData",
  props: ["isShow"],
};
</script>
<style>
.loading-container {
  z-index: 9999999999;
  position: absolute;
  display: flex;
  flex-direction: column;
  background: #fff;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  justify-content: center;
  align-items: center;
}

.loading-icon {
  background: url(../../assets/loading.svg) no-repeat 0 0;
  width: 32px;
  height: 32px;
  -webkit-animation: spin 2s infinite linear;
}

@-webkit-keyframes spin {
  0% {
    -webkit-transform: rotate(0deg);
  }
  100% {
    -webkit-transform: rotate(360deg);
  }
}
</style>
