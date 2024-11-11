module.exports = {
    configureWebpack: {
        devServer: {
            disableHostCheck: true
        },
        optimization: {
            usedExports: true,
        },
        plugins: [],
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