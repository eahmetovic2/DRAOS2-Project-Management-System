<template>
<page>
    <material-card color="primary">
        <v-flex v-if="childOmogucenHeader" slot="header">
            <v-layout row justify-space-between>
                <v-flex>
                    <span class="card-naslov">Korisnici </span>
                </v-flex>
                <v-flex class="text-xs-right toolbar-btn">
                    <v-btn flat :to="{ name: 'home.korisnik.dodavanje' }">
                        <v-icon class="mr-2">add_circle</v-icon>
                        DODAJ
                    </v-btn>

                    <v-menu :close-on-content-click="false" content-class="dropdown-menu" v-model="filterMenu" bottom left offset-y transition="slide-y-transition">
                        <v-btn slot="activator" flat class="toolbar-btn">
                            <v-icon class="mr-2">filter_list</v-icon>Filtriraj
                        </v-btn>
                        <v-card>
                            <v-card-title>
                                <div class="filter-title">
                                    Filtriraj po
                                </div>
                            </v-card-title>
                            <v-form class="filter" @submit="filtriraj">
                                <v-card-text class="pt-0">
                                    <v-text-field label="Ime i prezime" hide-details v-model="inputs.filter"></v-text-field>
                                </v-card-text>
                                <v-card-actions>
                                    <v-spacer></v-spacer>
                                    <v-btn flat color="primary" @click="ponistiFilter">Poništi</v-btn>
                                    <v-btn flat color="primary" type="submit">Filtriraj</v-btn>
                                </v-card-actions>
                            </v-form>
                        </v-card>
                    </v-menu>
                </v-flex>
            </v-layout>
        </v-flex>

        <v-card-text>
            <v-data-table v-bind:headers="headers" v-bind:items="model.items" v-bind:pagination.sync="pagination" :total-items="totalItems" no-data-text="Nema korisnika za prikaz." :loading="loading" :rows-per-page-items="rowsPerPageItems">
                <template slot="items" scope="props">
                    <tr @click="otvoriPregledKorisnika(props.item.korisnickoIme)">
                    <td>{{ props.item.korisnickoIme }}</td>
                    <td>{{ props.item.punoIme }}</td>
                    <td>{{ props.item.uloga }}</td>
                    <td>{{ props.item.onemogucen ? 'Nije aktivan':'Aktivan'}}</td>
                    <td class="text-xs-right">
                        <v-menu open-on-hover offset-y class="toolbar-menu" bottom left content-class="dropdown-menu" transition="slide-y-transition">
                            <v-icon slot="activator">settings</v-icon>
                            <v-list>
                                <v-list-tile :to="{ name: 'home.korisnik.pregled', params: { korisnickoIme: props.item.korisnickoIme } }">
                                    <v-list-tile-title>Pregled korisnika</v-list-tile-title>
                                </v-list-tile>
                                <v-list-tile :to="{ name: 'home.korisnik.edit', params: { korisnickoIme: props.item.korisnickoIme } }">
                                    <v-list-tile-title>Izmijeni korisnika</v-list-tile-title>
                                </v-list-tile>
                            </v-list>
                        </v-menu>
                    </td>
                    </tr>
                </template>
                <template slot="pageText" scope="{ pageStart, pageStop }">
                    Od {{ pageStart }} do {{ pageStop }}
                </template>
            </v-data-table>
        </v-card-text>
    </material-card>
    <help-tip-dialog v-if="activeHelpTip" :title="VratiTitlePoId('korisnik-list')" :content="VratiContentPoId('korisnik-list')" @zatvori="ZatvoriHelpDialog"> </help-tip-dialog>
</page>
</template>

<script>
import HelpTipDialogMixin from 'helpers/help-tip-dialog-mixin';
import debounce from 'lodash.debounce';
import {
    KorisnikResource
} from 'api/resources';
import Identity from 'auth/identity';
import PaginationMixin from 'helpers/pagination-mixin';

export default {
    name: 'ListaKorisnika',
    mixins: [PaginationMixin, HelpTipDialogMixin],
    components: {
        debounce
    },
    props: ['listaKorisnika', 'omogucenHeader','projekatId'],

    data() {
        return {
            activeHelpTip: false,
            model: {
                items: []
            },
            childOmogucenHeader: this.omogucenHeader,
            totalItems: 0,
            filterMenu: false,
            filtrirano: false,
            loading: false,
            headers: [{
                text: 'Korisničko ime',
                align: 'left',
                sortable: false,
                value: 'korisnickoIme'
            }, {
                text: 'Ime i prezime',
                align: 'left',
                sortable: false,
                value: 'punoIme'
            }, {
                text: 'Uloga',
                align: 'left',
                sortable: false,
                value: 'uloga'
            }, {
                text: 'Aktivan',
                align: 'left',
                sortable: false,
                value: 'onemogucen'
            }, {
                text: '',
                align: 'left',
                sortable: false,
                value: ''
            }],
            inputs: {
                filter: null,
                page: 1,
                count: 30,
                projekatId: this.projekatId
            },
            identity: Identity
        };
    },

    created() {
        this.inputs = this.mergeQueryToInput(this.$route.query, this.inputs);

        if (this.omogucenHeader == null)
            this.childOmogucenHeader = true;
    },

    mounted() {
        if (this.inputs.page) {
            this.pagination.page = this.inputs.page;
        }
        if (this.inputs.count) {
            this.pagination.count = this.inputs.count;
        }
    },

    methods: {
        filtriraj() {
            if (this.inputs.filter) {
                this.filtrirano = true;
            } else {
                this.filtrirano = false;
            }
            this.filterMenu = false;
            this.resetujPaginaciju();
        },
        ponistiFilter() {
            this.inputs.filter = "";
            this.filterMenu = false;
            this.filtrirano = false;
            this.resetujPaginaciju();
        },
        updateData() {
            var that = this;

            if (that.listaKorisnika == null) {

                this.osvjeziQuery(this.inputs);

                this.loading = true;
                var success = (model) => {
                    that.model = model.body;
                    that.totalItems = model.body.total;
                    that.loading = false;
                };

                var error = () => {
                    that.$toast.error("Filtriranje nije uspješno");
                };

                KorisnikResource()
                    .get(that.inputs)
                    .then(success, error);
            } else {

                that.model = that.listaKorisnika;
                that.totalItems = that.listaKorisnika.total;

                that.loading = false;

            }
        },
        otvoriPregledKorisnika(korisnickoIme)
        {
            this.$router.push({
                name: 'home.korisnik.pregled',
                params: {
                    korisnickoIme: korisnickoIme
                }
            })
        }
    }

};
</script>
