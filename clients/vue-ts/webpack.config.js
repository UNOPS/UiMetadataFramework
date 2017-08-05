'use strict';

var path = require('path')
var webpack = require('webpack')
const TsConfigPathsPlugin = require('awesome-typescript-loader').TsConfigPathsPlugin;

const rootDir = path.resolve('./');
console.log(path.resolve('./'));

var ignore = new webpack.IgnorePlugin(new RegExp("(uimf-core)/"))

module.exports = {
  context: path.resolve(__dirname, 'src'),
  entry: './main.ts',
  resolve: {
    extensions: ['.js', '.ts', '.vue', '.json'],
    alias: {
      vue$: 'vue/dist/vue.esm.js'
    },
    plugins: [
      new TsConfigPathsPlugin()
    ]
  },
  output: {
    path: path.resolve(__dirname, './dist'),
    publicPath: '/dist/',
    filename: 'build.js'
  },
  module: {
    rules: [{
      test: /\.ts$/,
      loader: 'ts-loader',
      exclude: /node_modules|vue\/src/,
      options: {
        appendTsSuffixTo: [/\.vue$/]
      }
    },
    {
      test: /\.vue$/,
      loader: 'vue-loader',
      options: {
        esModule: true
      }
    },
    {
      test: /\.(png|jpg|gif|svg)$/,
      loader: 'file-loader',
      options: {
        name: '[name].[ext]?[hash]'
      }
    }
    ]
  },
  devServer: {
    historyApiFallback: true,
    noInfo: true,
    contentBase: path.resolve(rootDir),
    port: 9000
  },
  performance: {
    hints: false
  },
  devtool: '#eval-source-map'
}

if (process.env.NODE_ENV === 'production') {
  module.exports.devtool = '#source-map'
  // http://vue-loader.vuejs.org/en/workflow/production.html
  module.exports.plugins = (module.exports.plugins || []).concat([
    new webpack.DefinePlugin({
      'process.env': {
        NODE_ENV: '"production"'
      }
    })
  ])
}