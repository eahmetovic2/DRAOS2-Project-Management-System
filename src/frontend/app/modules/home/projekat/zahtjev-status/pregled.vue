<!-- <style>
.ghost {
    opacity: 0.5;
    background: #c8ebfb;
}
</style>
<template>
<page>

    <template v-if="ucitaniZahtjevStatusi">

        <v-flex class="text-xs-right toolbar-btn">
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
                <v-flex class="text-xs-right toolbar-btn">
                    <v-btn @click="dialogStatusiZahtjeva=true">
                        <v-icon class="mr-2">add_circle</v-icon>
                        DODAJ
                    </v-btn>
                </v-flex>
            </v-layout>
        </v-flex>

        <v-card-text>
            <v-data-table v-bind:headers="headersStatusi" v-bind:items="zahtjevStatusi" no-data-text="Nema statusa zahtjeva.">
                <template slot="items" scope="props">
                    <td>{{ props.item.naziv }}</td>
                    <td>{{ props.item.oznaka }}</td>

                    <td class="text-xs-right">
                    </td>
                </template>

            </v-data-table>

        </v-card-text>

        <v-select required class="required" :disabled="true" :items="zahtjevStatusi" item-text="naziv" item-value="id" label="Defaultni status" v-model="defaultniStatus" bottom></v-select>

        <v-card-text>
            <draggable :list="zahtjevStatusi" class="list-group" ghost-class="ghost">
                <div class="list-group-item" v-for="element in zahtjevStatusi" :key="element.id">
                    {{ element.name }}
                </div>
            </draggable>
        </v-card-text>

    </template>

    <template>
        <v-layout row justify-center>
            <v-dialog v-model="dialogStatusiZahtjeva" persistent max-width="600px">

                <v-card>
                    <v-card-title>
                        <span class="headline">Dodavanje nove vrste statusa zahtjeva</span>
                    </v-card-title>
                    <v-card-text>
                        <v-container grid-list-md>
                            <v-layout wrap>
                                <v-flex xs12 sm6 md6>
                                    <v-text-field label="Novi status zahtjeva" required v-model="zahtjevStatusModel.naziv"></v-text-field>
                                </v-flex>
                                <v-flex xs12 sm6 md6 class="text-xs-right toolbar-autocomplete">
                                    <v-select required class="required" :items="oznakeStatusa" item-text="naziv" item-value="id" v-model="zahtjevStatusModel.oznaka" label="Oznaka statusa" bottom></v-select>

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
    name: "PregledZahtjevStatusa",

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
                that.ucitaniZahtjevStatusi = true;
                that.dodajNazivOznakeStatusa();
                that.odrediDefaultniStatus();

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

        odrediDefaultniStatus() {

            for (var i = 0; i < this.zahtjevStatusi.length; i++) {

                if (this.zahtjevStatusi[i].default == true) {
                    this.defaultniStatus = this.zahtjevStatusi[i];
                    break;
                }
            }
        },

        dodajNoviStatusZahtjeva() {

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
            var error = () => {
                //that.$toast.error('Promjena podataka nije uspjela.');
            };

            var promise = ProjekatResource().dodajNoviStatusZahtjevaZaProjekat(
                that.$route.params, this.zahtjevStatusModel
            );
            promise.then(success, error);
        },
        dodajNazivOznakeStatusa() {
            for (var i = 0; i < this.zahtjevStatusi.length; i++) {
                console.log("oznaka",this.zahtjevStatusi[i].oznaka);
                if (this.zahtjevStatusi[i].oznaka == 0) {
                    this.zahtjevStatusi[i].naziv += " (Oznaka statusa: Potrebno uraditi)";
                    return;
                }

                if (this.zahtjevStatusi[i].oznaka == 1) {
                    this.zahtjevStatusi[i].naziv += " (Oznaka statusa: Radi se)";
                    return;
                }

                if (this.zahtjevStatusi[i].oznaka == 2) {
                    this.zahtjevStatusi[i].naziv += " (Oznaka statusa: Završeno)";
                    return;
                }
            }

        }
    }
    };
</script>
-->