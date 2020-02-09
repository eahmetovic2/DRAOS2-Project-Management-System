<template>
<page>

    <template v-if="ucitaneZahtjevKategorije">
          <v-flex class="text-xs-right toolbar-btn">
                        <v-btn @click="dialogPrioritetiZahtjeva=true">
                            <v-icon class="mr-2">add_circle</v-icon>
                            DODAJ
                        </v-btn>
                    </v-flex>
            <v-flex slot="header">
                <v-layout row justify-space-between>
                    <v-flex>
                        <span class="card-naslov">Prioriteti zahtjeva </span>
                    </v-flex>
                    <v-flex class="text-xs-right toolbar-btn">
                        <v-btn @click="dialogPrioritetiZahtjeva=true">
                            <v-icon class="mr-2">add_circle</v-icon>
                            DODAJ
                        </v-btn>
                    </v-flex>
                </v-layout>
            </v-flex>

            <v-card-text>
                <v-data-table v-bind:headers="headersPrioriteti" v-bind:items="zahtjevKategorije" no-data-text="Nema kategorija zahtjeva.">
                    <template slot="items" scope="props">
                        <td>{{ props.item.naziv }}</td>
                        <td class="text-xs-right">
                        </td>
                    </template>
                </v-data-table>
            </v-card-text>

            <v-select required class="required" :items="zahtjevKategorije" item-text="naziv" item-value="id" label="Defaultni prioritet" v-model="defaultniPrioritet" bottom></v-select>
            <v-btn color="primary" v-on:click="azurirajDefaultniZahtjevPrioritetProjekta()">Snimi</v-btn>

    </template>

    <template>
        <v-layout row justify-center>
            <v-dialog v-model="dialogDodajKategoriju" persistent max-width="600px">

                <v-card>
                    <v-card-title>
                        <span class="headline">Dodavanje nove kategorije zahtjeva</span>
                    </v-card-title>
                    <v-card-text>
                        <v-container grid-list-md>
                            <v-layout wrap>
                                <v-flex xs12 sm6 md6>
                                    <v-text-field label="Naziv" required v-model="zahtjevPrioritetModel.naziv"></v-text-field>
                                </v-flex>
                            </v-layout>
                        </v-container>
                    </v-card-text>
                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn color="blue darken-1" flat @click="dialogDodajKategoriju = false">Odustani</v-btn>
                        <v-btn color="blue darken-1" flat @click="dodajZahtjevKategoriju()">Sačuvaj</v-btn>
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
    name: "EditZahtjevKategorija",

    components: {
        Upit
    },
    mixins: [HelpTipDialogMixin],
    data() {
        return {
            defaultnaKategorija: null,
            zahtjevKategorije: {},
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

           
            headersPrioriteti: [{
                    text: "Naziv",
                    align: 'left',
                    sortable: false,
                }/*,
                {
                    text: "Default",
                    align: 'left',
                    sortable: false,

                }*/
            ],
            ucitaneZahtjevKategorije: false,
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
        ucitajKategorijeZahtjeva()
        {
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
        
        dodajZahtjevKategoriju()
        {
            var that = this;

            var success = () => {
                this.$toast.success(
                    "Uspješno dodano."
                );
                that.$router.push({
                    name: 'home.projekat.pregled'
                });
                that.dialogZahtjevKategorija = false;
                this.ucitajKategorijeZahtjeva();
            };
            var error = () => {
                this.$toast.error('Promjena podataka nije uspjela.');
            };

            var promise = ProjekatResource().dodajNovuZahtjevKategoriju(
                that.$route.params, this.dioProjektaModel
            );
            promise.then(success, error);

        },
        odrediDefaultnuKategoriju() {

            for (var i = 0; i < this.zahtjevKategorije.length; i++) {

                if (this.zahtjevKategorije[i].default == true) {
                    this.defaultniPrioritet = this.zahtjevKategorije[i];
                    break;
                }
            }
        },
        
        azurirajDefaultnuZahtjevKategorijuDijelaProjekta() {
            var that = this;

            var success = () => {
                this.$toast.success(
                    "Uspješno promijenjeno."
                );
                that.$router.push({
                    name: 'home.projekat.pregled'
                });
                this.ucitajKategorijeZahtjeva();
            };
            var error = () => {
                //that.$toast.error('Promjena podataka nije uspjela.');
            };
            var promise = ProjekatResource().azurirajDefaultnuZahtjevKategorijuDijelaProjekta(
                that.$route.params, {Id:this.defaultniPrioritet}
            );
            promise.then(success, error);
        }
    }
};
</script>
