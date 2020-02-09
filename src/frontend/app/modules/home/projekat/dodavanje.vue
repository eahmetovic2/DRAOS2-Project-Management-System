<template>
<page>
    <material-card title="Dodaj projekat" color="primary">
        <v-card-text>
            <v-form v-model="valid" ref="form">
                <v-text-field :rules="rules.nazivProjekta" :counter="128" label="Naziv projekta" v-model="model.naziv" required class="required"></v-text-field>
                <v-text-field :counter="128" label="Opis" v-model="model.opis" required class="required"></v-text-field>
                <br>

                <div class="text-xl-right my-3">
                    <odustani-btn @odustani="Odustani" />
                    <v-btn color="primary" @click="onSubmit">Dodaj</v-btn>
                </div>
            </v-form>

        </v-card-text>
    </material-card>
    <Upit v-if="activeUpozorenje" @odgovor="Odgovor"></Upit>
    <!--<help-tip-dialog v-if="activeHelpTip" :title="VratiTitlePoId('korisnik-dodavanje')" :content="VratiContentPoId('korisnik-dodavanje')" @zatvori="ZatvoriHelpDialog"> </help-tip-dialog> -->
</page>
</template>

<script>
import {
    KorisnikResource
} from "api/resources";
import {
    ProjekatResource
} from 'api/resources';
import Identity from "auth/identity";
import Upit from "../components/upit";
import HelpTipDialogMixin from "helpers/help-tip-dialog-mixin";
export default {
    name: "DodavanjeProjekta",

    components: {
        Upit
    },
    mixins: [HelpTipDialogMixin],
    data() {
        return {
            activeHelpTip: false,
            proslijedi: false,
            activeUpozorenje: false,
            next: {},
            resolved: null,
            valid: false,
            identity: Identity,
            //boje: ["indigo", "blue", "cyan", "teal", "blue-grey", "green"],
            rules: {
                nazivProjekta: [
                    v => !!v || "Morate da unesete naziv projekta",
                    v =>
                                    (v && v.length <= 128) ||
                                    "Naziv projekta mora biti manji od 128 karaktera"
                ]
            },
            model: {
                naziv: null,
                opis: null
            },

            uloge: []
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
    created() {

      //  this.ucitajUloge();
    },
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

       /* ucitajUloge() {

            this.uloge = [{
                text: "Administrator",
                value: 1
            }];
        },*/
        onSubmit() {

            this.valid = this.$refs.form.validate();
            this.focusInvalidInput(this.$refs.form);
            var that = this;
            if (that.valid) {
                var success = model => {
                    this.proslijedi = true;
                    that.$router.push({
                        name: "home.projekat.pregled",
                        params: {
                            projekatId: model.body.id
                        }
                    });
                    that.$toast.success("Uspješno kreiran novi projekat.");

                };
                var error = poruka => {
                    that.$toast.error(poruka.body);
                };

                var promise = ProjekatResource().save(that.$route.params, that.model);
                promise.then(success, error);
            }
        }
    }
};
</script>
