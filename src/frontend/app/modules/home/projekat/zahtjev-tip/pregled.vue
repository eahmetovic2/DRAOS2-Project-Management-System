<template>
<page>
    <template v-if="ucitaniZahtjevTipovi">
         <v-flex class="text-xs-right toolbar-btn">
                        <v-btn @click="dialogTipoviZahtjeva=true">
                            <v-icon class="mr-2">add_circle</v-icon>
                            DODAJ
                        </v-btn>
                    </v-flex>
            <v-flex slot="header">
                <v-layout row justify-space-between>
                    <v-flex>
                        <span class="card-naslov">Tipovi zahtjeva </span>
                    </v-flex>
                    <v-flex class="text-xs-right toolbar-btn">
                        <v-btn @click="dialogTipoviZahtjeva=true">
                            <v-icon class="mr-2">add_circle</v-icon>
                            DODAJ
                        </v-btn>
                    </v-flex>
                </v-layout>
            </v-flex>

            <v-card-text>
                <v-data-table v-bind:headers="headersTipovi" v-bind:items="zahtjevTipovi" no-data-text="Nema tipova zahtjeva.">
                    <template slot="items" scope="props">
                        <td>{{ props.item.naziv }}</td>
                        <td class="text-xs-right">
                        </td>
                    </template>
                </v-data-table>
            </v-card-text>

            <v-select required class="required" :disabled="true" :items="zahtjevTipovi" item-text="naziv" item-value="id" label="Defaultni tip" v-model="defaultniTip" bottom></v-select>
    </template>

    <template>
        <v-layout row justify-center>
            <v-dialog v-model="dialogTipoviZahtjeva" persistent max-width="600px">

                <v-card>
                    <v-card-title>
                        <span class="headline">Dodavanje novog tipa zahtjeva</span>
                    </v-card-title>
                    <v-card-text>
                        <v-container grid-list-md>
                            <v-layout wrap>
                                <v-flex xs12 sm6 md6>
                                    <v-text-field label="Naziv" required v-model="zahtjevTipModel.naziv"></v-text-field>
                                </v-flex>

                            </v-layout>
                        </v-container>
                    </v-card-text>
                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn color="blue darken-1" flat @click="dialogTipoviZahtjeva = false">Odustani</v-btn>
                        <v-btn color="blue darken-1" flat @click="dodajNoviTipZahtjeva()">Sačuvaj</v-btn>
                    </v-card-actions>
                </v-card>
            </v-dialog>
        </v-layout>
    </template>

</page>
</template>

<script>
import {
    ProjekatResource
} from "api/resources";

import Identity from "auth/identity";
import Upit from "../../components/upit";
import HelpTipDialogMixin from "helpers/help-tip-dialog-mixin";

export default {
    name: "PregledZahtjevTipova",

    components: {
        Upit
    },
    mixins: [HelpTipDialogMixin],
    data() {
        return {
            defaultniTip: null,
            zahtjevTipModel: {},

            headersTipovi: [{
                text: "Naziv",
                align: 'left',
                sortable: false,
            }],
            zahtjevTipovi: null,
            ucitaniZahtjevTipovi: false,
            activeHelpTip: false,
            activeUpozorenje: false,
            resolved: null,
            identity: Identity,

            boje: [
                "secondary",
                "secondary",
                "cyan",
                "teal",
                "secondary-grey",
                "green"
            ],
            valid: false,
            model: {

            },
            projekatModel: {},

            rules: {
                punoIme: [
                    v => !!v || "Morate da unesete ime i prezime",
                    v =>
                    (v && v.length <= 128) ||
                    "Ime i prezime mora biti manje od 128 karaktera"
                ]
            },
            dialogTipoviZahtjeva: false,

        };
    },

    beforeRouteLeave(to, from, next) {
        if (!this.proslijedi) {
            this.activeUpozorenje = true;
            this.next = next;
        } else {
            next();
        }
    },

    mounted() {
        this.ucitajZahtjevTipove();

    },
    created() {},
    methods: {
        ucitajZahtjevTipove() {
            var success = (response) => {

                var that = this;
                that.zahtjevTipovi = response.body;
                that.ucitaniZahtjevTipovi = true;
                this.odrediDefaultniTip();

            };
            var error = () => {
                console.log("nije uspjelo");

                //that.$toast.error('Nisu učitani podaci.');
            };

            var promise = ProjekatResource().vratiTipoveZahtjevaZaProjekat({
                projekatId: this.$route.params.projekatId
            });

            promise.then(success, error);
        },
        odrediDefaultniTip() {

            for (var i = 0; i < this.zahtjevTipovi.length; i++) {

                if (this.zahtjevTipovi[i].default == true) {
                    this.defaultniTip = this.zahtjevTipovi[i];
                    break;
                }
            }
        },
        

        dodajNoviTipZahtjeva() {
            var that = this;

            var success = () => {
                this.$toast.success(
                    "Uspješno dodano."
                );
                that.$router.push({
                    name: 'home.projekat.pregled'
                });
                that.dialogTipoviZahtjeva = false;
                this.ucitajZahtjevTipove();
            };
            var error = () => {
                //that.$toast.error('Promjena podataka nije uspjela.');
            };

            var promise = ProjekatResource().dodajNoviTipZahtjevaZaProjekat(
                that.$route.params, this.zahtjevTipModel
            );
            promise.then(success, error);
        },
    }
};
</script>
