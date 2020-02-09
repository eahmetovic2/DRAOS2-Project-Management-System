<template>
<page>
<template v-if="ucitanaKonfiguracijaProjekta">

    
    <v-flex slot="header">
        <v-layout row justify-space-between>
            <v-flex>
                <span class="card-naslov">Konfiguracija projekta </span>
            </v-flex>
        </v-layout>
    </v-flex>

    <table class="v-table">
        
                <time-picker-modal style="float: right;" @potvrda="ispisiRadnoVrijeme" :pocetnoVrijeme="konfiguracijaProjekta.radnoVrijemeOd" :krajnjeVrijeme="konfiguracijaProjekta.radnoVrijemeDo">
                    <v-btn slot="activator" color="primary" class="potvrdiBtn">
                        <v-icon>edit</v-icon>Edit
                    </v-btn>
                </time-picker-modal>
        <tbody>
            <tr>
                <td>Radno vrijeme od: </td>
                <td>{{ konfiguracijaProjekta.radnoVrijemeOd }}</td>
            </tr>

            <tr>
                <td>Radno vrijeme do: </td>
                <td>{{ konfiguracijaProjekta.radnoVrijemeDo }}</td>
            </tr>

            <tr>
                <v-select :multiple="true" :disabled="true" required class="required" :items="sviDani" item-text="naziv" v-model="radniDani" label="Radni dani" bottom></v-select>

            </tr>

        </tbody>
    </table>

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
    name: "PregledKonfiguracijeProjekta",

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
            sati: [],
            minute: [],

            rules: {
                punoIme: [
                    v => !!v || "Morate da unesete ime i prezime",
                    v =>
                    (v && v.length <= 128) ||
                    "Ime i prezime mora biti manje od 128 karaktera"
                ]
            },
            

            konfiguracijaProjekta: null,
            ucitanaKonfiguracijaProjekta: false,

            radniDani: [],

            sviDani: [
                "Ponedjeljak",
                "Utorak",
                "Srijeda",
                "Četvrtak",
                "Petak",
                "Subota",
                "Nedjelja"
            ]
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
        this.ucitajKonfiguracijuProjekta();
    },
    created() {},
    methods: {
        konvertujStringURadneDane() {
            var nizDani = this.konfiguracijaProjekta.radniDani.split("");

            for (var i = 0; i < nizDani.length; i++) {
                if (nizDani[i] == "1") {
                    switch (i) {
                        case 0:
                            this.radniDani.push("Ponedjeljak");
                            break;
                        case 1:
                            this.radniDani.push("Utorak");
                            break;
                        case 2:
                            this.radniDani.push("Srijeda");
                            break;
                        case 3:
                            this.radniDani.push("Četvrtak");
                            break;
                        case 4:
                            this.radniDani.push("Petak");
                            break;
                        case 5:
                            this.radniDani.push("Subota");
                            break;
                        case 6:
                            this.radniDani.push("Nedjelja");
                            break;
                    }
                }
            }
        },

        ucitajKonfiguracijuProjekta() {

            var success = (response) => {

                var that = this;
                that.konfiguracijaProjekta = response.body;

                that.ucitanaKonfiguracijaProjekta = true;
                this.konvertujStringURadneDane();

            };
            var error = () => {
                console.log("nije uspjelo");

                //that.$toast.error('Nisu učitani podaci.');
            };

            var promise = ProjekatResource().vratiKonfiguracijuProjekta({
                projekatId: this.$route.params.projekatId
            });

            promise.then(success, error);
        },
        snimiKonfiguracijuProjekta() {

            var dani = "0000000";
            var nizDani = dani.split("");

            for (var i = 0; i < this.radniDani.length; i++) {

                switch (this.radniDani[i].toString()) {
                    case "Ponedjeljak":
                        nizDani[0] = "1";
                        break;
                    case "Utorak":
                        nizDani[1] = "1";
                        break;
                    case "Srijeda":
                        nizDani[2] = "1";
                        break;
                    case "Četvrtak":
                        nizDani[3] = "1";
                        break;
                    case "Petak":
                        nizDani[4] = "1";
                        break;
                    case "Subota":
                        nizDani[5] = "1";
                        break;
                    case "Nedjelja":
                        nizDani[6] = "1";
                        break;
                }
            }

            dani = nizDani.join("");
            this.konfiguracijaProjekta.radniDani = dani;

            var that = this;

            var success = () => {
                this.$toast.success(
                    "Uspješno ažurirana konfiguracija."
                );
                that.$router.push({
                    name: 'home.projekat.pregled'
                });
            };
            var error = () => {
                //that.$toast.error('Promjena podataka nije uspjela.');
            };

            var promise = ProjekatResource().snimiKonfiguracijuProjekta(
                that.$route.params, this.konfiguracijaProjekta);

            promise.then(success, error);
        },
        ispisiRadnoVrijeme(vrijemeOd, vrijemeDo) {
            this.konfiguracijaProjekta.radnoVrijemeOd = vrijemeOd;
            this.konfiguracijaProjekta.radnoVrijemeDo = vrijemeDo;
        }
    }
};
</script>
