<style lang="scss">
#app-drawer {
    .v-list__tile {
        border-radius: 4px;

        &--buy {
            margin-top: auto;
            margin-bottom: 17px;
        }
    }

    .v-image__image--contain {
        top: 9px;
        height: 60%;
    }

    .search-input {
        margin-bottom: 30px !important;
        padding-left: 15px;
        padding-right: 15px;
    }

    div.v-responsive.v-image>div.v-responsive__content {
        overflow-y: auto;
    }
}

.sidebar-list {
    .v-list__group__header {
        padding: 10px 15px;
        border-radius: 4px;
        transition: background .3s cubic-bezier(.25, .8, .5, 1);

        .v-list__group__header__prepend-icon {
            padding: 0;
            margin-right: 15px;
            min-width: 0;
            color: white !important;
            opacity: 0.8;
        }

        .v-list__group__header__append-icon {
            padding: 0 !important;
        }
    }
}

.sidebar-list {
    .v-list__group__header:hover {
        padding: 10px 15px;
        border-radius: 4px;
        transition: background .3s cubic-bezier(.25, .8, .5, 1);
        background: rgba(200, 200, 200, 0.2) !important;
    }
}

.sidebar-list-title {
    .v-list__tile--avatar {
        padding: 0 !important;
    }

    .v-list__tile__title {
        font-size: 14px !important;
        line-height: 28px !important;
        padding: 0 0 0 3px !important;
    }
}
.logo {
    height: 80px;
}
</style>

