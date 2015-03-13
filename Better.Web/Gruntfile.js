module.exports = function (grunt) {
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),

        watch: {
            browserify: {
                files: ['Client/src/js/**/*.js', 'Client/src/js/**/*.jsx'],
                tasks: ['browserify:dev']
            },
            less: {
                files: 'Client/src/styles/**/*.less',
                tasks: ['less:dev']
            }
        },

        browserify: {
            dev: {
                options: {
                    browserifyOptions: {
                        debug: true
                    },
                    watch: true,
                    keepAlive: true,
                    watchifyOptions: {
                        fullPaths: true
                    },
                    transform: [require('grunt-react').browserify]
                },
                src: ['Client/src/js/**/*.js', 'Client/src/js/**/*.jsx'],
                dest: 'Client/build/js/application.js'
            }
        },

        uglify: {
            dev: {
                options: {
                    sourceMap: true,
                    sourceMapName: 'Client/build/js/application.min.js.sourcemap'
                },
                files: {
                    'Client/build/js/application.min.js': ['Client/build/js/application.js']
                }
            }
        },

        less: {
            dev: {
                options: {
                    paths: ["Client/src/styles"],
                    sourceMap: true,
                    sourceMapFilename: "Client/build/styles/site.css.map",
                    sourceMapBasepath: "Client/build/styles"
                },
                files: {
                    "Client/build/styles/site.css": "Client/src/styles/site.less"
                }
            },
            build: {
                options: {
                    paths: ["Client/build/styles"],
                    compress: true
                },
                files: {
                    "Client/build/styles/site.min.css": "CLient/src/styles/site.less"
                }
            }
        }
    });

    grunt.loadNpmTasks('grunt-browserify');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-less');

    grunt.registerTask('default', ['browserify', 'less:dev']);
    grunt.registerTask('auto', ['browserify', 'less:dev', 'watch']);
};