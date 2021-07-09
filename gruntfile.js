const sass = require("node-sass")

module.exports = function (grunt) {
    "use strict"
    // Project configuration.
    grunt.initConfig({
        pkg: grunt.file.readJSON("package.json"),
        watch: {
            sass: {
                files: "**/*.scss",
                tasks: ["sass"],
                options: {
                    livereload: 35729,
                },
            },
        },

        // Sass
        sass: {
            options: {
                implementation: sass,
                sourceMap: false, // Create source map
                outputStyle: "uncompressed", // Minify output
            },
            dist: {
                files: [
                    {
                        expand: true, // Recursive
                        cwd: "src/assets/themes", // The startup directory
                        //src: ["**/*.scss"], // Source files
                        src: ["**/sass-main.scss", "**/sass-login.scss"], // Source files
                        dest: "src/assets/themes", // Destination
                        ext: ".css", // File extension,
                        rename: function (dest, src) {
                            console.log(src)
                            var file = src.replace(/SASS-/gi, "").toLowerCase()
                            var args = file.split("/")
                            var tenant = args.shift()
                            var result = dest + "/" + tenant + "/" + args[0]
                            return result
                        },
                    },
                ],
            },
        },
    })

    // Load the plugin
    grunt.loadNpmTasks("grunt-sass")
    grunt.loadNpmTasks("grunt-contrib-watch")
    // Default task(s).
    grunt.registerTask("default", ["sass"])
}