<template>
<v-navigation-drawer id="app-drawer" v-model="inputValue" app dark floating persistent mobile-break-point="991" width="260">
    <v-img :src="image" gradient="rgba(27, 27, 27, 0.54), rgba(27, 27, 27, 0.54)" height="100%">
        <v-layout class="fill-height" tag="v-list" column>

            <div class="logo">

                <v-img :src="logo" height="95" contain />

            </div>
            <v-divider />
            <v-list-tile :to="{name: 'home.dashboard'}" class="v-list-item" :active-class="color">
                <v-list-tile-action>
                    <v-icon>dashboard</v-icon>
                </v-list-tile-action>
                <v-list-tile-content>
                    <v-list-tile-title color='white'>Dashboard</v-list-tile-title>
                </v-list-tile-content>
            </v-list-tile>

            <!-- Zahtjevi -->
            <v-list-group no-action class="sidebar-list v-list-item">
                <v-list-tile slot="activator" class="sidebar-list-title" avatar>
                    <v-list-tile-action>
                        <v-icon>assignment</v-icon>
                    </v-list-tile-action>
                    <v-list-tile-content>
                        <v-list-tile-title>Zadaci</v-list-tile-title>
                    </v-list-tile-content>
                </v-list-tile>
                <v-list-tile :to="{name: 'home.zahtjev.list'}" class="v-list-item" :active-class="color">
                    <v-list-tile-content>
                        <v-list-tile-title color='white'>Lista svih zadataka</v-list-tile-title>
                    </v-list-tile-content>
                </v-list-tile>
                <v-list-tile v-if="imaPravo('osnovno_dashboard_broj_zahtjeva_po_projektu')" :to="{name: 'home.zahtjev.list-draggable'}" class="v-list-item" :active-class="color">
                    <v-list-tile-content>
                        <v-list-tile-title color='white'>Zadaci po statusima</v-list-tile-title>
                    </v-list-tile-content>
                </v-list-tile>
            </v-list-group>
            <!-- Zahtjevi -->


            <v-list-tile v-if="imaPravo('glavni_meni_osnovno_sidebar_projekti')" :to="{name: 'home.projekat.list'}" class="v-list-item" :active-class="color">
                <v-list-tile-action>
                    <v-icon>view_comfy</v-icon>
                </v-list-tile-action>
                <v-list-tile-content>
                    <v-list-tile-title color='white'>Projekti</v-list-tile-title>
                </v-list-tile-content>
            </v-list-tile>

            <v-list-tile v-if="imaPravo('glavni_meni_osnovno_sidebar_korisnici')" :to="{name: 'home.korisnik.list'}" class="v-list-item" :active-class="color">
                <v-list-tile-action>
                    <v-icon>people</v-icon>
                </v-list-tile-action>
                <v-list-tile-content>
                    <v-list-tile-title color='white'>Korisnici</v-list-tile-title>
                </v-list-tile-content>
            </v-list-tile>

            <v-list-tile v-if="imaPravo('glavni_meni_osnovno_sidebar_uloge')" :to="{name: 'home.uloga.list'}" class="v-list-item" :active-class="color">
                <v-list-tile-action>
                    <v-icon>portrait</v-icon>
                </v-list-tile-action>
                <v-list-tile-content>
                    <v-list-tile-title color='white'>Uloge</v-list-tile-title>
                </v-list-tile-content>
            </v-list-tile>

            <!-- Šifarnici -->
            <!-- <v-list-group v-if="imaPravo('glavni_meni_osnovno_sidebar_sifarnici')" no-action class="sidebar-list v-list-item">
                <v-list-tile slot="activator" class="sidebar-list-title" avatar>
                    <v-list-tile-action>
                        <v-icon>library_books</v-icon>
                    </v-list-tile-action>
                    <v-list-tile-content>
                        <v-list-tile-title>Šifarnici</v-list-tile-title>
                    </v-list-tile-content>
                </v-list-tile>
                <v-list-tile class="v-list-item" :active-class="color" :to="{name: 'home.sifarnik', params: {tipSifarnika: 'Pol', nazivSifarnika: 'Pol'} }">
                    <v-list-tile-content>
                        <v-list-tile-title color='white'>Pol</v-list-tile-title>
                    </v-list-tile-content>
                </v-list-tile>
            </v-list-group> -->
            <!-- Šifarnici -->

            <!-- <v-list-tile class="v-list-item" :active-class="color">
                <v-list-tile-action>
                    <v-icon>insert_chart</v-icon>
                </v-list-tile-action>
                <v-list-tile-content>
                    <v-list-tile-title color='white'>Izvještaji</v-list-tile-title>
                </v-list-tile-content>
            </v-list-tile> -->

            <!-- <v-list-tile v-if="imaPravo('glavni_meni_osnovno_sidebar_postavke')" :to="{name: 'home.postavke.postavke-sistema'}" class="v-list-item" :active-class="color">
                <v-list-tile-action>
                    <v-icon>settings</v-icon>
                </v-list-tile-action>
                <v-list-tile-content>
                    <v-list-tile-title color='white'>Postavke</v-list-tile-title>
                </v-list-tile-content>
            </v-list-tile> -->

        </v-layout>
    </v-img>
</v-navigation-drawer>
</template>

<script>
import Identity from "auth/identity";
import EventBus from "../event-bus";
export default {
    name: "Sidebar",

    props: ["toggleState"],

    data() {
        return {
            color: 'primary',
            image: 'https://demos.creative-tim.com/vue-material-dashboard/img/sidebar-2.32103624.jpg',
            sidebarBackgroundColor: 'rgba(27, 27, 27, 0.54)',
            logo: './vuetifylogo.png',
            responsive: false,
            identity: Identity,
            showNav: true,
            aktivanKonkursniRok: false,
            router: this.$router
        };
    },
    computed: {
        inputValue: {
            get() {
                return this.$store.getters['home/drawer'];
            },
            set(val) {
                this.$store.commit('home/setDrawer', val);
            }
        },
    },

    methods: {

        sidebarOverlayGradiant() {
            return `${this.sidebarBackgroundColor}, ${this.sidebarBackgroundColor}`
        },
        signOut() {
            this.$emit("sign-out");
        },

        handleClick(e) {
            console.log("CLICK", e)
            e.preventDefault();
            e.target.parentElement.classList.toggle("open");
        }
    }
};
</script>
