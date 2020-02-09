<template>
<page>
    <material-card title="Pregled prijava u sistem" color="primary">
        <v-card-text>
            <v-data-table v-bind:headers="fields" v-bind:items="model.items" v-bind:pagination.sync="pagination" :total-items="totalItems" :rows-per-page-items="rowsPerPageItems" >
                <template slot="items" scope="props">
                    <td>
                        <date-format :vrijednost="props.item.datumKreiranja"></date-format>
                    </td>
                    <td>
                        <date-format :vrijednost="props.item.datumPosljednjeAkcije"> </date-format>
                    </td>
                    <td>{{ props.item.posljednjiIp }}</td>
                    <td>{{ props.item.posljednjiKlijent }}</td>
                    <td class="text-xs-center">
                        <span v-if="isCurrent(props.item)">
                    Trenutna sesija <v-icon>warning</v-icon>
                  </span>

                        <v-btn class="ma-0" v-else error @click="endSession(props.item)">
                            Prekini sesiju
                        </v-btn>
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
import Identity from 'auth/identity';
import {
    TokenResource
}
from 'api/resources';

import Page from '../components/page';

export default {
    name: 'Sesije',

    data() {
        return {
            model: {
                items: []
            },
            pagination: {
                page: 1,
                rowsPerPage: 30
            },
            totalItems: 0,
            rowsPerPageItems: [10, 20, 30, 50],
            fields: [{
                text: 'Vrijeme prijave',
                align: 'left',
                sortable: false,
                value: 'datumKreiranja'
            }, {
                text: 'Vrijeme zadnje akcije',
                align: 'left',
                sortable: false,
                value: 'datumPosljednjeAkcije'
            }, {
                text: 'Poslijednja IP adresa',
                align: 'left',
                sortable: false,
                value: 'posljednjiIp'
            }, {
                text: 'Poslijednji klijent',
                align: 'left',
                sortable: false,
                value: 'posljednjiKlijent'
            }, {
                text: 'Akcije',
                align: 'center',
                sortable: false,
                value: 'akcije'
            }],

            inputs: {
                korisnickoIme: this.$route.params.korisnickoIme,
                page: 1,
                count: 30
            }
        };
    },

    methods: {
        endSession(token) {
            var that = this;

            var success = () => {
                var items = that.model.items;
                var index = items.indexOf(token);
                items.splice(index, 1);
            };
            var error = () => {
                that.$toast.error('Prekidanje izabrane sesije nije uspjelo.');
            };

            var tokenResource = new TokenResource();
            var promise = tokenResource.remove({
                korisnickoIme: that.$route.params.korisnickoIme,
                tokenId: token.id
            });

            promise.then(success, error);
        },

        updateData() {
            var that = this;

            var success = (model) => {
                that.model.items = model.body.items;
                that.totalItems = model.body.total;
            };
            var a = this.inputs;
            console.log(a);
            TokenResource()
                .get(this.inputs)
                .then(success);
        },

        geoUrl(ip) {
            return `http://ip-api.com/#${ip}`;
        },

        isCurrent(token) {
            return Identity.id() === token.id;
        }
    },

    watch: {
        pagination: {
            handler(value) {
                this.inputs.page = value.page;
                this.inputs.count = value.rowsPerPage;
                this.updateData();
            },
            deep: true
        }
    },

    components: {
        Page
    }
};
</script>
