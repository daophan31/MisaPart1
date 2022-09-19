<template>
  <div class="header">
    <div
      id="collapse-nav"
      @click="collapseNavbar"
      class="header-icon mi mi-24 three-strips-icon"
      v-show="isToggle"
    ></div>
    <div class="header-branch">
      <div class="tooltip-content">
        <div class="header-branch-name">Công ty cổ phần Vô cùng kỳ diệu</div>
      </div>
      <a class="item-center">
        <div
          class="header-icon mi mi-14 icon-chevron-right header-branch-icon"
        ></div>
      </a>
    </div>
  </div>
</template>

<script>
export default {
  name: "TheHeader",
  props: {
    isToggle: Boolean,
  },
  emits: ["collapseNav"],
  methods: {
    collapseNavbar() {
      console.log("haha");
      this.$emit("collapseNav", false);
    },
  },
};
</script>
