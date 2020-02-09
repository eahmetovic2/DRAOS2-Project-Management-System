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
            <v-data-table :hide-actions="true" v-bind:headers="headersPrioriteti" v-bind:items="zahtjevPrioriteti" no-data-text="Nema prioriteta zahtjeva.">
                <template slot="items" scope="props">
                    <td>{{ props.item.naziv }}</td>
                    <td>{{ props.item.poredak }}</td>
                    <td class="text-xs-right">
                    </td>
                </template>
            </v-data-table>
        </v-card-text>

        <v-select required class="required" :items="zahtjevPrioriteti" item-text="naziv" item-value="id" label="Defaultni prioritet" v-model="defaultniPrioritet" bottom></v-select>
        <v-btn color="primary" v-on:click="azurirajDefaultniZahtjevPrioritetProjekta()">Snimi</v-btn>

    </template>

    <template>
        <v-layout row justify-center>
            <v-dialog v-model="dialogPrioritetiZahtjeva" persistent max-width="600px">
                <v-form v-model="valid" ref="form">

                    <v-card>
                        <v-card-title>
                            <span class="headline">Dodavanje nove vrste prioriteta zahtjeva</span>
                        </v-card-title>
                        <v-card-text>
                            <v-container grid-list-md>
                                <v-layout wrap>
                                    <v-flex xs12 sm6 md6>
                                        <v-text-field  :rules="rules.nazivPrioriteta" required label="Naziv" v-model="zahtjevPrioritetModel.naziv"></v-text-field>
                                    </v-flex>
                                    <v-flex xs12 sm6 md6>
                                        <!--<v-text-field label="Poredak" required v-model="zahtjevPrioritetModel.poredak"></v-text-field> -->
                                        <v-select required class="required" :items="zahtjevPrioriteti" item-text="naziv" item-value="poredak" label="Prioritet iznad kojeg je novi zahtjev" v-model="zahtjevPrioritetModel.poredak" bottom></v-select>
                                        <small>*Najmanji prioritet će imati, ako je ovo polje prazno</small>
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
                </v-form>

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
    name: "EditZahtjevPrioriteta",

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
                nazivPrioriteta: [
                    v => !!v || "Morate da unesete naziv",
                    v =>
                    (v && v.length <= 20) ||
                    "Naziv ne može biti veći od 20 karaktera."
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
            }],
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

        dodajNoviPrioritetZahtjeva() {
            this.valid = this.$refs.form.validate();
            this.focusInvalidInput(this.$refs.form);

            if (!this.valid) return;
            else {
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
            }
        },
        odrediDefaultniPrioritet() {

            for (var i = 0; i < this.zahtjevPrioriteti.length; i++) {

                if (this.zahtjevPrioriteti[i].default == true) {
                    this.defaultniPrioritet = this.zahtjevPrioriteti[i];
                    break;
                }
            }
        },

        azurirajDefaultniZahtjevPrioritetProjekta() {
            var that = this;

            var success = () => {
                this.$toast.success(
                    "Uspješno promijenjeno."
                );
                that.$router.push({
                    name: 'home.projekat.pregled'
                });
                this.ucitajZahtjevPrioritete();
            };
            var error = () => {
                //that.$toast.error('Promjena podataka nije uspjela.');
            };
            var promise = ProjekatResource().azurirajDefaultniZahtjevPrioritetProjekta(
                that.$route.params, {
                    Id: this.defaultniPrioritet
                }
            );
            promise.then(success, error);
        },
        otvoriDialogPrioritetiZahtjeva() {

            this.dialogPrioritetiZahtjeva = true;
            this.zahtjevPrioritetModel={};
            //this.zahtjevPrioritetModel.naziv = '';
            this.zahtjevPrioritetModel.poredak = this.zahtjevPrioriteti.length;
        }
    }
};
</script>
