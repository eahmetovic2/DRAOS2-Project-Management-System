<template>
<router-view>
</router-view>
</template>

<script>
import LicniDetalji from './licni-detalji';
import Lozinka from './lozinka';
import Sesije from './sesije';


export default {
    name: 'Racun',

    routes: [{
        path: 'licni-detalji',
        name: 'home.racun.licni-detalji',
        component: LicniDetalji
    }, {
        path: 'lozinka',
        name: 'home.racun.lozinka',
        component: Lozinka
    }, {
        path: 'sesije',
        name: 'home.racun.sesije',
        component: Sesije
    }]
};
</script>
