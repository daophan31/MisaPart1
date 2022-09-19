const { defineConfig } = require("@vue/cli-service");
module.exports = defineConfig({
  transpileDependencies: true,
  // devServer: {
  //   proxy: "http://localhost:25312",
  // },
  configureWebpack: {
    devtool: "source-map",
  },
});
