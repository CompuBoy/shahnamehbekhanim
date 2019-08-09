const scriptsDir = 'Static/scripts/',
  stylesDir = 'Static/styles/';

const gulp = require('gulp');

gulp.task('build', ['scripts', 'css']);
gulp.task('default', ['build', 'watch']);

gulp.task('watch', function() {
  gulp.watch(['ts', 'tsx', 'vue'].map(e => scriptsDir + '**/*.' + e), ['scripts']);
  gulp.watch([stylesDir + '**/*.scss'], ['css']);
});

const webpackStream = require('webpack-stream');
const webpack = require('webpack');
const VueLoaderPlugin = require('vue-loader/lib/plugin');

gulp.task('scripts', function() {
  return gulp
    .src(scriptsDir + '*.js')
    .pipe(
      webpackStream(
        {
          mode: 'development',
          mode: 'production',
          entry: './' + scriptsDir + 'index.ts',
          output: {
            filename: 'main.js'
          },
          module: {
            rules: [
              {
                test: /\.tsx?$/,
                loader: "ts-loader",
                options: {
                  appendTsSuffixTo: [/\.vue$/]
                },
                exclude: /node_modules/,
              },
              {
                test: /\.vue$/,
                loader: 'vue-loader'
              },
              {
                test: /\.css$/,
                use: [
                  'vue-style-loader',
                  'css-loader'
                ]
              }
            ]
          },
          resolve: {
            extensions: ['.tsx', '.ts', '.js', '.vue'],
            alias: {
              vue: 'vue/dist/vue.js'
            }
          },
          plugins: [
            // make sure to include the plugin!
            new VueLoaderPlugin()
          ]
        },
        webpack
      )
    )
    .pipe(gulp.dest(scriptsDir + '_dist'));
});

// Sass --------------------------------------

const sass = require('gulp-sass');
const postcss = require('gulp-postcss');

const processors = [
  require('postcss-cssnext')(),
  require('postcss-assets')({ loadPaths: ['./Static/assets'] })
];

gulp.task('css', function() {
  return gulp
    .src(['./Static/styles/main.scss'])
    .pipe(sass())
    .pipe(postcss(processors))
    .pipe(gulp.dest('./Static/styles/_dist'));
});
