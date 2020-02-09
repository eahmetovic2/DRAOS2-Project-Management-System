<template>
<page :resolved="resolved">
    <v-layout justify-center wrap>
        <v-flex xs12>
            <material-card color="primary">
                <v-flex slot="header">
                    <v-layout row justify-space-between>
                        <v-flex>
                            <span class="card-naslov">Uloge </span>
                        </v-flex>
                        <v-flex v-if="imaPravo('korisnik_uloga_dodavanje')" class="text-xs-right toolbar-btn">
                            <v-btn flat :to="{ name: 'home.uloga.dodavanje' }">
                                <v-icon class="mr-2">add_circle</v-icon>DODAJ
                            </v-btn>
                        </v-flex>
                    </v-layout>
                </v-flex>
                <v-data-table v-if="resolved" v-bind:headers="headers" v-bind:items="resolved.uloge.items" v-bind:pagination.sync="pagination" :total-items="totalItems" no-data-text="Nema uloga za prikaz." :loading="loading" :rows-per-page-items="rowsPerPageItems">
                    <template slot="headerCell" slot-scope="{ header }">
                        <span class="subheading font-weight-light text--darken-3" v-text="header.text"/>
                    </template>
                    <template slot="items" scope="props">
                        <td>{{ props.item.naziv }}</td>
                        <td>{{ props.item.isDeleted ? 'Ne' : 'Da' }}</td>
                        <td class="text-xs-right">
                            <v-menu open-on-hover offset-y class="toolbar-menu" bottom left content-class="dropdown-menu" transition="slide-y-transition">
                                <v-icon slot="activator">settings</v-icon>
                                <v-list>
                                    <v-list-tile :to="{ name: 'home.uloga.edit', params: { ulogaId: props.item.id }, query:{ naziv: props.item.naziv } }">
                                        <v-list-tile-title>Izmijeni ulogu</v-list-tile-title>
                                    </v-list-tile>
                                </v-list>
                            </v-menu>
                        </td>
                    </template>
                    <template slot="pageText" scope="{ pageStart, pageStop }">
                        Od {{ pageStart }} do {{ pageStop }}
                    </template>
                </v-data-table>
            </material-card>
        </v-flex>
    </v-layout>

</page>
</template>

<script>
import {
    UlogaResource
} from 'api/resources';
import PaginationMixin from 'helpers/pagination-mixin';

export default {
    name: 'ListaUloga',
    mixins: [PaginationMixin],
    data() {
        return {
            totalItems: 0,
            resolved: null,
            headers: [{
                text: 'Naziv',
                align: 'left',
                sortable: false,
                value: 'naziv'
            }, {
                text: 'Aktivan',
                align: 'left',
                sortable: false,
                value: 'aktivan'
            }, {
                text: '',
                align: 'left',
                sortable: false,
                value: ''
            }],
            inputs: {
                page: 1,
                count: 30
            }
        };
    },

    created() {
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

    resolve: {
        uloge() {
            return UlogaResource().get();
        }
    }

};
</script>
