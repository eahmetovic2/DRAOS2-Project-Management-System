<template>
<router-view>
</router-view>
</template>

<script>
import ListaIzvjestaja from './list';

export default {
    name: 'Izvjestaj',

    routes: [{
            path: '',
            name: 'home.izvjestaj.home',
            component: ListaIzvjestaja
        },
        {
            path: 'list',
            name: 'home.izvjestaj.list',
            component: ListaIzvjestaja
        }
    ]
};
</script>
