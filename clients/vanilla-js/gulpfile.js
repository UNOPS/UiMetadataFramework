var gulp = require("gulp"),
    rollup = require("rollup"),
    typescript = require("rollup-plugin-typescript2"),
    commonjs = require('rollup-plugin-commonjs'),
    browserSync = require('browser-sync').create(),
    gulpSvelte = require('gulp-svelte'),
    resolve = require('rollup-plugin-node-resolve'),
    builtins = require('rollup-plugin-node-builtins'),
    globals = require('rollup-plugin-node-globals'),
    json = require('rollup-plugin-json');

function build(entry, tsconfig, outfile) {
    return rollup.rollup(
        {
            entry: entry,
            plugins: [
                json(),
                resolve({
                    jsnext: true,
                    main: true,
                    browser: true
                }),
                commonjs(),
                typescript({
                    tsconfig: tsconfig
                }),
                globals(),
                builtins()
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

gulp.task("build", ["build-app"], function () {
    build("src/umf-vanilla.ts", "src/tsconfig.json", "dist/umf-vanilla.js");
});

gulp.task("build-app", ["build-svelte"], function () {
    build("demo/src/app.ts", "demo/src/tsconfig.json", "demo/js/app.js");
});

gulp.task("browser-sync", function () {
    browserSync.init({
        server: {
            baseDir: "./demo"
        }
    });
});

gulp.task("build-svelte", function () {
    const svelteComponentsDir = "demo/svelte-components";
    gulp.src("node_modules/svelte/shared.js")
        .pipe(gulp.dest(svelteComponentsDir));

    return gulp.src('demo/src/**/*.html')
        .pipe(gulpSvelte({
            format: "es",
            generate: "dom",
            shared: true //"./shared.js"
        }))
        .pipe(gulp.dest(svelteComponentsDir));
});

gulp.task("watch", ["build-svelte", "build", "browser-sync"], function () {
    gulp.watch("src/**/*.ts", ["build"]);
    gulp.watch("demo/src/**/*.ts", ["build-app"]);
    gulp.watch("demo/src/**/*.html", ["build-svelte", "build-app"]);
});