const helpers = require('./helpers'),
  CopyWebpackPlugin = require('copy-webpack-plugin');

let config = {
  entry: {
    'main': helpers.root('/src/main.ts')
  },
  output: {
    path: helpers.root('/dist'),
    filename: 'js/[name].[hash].js'
  },
  devtool: 'source-map',
  resolve: {
    extensions: ['.ts', '.js', '.html','.json'],
    alias: {
      'vue$': 'vue/dist/vue.esm.js',
      'router': helpers.root('/src/router.ts'),
      'core-form': helpers.root('/src/components/form/index.ts'),
      'uimf-core': helpers.root('./node_modules/uimf-core/src/index.ts'),
      'core-ui': helpers.root('/src/core/ui'),
      'core-framework': helpers.root('/src/core/framework/index.ts'),
      'core-handlers': helpers.root('/src/core/handlers/index.ts'),
      'core-eventHandlers': helpers.root('/src/core/eventHandlers/index.ts'),
      'core-functions': helpers.root('/src/core/functions/index.ts'),
      'components': helpers.root('/src/components')
    }
  },
  module: {
    rules: [{
      test: /\.ts$/,
      enforce: 'pre',
      loader: 'tslint-loader'
    },
    {
      test: /\.ts$/,
      loader: 'awesome-typescript-loader'
    },
    {
      test: /\.html$/,
      loader: 'raw-loader',
      exclude: ['./src/index.html']
    }
    ],
  },
  plugins: [
    new CopyWebpackPlugin([{
      from: 'src/assets',
      to: './assets'
    },]),
  ]
};

module.exports = config;
