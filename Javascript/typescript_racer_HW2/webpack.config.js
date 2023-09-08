const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
    entry: './src/app.ts',

    module: {
        rules: [
            {
                test: /\.tsx?$/,
                use: 'ts-loader',
                exclude: /node_modules/,
            },
            {
                test: /\.css$/i,
                use: [
                 'style-loader', 'css-loader'
                ]
              },
                      {
            test: /\.(png|jpg|gif)$/i,
            use: {
                loader: 'file-loader',
                options: {
                    outputPath: './assets',
                },
            },
        },
        ],
    },
    resolve: {
        extensions: ['.ts', '.tsx', '.js', '.png'],
          alias: {
        assets: path.resolve(__dirname, 'src/assets')
        }
    },
    output: {
        filename: 'app.js',
        path: path.resolve(__dirname, 'dist'),
    },
    devServer: {
        static: {
            directory: path.join(__dirname, 'public')
        }
    },
    plugins: [
        new HtmlWebpackPlugin({
            template: "./src/index.html",
            inject: false,
            minify: false
        })
    ]
};