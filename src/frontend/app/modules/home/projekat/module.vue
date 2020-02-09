<template>
<router-view>
</router-view>
</template>

<script>
import ListaProjekata from './list';
import DodavanjeProjekta from './dodavanje';
import PregledProjekta from './pregled';
import EditProjekta from './edit';

export default {
    name: 'Projekat',

    routes: [{
            path: '',
            name: 'home.projekat.list',
            component: ListaProjekata
        },
        {
            path: 'dodavanje',
            name: 'home.projekat.dodavanje',
            component: DodavanjeProjekta
        }, {
            path: 'edit/:projekatId',
            name: 'home.projekat.edit',
            component: EditProjekta
        }, {
            path: ':projekatId',
            name: 'home.projekat.pregled',
            component: PregledProjekta
        }
    ]
};
</script>
