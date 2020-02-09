<template>
<router-view>
</router-view>
</template>

<script>
import ListaKorisnika from './list';
import DodavanjeKorisnika from './dodavanje';
import PregledKorisnik from './pregled';
import EditKorisnika from './edit';

export default {
    name: 'Korisnik',

    routes: [{
            path: '',
            name: 'home.korisnik.list',
            component: ListaKorisnika
        },
        {
            path: 'dodavanje',
            name: 'home.korisnik.dodavanje',
            component: DodavanjeKorisnika
        }, {
            path: 'edit/:korisnickoIme',
            name: 'home.korisnik.edit',
            component: EditKorisnika
        }, {
            path: 'pregled/:korisnickoIme',
            name: 'home.korisnik.pregled',
            component: PregledKorisnik
        }
    ]
};
</script>
