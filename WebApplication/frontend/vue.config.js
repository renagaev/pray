const BundleAnalyzerPlugin = require('webpack-bundle-analyzer').BundleAnalyzerPlugin;
module.exports = {
    configureWebpack: {
        devServer: {
            disableHostCheck: true
        },
        optimization: {
            usedExports: true,
        },
        plugins: [new BundleAnalyzerPlugin()],
        module:{
            rules:[
                {
                    test: /\.s[ac]ss$/i,
                    use: [
                        // Compiles Sass to CSS
                        "sass-loader",
                    ],
                },
            ]
        }
    }
}