<template>
<page>
    <material-card title="Promjena lozinke" color="primary">
        <v-card-text>
            <v-form v-model="valid" ref="form" :lazy-validation="true">
                <v-text-field label="Trenutna lozinka" type="password" v-model="lozinka" :rules="rules.obavezno" required class="required"></v-text-field>

                <v-text-field label="Nova lozinka" type="password" v-model="novaLozinka" :rules="rules.lozinka" required class="required"></v-text-field>

                <v-text-field label="Nova ponovljena lozinka" type="password" v-model="ponovljenaLozinka" :rules="rules.lozinka" required class="required"></v-text-field>

                <div class="text-xl-right my-3">
                    <odustani-btn />
                    <v-btn color="primary" @click="onSubmit">Promijeni</v-btn>
                </div>
            </v-form>
        </v-card-text>
    </material-card>
</page>
</template>

<script>
import {
    KorisnikResource
} from 'api/resources';

import Page from '../components/page';

export default {
    name: 'Lozinka',

    data() {
        return {
            lozinka: '',
            novaLozinka: '',
            ponovljenaLozinka: '',
            valid: false,
            rules: {
                obavezno: [
                    (v) => !!v || 'Polje je obavezno'
                ],
                lozinka: [
                    (v) => !!v || 'Polje je obavezno',
                    (v) => !!v && /^.{6,}$/.test(v) || 'Lozinka mora imati najmanje 6 karaktera'
                ]
            },
        };
    },

    components: {
        Page
    },

    methods: {
        onSubmit() {
            this.$refs.form.validate();
            this.focusInvalidInput(this.$refs.form);

            if (this.valid) {
                var that = this;

                if (that.novaLozinka !== that.ponovljenaLozinka) {
                    that.$toast.error("Lozinka i ponovljena lozinka se ne poklapaju");
                    return;
                }

                var success = () => {
                    that.$toast.success('Lozinka promijenjena.');
                    that.$router.push({
                        name: 'home.dashboard'
                    });
                };
                var error = () => {
                    that.$toast.error('Promjena lozinke nije uspjela. Provjerite unesene podatke.');
                };

                var promise = KorisnikResource().promijeniLozinku(
                    that.$route.params, {
                        lozinka: that.lozinka,
                        novaLozinka: that.novaLozinka
                    });

                promise.then(success, error);
            }

        }
    }
};
</script>
