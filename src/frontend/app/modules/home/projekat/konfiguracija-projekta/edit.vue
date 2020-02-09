<template>
<page>
    <template v-if="ucitanaKonfiguracijaProjekta">

        <table class="v-table">
            <tbody>
                <tr>
                    <td>Radno vrijeme od: </td>
                    <td style="float: left;">{{ konfiguracijaProjekta.radnoVrijemeOd }}</td>
                </tr>

                <tr>
                    <td>Radno vrijeme do: </td>
                    <td style="float: left;">{{ konfiguracijaProjekta.radnoVrijemeDo }}</td>
                </tr>

                <tr>
                    <td></td>
                    <td>
                        <time-picker-modal style="float: left;" @potvrda="ispisiRadnoVrijeme" :pocetnoVrijeme="konfiguracijaProjekta.radnoVrijemeOd" :krajnjeVrijeme="konfiguracijaProjekta.radnoVrijemeDo">
                            <v-btn slot="activator" color="primary" class="potvrdiBtn">
                                <v-icon>add</v-icon> Izmjena radnog vremena
                            </v-btn>
                        </time-picker-modal>
                    </td>
                </tr>

                <tr>
                </tr>
                <tr>
                    <v-select :multiple="true" required class="required" :items="sviDani" item-text="naziv" v-model="radniDani" label="Radni dani" bottom></v-select>
                </tr>

            </tbody>
        </table>
        <v-btn color="primary" v-on:click="snimiKonfiguracijuProjekta()">Snimi</v-btn>

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
import TimePickerModal from "./time-picker-modal.vue";

export default {
    name: "EditKonfiguracijeProjekta",

    components: {
        Upit,
        TimePickerModal
    },
    mixins: [HelpTipDialogMixin],
    data() {
        return {
            activeHelpTip: false,
            activeUpozorenje: false,
            resolved: null,
            identity: Identity,
            valid: false,
            model: {

            },
            projekatModel: {},
            sati: [],
            minute: [],

            rules: {},

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
            var error = (poruka) => {
                console.log("nije uspjelo");

                this.$toast.error(poruka.body);
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
            var error = (poruka) => {
                that.$toast.error(poruka.body);
            };

            var promise = ProjekatResource().snimiKonfiguracijuProjekta(
                that.$route.params, this.konfiguracijaProjekta);

            promise.then(success, error);
        },
        ispisiRadnoVrijeme(vrijemeOd, vrijemeDo) {
            if (vrijemeOd >= vrijemeDo)
                this.$toast.error('Početak radnog vremena mora biti prije kraja radnog vremena.');
            else {
                this.konfiguracijaProjekta.radnoVrijemeOd = vrijemeOd;
                this.konfiguracijaProjekta.radnoVrijemeDo = vrijemeDo;
                this.snimiKonfiguracijuProjekta();
            }
        }
    }
};
</script>
