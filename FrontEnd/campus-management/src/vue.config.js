//const fs = require("fs");

module.exports = {
  configureWebpack: {
    devtool: "source-map"
  },
  node: {
    fs: "empty"
  }
};
