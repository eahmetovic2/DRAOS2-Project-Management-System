var outputDir = 'public';
if (process.env.NODE_ENV === 'production')
    outputDir = '../../artifacts/frontend';

module.exports = {
    paths: {
        public: outputDir,
        watched: ['app']
    },
    files: {
        javascripts: {
            joinTo: {
                'vendor.js': /^(?!app)/,
                'app.js': /^app/
            }
        },
        stylesheets: {
            joinTo: 'vendor.css'
        }
    },
    plugins: {
        // config for auto-reload server
        autoReload: {
            enabled: {
                js: true,
                css: true,
                assets: false
            }
        },

        // use es2015 transpiler
        babel: {
            presets: ['es2015'],
            plugins: ['transform-runtime']
        },

        // use libsass compilation for sass
        sass: {
            mode: 'native',
            options: {
                includePaths: [
                    'node_modules/'
                ]
            }
        },

        // vue compilation options
        vue: {
            extractCSS: true,
            out: outputDir + '/app.css'
        },

        // linting options
        eslint: {
            pattern: /^app\/.*(\.js|\.vue)$/,
            warnOnly: true
        },

        // copy files to output
        copycat: {
            fonts: [
                "node_modules/font-awesome/fonts"
            ],
            images: [
                "node_modules/leaflet/dist/images"
            ],
            verbose: false,
            onlyChanged: true
        }
    },
    npm: {
        enabled: true,
        styles: {
            vuetify: ['dist/vuetify.min.css'],
            'pace-progress': ['themes/blue/pace-theme-flash.css'],
            'vue2-dropzone':['dist/vue2Dropzone.css']
        },
        aliases: {
            'vue': 'vue/dist/vue.common.js',
            'vue-spinner': 'vue-spinner/dist/vue-spinner.js'
        }
    }
};
