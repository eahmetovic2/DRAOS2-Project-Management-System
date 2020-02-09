<template>
<page>
    <template v-if="ucitaniDijeloviProjekta">

        <v-flex class="text-xs-right toolbar-btn">
            <v-btn @click="dialogDijeloviProjekta=true">
                <v-icon class="mr-2">add_circle</v-icon>
                DODAJ
            </v-btn>
        </v-flex>

        <v-flex slot="header">
            <v-layout row justify-space-between>
                <v-flex>
                    <span class="card-naslov">Dijelovi projekta </span>
                </v-flex>

            </v-layout>
        </v-flex>

        <v-card-text>
            <v-data-table :hide-actions="true" v-bind:headers="headersDijeloviProjekta" v-bind:items="dijeloviProjektaModel" no-data-text="Nema dijelova projekta.">
                <template slot="items" scope="props">
                    <td>{{ props.item.naziv }}</td>
                    <td class="text-xs-right">
                        <v-menu open-on-hover offset-y class="toolbar-menu" bottom left content-class="dropdown-menu" transition="slide-y-transition">
                            <v-icon slot="activator">settings</v-icon>
                            <v-list>
                                <v-list-tile @click="otvoriKategorijeZahtjeva(props.item.id)">
                                    <v-list-tile-title>Kategorije zahtjeva</v-list-tile-title>
                                </v-list-tile>
                            </v-list>
                        </v-menu>
                    </td>
                </template>
            </v-data-table>
        </v-card-text>

    </template>

    <template>
        <v-layout row justify-center>
            <v-dialog v-model="dialogDijeloviProjekta" persistent max-width="600px">
                <v-form v-model="validDioProjekta" ref="formDioProjekta">
                    <v-card>
                        <v-card-title>
                            <span class="headline">Dodavanje novog dijela projekta</span>
                        </v-card-title>
                        <v-card-text>
                            <v-container grid-list-md>
                                <v-layout wrap>
                                    <v-flex xs12 sm6 md6>
                                        <v-text-field :rules="rules.nazivDioProjekta" label="Naziv" required v-model="dioProjektaModel.naziv"></v-text-field>
                                    </v-flex>
                                </v-layout>
                            </v-container>
                        </v-card-text>
                        <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn color="blue darken-1" flat @click="dialogDijeloviProjekta = false">Odustani</v-btn>
                            <v-btn color="blue darken-1" flat @click="dodajNoviDioProjekta()">Sačuvaj</v-btn>
                        </v-card-actions>
                    </v-card>
                </v-form>
            </v-dialog>
        </v-layout>
    </template>

    <template>
        <v-layout row justify-center>
            <v-dialog v-model="dialogZahtjevKategorije" persistent max-width="600px">
                <v-form v-model="validKategorija" ref="formKategorija">
                    <v-card>
                        <v-card-title>
                            <span class="headline">Dodaj kategoriju zahtjeva za dio projekta</span>
                        </v-card-title>
                        <v-card-text>
                            <v-container grid-list-md>
                                <v-layout wrap>
                                    <v-flex xs12 sm6 md6>
                                        <v-text-field :rules="rules.nazivKategorija" label="Naziv" required v-model="kategorijaZahtjevaModel.naziv"></v-text-field>
                                    </v-flex>
                                </v-layout>
                            </v-container>
                        </v-card-text>
                        <v-card-text>
                            <v-data-table v-bind:headers="headersZahtjevKategorije" v-bind:items="zahtjevKategorijeDijelaProjekta" no-data-text="Nema kategorija zahtjeva.">
                                <template slot="items" scope="props">
                                    <td>{{ props.item.naziv }}</td>
                                </template>
                            </v-data-table>

                            <v-card-actions>
                                <v-spacer></v-spacer>
                                <v-btn color="blue darken-1" flat @click="dialogZahtjevKategorije = false">Odustani</v-btn>
                                <v-btn color="blue darken-1" flat @click="dodajZahtjevKategoriju(dioProjektaId)">Sačuvaj</v-btn>
                            </v-card-actions>

                        </v-card-text>

                    </v-card>
                </v-form>
            </v-dialog>
        </v-layout>
    </template>

</page>
</template>

<script>
import {
    KorisnikResource
} from "api/resources";

import {
    ProjekatResource
} from "api/resources";

import Identity from "auth/identity";
import Upit from "../../components/upit";
import HelpTipDialogMixin from "helpers/help-tip-dialog-mixin";

