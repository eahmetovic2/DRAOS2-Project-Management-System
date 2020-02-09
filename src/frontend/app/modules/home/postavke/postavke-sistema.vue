<template>
<page :resolved="resolved">
    <material-card v-if="resolved" title="Postavke sistema" color="primary">
        <v-card-text>
            <v-form v-model="valid" ref="form">
                <template>
                    <v-text-field :rules="rules.obavezno64" :counter="64" label="Glavni naslov sistema" v-model="resolved.postavke.naslovSistema" required class="required"></v-text-field>
                    <v-text-field :type="number" min="0" max="10" label="Vrijeme trajanja sesije (u danima)" v-model="resolved.postavke.trajanjeSesije" required class="required"></v-text-field>
                    <v-text-field :rules="rules.obavezno128" :counter="128" label="URL Karte" v-model="resolved.postavke.urlKarte" required class="required"></v-text-field>
                    <v-text-field :rules="rules.obavezno256" :counter="256" label="Autorska prava karte" v-model="resolved.postavke.autorskaPravaKarte" required class="required"></v-text-field>
                   
                </template>
              
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
import Postavke from 'postavke/postavke';
import {
    PostavkeResource
} from 'api/resources';
import Identity from 'auth/identity';

export default {
    name: 'PostavkeSistema',

    data() {
        return {
            resolved: null,
            rules: {
                obavezno: [
                    (v) => !!v || 'Polje je obavezno'
                ],
                obavezno32: [
                    (v) => !!v || 'Polje je obavezno',
                    (v) => !!v && v.length <= 32 || 'Polje mora biti manje od 32 karaktera'
                ],
                obavezno64: [
                    (v) => !!v || 'Polje je obavezno',
                    (v) => !!v && v.length <= 64 || 'Polje mora biti manje od 64 karaktera'
                ],
                obavezno128: [
                    (v) => !!v || 'Polje je obavezno',
                    (v) => !!v && v.length <= 128 || 'Polje mora biti manje od 64 karaktera'
                ],
                obavezno256: [
                    (v) => !!v || 'Polje je obavezno',
                    (v) => !!v && v.length <= 256 || 'Polje mora biti manje od 64 karaktera'
                ]
            },
            identity: Identity
        };
    },

    methods: {
        onSubmit() {
            var that = this;

            var success = (response) => {
                Postavke.setPostavke(response.body);
                that.$toast.success('Uspješno ažuriranje postavki.');
            };
            var error = () => {
                that.$toast.error('Ažuriranje postavki nije uspjelo.');
            };

            var promise = PostavkeResource().update(
                that.$route.params, that.resolved.postavke);

            promise.then(success, error);
        }
    },

    resolve: {
        postavke() {
            return PostavkeResource().get();
        }
    }
};
</script>
