import { createRouter, createWebHistory } from "vue-router";
import EmployeePage from "../views/EmployeePage.vue";
import HomePage from "../views/HomePage.vue";
import CashPage from "../views/CashPage.vue";
import BankPage from "../views/BankPage.vue";
import BuyPage from "../views/BuyPage.vue";
import SalePage from "../views/SalePage.vue";
import InvoicePage from "../views/InvoicePage.vue";
import StockPage from "../views/StockPage.vue";
import ToolPage from "../views/ToolPage.vue";
import AssetPage from "../views/AssetPage.vue";
import TaxPage from "../views/TaxPage.vue";
import GeneralPage from "../views/GeneralPage.vue";
import BudgetPage from "../views/BudgetPage.vue";
import ReportPage from "../views/ReportPage.vue";
import FinancePage from "../views/FinancePage.vue";
import PricePage from "../views/PricePage.vue";

// *Khai báo router
const routes = [
  { path: "/cash", component: CashPage },
  { path: "/bank", component: BankPage },
  { path: "/buy", component: BuyPage },
  { path: "/sale", component: SalePage },
  { path: "/invoice", component: InvoicePage },
  { path: "/stock", component: StockPage },
  { path: "/tools", component: ToolPage },
  { path: "/fixed-asset", component: AssetPage },
  { path: "/tax", component: TaxPage },
  { path: "/general", component: GeneralPage },
  { path: "/budget", component: BudgetPage },
  { path: "/report", component: ReportPage },
  { path: "/finance", component: FinancePage },
  { path: "/price", component: PricePage },

  { path: "/dashboard", component: HomePage },
  { path: "/", component: EmployeePage },
];

// *Khởi tạo router
const vueRouter = createRouter({
  history: createWebHistory(),
  routes: routes,
});

export default vueRouter;
