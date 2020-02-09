<style>
#core-toolbar a {
    text-decoration: none;
}

.toolbar-menu-btn {
    padding: 10px 15px !important;
}

.toolbar-menu-btn {
    padding: 0;
    margin: 0 0 0 15px !important;
}

.hamburger-menu {
    color: #828282 !important;
}

.nova-notifikacija {
    background-color: #f1f1f1;
}
</style>

<template>
<v-toolbar id="core-toolbar" flat prominent style="background: #eee;">
    <div class="v-toolbar-title">
        <v-toolbar-title class="tertiary--text font-weight-light">
            <v-btn v-if="responsive" class="default v-btn--simple" icon @click.stop="onClickBtn" color="tertiary">
                <v-icon color="tertiary">menu</v-icon>
            </v-btn>
            {{ title }}
        </v-toolbar-title>
    </div>

    <v-spacer />
    <v-toolbar-items>
        <v-flex align-center layout py-2>
            <v-menu bottom left content-class="dropdown-menu" offset-y transition="slide-y-transition">
                <router-link v-ripple slot="activator" @click.native="vratiNotifikacije" class="toolbar-items" to="">
                    <v-badge color="error" overlap>
                        <template v-if="ucitaneNotifikacije" slot="badge">
                            {{ notifications.length }}
                        </template>
                        <v-icon color="tertiary">notifications</v-icon>
                    </v-badge>
                </router-link>
                <v-card>
                    <v-list three-line>
                        <v-list-tile v-for="notification in notifications" :key="notification.id" :class="{'nova-notifikacija': false}" @click="onClick(notification)">

                            <v-list-tile-content>

                                <v-list-tile-title v-text="notification.naslov" />
                                <v-list-tile-sub-title v-text="notification.poruka" />
                                <v-list-tile-action-text class="text-xs-right">
                                    <date-format :vrijednost="notification.datumKreiranja" :format="'DD.MM.YYYY. hh:mm'"></date-format>
                                </v-list-tile-action-text>

                            </v-list-tile-content>

                            <!-- <v-list-tile-action>
                                <v-list-tile-action-text>
                                    <date-format :vrijednost="notification.datumKreiranja" :format="'DD.MM.YYYY. hh:mm'"></date-format>
                                </v-list-tile-action-text>
                            </v-list-tile-action> -->

                        </v-list-tile>
                    </v-list>
                </v-card>
            </v-menu>

            <v-menu offset-y class="toolbar-menu" bottom left content-class="dropdown-menu" transition="slide-y-transition">
                <v-btn flat right slot="activator" color="tertiary" class="toolbar-menu-btn" v-ripple="{ class: 'primary--text' }">
                    <v-icon class="pr-2">person</v-icon>
                    <div v-if="identity.punoIme().length>30">{{identity.punoIme().slice(0,27)+"..."}}</div>
                    <div v-else>{{identity.punoIme()}}</div>

                </v-btn>
                <v-list id="lista-akcija">
                    <v-list-tile :to="{ name: 'home.racun.licni-detalji', params: { korisnickoIme: identity.korisnickoIme() } }">
                        <v-list-tile-title>
                            <v-icon class="pr-2">settings</v-icon>Lični detalji
                        </v-list-tile-title>
                    </v-list-tile>
                    <v-list-tile :to="{ name: 'home.racun.lozinka', params: { korisnickoIme: identity.korisnickoIme() } }">
                        <v-list-tile-title>
                            <v-icon class="pr-2">vpn_key</v-icon>Promjena lozinke
                        </v-list-tile-title>
                    </v-list-tile>
                    <v-list-tile :to="{ name: 'home.racun.sesije', params: { korisnickoIme: identity.korisnickoIme() } }">
                        <v-list-tile-title>
                            <v-icon class="pr-2">lock_open</v-icon>Pregled prijava
                        </v-list-tile-title>
                    </v-list-tile>
                    <v-list-tile href="#" @click.native="signOut">
                        <v-list-tile-title>
                            <v-icon class="pr-2">exit_to_app</v-icon>Odjava
                        </v-list-tile-title>
                    </v-list-tile>
                </v-list>
            </v-menu>
        </v-flex>
    </v-toolbar-items>



    <!-- v-if="zahtjevId!=null" -->
    <pregled-zahtjev v-if="ucitaneNotifikacije" :zahtjevId="id" @azuriraniZahtjev="azuriranjeZahtjeva"></pregled-zahtjev>
</v-toolbar>
</template>

<script>
import Auth from "auth";
import Identity from "auth/identity";
import PregledZahtjev from './zahtjev/pregled.vue';
import {
    ZahtjevResource,
    NotifikacijeResource
} from 'api/resources'

export default {
    name: "Toolbar",
    components:{
        PregledZahtjev
    },
    data: () => ({
        identity: Identity,
        notifications: [],
        title: 'Project Management System',
        responsive: false,
        ucitaneNotifikacije: false
    }),

    watch: {
        '$route'(val) {
            this.title = window.document.title
        },
    },

    mounted() {
        this.onResponsiveInverted()
        window.addEventListener('resize', this.onResponsiveInverted)
        this.vratiNotifikacije();
    },
    beforeDestroy() {
        window.removeEventListener('resize', this.onResponsiveInverted)
    },

    methods: {
        
        otvoriZahtjev(id) {
            this.$route.params.zahtjevId = id;
            this.ucitaneNotifikacije = true;
           


            // if (this.$route.params.zahtjevId == id) {
            //      this.$router.go({
            //          name: 'home.zahtjev.pregled'
            //     });
            
                //  this.$router.push({
                //      name: 'home.zahtjev.pregled',
                //       params: {
                //          zahtjevId: id
                //       }
                //  });
            

        },
        onClickBtn() {
            this.$store.commit('home/setDrawer', !this.$store.getters['home/drawer']);
        },
        onClick(notification) {
            var that = this;
            var success = response => {
                that.otvoriZahtjev(notification.zahtjevId);
                that.vratiNotifikacije();

            };

            var error = (poruka) => {
                that.$toast.error(poruka);
            };

            var promise = NotifikacijeResource().otvori({
                notifikacijaId: notification.id
            });

            promise.then(success, error);
        },
        signOut() {
            Auth.signOut();

            this.$router.push({
                name: "auth.prijava"
            });

        },
        onResponsiveInverted() {
            if (window.innerWidth < 991) {
                this.responsive = true
            } else {
                this.responsive = false
            }
        },
        vratiNotifikacije() {
            var that = this;
            that.ucitaneNotifikacije = false;
           // that.notifications=[];

            var success = response => {
                that.notifications = response.body;
                that.ucitaneNotifikacije = true;
            };

            var error = (poruka) => {
                that.$toast.error(poruka);
            };

            var promise = NotifikacijeResource().vratiNotifikacije();

            promise.then(success, error);
        },

        azuriranjeZahtjeva(zahtjev, azuriran) {

            this.ucitaneNotifikacije = true;

            if (azuriran) {
                this.updateData;
            }

        }
    }
}
</script>
