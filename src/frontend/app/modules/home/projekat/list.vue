<template>
<page>
    <material-card color="primary">
        <v-flex slot="header">
            <v-layout row justify-space-between>
                <v-flex>
                    <span class="card-naslov">Projekti </span>
                </v-flex>
                <v-flex v-if="childOmogucenoDodavanjeIFiltriranje" class="text-xs-right toolbar-btn">
                    <v-btn flat :to="{ name: 'home.projekat.dodavanje' }">
                        <v-icon class="mr-2">add_circle</v-icon>
                        DODAJ
                    </v-btn>

                  <!--  <v-menu :close-on-content-click="false" content-class="dropdown-menu" v-model="filterMenu" bottom left offset-y transition="slide-y-transition">
                        <v-btn slot="activator" flat class="toolbar-btn">
                            <v-icon class="mr-2">filter_list</v-icon>Filtriraj
                        </v-btn>
                        <v-card>
                            <v-card-title>
                                <div class="filter-title">
                                    Filtriraj po
                                </div>
                            </v-card-title>
                            <v-form class="filter" @submit="filtriraj()">
                                <v-card-text class="pt-0">
                                    <v-text-field label="Naziv" hide-details v-model="inputs.filter"></v-text-field>
                                </v-card-text>
                                <v-card-actions>
                                    <v-spacer></v-spacer>
                                    <v-btn flat color="primary" @click="ponistiFilter()">Poništi</v-btn>
                                    <v-btn flat color="primary" type="submit">Filtriraj</v-btn>
                                </v-card-actions>
                            </v-form>
                        </v-card>
                    </v-menu>-->
                </v-flex>
            </v-layout>
        </v-flex>

        <v-card-text>
            <v-data-table v-bind:headers="headers" v-bind:items="model.items" v-bind:pagination.sync="pagination" :total-items="totalItems" no-data-text="Nema projekata za prikaz." :loading="loading" :rows-per-page-items="rowsPerPageItems">
                <template slot="items" scope="props">

                    <tr v-if="imaPravo('projekat_projekat_pregled')" @click="otvoriProjekat(props.item.id)" :to="{ name: 'home.projekat.pregled', params: {projekatId: props.item.id }}">
                        <td>{{ props.item.naziv }}</td>
                        <td>{{ props.item.opis }}</td>

                        <td class="text-xs-right">
                            <v-menu open-on-hover offset-y>
                                <v-icon slot="activator">settings</v-icon>
                                <v-list>
                                    <v-list-tile :to="{ name: 'home.projekat.pregled', params: {projekatId: props.item.id }}">
                                        <v-list-tile-title>Pregled projekta</v-list-tile-title>
                                    </v-list-tile>
                                    <!-- <v-list-tile :to="{ name: 'home.projekat.edit', params: {projekatId: props.item.id }}">
                                    <v-list-tile-title>Izmijeni projekat</v-list-tile-title>
                                </v-list-tile> -->
                                </v-list>
                            </v-menu>
                        </td>
                    </tr>
                    <tr v-if="!imaPravo('projekat_projekat_pregled')">
                        <td>{{ props.item.naziv }}</td>
                        <td>{{ props.item.opis }}</td>
                    </tr>
                </template>
                <template slot="pageText" scope="{ pageStart, pageStop }">
                    Od {{ pageStart }} do {{ pageStop }}
                </template>
            </v-data-table>
        </v-card-text>
    </material-card>
    <!-- <help-tip-dialog v-if="activeHelpTip" :title="VratiTitlePoId('korisnik-list')" :content="VratiContentPoId('korisnik-list')" @zatvori="ZatvoriHelpDialog"> </help-tip-dialog>  -->
</page>
</template>

<script>
import HelpTipDialogMixin from 'helpers/help-tip-dialog-mixin';
import debounce from 'lodash.debounce';
import {
    ProjekatResource
} from 'api/resources';
import Identity from 'auth/identity';
import PaginationMixin from 'helpers/pagination-mixin';

export default {
    name: 'ListaProjekata',
    mixins: [PaginationMixin, HelpTipDialogMixin],
    components: {
        debounce
    },
    props: ['omogucenoDodavanjeIFiltriranje'],

    data() {
        return {
            childOmogucenoDodavanjeIFiltriranje: this.omogucenoDodavanjeIFiltriranje,
            activeHelpTip: false,
            model: {
                items: []
            },
            totalItems: 0,
            filterMenu: false,
            filtrirano: false,
            loading: false,
            headers: [{
                    text: 'Naziv projekta',
                    align: 'left',
                    sortable: false,
                    value: 'naziv'
                }, {
                    text: 'Opis',
                    align: 'left',
                    sortable: false,
                    value: 'opis'
                },
                {
                    text: '',
                    align: 'left',
                    sortable: false,
                    value: ''
                }
            ],
            inputs: {
                filter: null,
                page: 1,
                count: 10
            },
            identity: Identity
        };
    },

    created() {

        if (this.omogucenoDodavanjeIFiltriranje == null)
            this.childOmogucenoDodavanjeIFiltriranje = true;

        this.pagination.rowsPerPage = 10;
        //this.inputs = this.mergeQueryToInput(this.$route.query, this.inputs);
    },

    mounted() {

        if (this.inputs.page) {
            this.pagination.page = this.inputs.page;
        }
        if (this.inputs.count) {
            this.pagination.count = this.inputs.count;
        }
        //this.ucitajProjekte();
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
        ucitajProjekte() {

            var that = this;
            that.loading = true;
            var success = (response) => {
                that.model = response.body;
                that.totalItems = response.body.total;

                that.loading = false;

            };
            var error = () => {
                this.$toast.error('Nisu ucitani projekti.');
            };

            var promise = ProjekatResource().vratiSveProjekte(that.inputs);
            promise.
            then(success, error);
        },
        updateData() {
            var that = this;
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
            var promise = ProjekatResource().vratiSveProjekte(that.inputs);
            promise.
            then(success, error);
        },
        otvoriProjekat(id) {
            this.$router.push({
                name: 'home.projekat.pregled',
                params: {
                    projekatId: id
                }
            })

        }
    }

};
</script>
