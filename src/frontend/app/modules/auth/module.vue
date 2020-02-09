<template>
<router-view>
</router-view>
</template>

<script>
import Prijava from './prijava';

import config from 'config';

import Auth from 'auth';

var routes = [{
    path: 'aktiviraj',
    name: 'auth.aktiviraj'
}];




routes.unshift({
    path: '',
    redirect: 'prijava'
});
routes.push({
    path: 'prijava',
    name: 'auth.prijava',
    component: Prijava
});


export default {
    name: 'Auth',
    routes: routes
};
</script>
