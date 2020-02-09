<template>
<page :resolved="resolved">
    <template v-if="resolved">
        <v-layout row justify-space-between>
            <v-flex>
                <h2 class="glavni-naslov">Pregled korisnika
                    <v-btn class="glavni-naslov" @click="() => this.activeHelpTip = true" flat icon color="secondary">
                        <v-icon>help</v-icon>
                    </v-btn>
                </h2>
            </v-flex>

            <v-flex  class="text-xs-right">
                <v-menu open-on-hover offset-y>
                    <v-icon slot="activator">more_vert</v-icon>
                    <v-list>
                        <v-list-tile :to="{ name: 'home.korisnik.edit'}">
                            <v-list-tile-title>Izmijeni korisnika</v-list-tile-title>
                        </v-list-tile>
                        <v-list-tile href="" v-if="resolved.model.onemogucen" @click="aktivirajKorisnika">
                            <v-list-tile-title>Aktiviraj korisnika</v-list-tile-title>
                        </v-list-tile>
                        <v-list-tile href="" v-if="!resolved.model.onemogucen" @click="deAktivirajKorisnika">
                            <v-list-tile-title>Deaktiviraj korisnika</v-list-tile-title>
                        </v-list-tile>
                    </v-list>
                </v-menu>
            </v-flex>
        </v-layout>
        <v-card>
            <v-card-title primary-title class="grey lighten-4">
                {{ resolved.model.punoIme }}
            </v-card-title>
            <v-card-text>
                <table class="v-table">
                    <tbody>
                        <tr>
                            <td>Korisničko ime</td>
                            <td>{{ resolved.model.korisnickoIme }}</td>
                        </tr>

                        <tr>
                            <td>Uloga korisnika</td>
                            <td>{{ resolved.model.uloga }}</td>
                        </tr>

                        <tr>
                            <td>Ime i prezime</td>
                            <td>{{ resolved.model.punoIme }}</td>
                        </tr>

                        <tr>
                            <td>E-mail</td>
                            <td>{{ resolved.model.email }}</td>
                        </tr>

                        <tr>
                            <td>Datum nastanka</td>
                            <td>{{ resolved.model.datumKreiranja.substring(8,10) + '. ' + resolved.model.datumKreiranja.substring(5,7)+ '. '+resolved.model.datumKreiranja.substring(0,4) + '.' }}</td>
                        </tr>

                        <tr>
                            <td>Status</td>
                            <td v-if="!resolved.model.onemogucen">Aktivan</td>
                            <td v-if="resolved.model.onemogucen">Nije aktivan</td>
                        </tr>

                    </tbody>
                </table>
            </v-card-text>
        </v-card>
    </template>
    <help-tip-dialog v-if="activeHelpTip" :title="helpTipDialogTitle" :content="helpTipDialogContent" @zatvori="ZatvoriHelpDialog"> </help-tip-dialog>
</page>
</template>

<script>
import {
    KorisnikResource
} from 'api/resources';
import Identity from 'auth/identity';
import HelpTipDialogMixin from 'helpers/help-tip-dialog-mixin';
export default {
    name: 'PregledUloga',
    mixins: [HelpTipDialogMixin],
    data() {
        return {
            activeHelpTip: false,
            helpTipDialogTitle: "Pregled korisnika",
            helpTipDialogContent: `Pregled podataka o korisniku.`,
            resolved: null,
            identity: Identity
        };
    },

    methods: {
        aktivirajKorisnika() {
            var that = this;

            var success = () => {
                that.resolved.model.onemogucen = false;
                that.$toast.info("Korisnik aktiviran");
            };

            var error = (poruka) => {
                that.$toast.error(poruka);
            };

            var promise = KorisnikResource().onemogucen(
                that.$route.params, {
                    onemogucen: false
                });

            promise.then(success, error);
        },
        deAktivirajKorisnika() {
            var that = this;

            var success = () => {
                that.resolved.model.onemogucen = true;
                that.$toast.info("Korisnik deaktiviran");
            };

            var error = (poruka) => {
                that.$toast.error(poruka);
            };

            var promise = KorisnikResource().onemogucen(
                that.$route.params, {
                    onemogucen: true
                });

            promise.then(success, error);
        }
    },
    resolve: {
        model() {
            return KorisnikResource().get(
                this.$route.params);
        }
    }
};
</script>
