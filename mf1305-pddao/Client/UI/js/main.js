import initEvent from "./initEvent.js";
import dataHandleJS from "./dataHandle.js";

$(document).ready(function () {
  initEvent();
  
  dataHandleJS.loadData();
});