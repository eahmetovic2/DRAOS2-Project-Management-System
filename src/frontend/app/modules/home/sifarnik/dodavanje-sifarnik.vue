<template>
<page :resolved="resolved" :title="tipSifarnika">
    <material-card v-if="resolved" title="Dodavanje" color="primary">
        <v-card-text>
            <v-form v-model="valid" ref="form">
                <template v-for="polje in resolved.polja">
                    <template v-if="prikaziPolje('dodavanje', polje.vidljivosti)">
                        <v-autocomplete :required="polje.required" :class="{'required': polje.required}" v-if="getTipPolja(polje.tip) == 'select'" item-text="naziv" item-value="id" :rules="polje.required ? rules.obavezno : rules.nijeObavezno" :label="polje.naziv" :items="polje.opcije" v-model="vrijednosti[polje.poljeUTabeli]"
                            bottom></v-autocomplete>
                        <v-checkbox v-else-if="getTipPolja(polje.tip) == 'checkbox'" :rules="polje.required ? rules.obavezno : rules.nijeObavezno" :label="polje.naziv" v-model="vrijednosti[polje.poljeUTabeli]" light></v-checkbox>
                        <v-text-field v-else-if="getTipPolja(polje.tip) == 'number'" type="text" pattern="[0-9]*" :required="polje.required" :class="{'required': polje.required}" :rules="polje.required ? rules.obaveznoBroj : rules.nijeObavezno" :label="polje.naziv" v-model="vrijednosti[polje.poljeUTabeli]"></v-text-field>
                        <v-text-field v-else :required="polje.required" :class="{'required': polje.required}" :rules="polje.required ? rules.obavezno : rules.nijeObavezno" :label="polje.naziv" v-model="vrijednosti[polje.poljeUTabeli]"></v-text-field>
                    </template>
                </template>

                <div class="text-xl-right my-3">
                    <odustani-btn @odustani="Odustani" />
                    <v-btn secondary @click="onSubmit">Dodaj</v-btn>
                </div>
            </v-form>

        </v-card-text>
    </material-card>
    <Upit v-if="activeUpozorenje" @odgovor="Odgovor"></Upit>
</page>
</template>

<script>
import {
    SifarnikResource
}
from 'api/resources';

import Page from '../components/page';
import Upit from '../components/upit';
export default {
    name: 'DodavanjeSifarnik',

    data() {
        return {
            proslijedi: false,
            activeUpozorenje: false,
            next: {},
            resolved: null,
            valid: false,
            vrijednosti: {},
            rules: {
                obavezno: [
                    (v) => !!v || v === false || 'Polje je obavezno'
                ],
                obaveznoBroj: [
                    (v) => (!!v || v == 0) && /^[0-9]*$/.test(v) || 'Polje je obavezno i mora biti validan broj'
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
            this.valid = this.$refs.form.validate();
            this.focusInvalidInput(this.$refs.form);

            if (this.valid) {
                var that = this;
                var success = () => {
                    this.proslijedi = true;
                    that.$router.push({
                        name: 'home.sifarnik',
                        params: that.$route.params
                    });
                };
                var error = () => {
                    that.$toast.error('Dodavanje šifarnika nije uspjelo. Provjerite unesene podatke.');
                };

                var promise = SifarnikResource().save({
                    tipSifarnika: that.$route.params.tipSifarnika
                }, {
                    sifarnik: that.vrijednosti
                });

                promise.then(success, error);
            }
        }
    },

    resolve: {
        polja() {
            return SifarnikResource().polja(this.$route.params);
        }
    },

    watch: {
        resolved(resolved) {
            if (resolved) {
                this.resolved.polja.forEach(field => this.$set(this.vrijednosti, field.poljeUTabeli, null));
            }
        }
    },

    components: {
        Page,
        Upit
    }
};
</script>
