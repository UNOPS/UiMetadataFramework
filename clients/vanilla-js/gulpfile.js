var gulp = require("gulp"),
    rollup = require("rollup"),
    typescript = require("rollup-plugin-typescript2"),
    browserSync = require('browser-sync').create(),
    gulpSvelte = require('gulp-svelte');

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

gulp.task("build-svelte", function() {
    const svelteComponentsDir = "demo/svelte-components";
    gulp.src("node_modules/svelte/shared.js")
        .pipe(gulp.dest(svelteComponentsDir));

    return gulp.src('demo/src/**/*.html')
        .pipe(gulpSvelte({
            format: "es",
            generate: "dom",
            shared: "./shared.js"
        }))
        .pipe(gulp.dest(svelteComponentsDir));
});

gulp.task("watch", ["build", "build-svelte", "browser-sync"], function () {
    gulp.watch("src/**/*.ts", ["build"]);
    gulp.watch("demo/src/**/*.ts", ["build-app"]);
    gulp.watch("demo/src/**/*.html", ["build-svelte", "build-app"]);
});