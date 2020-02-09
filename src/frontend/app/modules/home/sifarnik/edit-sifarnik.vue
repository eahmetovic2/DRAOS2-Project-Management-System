<template>
<page :resolved="resolved">
    <v-card v-if="resolved">
        <v-card-title primary-title class="grey lighten-4">
            Izmjena
        </v-card-title>
        <v-card-text>
            <v-form v-model="valid" ref="form">
                <template v-for="polje in resolved.polja">
                    <template v-if="prikaziPolje('edit', polje.vidljivosti)">
                        <v-autocomplete :required="polje.required" :class="{'required': polje.required}" v-if="getTipPolja(polje.tip) == 'select'" item-text="naziv" item-value="id" :rules="polje.required ? rules.obavezno : rules.nijeObavezno" :label="polje.naziv" :items="polje.opcije" v-model="vrijednosti[polje.poljeUTabeli]"
                            bottom></v-autocomplete>
                        <v-checkbox v-else-if="getTipPolja(polje.tip) == 'checkbox'" :label="polje.naziv" v-model="vrijednosti[polje.poljeUTabeli]" light></v-checkbox>
                        <v-text-field v-else-if="getTipPolja(polje.tip) == 'number'" type="tel" pattern="[0-9]*" :required="polje.required" :class="{'required': polje.required}" :rules="polje.required ? rules.obaveznoBroj : rules.nijeObavezno" :label="polje.naziv" v-model="vrijednosti[polje.poljeUTabeli]"></v-text-field>
                        <v-text-field v-else :required="polje.required" :class="{'required': polje.required}" :rules="polje.required ? rules.obavezno : rules.nijeObavezno" :label="polje.naziv" v-model="vrijednosti[polje.poljeUTabeli]"></v-text-field>
                    </template>
                </template>

                <div class="text-xl-right my-3">
                    <odustani-btn @odustani="Odustani" />
                    <v-btn secondary @click="onSubmit">Spasi</v-btn>
                </div>
            </v-form>

        </v-card-text>
    </v-card>
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
    name: 'EditSifarnik',

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
                ],
                nijeObavezno: [
                    () => true
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

                SifarnikResource().update(that.$route.params, {
                    sifarnik: that.vrijednosti,
                    id: that.$route.params.id
                }).then(success, error);
            }
        },
        updateVrijednosti(naziv, vrijednost) {
            this.vrijednosti[naziv] = vrijednost;
        },
        lowercaseFirstLetter(string) {
            return string.charAt(0).toLowerCase() + string.slice(1);
        }
    },

    resolve: {
        polja() {
            return SifarnikResource().polja(this.$route.params);
        },
        vrijednosti() {
            return SifarnikResource().get(this.$route.params);
        }
    },

    watch: {
        resolved(resolved) {
            if (resolved) {
                this.resolved.polja.forEach(field => this.$set(this.vrijednosti, field.poljeUTabeli, resolved.vrijednosti[field.poljeUTabeli]));
            }
        }

    },

    components: {
        Page,
        Upit
    }
};
</script>
