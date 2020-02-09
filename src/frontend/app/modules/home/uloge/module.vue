<template>
<router-view>
</router-view>
</template>

<script>
import ListaUloga from './list';
import DodavanjeUloga from './dodavanje';
import EditUloga from './edit';

export default {
    name: 'Korisnik',

    routes: [{
            path: '',
            name: 'home.uloga.list',
            component: ListaUloga
        },
        {
            path: 'dodavanje',
            name: 'home.uloga.dodavanje',
            component: DodavanjeUloga
        }, {
            path: 'edit/:ulogaId',
            name: 'home.uloga.edit',
            component: EditUloga
        }
    ]
};
</script>