export default {
    name: "ListDioProjekta",

    components: {
        Upit
    },
    mixins: [HelpTipDialogMixin],
    data() {
        return {
            activeHelpTip: false,
            activeUpozorenje: false,
            resolved: null,
            identity: Identity,
            validDioProjekta: false,
            validKategorija: false,
            model: {

            },
            projekatModel: {},

            dioProjektaModel: {},
            dijeloviProjektaModel: [],
            rules: {
                nazivDioProjekta: [
                    v => !!v || "Morate da unesete naziv",
                    v =>
                    (v && v.length <= 32) ||
                    "Naziv ne može biti veći od 32 karaktera"
                ],
                
                nazivKategorija: [
                    v => !!v || "Morate da unesete naziv",
                    v =>
                    (v && v.length <= 128) ||
                    "Naziv ne može biti veći od 128 karaktera"
                ]
            },
            headersDijeloviProjekta: [{
                text: "Naziv",
                align: 'left',
                sortable: false,
            }],
            headersZahtjevKategorije: [{
                text: "Naziv",
                align: 'left',
                sortable: false,
            }],

            ucitaniDijeloviProjekta: false,
            dialogDijeloviProjekta: false,
            dialogZahtjevKategorije: false,
            zahtjevKategorijeDijelaProjekta: [],
            kategorijaZahtjevaModel: {},
            dioProjektaId: null,

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
        this.ucitajDijeloveProjekta();
    },
    created() {},
    methods: {
        Odustani() {
            this.proslijedi = true;
        },
        Odgovor(odgovor) {
            if (odgovor) {
                this.next();
            } else {
                this.next(false);
                this.activeUpozorenje = false;
            }
        },

        colorClass(color) {
            return color + " lighten-2";
        },
        ucitajDijeloveProjekta() {

            var success = (response) => {

                var that = this;
                that.dijeloviProjektaModel = response.body;
                that.ucitaniDijeloviProjekta = true;

            };
            var error = () => {
                console.log("nije uspjelo");

                //that.$toast.error('Nisu učitani podaci.');
            };

            var promise = ProjekatResource().vratiDijeloveProjekta({
                projekatId: this.$route.params.projekatId
            });

            promise.then(success, error);
        },

        dodajNoviDioProjekta() {
            this.validDioProjekta = this.$refs.formDioProjekta.validate();
            this.focusInvalidInput(this.$refs.formDioProjekta);

            if (!this.validDioProjekta) return;
            else {
                var that = this;

                var success = () => {
                    this.$toast.success(
                        "Uspješno dodano."
                    );
                    that.$router.push({
                        name: 'home.projekat.pregled'
                    });
                    that.dialogDijeloviProjekta = false;
                    this.ucitajDijeloveProjekta();
                };
                var error = () => {
                    this.$toast.error('Promjena podataka nije uspjela.');
                };

                var promise = ProjekatResource().dodajNoviDioProjekta(
                    that.$route.params, this.dioProjektaModel
                );
                promise.then(success, error);
            }
        },
        dodajZahtjevKategoriju(id) {
             this.validKategorija = this.$refs.formKategorija.validate();
            this.focusInvalidInput(this.$refs.formKategorija);

            if (!this.validKategorija) return;
            else {
            var that = this;

            var success = () => {
                this.$toast.success(
                    "Uspješno dodano."
                );
                that.$router.push({
                    name: 'home.projekat.pregled'
                });
                that.dialogZahtjevKategorije = false;
                this.ucitajKategorijeZahtjeva(id);
            };
            var error = () => {
                this.$toast.error('Promjena podataka nije uspjela.');
            };

            var promise = ProjekatResource().dodajNovuZahtjevKategoriju({
                dioProjektaId: id
            }, this.kategorijaZahtjevaModel);
            promise.then(success, error);
            }

        },
        ucitajKategorijeZahtjeva(id) {
            var success = (response) => {

                var that = this;
                that.zahtjevKategorijeDijelaProjekta = response.body;
                //that.ucit = true;

            };
            var error = () => {
                console.log("nije uspjelo");

                //that.$toast.error('Nisu učitani podaci.');
            };

            var promise = ProjekatResource().vratiZahtjevKategorijeZaDioProjekta({
                dioProjektaId: id
            });

            promise.then(success, error);
        },
        otvoriKategorijeZahtjeva(id) {
            this.dialogZahtjevKategorije = true;
            this.dioProjektaId = id;
            this.ucitajKategorijeZahtjeva(id);
            //this.zahtjevKategorijeDijelaProjekta=;

        }

    },
    resolve: {
        model() {
            return ProjekatResource().vratiProjekat({
                projekatId: this.$route.params.projekatId
            });
        }
    }

};
</script>
