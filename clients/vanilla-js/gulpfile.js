var gulp = require("gulp"),
    rollup = require("rollup"),
    typescript = require("rollup-plugin-typescript2"),
    commonjs = require("rollup-plugin-commonjs"),
    browserSync = require("browser-sync").create(),
    gulpSvelte = require("gulp-svelte"),
    async = require('rollup-plugin-async'),
    resolve = require("rollup-plugin-node-resolve"),
    builtins = require("rollup-plugin-node-builtins"),
    globals = require("rollup-plugin-node-globals"),
    json = require("rollup-plugin-json"),
    sass = require("gulp-sass"),
    concat = require("gulp-concat");

const distDir = "./wwwroot";

function build(entry, tsconfig, outfile, moduleName) {
    return rollup.rollup(
        {
            entry: entry,
            plugins: [
                json(),
                async(),
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
                sourceMap: true,
                moduleName: moduleName
            });
        });
}

gulp.task("build-app", ["build-svelte"], function () {
    build("src/app.ts", "src/tsconfig.json", distDir + "/js/app.js");
});

gulp.task("browser-sync", function () {
    browserSync.init({
        server: {
            baseDir: distDir
        }
    });
});

gulp.task("build-svelte", function () {
    const svelteComponentsDir = "build/svelte";
    gulp.src("node_modules/svelte/shared.js")
        .pipe(gulp.dest(svelteComponentsDir));

    return gulp.src("src/**/*.html")
        .pipe(gulpSvelte({
            format: "es",
            generate: "dom",
            shared: true
        }))
        .pipe(gulp.dest(svelteComponentsDir));
});

gulp.task("copy-assets", function () {
    gulp.src(["wwwroot/assets/*"])
        .pipe(gulp.dest(distDir + "/assets"));

    return gulp.src(["wwwroot/index.html"])
        .pipe(gulp.dest(distDir));
});

gulp.task("sass", function () {
    return gulp.src(["src/**/*.scss", "src/**/*.css"])
        .pipe(sass().on("error", sass.logError))
        .pipe(concat("main.css"))
        .pipe(gulp.dest(distDir + "/css/"));
});

gulp.task("watch", ["build-svelte", "build-app", "sass", "copy-assets", "browser-sync"], function () {
    gulp.watch("src/**/*.ts", ["build-app"]);
    gulp.watch("src/**/*.html", ["build-svelte", "build-app"]);
    gulp.watch("src/**/*.scss", ["sass"]);

    if (distDir != "./wwwroot") { 
        gulp.watch(["wwwroot/index.html", "wwwroot/assets/*"], ["copy-assets"]);
    }
});