const gulp = require('gulp');
const concat = require('gulp-concat');
const cleanCSS = require('gulp-clean-css');
const minify = require('gulp-minify');

const vendorStyles = [
    "node_modules/bootstrap/dist/css/bootstrap.min.css",
    "node_modules/@fortawesome/fontawesome-free/css/all.min.css",
    "wwwroot/css/site.css"
];
const vendorScripts = [
    "node_modules/jquery/dist/jquery.min.js",
    "node_modules/popper.js/dist/umd/popper.min.js",
    "node_modules/bootstrap/dist/js/bootstrap.min.js",
    "wwwroot/js/site.js"
];

gulp.task('build-vendor-css', () => {
    return gulp.src(vendorStyles)
        .pipe(concat('vendor.min.css'))
        .pipe(cleanCSS({ compatibility: 'ie8' }))
        .pipe(gulp.dest('wwwroot'));
});

gulp.task('build-vendor-js', () => {
    return gulp.src(vendorScripts)
        .pipe(concat('vendor.min.js'))
        .pipe(minify())
        .pipe(gulp.dest('wwwroot'));
});

gulp.task('font-awesome-fonts', () => {
    return gulp.src(['node_modules/@fortawesome/fontawesome-free/webfonts/fa-*'])
        .pipe(gulp.dest('wwwroot/webfonts'));
});

gulp.task('build-vendor', gulp.series('build-vendor-css', 'build-vendor-js'));

gulp.task('default', gulp.series('build-vendor'));