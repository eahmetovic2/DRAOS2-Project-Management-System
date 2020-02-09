<template>
<router-view>
</router-view>
</template>

<script>
import ListaZahtjeva from './list';
import DodavanjeZahtjeva from './dodavanje';
import PregledZahtjeva from './pregled';
import EditZahtjeva from './edit';
import ListaZahtjevaDraggable from './list-draggable';

export default {
    name: 'Zahtjev',

    routes: [{
            path: 'list',
            name: 'home.zahtjev.list',
            component: ListaZahtjeva
        },
        {
            path: 'list-draggable',
            name: 'home.zahtjev.list-draggable',
            component: ListaZahtjevaDraggable
        },
        {
            path: 'dodavanje',
            name: 'home.zahtjev.dodavanje',
            component: DodavanjeZahtjeva
        }, {
            path: 'edit/:zahtjevId',
            name: 'home.zahtjev.edit',
            component: EditZahtjeva
        }, {
            path: ':zahtjevId',
            name: 'home.zahtjev.pregled',
            component: PregledZahtjeva
        }
    ]
};
</script>
