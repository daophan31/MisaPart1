<template>
  <div class="navbar">
    <div class="fixed-nav">
      <div tabindex="-1" class="navbar__top">
        <!-- <div class="navbar__top__content"> -->

        <div
          id="expand-nav"
          class="header-icon mi mi-24 three-strips-icon"
          v-show="isToggle == false"
          @click="expandNavBar"
        ></div>

        <a href="" class="icon" v-show="isToggle"></a>
        <router-link to="/" class="logo"
          ><img
            v-show="isToggle"
            src="../../assets/img/Logo_Module_TiengViet_White.66947422.svg"
            alt=""
            class="img__icon"
        /></router-link>
        <!-- </div> -->
      </div>
      <div class="navbar-item__list">
        <router-link
          tabindex="-1"
          v-for="item in menuItems"
          :key="item.link"
          v-bind:to="item.link"
          class="navbar-item"
          v-bind:class="selectedItem(item.link)"
          @click="setSelectedItem(item.link)"
        >
          <div class="navbar-item__icon">
            <div v-bind:class="item.icon"></div>
          </div>
          <div v-show="isToggle" class="navbar-item__title">
            {{ item.title }}
          </div>
        </router-link>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "TheNavbar",
  data() {
    return {
      menuItems: [
        {
          icon: "nav-icon icon-dashboard",
          title: "Tổng quan",
          link: "/dashboard",
        },
        {
          icon: "nav-icon icon-cash",
          title: "Tiền mặt",
          link: "/cash",
        },
        {
          icon: "nav-icon icon-bank",
          title: "Tiền gửi",
          link: "/bank",
        },
        {
          icon: "nav-icon icon-buy",
          title: "Mua hàng",
          link: "/buy",
        },
        {
          icon: "nav-icon icon-sale",
          title: "Bán hàng",
          link: "/sale",
        },
        {
          icon: "nav-icon icon-invoice",
          title: "Quản lý hóa đơn",
          link: "/invoice",
        },
        {
          icon: "nav-icon icon-stock",
          title: "Kho",
          link: "/stock",
        },
        {
          icon: "nav-icon icon-tools",
          title: "Công cụ dụng cụ",
          link: "/tools",
        },
        {
          icon: "nav-icon icon-fixed-asset",
          title: "Tài sản cố định",
          link: "/fixed-asset",
        },
        {
          icon: "nav-icon icon-tax",
          title: "Thuế",
          link: "/tax",
        },
        {
          icon: "nav-icon icon-price",
          title: "Giá thành",
          link: "/price",
        },
        {
          icon: "nav-icon icon-general",
          title: "Tổng hợp",
          link: "/general",
        },
        {
          icon: "nav-icon icon-budget",
          title: "Ngân sách",
          link: "/budget",
        },
        {
          icon: "nav-icon icon-report",
          title: "Báo cáo",
          link: "/report",
        },
        {
          icon: "nav-icon icon-finance",
          title: "Phân tích tài chính",
          link: "/finance",
        },
      ],
      selectedMark: "",
    };
  },
  props: {
    isToggle: Boolean,
  },
  emits: ["expandNav"],
  methods: {
    selectedItem(link) {
      return this.selectedMark == link ? "menu-item-selected" : "";
    },
    setSelectedItem(link) {
      this.selectedMark = link;
    },
    expandNavBar() {
      this.$emit("expandNav", true);
    },
  },
};
</script>
