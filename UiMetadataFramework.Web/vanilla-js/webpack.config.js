const path = require("path");
const webpack = require("webpack");

module.exports = {
	target: "web",
	entry: "./window.umf.ts",
	output: {
		filename: "umf.js",
		path: path.resolve(__dirname, "dist")
	},
	devServer: {
		contentBase: "./",
		//publicPath: "./wwwroot",
		compress: true,
		port: 9000
	},
	module: {
		rules: [
			{
				test: /\.ts?$/,
				exclude: /(node_modules|bower_components)/,
				use: [
					{
						loader: "babel-loader",
						options: {
							presets: ["env"]
							//plugins: ['transform-runtime']
						}
					},
					{
						loader: "ts-loader",
						options: {
							configFileName: "tsconfig.json"
						}
					}]
			},
			{
				enforce: "pre",
				test: /\.js$/,
				loader: "source-map-loader"
			},
			{
				enforce: "pre",
				test: /\.ts$/,
				use: "source-map-loader"
			}
		]
	},
	resolve: {
		extensions: [".ts", ".js"]
	},
	plugins: [
		new webpack.optimize.UglifyJsPlugin()
	],
	devtool: "inline-source-map"
};