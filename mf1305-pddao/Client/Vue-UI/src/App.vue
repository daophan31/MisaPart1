<template>
  <TheNavbar :isToggle="isToggle" @expandNav="changeToggle" />
  <div class="main">
    <TheHeader :isToggle="isToggle" @collapseNav="changeToggle" />
    <TheContent />
  </div>
</template>

<script>
import TheHeader from "./components/layout/TheHeader.vue";
import TheNavbar from "./components/layout/TheNavbar.vue";
import TheContent from "./components/layout/TheContent.vue";

export default {
  name: "App",
  components: {
    TheHeader,
    TheNavbar,
    TheContent,
  },
  data() {
    return {
      isToggle: true,
    };
  },
  methods: {
    changeToggle(bool) {
      this.isToggle = bool;
    },
  },
};
</script>

<style>
@import url(./css/main.css);
</style>
