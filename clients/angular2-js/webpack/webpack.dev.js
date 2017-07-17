'use strict';

const HtmlWebpack = require('html-webpack-plugin');
var ExtractTextPlugin = require('extract-text-webpack-plugin');
const path = require('path');
const webpack = require('webpack');
const ChunkWebpack = webpack.optimize.CommonsChunkPlugin;

const rootDir = path.resolve('./');

var webpackNode = {
  // do not include poly fills...
  console: false,
  process: false,
  global: false,
  buffer: false,
  __filename: false,
  __dirname: false
};


console.log(rootDir);

module.exports = {
    debug: true,
    devServer: {
        contentBase: path.resolve(rootDir),
        port: 9000
    },
    devtool: 'source-map',
    entry: {
        app: [path.resolve(rootDir, 'src', 'bootstrap')],
        vendor: [path.resolve(rootDir, 'src', 'vendor')],
        
    },
    node: webpackNode,
    module: {
        loaders: [
            { loader: 'raw', test: /\.(css|html)$/ },
            { exclude: /node_modules/, loader: 'ts', test: /\.ts$/ },
            { test: /\.scss$/,  exclude: /node_modules/,  loaders: ['raw-loader','sass-loader?outputStyle=compressed&sourceComments=false'] },
        ]
    },
    output: {
        filename: '[name].bundle.js',
        path: path.resolve(rootDir)
    },
    plugins: [
        new ChunkWebpack({
            filename: 'vendor.bundle.js',
            minChunks: Infinity,
            name: 'vendor'
        }),
        new webpack.DefinePlugin({
            'process.env.BROWSER': JSON.stringify(true),
        }),
        new ExtractTextPlugin({ filename: 'bundle.css', disable: false, allChunks: true }),

        new HtmlWebpack({
            filename: 'index.html',
            inject: 'body',
            template: path.resolve(rootDir, 'src', 'index.html')
        })
    ],
    resolve: {
        extensions: ['', '.js', '.ts', '.css', '.scss'],
        root: path.resolve(rootDir),
    }
};