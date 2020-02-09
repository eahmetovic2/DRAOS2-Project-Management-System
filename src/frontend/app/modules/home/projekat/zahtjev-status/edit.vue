<style>
.ghost {
    opacity: 0.5;
    background: #c8ebfb;
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
>
<template>
<page>

    <template v-if="ucitaniZahtjevStatusi">
        <v-flex class="text-xs-left toolbar-btn">
            <v-btn @click="dialogStatusiZahtjeva=true">
                <v-icon class="mr-2">add_circle</v-icon>
                DODAJ
            </v-btn>
        </v-flex>

        <v-flex slot="header">
            <v-layout row justify-space-between>
                <v-flex>
                    <span class="card-naslov">Statusi zahtjeva </span>
                </v-flex>

            </v-layout>
        </v-flex>

        <br>
        <v-flex>Poredak statusa zahtjeva: </v-flex>
        <br>

        <v-flex xs12 md4 lg4 justify-right>
            <draggable ghost-class="ghost" class='dragArea' v-if="ucitaniZahtjevStatusi" :list="zahtjevStatusi">
                <v-card-text style="border:1px solid grey;" v-for="element in zahtjevStatusi" :key="element.id">
                    <span v-if="element.oznaka=='0'">{{ element.naziv }} (Oznaka statusa: <span class="status-to-do">Potrebno uraditi</span>)</span>
                    <span v-if="element.oznaka=='1'"> {{ element.naziv }} (Oznaka statusa: <span class="status-in-progress">Radi se</span>)</span>
                    <span v-if="element.oznaka=='2'"> {{ element.naziv }} (Oznaka statusa: <span class="status-done">Završeno</span>)</span>

                </v-card-text>

                <!--  <td>Status: </td>
                                <td v-if="zahtjevModel.zahtjevStatus.oznaka=='0'" class="status-to-do">{{ zahtjevModel.zahtjevStatus.naziv }}</td>
                                <td v-if="zahtjevModel.zahtjevStatus.oznaka=='1'" class="status-in-progress">{{ zahtjevModel.zahtjevStatus.naziv }}</td>
                                <td v-if="zahtjevModel.zahtjevStatus.oznaka=='2'" class="status-done">{{ zahtjevModel.zahtjevStatus.naziv }}</td>  -->
            </draggable>
        </v-flex>
        <v-btn color="primary" v-on:click="azurirajPoredakZahtjevStatusa()">Snimi poredak</v-btn>

        <v-select required class="required" :items="zahtjevStatusi" item-text="naziv" item-value="id" label="Defaultni status" v-model="defaultniStatus" bottom></v-select>
        <v-btn color="primary" v-on:click="azurirajDefaultniZahtjevStatusProjekta()">Snimi</v-btn>
    </template>

    <template>
        <v-layout row justify-center>
            <v-dialog v-model="dialogStatusiZahtjeva" persistent max-width="600px">
                <v-form v-model="valid" ref="form">

                    <v-card>
                        <v-card-title>
                            <span class="headline">Dodavanje nove vrste statusa zahtjeva</span>
                        </v-card-title>
                        <v-card-text>
                            <v-container grid-list-md>

                                <v-layout wrap>
                                    <v-flex xs12 sm6 md6>
                                        <v-text-field :rules="rules.nazivStatusZahtjeva" label="Novi status zahtjeva" required v-model="zahtjevStatusModel.naziv"></v-text-field>
                                    </v-flex>
                                    <v-flex xs12 sm6 md6>
                                        <v-select :rules="rules.oznakaStatusa" required class="required" :items="oznakeStatusa" item-text="naziv" item-value="id" v-model="zahtjevStatusModel.oznaka" label="Oznaka statusa" bottom></v-select>

                                    </v-flex>
                                </v-layout>

                            </v-container>
                        </v-card-text>
                        <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn color="blue darken-1" flat @click="dialogStatusiZahtjeva = false">Odustani</v-btn>
                            <v-btn color="blue darken-1" flat @click="dodajNoviStatusZahtjeva()">Sačuvaj</v-btn>
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
    name: "EditZahtjevStatusa",

    components: {
        Upit
    },
    mixins: [HelpTipDialogMixin],
    data() {
        return {
            defaultniStatus: null,

            odabranaOznakaStatusa: null,
            oznakeStatusa: [{
                    id: 0,
                    naziv: 'Potrebno uraditi'
                },
                {
                    id: 1,
                    naziv: 'Radi se'
                },
                {
                    id: 2,
                    naziv: 'Završeno'
                }
            ],
            zahtjevStatusModel: {},
            activeHelpTip: false,
            activeUpozorenje: false,
            resolved: null,
            identity: Identity,

            poredakZahtjevaId: [],
            valid: false,
            model: {

            },
            projekatModel: {},

            rules: {
                nazivStatusZahtjeva: [
                    v => !!v || "Morate da unesete naziv",
                    v =>
                    (v && v.length <= 20) ||
                    "Naziv ne može biti veći 20 karaktera"
                ],
                oznakaStatusa: [
                    v => v != null || "Morate da unesete oznaku"
                ]
            },

            headersStatusi: [{
                text: "Naziv",
                align: 'left',
                sortable: false,
            }, {
                text: "Oznaka",
                align: 'left',
                sortable: false,
            }],

            zahtjevStatusi: null,
            ucitaniZahtjevStatusi: false,
            dialogStatusiZahtjeva: false,

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
        this.ucitajZahtjevStatuse();

    },
    created() {},
    methods: {

        ucitajZahtjevStatuse() {
            var success = (response) => {

                var that = this;
                that.zahtjevStatusi = response.body;
                console.log(that.zahtjevStatusi);
                that.ucitaniZahtjevStatusi = true;
                this.odrediDefaultniStatus();
                //that.dodajNazivOznakeStatusa();

            };
            var error = () => {
                console.log("nije uspjelo");

                //that.$toast.error('Nisu učitani podaci.');
            };

            var promise = ProjekatResource().vratiStatuseZahtjevaZaProjekat({
                projekatId: this.$route.params.projekatId
            });

            promise.then(success, error);
        },

        dodajNoviStatusZahtjeva() {
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
                    that.dialogStatusiZahtjeva = false;
                    this.ucitajZahtjevStatuse();
                };
                var error = (poruka) => {
                    that.$toast.error(poruka.body);
                };

                var promise = ProjekatResource().dodajNoviStatusZahtjevaZaProjekat(
                    that.$route.params, this.zahtjevStatusModel
                );
                promise.then(success, error);
            }
        },
        odrediDefaultniStatus() {

            for (var i = 0; i < this.zahtjevStatusi.length; i++) {

                if (this.zahtjevStatusi[i].default == true) {
                    this.defaultniStatus = this.zahtjevStatusi[i];
                    break;
                }
            }
        },

        azurirajDefaultniZahtjevStatusProjekta() {
            var that = this;

            var success = () => {
                this.$toast.success(
                    "Uspješno promijenjeno."
                );
                that.$router.push({
                    name: 'home.projekat.pregled'
                });
                this.ucitajZahtjevStatuse();
            };
            var error = () => {
                //that.$toast.error('Promjena podataka nije uspjela.');
            };
            var promise = ProjekatResource().azurirajDefaultniZahtjevStatusProjekta(
                that.$route.params, {
                    Id: this.defaultniStatus
                }
            );
            promise.then(success, error);
        },

        azurirajPoredakZahtjevStatusa() {
            var that = this;
            console.log("statusi", that.zahtjevStatusi);
            var success = () => {
                this.$toast.success(
                    "Uspješno promijenjeno."
                );
                that.$router.push({
                    name: 'home.projekat.pregled'
                });
                that.poredakZahtjevaId = [];
                //this.ucitajZahtjevStatuse();
            };
            var error = () => {
                //that.$toast.error('Promjena podataka nije uspjela.');
            };

            that.zahtjevStatusi.forEach(element => {
                that.poredakZahtjevaId.push(element.id);
            });
            var promise = ProjekatResource().azurirajPoredakStatusa(that.$route.params, {
                zahtjeviId: that.poredakZahtjevaId
            });
            promise.then(success, error);
        },
        dodajNazivOznakeStatusa() {
            for (var i = 0; i < this.zahtjevStatusi.length; i++) {
                if (this.zahtjevStatusi[i].oznaka == 0) {
                    this.zahtjevStatusi[i].naziv += "  (Oznaka statusa: Potrebno uraditi)";
                } else if (this.zahtjevStatusi[i].oznaka == 1) {
                    this.zahtjevStatusi[i].naziv += "  (Oznaka statusa: Radi se)";
                } else if (this.zahtjevStatusi[i].oznaka == 2) {
                    this.zahtjevStatusi[i].naziv += "  (Oznaka statusa: Završeno)";
                }
            }

        }
    },

};
</script>
