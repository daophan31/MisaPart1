import { createApp } from "vue";

import App from "./App.vue";
import vueRouter from "./routes/router.js";
import "element-plus/dist/index.css";
// import "./config/css/index.css";
import "./config/css/element-ui.css";
import ElementPlus from "element-plus";
import locale from "element-plus/es/locale/lang/vi";
import JsonExcel from "vue-json-excel3";

// *Sử dụng
const app = createApp(App);

app.component("downloadExcel", JsonExcel);
app.use(ElementPlus, { locale });
app.use(vueRouter);
app.mount("#app");
