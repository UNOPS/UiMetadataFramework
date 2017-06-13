var gulp = require("gulp"),
    rollup = require("rollup"),
    typescript = require("rollup-plugin-typescript2"),
    browserSync = require('browser-sync').create();

function build(entry, tsconfig, outfile) {
    return rollup.rollup(
        {
            entry: entry,
            plugins: [
                typescript({
                    tsconfig: tsconfig
                })
            ]
        })
        .then(function (bundle) {
            bundle.write({
                format: "iife",
                dest: outfile,
                sourceMap: true
            });
        });
}

gulp.task("build", function () {
    build("src/umf-vanilla.ts", "src/tsconfig.json", "demo/js/umf-vanilla.js");
    build("demo/src/app.ts", "demo/src/tsconfig.json", "demo/js/app.js");
});

gulp.task("build-app", function () {
    build("demo/src/app.ts", "demo/src/tsconfig.json", "demo/js/app.js");
});

gulp.task("browser-sync", function () {
    browserSync.init({
        server: {
            baseDir: "./demo"
        }
    });
});

gulp.task("watch", ["build", "browser-sync"], function () {
    gulp.watch("src/**/*.ts", ["build"]);
    gulp.watch("demo/src/**/*.ts", ["build-app"]);
});