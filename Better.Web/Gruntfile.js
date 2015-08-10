module.exports = function (grunt) {
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),

        watch: {
            browserify: {
                files: ['client/src/js/**/*.js', 'client/src/js/**/*.jsx'],
                tasks: ['browserify:dev']
            },
            less: {
                files: 'client/src/styles/**/*.less',
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
                    keepAlive: false,
                    watchifyOptions: {
                        fullPaths: true
                    },
                    transform: [require('grunt-react').browserify]
                },
                src: ['client/src/js/**/*.js'],
                dest: 'client/build/application.js'
            },
            build: {
                options: {
                    browserifyOptions: {
                        debug: false
                    },
                    watch: false,
                    keepAlive: false,
                    watchifyOptions: {
                        fullPaths: false
                    },
                    transform: [require('grunt-react').browserify]
                },
                src: ['client/src/js/**/*.js'],
                dest: 'client/build/application.js'
            }
        },
        uglify: {
            client: {
                options: {
                    sourceMap: true,
                    sourceMapName: 'client/build/application.min.js.sourcemap'
                },
                files: {
                    'client/build/application.min.js': ['client/build/application.js']
                }
            }
        },

        less: {
            dev: {
                options: {
                    paths: ["styles"],
                    sourceMap: true,
                    sourceMapFilename: "client/build/site.css.map",
                    sourceMapBasepath: "client/build"
                },
                files: {
                    "client/build/site.css": "client/src/styles/import.less"
                }
            },
            build: {
                options: {
                    paths: ["styles"],
                    compress: true
                },
                files: {
                    "client/build/site.min.css": "client/src/styles/import.less"
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
    grunt.registerTask('build', ['browserify:build', 'uglify', 'less:build']);
};