<template>
<router-view>
</router-view>
</template>

<script>
import ListaSifarnik from './lista-sifarnik';
import DodavanjeSifarnik from './dodavanje-sifarnik';
import EditSifarnik from './edit-sifarnik';
export default {
    name: 'Sifarnik',
    routes: [{
        path: ':tipSifarnika',
        name: 'home.sifarnik',
        component: ListaSifarnik
    }, {
        path: ':tipSifarnika/dodavanje',
        name: 'home.sifarnik.dodavanje',
        component: DodavanjeSifarnik
    }, {
        path: ':tipSifarnika/edit/:id',
        name: 'home.sifarnik.edit',
        component: EditSifarnik
    }]
};
</script>
