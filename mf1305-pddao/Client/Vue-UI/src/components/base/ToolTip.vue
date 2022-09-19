<template>
  <div class="tool-tip">
    <div class="tool-tip__arrow">
      <div class="arrow-up"></div>
      <!-- <div class="arrow-down"></div>
      <div class="arrow-left"></div>
      <div class="arrow-right"></div> -->
    </div>
    <div class="tool-tip__content">
      {{ message }}
    </div>
  </div>
</template>
<script>
export default {
  name: "ToolTip",
  props: { message: String },
};
</script>
<style>
.tool-tip {
  position: absolute;
  top: 100%;
  left: 50%;
  transform: translateX(-50%);
  z-index: 20;
  white-space: nowrap;
  animation: fadein 1s;
  -moz-animation: fadein 1s; /* Firefox */
  -webkit-animation: fadein 1s; /* Safari and Chrome */
  -o-animation: fadein 1s; /* Opera */
}

@keyframes fadein {
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
}
@-moz-keyframes fadein {
  /* Firefox */
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
}
@-webkit-keyframes fadein {
  /* Safari and Chrome */
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
}
@-o-keyframes fadein {
  /* Opera */
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
}
.tool-tip__arrow {
  display: flex;
  width: 100%;
  justify-content: center;
}

.arrow-up {
  width: 0;
  height: 0;
  border-left: 5px solid transparent;
  border-right: 5px solid transparent;

  border-bottom: 5px solid red;
}

.tool-tip__content {
  border-radius: 3px;
  padding: 4px 8px;
  background: red;
  color: white;
}

/* .arrow-down {
  width: 0;
  height: 0;
  border-left: 20px solid transparent;
  border-right: 20px solid transparent;

  border-top: 20px solid #f00;
}

.arrow-right {
  width: 0;
  height: 0;
  border-top: 60px solid transparent;
  border-bottom: 60px solid transparent;

  border-left: 60px solid green;
}

.arrow-left {
  width: 0;
  height: 0;
  border-top: 10px solid transparent;
  border-bottom: 10px solid transparent;

  border-right: 10px solid blue;
} */
</style>
