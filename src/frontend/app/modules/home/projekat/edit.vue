<template>
<page>
    <template>
        <v-card-text>
            <v-form v-model="valid" ref="form">
                <v-text-field :rules="rules.punoIme" :counter="128" label="Naziv" v-model="projekatModel.naziv" required class="required"></v-text-field>

                <v-text-field :counter="256" label="Opis" v-model="projekatModel.opis" required class="required"></v-text-field>

                <br>

                <div class="text-xl-right my-3">
                    <v-btn color="tertiary" @click="Odustani">Odustani</v-btn>
                    <v-btn color="primary" @click="onSubmit">Snimi</v-btn>

                </div>
            </v-form>

        </v-card-text>
    </template>
    <!--<help-tip-dialog v-if="activeHelpTip" :title="VratiTitlePoId('korisnik-edit')" :content="VratiContentPoId('korisnik-edit')" @zatvori="ZatvoriHelpDialog"> </help-tip-dialog> -->

    <!-- <list-dio-projekta></list-dio-projekta>
    <edit-konfiguracije-projekta></edit-konfiguracije-projekta>

    <edit-zahtjev-prioriteta></edit-zahtjev-prioriteta>

    <edit-zahtjev-statusa></edit-zahtjev-statusa>
    <edit-zahtjev-tipova></edit-zahtjev-tipova>-->
</page>
</template>

<script>
import {
    ProjekatResource
} from "api/resources";

import Identity from "auth/identity";
import Upit from "../components/upit";
import HelpTipDialogMixin from "helpers/help-tip-dialog-mixin";
import ListDioProjekta from "./dio-projekta/list.vue";
import EditKonfiguracijeProjekta from "./konfiguracija-projekta/edit.vue";
import EditZahtjevPrioriteta from "./zahtjev-prioritet/edit.vue";
import EditZahtjevStatusa from "./zahtjev-status/edit.vue";
import EditZahtjevTipova from "./zahtjev-tip/edit.vue";

export default {
    name: "EditProjekta",

    components: {
        Upit,
        ListDioProjekta,
        EditKonfiguracijeProjekta,
        EditZahtjevPrioriteta,
        EditZahtjevStatusa,
        EditZahtjevTipova
    },
    mixins: [HelpTipDialogMixin],
    data() {
        return {
            editProjekta: false,
            activeHelpTip: false,
            activeUpozorenje: false,
            resolved: null,
            ucitanProjekat: false,
            identity: Identity,
            valid: false,
            model: {

            },
            projekatModel: {},
            sati: [],
            minute: [],
            rules: {
                punoIme: [
                    v => !!v || "Morate da unesete naziv",
                    v =>
                    (v && v.length <= 128) ||
                    "Naziv mora biti manji od 128 karaktera"
                ]
            },

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
        this.ucitajProjekat();
    },
    created() {},
    methods: {
        Odustani() {
            this.edit = true;
            this.$emit('onemogucenEdit');
        },
        Odgovor(odgovor) {
            if (odgovor) {
                this.next();
            } else {
                this.next(false);
                this.activeUpozorenje = false;
            }
        },

        onSubmit() {
            this.$refs.form.validate();
            this.focusInvalidInput(this.$refs.form);
            console.log("valid", this.valid);
            if (this.valid) {
                var that = this;

                var success = model => {
                    this.$toast.success(
                        "Uspješno ažuriran projekat."
                    );
                    this.$emit('onemogucenEdit');

                };
                var error = () => {
                    that.$toast.error('Promjena podataka nije uspjela.');
                };

                var promise = ProjekatResource().azurirajProjekat(
                    that.$route.params, that.projekatModel);

                promise.then(success, error);
            }
        },

        colorClass(color) {
            return color + " lighten-2";
        },
        ucitajProjekat() {

            var success = (response) => {

                var that = this;
                that.projekatModel = response.body;

                that.ucitanProjekat = true;

            };
            var error = () => {
                console.log("nije uspjelo");

                //that.$toast.error('Nisu učitani podaci.');
            };

            var promise = ProjekatResource().vratiProjekat({
                projekatId: this.$route.params.projekatId
            });

            promise.then(success, error);
        },

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
