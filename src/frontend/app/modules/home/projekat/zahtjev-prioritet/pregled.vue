<template>
<page>

    <template v-if="ucitaniZahtjevPrioriteti">

         <v-flex class="text-xs-right toolbar-btn">
            <v-btn @click="otvoriDialogPrioritetiZahtjeva()">
                <v-icon class="mr-2">add_circle</v-icon>
                DODAJ
            </v-btn>
        </v-flex>

            <v-card-text>
                <v-data-table v-bind:headers="headersPrioriteti" v-bind:items="zahtjevPrioriteti" no-data-text="Nema prioriteta zahtjeva.">
                    <template slot="items" scope="props">
                        <td>{{ props.item.naziv }}</td>
                        <td>{{ props.item.poredak }}</td>
                        <td class="text-xs-right">
                        </td>
                    </template>
                </v-data-table>
            </v-card-text>

            <v-select required class="required" :disabled="true"  :items="zahtjevPrioriteti" item-text="naziv" item-value="id" label="Defaultni prioritet" v-model="defaultniPrioritet" bottom></v-select>

    </template>

    <template>
        <v-layout row justify-center>
            <v-dialog v-model="dialogPrioritetiZahtjeva" persistent max-width="600px">

                <v-card>
                    <v-card-title>
                        <span class="headline">Dodavanje nove vrste prioriteta zahtjeva</span>
                    </v-card-title>
                    <v-card-text>
                        <v-container grid-list-md>
                            <v-layout wrap>
                                <v-flex xs12 sm6 md6>
                                    <v-text-field label="Naziv" required v-model="zahtjevPrioritetModel.naziv"></v-text-field>
                                </v-flex>
                                <v-flex xs12 sm6 md6>
                                    <v-text-field label="Poredak" required v-model="zahtjevPrioritetModel.poredak"></v-text-field>
                                </v-flex>
                            </v-layout>
                        </v-container>
                    </v-card-text>
                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn color="blue darken-1" flat @click="dialogPrioritetiZahtjeva = false">Odustani</v-btn>
                        <v-btn color="blue darken-1" flat @click="dodajNoviPrioritetZahtjeva()">Sačuvaj</v-btn>
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
    name: "PregledZahtjevPrioriteta",

    components: {
        Upit
    },
    mixins: [HelpTipDialogMixin],
    data() {
        return {
            defaultniPrioritet: null,
            zahtjevPrioritetModel: {},
            activeHelpTip: false,
            activeUpozorenje: false,
            resolved: null,
            identity: Identity,

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

           
            headersPrioriteti: [{
                    text: "Naziv",
                    align: 'left',
                    sortable: false,
                }, {
                    text: "Poredak",
                    align: 'left',
                    sortable: false,
                }/*,
                {
                    text: "Default",
                    align: 'left',
                    sortable: false,

                }*/
            ],
                        zahtjevPrioriteti: null,
            ucitaniZahtjevPrioriteti: false,
            dialogPrioritetiZahtjeva: false,

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
                this.ucitajZahtjevPrioritete();

    },
    created() {},
    methods: {
        
        ucitajZahtjevPrioritete() {
            var success = (response) => {

                var that = this;
                that.zahtjevPrioriteti = response.body;
                that.ucitaniZahtjevPrioriteti = true;
                this.odrediDefaultniPrioritet();

            };
            var error = () => {
                console.log("nije uspjelo");

                //that.$toast.error('Nisu učitani podaci.');
            };

            var promise = ProjekatResource().vratiZahtjevPrioriteteZaProjekat({
                projekatId: this.$route.params.projekatId
            });

            promise.then(success, error);

        },
        
        odrediDefaultniPrioritet() {

            for (var i = 0; i < this.zahtjevPrioriteti.length; i++) {

                if (this.zahtjevPrioriteti[i].default == true) {
                    this.defaultniPrioritet = this.zahtjevPrioriteti[i];
                    break;
                }
            }
        },
        dodajNoviPrioritetZahtjeva() {

            var that = this;

            var success = () => {
                this.$toast.success(
                    "Uspješno dodano."
                );
                that.$router.push({
                    name: 'home.projekat.pregled'
                });
                that.dialogPrioritetiZahtjeva = false;
                this.ucitajZahtjevPrioritete();
            };
            var error = () => {
                //that.$toast.error('Promjena podataka nije uspjela.');
            };

            var promise = ProjekatResource().dodajNoviPrioritetZahtjevaZaProjekat(
                that.$route.params, this.zahtjevPrioritetModel
            );
            promise.then(success, error);
        },
        
    }
};
</script>
