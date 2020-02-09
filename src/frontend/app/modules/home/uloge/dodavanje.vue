<template>
<page :resolved="resolved">
    <material-card  v-if="resolved"
        title="Dodaj ulogu"
        color="primary">
            <v-form v-model="valid" ref="form">
                <v-text-field :rules="rules.obavezno100" :counter="100" label="Naziv" v-model="uloga.naziv" required class="required"></v-text-field>
                <v-text-field :rules="rules.obavezno100" :counter="100" label="Šifra" v-model="uloga.sifra" required class="required"></v-text-field>
                <!-- <v-select :rules="rules.obavezno" required class="required" :items="resolved.frontendModuli" item-text="naziv" item-value="id" v-model="uloga.frontendModulId" label="Korisnički interfejs" bottom></v-select>
                <v-select :rules="rules.obavezno" :multiple="true" required class="required" :items="resolved.uloge.items" item-text="naziv" item-value="id" v-model="uloga.dozvoljeneUlogeZaUpravljanje" label="Dozvoljene uloge za dodavanje" bottom></v-select> -->
                <div class="text-xl-right my-3">
                    <odustani-btn @odustani="Odustani" />
                    <v-btn secondary @click="onSubmit">Dodaj</v-btn>
                </div>
            </v-form>

    </material-card>
    <Upit v-if="activeUpozorenje" @odgovor="Odgovor"></Upit>
</page>
</template>

<script>
import {
    UlogaResource
} from "api/resources";
import {
    SifarnikResource
} from 'api/resources';
import Upit from "../components/upit";
export default {
    name: "DodavanjeUloga",

    components: {
        Upit
    },
    data() {
        return {
            proslijedi: false,
            activeUpozorenje: false,
            next: {},
            resolved: null,
            valid: true,
            uloga: {
                naziv: null,
                sifra: null
            },
            rules: {
                obavezno: [v => !!v || "Polje je obavezno"],
                obavezno100: [
                    v => !!v || "Polje je obavezno",
                    v =>
                    (!!v && v.length <= 100) || "Polje mora biti manje od 100 karaktera"
                ]
            }
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
        onSubmit() {
            var that = this;

            this.$refs.form.validate();
            this.focusInvalidInput(this.$refs.form);

            if (!this.valid) return;

            var success = (model) => {
                this.proslijedi = true;
                that.$router.push({
                    name: "home.prava.uloga.edit",
                    params: {
                        id: model.body.id
                    },
                    query: {
                        naziv: model.body.naziv
                    }
                });
            };
            var error = () => {
                that.$toast.error(
                    "Dodavanje uloge uspjelo. Provjerite unesene podatke."
                );
            };

            var promise = UlogaResource().save(that.$route.params, that.uloga);

            promise.then(success, error);
        }
    },
    resolve: {
        frontendModuli() {
            return SifarnikResource().get({
                sifarnik: 'FrontendModul'
            });
        },
        uloge() {
            return UlogaResource().get();
        },
    },
};
</script>
