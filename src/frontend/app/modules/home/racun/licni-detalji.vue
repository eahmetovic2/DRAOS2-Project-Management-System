<template>
<page :resolved="resolved">
    <material-card v-if="resolved" title="Lični podaci" color="primary">
        <v-card-text>
            <v-form v-model="valid" ref="form" :lazy-validation="true">
                <v-text-field  label="Puno ime" v-model="resolved.korisnik.punoIme" :rules="rules.obavezno" :counter="128" required class="required"></v-text-field>

                <v-text-field  label="E-mail" v-model="resolved.korisnik.email" :rules="rules.email" :counter="256" required class="required"></v-text-field>

                <div  class="text-xl-right my-3">
                    <odustani-btn />
                    <v-btn color="primary" @click="onSubmit">Spasi</v-btn>
                </div>
            </v-form>
        </v-card-text>
    </material-card>
</page>
</template>

<script>
import Identity from 'auth/identity';
import {
    KorisnikResource
}
from 'api/resources';
export default {
    name: 'LicniDetalji',

    data() {
        return {
            identity: Identity,
            resolved: null,
            valid: false,
            rules: {
                obavezno: [
                    (v) => !!v || 'Polje je obavezno'
                ],
                email: [
                    (v) => !!v || 'Polje je obavezno',
                    (v) => !!v || /\S+@\S+\.\S+/.test(v) || 'E-mail nije ispravan'
                ]
            },
        };
    },

    methods: {
        onSubmit() {
            this.$refs.form.validate();
            this.focusInvalidInput(this.$refs.form);

            if (this.valid) {
                var that = this;

                var success = () => {
                    if (Identity.korisnickoIme() === that.$route.params.korisnickoIme)
                        Identity.getToken().vlasnik.punoIme = that.resolved.korisnik.punoIme;
                    this.$toast.success(
                        "Uspješno ažurirani lični detalji."
                    );
                    that.$router.push({
                        name: 'home.dashboard'
                    });
                };
                var error = () => {
                    that.$toast.error('Promjena podataka nije uspjela.');
                };

                var promise = KorisnikResource().azurirajLicneDetalje(
                    that.$route.params, that.resolved.korisnik);

                promise.then(success, error);
            }
        }
    },
    resolve: {
        korisnik() {
            return KorisnikResource()
                .get(this.$route.params);
        }
    }
};
</script>
