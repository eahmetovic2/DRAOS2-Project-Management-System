<template>
<div>
    <transition name="fade">
        <router-view></router-view>
    </transition>
</div>
</template>

<script>
import Vue from 'vue';

import Auth from 'auth';

import AuthMod from './auth/module';
import HomeMod from './home/module';

var requireAuth = (to, from, next) => {
    if (!Auth.isAuthenticated()) {
        next({
            name: 'auth.prijava'
        });
    } else {
        next();
    }
};

export default {
    name: 'App',
    created() {
        var toastOptions = {
            className: "v-alert v-alert--notification",
            position: "top-center",
            duration: 5000,
            action : {
                icon : 'cancel',
                onClick : (e, toastObject) => {
                    toastObject.goAway(0);
                }
            },
        };

        // add notifications
        Vue.prototype.$toast = {
            error: (message) => this.$toasted.error(message, toastOptions),
            show: (message) => this.$toasted.show(message, toastOptions),
            info: (message) => this.$toasted.info(message, toastOptions),
            success: (message) => this.$toasted.success(message, toastOptions)
        };
    },

    mounted() {},

    routes: [{
        path: '*',
        redirect: '/home/dashboard'
    }, {
        path: '/auth',
        component: AuthMod,
        children: AuthMod.routes
    }, {
        path: '/home',
        component: HomeMod,
        children: HomeMod.routes,
        beforeEnter: requireAuth
    }]
};
</script>
