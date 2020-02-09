import Vue from 'vue';

// fix for https://github.com/nblackburn/vue-brunch/issues/6
import 'lodash.debounce';
import 'simplemde';
import 'marked';
import 'babel-runtime/core-js/json/stringify';
import 'vue-select';
import 'codemirror';
import './lang/ml';

import Sortable from 'sortablejs';
window.Sortable = Sortable;

import Toasted from 'vue-toasted';

Vue.use(Toasted);

// set up config
import VueConfig from 'vue-config';
import configOptions from 'config';
Vue.use(VueConfig, configOptions);

// use vue-router
import VueRouter from 'vue-router';
Vue.use(VueRouter);

// use vue-resource
import VueResource from 'vue-resource';
Vue.use(VueResource);

import Vuetify from 'vuetify';
Vue.use(Vuetify, {
    theme: {
        primary: '#2C9898',
        primary_dark: '#077070',
        secondary: '#FE4A4A',
        tertiary: '#828282',
        accent: '#B5ED45',
        error: '#f55a4e',
        info: '#00d3ee',
        success: '#5cb860',
        warning: '#ffa21a'
    }
});

import Datepicker from 'vuejs-datepicker';
Vue.component('Datepicker', Datepicker);

import 'pace-progress/pace.js';
window.Pace.start({
    restartOnRequestAfter: true
});
// use vue leaflet
import Vue2Leaflet from 'vue2-leaflet';
Vue.component('v-map', Vue2Leaflet.Map);
Vue.component('v-tilelayer', Vue2Leaflet.TileLayer);
Vue.component('v-marker', Vue2Leaflet.Marker);
Vue.component('v-tooltip', Vue2Leaflet.Tooltip);
Vue.component('v-map', Vue2Leaflet.Map);

// import vSelect from 'vue-select';
// Vue.component('v-select', vSelect);

// vue charts
import 'chart.js';
import 'hchs-vue-charts';
Vue.use(window.VueCharts);

import VueCharts from 'vue-charts';
Vue.use(VueCharts);

// use vue-moment
import VueMoment from 'vue-moment';
Vue.use(VueMoment);

// use font awesome icon component
import {
    icon
} from 'vue-fontawesome';
Vue.component('icon', icon);

// loading spinner component
import {
    SyncLoader
} from 'vue-spinner';
Vue.component('spinner', SyncLoader);

// add resolve mixin
import ResolveMixin from './helpers/resolve-mixin';
Vue.mixin(ResolveMixin);

import FormMixin from 'helpers/form-mixin';
Vue.mixin(FormMixin);

import Page from './modules/home/components/page';
Vue.component('page', Page);

import Vuex from 'vuex';
Vue.use(Vuex);
import vuexStore from './vuex-store';

import MDatePicker from './modules/home/components/m-datepicker';
Vue.component('m-date-picker', MDatePicker);

import OdustaniBtn from './modules/home/components/odustani-btn';
Vue.component('odustani-btn', OdustaniBtn);

import Oiframe from './modules/home/components/o-iframe';
Vue.component('o-iframe', Oiframe);

import MInfoDialog from './modules/home/components/m-info-dialog';
Vue.component('m-info-dialog', MInfoDialog);

import HelpTipDialog from './modules/home/components/help-tip-dialog';
Vue.component('help-tip-dialog', HelpTipDialog);

import DateFormat from './modules/home/components/date-format';
Vue.component('date-format', DateFormat);

import Prettyprint from './modules/home/components/prettyprint';
Vue.component('prettyprint', Prettyprint);

import VueDropzone from 'vue2-dropzone';

Vue.component('vue-dropzone', VueDropzone);

Vue.use(VueDropzone);

import CommonMixin from './helpers/common-mixin.js';
Vue.mixin(CommonMixin);

import CoreMixin from './helpers/core-mixin.js';
Vue.mixin(CoreMixin);

import ObrisiModal from './modules/home/components/obrisi-modal';
Vue.component('obrisi-modal', ObrisiModal);

import PotvrdaModal from './modules/home/components/m-question-dialog';
Vue.component('potvrda-modal', PotvrdaModal);

import Upit from './modules/home/components/upit';
Vue.component('upit', Upit);

import Notification from './modules/home/components/notification';
Vue.component('notification', Notification);

import Loading from './modules/home/components/loading';
Vue.component('loading', Loading);

import Card from './modules/home/components/card';
Vue.component('material-card', Card);

import ChartCard from './modules/home/components/chart-card';
Vue.component('material-chart-card', ChartCard);

import StatsCard from './modules/home/components/stats-card';
Vue.component('material-stats-card', StatsCard);

import vueScrollBehavior from 'vue-scroll-behavior';


import draggable from 'vuedraggable';
Vue.component('draggable', draggable);

// load app component
import App from './modules/app';

// use bootstrapping to start application
import Api from './api';
import Postavke from './postavke';
import Auth from './auth';
import Identity from './auth/identity';
import PostavkeConfig from './postavke/postavke';

Api.init()
    .then(() => Auth.init())
    .then(() => Postavke.init())
    .then(() => {
        // set up routes
        var appRouter = new VueRouter({
            // mode:'history',
            routes: App.routes,
            scrollBehavior(to, from, savedPosition) {
                if (savedPosition) {
                    return savedPosition;
                } else {
                    return {
                        x: 0,
                        y: 0
                    };
                }
            }
        });

        Vue.use(vueScrollBehavior, {
            router: appRouter
        });
        // mapiranje pocetka naziva rute u Naziv stranice
        const PAGE_TITLE = [{
            naziv: "Postavke",
            ruta: "home.postavke"
        }, {
            naziv: "Dashboard",
            ruta: "home.dashboard"
        }, {
            naziv: "Log",
            ruta: "home.log"
        }, {
            naziv: "Šifarnici",
            ruta: "home.sifarnik"
        }, {
            naziv: "Korisnici",
            ruta: "home.korisnik"
        },  {
            naziv: "Izvještaji",
            ruta: "home.izvjestaj"
        },
		{
            naziv: "Projekti",
            ruta: "home.projekat"
        },
        {
            naziv: "Zadaci",
            ruta: "home.zahtjev"
        }
		];

        appRouter.beforeEach((toRoute, fromRoute, nextRoute) => {
            var next_required_permission = toRoute.meta.permission_name;

            if (next_required_permission) {
                if (!Identity.imaPravo(next_required_permission)) {
                    console.log("nema prava", next_required_permission);
                    nextRoute(false);
                    return;
                }
            }
            nextRoute();
        });

        appRouter.afterEach((toRoute) => {
            for (var i = 0; i < PAGE_TITLE.length; i++) {
                if (toRoute.name.startsWith(PAGE_TITLE[i].ruta)) {
                    window.document.title = PAGE_TITLE[i].naziv;
                }
            }
        });


        // create and mount vue app
        var vm = new Vue({
            router: appRouter,
            store: vuexStore,
            render: h => h(App),
            created() {
                var jezik = this.$store.getters['home/odabraniJezik'];
                if(jezik)
                {
                    console.log('nakon refresha',jezik);
                    this.$ml.change(jezik);                
                }

                let naslovSistema = PostavkeConfig.naslovSistema();
                this.$store.commit('home/setNaslovSistema', naslovSistema);
                document.title = naslovSistema;
            }
        });

        vm.$mount('#app');
    });
