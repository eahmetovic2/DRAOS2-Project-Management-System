<template>
<v-app >
    <div >
        <sidebar v-on:sign-out="signOut" :toggleState="toggleState" app></sidebar>
        <toolbar />
    </div>
    <v-content>
        <v-container fluid id="core-view">
            <div >
                <router-view></router-view>
            </div>
        </v-container>
        <v-footer id="core-footer" absolute height="42">
            <v-spacer/>
            <span class="font-weight-light copyright">
                &copy;
                {{ (new Date()).getFullYear() }} DRAOS2
            </span>
        </v-footer>
    </v-content>         
    

</v-app>
</template>

<style>
#main-toolbar {
    display: flex;
    flex-direction: column;
    background-image: url("header_stars.png");
    background-repeat: no-repeat;
    background-position: right bottom, left top;
    background-size: contain, cover;
    background-origin: content-box, border-box;
}

#account-menu {
    margin-right: 36px;
}

.poweredby {
    margin: 40px 0;
}
#core-view {
  padding-bottom: 60px;
}
@import '../assets/upis.css';
</style>

<script>
import Auth from "auth";
import Dashboard from "./dashboard";
import Toolbar from "./toolbar";
import RacunMod from "./racun/module";
import PostavkeMod from "./postavke/module";
import KorisnikMod from "./korisnik/module";
import UlogaMod from "./uloge/module";
import SifarnikMod from "./sifarnik/module";
import Sidebar from "./sidebar";
import IzvjestajMod from "./izvjestaj/module";
import DokumentiMod from "./dokumenti/module";
import Identity from "auth/identity";
import ProjekatMod from "./projekat/module";
import ZahtjevMod from "./zahtjev/module";



import {
    DashboardResource
} from "api/resources";
import config from 'config';

export default {
    name: "Home",

    data() {
        return {
            identity: Identity,
            toggleState: {
                mini: false,
                drawer: true
            },
            toolbarTitle: window.document.title
       
        };
    },
    created() {
        
            

        // Prevedi vuetify
        this.$vuetify.lang.locales.en = {
            dataIterator: {
                rowsPerPageText: 'Broj redova po stranici:',
                rowsPerPageAll: 'Sve',
                pageText: '{0}-{1} od {2}',
                noResultsText: 'Nema rezultata',
                nextPage: 'Sljedeća stranica',
                prevPage: 'Prethodna stranica'
            },
            dataTable: {
                rowsPerPageText: 'Broj redova po stranici:'
            },
            noDataText: 'Nema podataka'
        };
    },
    methods: {
        signOut() {
            Auth.signOut();

       
                this.$router.push({
                    name: "auth.prijava"
                });
         

        },

        toggleSidebar() {
            this.sidebarToggled = !this.sidebarToggled;
        },

        // goToDashboard() {
        //     this.$router.push('home.dashboard');
        // }
    },
    watch: {
        $route: function () {
            this.toolbarTitle = window.document.title;
        }
    },
    components: {
        Toolbar,
        sidebar: Sidebar
    },

    routes: [
        {
            path: "",
            redirect: "dashboard"
        },
        {
            path: "dashboard",
            name: "home.dashboard",
            component: Dashboard
        },
        {
            path: "racun/:korisnickoIme",
            name: "home.racun",
            component: RacunMod,
            children: RacunMod.routes
        },
        {
            path: "korisnik",
            component: KorisnikMod,
            children: KorisnikMod.routes
        },
        {
            path: "uloga",
            component: UlogaMod,
            children: UlogaMod.routes
        },
        {
            path: "izvjestaj",
            component: IzvjestajMod,
            children: IzvjestajMod.routes
        },
        {
            path: "postavke",
            component: PostavkeMod,
            children: PostavkeMod.routes
        },
        {
            path: "sifarnik",
            component: SifarnikMod,
            children: SifarnikMod.routes
        },
        {
            path: "dokumenti",
            component: DokumentiMod,
            children: DokumentiMod.routes
        },
        {
            path: "projekat",
            component: ProjekatMod,
            children: ProjekatMod.routes
        },
        {
            path: "zahtjev",
            component: ZahtjevMod,
            children: ZahtjevMod.routes
        }

    ]
};
</script>
