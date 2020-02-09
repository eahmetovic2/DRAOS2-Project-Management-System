<template>
    <material-card v-if="model" color="primary">
        <v-flex slot="header">
            <v-layout row justify-space-between>
                <v-flex>
                    <span class="card-naslov">{{$route.params.tipSifarnika}}</span>
                </v-flex>
                <v-flex  class="text-xs-right toolbar-btn">
                    <v-btn flat :to="{ name: 'home.sifarnik.dodavanje', params: { tipSifarnika: inputs.sifarnik, nazivSifarnika: $route.params.nazivSifarnika }}">
                        <v-icon class="mr-2">add_circle</v-icon>
                            DODAJ
                        </v-btn>

                    <v-menu :close-on-content-click="false" content-class="dropdown-menu" v-model="filterMenu"
                            bottom left offset-y transition="slide-y-transition">
                        <v-btn slot="activator" flat class="toolbar-btn">
                            <v-icon class="mr-2">filter_list</v-icon>Filtriraj</v-btn>
                        <v-card>
                            <v-card-title>
                                <div class="filter-title">
                                    Filtriraj po
                                </div>
                            </v-card-title>
                            <v-form class="filter" @submit="filtriraj">
                                <v-card-text class="pt-0">
                                    <v-text-field label="Naziv" hide-details v-model="inputs.filter"></v-text-field>
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

        <obrisi-modal :aktivan="showModal" :item="idBrisanje" header="Brisanje šifarnika" body="Jeste li sigurni da želite obrisati šifarnik?" @close="closeModal($event)"></obrisi-modal>
        <v-card-text>
            <v-data-table no-data-text="Nema šifarnika za prikaz." v-bind:items="model.items" v-bind:pagination.sync="pagination" :total-items="model.total" :loading="loading" :rows-per-page-items="rowsPerPageItems" >
              <template slot="headers" scope="props">
                    <tr>
                        <th v-for="header in headers" class="text-xs-left">{{header.text}}</th>
                        <th></th>
                    </tr>
                </template>
                <template slot="items" scope="props">
                    <td v-for="header in headers">
                        {{props.item[header.value]}}
                    </td>
                    <td class="text-xs-right">
                        <v-menu open-on-hover  offset-y left>
                            <v-icon slot="activator">settings</v-icon>
                            <v-list>
                                <v-list-tile :to="{ name: 'home.sifarnik.edit', params: { tipSifarnika: inputs.sifarnik, id: props.item.id, nazivSifarnika: $route.params.nazivSifarnika} }">
                                    <v-list-tile-title>Izmijeni</v-list-tile-title>
                                </v-list-tile>
                                <v-list-tile v-if="inputs.sifarnik != 'VrstaPohvale'" id="show-modal" @click="prikaziModal(props.item.id)">
                                    <v-list-tile-title>Obriši</v-list-tile-title>
                                </v-list-tile>
                            </v-list>
                        </v-menu>
                    </td>
                </template>
                <template slot="pageText" scope="{ pageStart, pageStop }">
                    Od {{ pageStart }} do {{ pageStop }}
                </template>
            </v-data-table>
        </v-card-text>
    </material-card>
</page>
</template>

<script>
import {
    SifarnikResource
} from 'api/resources';
import Identity from 'auth/identity';
import PaginationMixin from 'helpers/pagination-mixin';

export default {
    name: 'ListaSifarnik',
    mixins: [PaginationMixin],
    data() {
        return {
            model: {
                fields: [],
                items: []
            },
            lista: null,
            showModal: false,
            idBrisanje: null,
            filterMenu: false,
            filtrirano: false,
            inputs: {
                filter: null,
                page: 1,
                count: 30,
                sifarnik: this.$route.params.tipSifarnika
            },
            loading: true,
            identity: Identity
        };
    },

    computed: {
        headers() {
            return this.model.fields.filter(function (polje) {
                return !(polje.class == "hidden");
            });
        }
    },

    created() {
        this.$watch(() => this.$route.params.tipSifarnika, function (tip) {
            this.inputs.sifarnik = tip;
            this.updateData();
        });
        this.inputs = this.mergeQueryToInput(this.$route.query, this.inputs);
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
            this.osvjeziQuery(this.inputs);

            var success = (lista) => {
                that.model = lista.body;
                that.totalItems = lista.body.total;
                that.loading = false;
            };

            var error = () => {
                that.$toast.error("Filtriranje nije uspješno");
            };

            SifarnikResource().nekesirano(this.inputs)
                .then(success, error);
        },
        prikaziModal(id) {
            this.idBrisanje = id;
            this.showModal = true;
        },
        closeModal(event) {
            if (event.obrisi) {
                var parametri = {
                    tipSifarnika: this.$route.params.tipSifarnika,
                    id: event.id
                };
                var success = () => {
                    this.updateData();
                };

                SifarnikResource().update(parametri, {
                    sifarnik: {
                        isDeleted: true,
                        onemogucena: true
                    },
                    id: event.id
                }).then(success);
            }
            this.showModal = false;
        },
    }
};
</script>
