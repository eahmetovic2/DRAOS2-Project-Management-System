<style>
.opis-zahtjeva-text {
    white-space: pre-wrap;
}

.text-centriran {
    text-align: center;
}

.status-to-do {
    color: orange;
}

.status-in-progress {
    color: blue;
}

.status-done {
    color: green;
}
</style>
<template>
<page>
    <template  v-if="ucitaneIzmjeneZahtjeva">
        <v-dialog v-if="ucitaneIzmjeneZahtjeva" v-model="ucitaneIzmjeneZahtjeva" @input="zatvoriDialog" max-width="1000px">

            <material-card color="primary">
                <v-flex slot="header">
                    <v-layout row justify-space-between>
                        <v-flex>
                            <span class="card-naslov">Historija statusa zahtjeva </span>
                        </v-flex>
                    </v-layout>
                </v-flex>
                <v-card-text>
                    <v-data-table v-bind:headers="headers" :hide-actions="true" v-bind:items="izmjeneZahtjeva">
                        <template slot="items" scope="props">

                            <tr>
                                <td>{{ props.item.datumKreiranja.substring(8,10) + '.' + props.item.datumKreiranja.substring(5,7)+ '.'+props.item.datumKreiranja.substring(0,4) + '. ' + props.item.datumKreiranja.substring(11,16) }}</td>
                                <td v-if="props.item.noviZahtjevStatus.oznaka=='0'" class="status-to-do">{{ props.item.noviZahtjevStatus.naziv }}</td>
                                <td v-if="props.item.noviZahtjevStatus.oznaka=='1'" class="status-in-progress">{{ props.item.noviZahtjevStatus.naziv }}</td>
                                <td v-if="props.item.noviZahtjevStatus.oznaka=='2'" class="status-done">{{ props.item.noviZahtjevStatus.naziv }}</td>
                            </tr>

                        </template>

                    </v-data-table>
                </v-card-text>

            </material-card>
        </v-dialog>
    </template>
</page>
</template>

<script>
import {
    KorisnikResource,
    ZahtjevResource
} from 'api/resources';
import Identity from 'auth/identity';
import HelpTipDialogMixin from 'helpers/help-tip-dialog-mixin';
import UploadMixin from "helpers/upload-mixin";

export default {
    name: 'IzmjenaZahtjevaHistorija',
    components: {},
    props: ['zahtjevId'],
    mixins: [HelpTipDialogMixin, UploadMixin],
    data() {
        return {
            childZahtjevId: Number(this.zahtjevId),
            activeHelpTip: false,
            resolved: null,
            identity: Identity,
            ucitaneIzmjeneZahtjeva: false,
            izmjeneZahtjeva: [],
            headers: [{
                text: 'Datum promjene',
                align: 'left',
                sortable: false,
            }, {
                text: 'Novi zahtjev status',
                align: 'left',
                sortable: false

            }]
        };
    },

    created() {
        this.vratiIzmjeneStatusaZahtjeva(this.childZahtjevId);
    },
    methods: {
        /* prilagodiPrikazPotrosenogVremena() {

             var vrijeme = '';
             var brojCifaraDani = 0;

             for (var i = 0; i < this.zahtjevModel.potrosenoVrijeme.length; i++) {
                 if (this.zahtjevModel.potrosenoVrijeme[i] == ':') {
                     brojCifaraDani = i;
                     break;
                 }
             }
             if (!(this.zahtjevModel.potrosenoVrijeme.substr(0, 2) == '00')) {
                 vrijeme = vrijeme + this.zahtjevModel.potrosenoVrijeme.substr(0, brojCifaraDani) + 'd ';
             }
             if (!(this.zahtjevModel.potrosenoVrijeme.substr(brojCifaraDani + 1, 2) == '00')) {
                 vrijeme = vrijeme + this.zahtjevModel.potrosenoVrijeme.substr(brojCifaraDani + 1, 2) + 'h ';
             }
             if (!(this.zahtjevModel.potrosenoVrijeme.substr(brojCifaraDani + 4, 2) == '00')) {
                 vrijeme = vrijeme + (this.zahtjevModel.potrosenoVrijeme.substr(brojCifaraDani + 4, 2) + 'm ');
             }
             if (!(this.zahtjevModel.potrosenoVrijeme.substr(brojCifaraDani + 7, 2) == '00')) {
                 vrijeme = vrijeme + (this.zahtjevModel.potrosenoVrijeme.substr(brojCifaraDani + 7, 2) + 's');
             }
             if (vrijeme == '')
                 vrijeme = "-";

             this.zahtjevModel.potrosenoVrijeme = vrijeme;

         },*/
        zatvoriDialog() {
            this.dialogEditZahtjeva = false;

            this.$emit("close");
        },
        vratiIzmjeneStatusaZahtjeva(id) {
            var that = this;

            var success = (response) => {
                that.izmjeneZahtjeva = response.body;
                that.ucitaneIzmjeneZahtjeva = true;

            };
            var error = (poruka) => {
                this.$toast.error(poruka.body);
            };
            var promise = ZahtjevResource().vratiIzmjeneStatusaZahtjeva({
                zahtjevId: id
            });

            promise.
            then(success, error);
        }
    },
    resolve: {}
};
</script>
