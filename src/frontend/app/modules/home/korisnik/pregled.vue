<template>
<page>
    <template v-if="ucitanKorisnik">
        <material-card color="primary">
            <v-flex slot="header">
                <v-layout row justify-space-between>
                    <v-flex>
                        <span class="card-naslov">Pregled korisnika </span>
                    </v-flex>
                    <v-flex class="text-xs-right toolbar-btn">
                        <v-menu open-on-hover offset-y class="toolbar-menu" bottom left content-class="dropdown-menu" transition="slide-y-transition">
                            <v-icon slot="activator">more_vert</v-icon>
                            <v-list>
                                <v-list-tile :to="{ name: 'home.korisnik.edit'}">
                                    <v-list-tile-title>Izmijeni korisnika</v-list-tile-title>
                                </v-list-tile>
                                <v-list-tile href="" v-if="korisnikModel.onemogucen" @click="aktivirajKorisnika">
                                    <v-list-tile-title>Aktiviraj korisnika</v-list-tile-title>
                                </v-list-tile>
                                <v-list-tile href="" v-if="!korisnikModel.onemogucen && imaPravo('korisnik_korisnik_aktivacija')" @click="deAktivirajKorisnika">
                                    <v-list-tile-title>Deaktiviraj korisnika</v-list-tile-title>
                                </v-list-tile>
                            </v-list>
                        </v-menu>
                    </v-flex>
                </v-layout>
            </v-flex>
            <v-card-text>
                <h4>
                    {{ korisnikModel.punoIme }}
                </h4>
                <v-divider />
                <table class="v-table">
                    <tbody>
                        <tr>
                            <td>Korisničko ime</td>
                            <td>{{ korisnikModel.korisnickoIme }}</td>
                        </tr>

                        <tr>
                            <td>Ime i prezime</td>
                            <td>{{ korisnikModel.punoIme }}</td>
                        </tr>

                        <tr>
                            <td>E-mail</td>
                            <td>{{ korisnikModel.email }}</td>
                        </tr>

                        <tr>
                            <td>Datum nastanka</td>
                            <td>{{ korisnikModel.datumKreiranja.substring(8,10) + '. ' + korisnikModel.datumKreiranja.substring(5,7)+ '. '+korisnikModel.datumKreiranja.substring(0,4) + '.' }}</td>
                        </tr>

                        <tr>
                            <td>Status</td>
                            <td v-if="!korisnikModel.onemogucen">Aktivan</td>
                            <td v-if="korisnikModel.onemogucen">Nije aktivan</td>
                        </tr>

                        <tr>
                            <td>Uloga korisnika</td>
                            <td>
                                <v-chip v-bind:key="uloga.id" v-for="uloga in korisnikModel.uloge">
                                    <strong>{{ uloga.vrstaUloge.uloga }}</strong>
                                </v-chip>
                               <!-- <v-flex style="width: 150px">
                                    <v-select required class="required" @input="ucitajProjekteZaKorisnikUlogu" :items="korisnikModel.uloge" item-text="vrstaUloge.uloga" item-value="vrstaUloge.id" v-model="odabranaUloga" bottom></v-select>
                                </v-flex> -->

                            </td>
                        </tr>

                        <tr v-if="!izabranaUlogaAdministrator">
                            <td>Projekti</td>
                            <td>
                                <v-chip v-bind:key="projekat.id" v-for="projekat in ulogaProjekti">
                                    <strong>{{ projekat.naziv }}</strong>
                                </v-chip>
                            </td>
                        </tr>

                        
                        <tr v-if="izabranaUlogaSupport">
                            <td>Kategorije</td>
                            <td>
                                <v-chip v-bind:key="kategorija.id" v-for="kategorija in kategorijeKorisnika">
                                    <strong>{{ kategorija.detaljanNaziv }}</strong>
                                </v-chip>
                            </td>
                        </tr>

                    </tbody>
                </table>
            </v-card-text>
        </material-card>
    </template>
    <help-tip-dialog v-if="activeHelpTip" :title="helpTipDialogTitle" :content="helpTipDialogContent" @zatvori="ZatvoriHelpDialog"> </help-tip-dialog>
</page>
</template>

<script>
import {
    KorisnikResource,
    ProjekatResource
} from 'api/resources';
import Identity from 'auth/identity';
import HelpTipDialogMixin from 'helpers/help-tip-dialog-mixin';
export default {
    name: 'PregledKorisnik',
    mixins: [HelpTipDialogMixin],
    data() {
        return {
            activeHelpTip: false,
            helpTipDialogTitle: "Pregled korisnika",
            helpTipDialogContent: `Pregled podataka o korisniku.`,
            resolved: null,
            identity: Identity,
            odabranaUloga: {},
            korisnikModel: {},
            ucitanKorisnik: false,
            ulogaProjekti: [],
            kategorijeKorisnika: [],
            izabranaUlogaSupport: false,
            izabranaUlogaAdministrator: false,
        };
    },
    created() {
        this.ucitajKorisnika();
    },

    methods: {
        aktivirajKorisnika() {
            var that = this;

            var success = () => {
                that.korisnikModel.onemogucen = false;
                that.$toast.info("Korisnik aktiviran");
            };

            var error = (poruka) => {
                that.$toast.error(poruka.body);
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
                that.korisnikModel.onemogucen = true;
                that.$toast.info("Korisnik deaktiviran");
            };

            var error = (poruka) => {
                that.$toast.error(poruka.body);
            };

            var promise = KorisnikResource().onemogucen(
                that.$route.params, {
                    onemogucen: true
                });

            promise.then(success, error);
        },

        ucitajKorisnika() {
            var that = this;

            var success = response => {
                that.korisnikModel = response.body;
                that.odabranaUloga = that.korisnikModel.uloge[0];
                if (that.odabranaUloga.vrstaUloge.uloga == 'Support') {
                    that.izabranaUlogaSupport = true;
                    that.izabranaUlogaAdministrator =false;
                }
                else if(that.odabranaUloga.vrstaUloge.uloga == 'Administrator')
                {
                    that.izabranaUlogaAdministrator=true;
                    that.izabranaUlogaSupport=false;
                }
                that.ucitajProjekteZaKorisnikUlogu(that.odabranaUloga.vrstaUloge.id);
                that.ucitajKategorijeKorisnika();
                that.ucitanKorisnik = true;
            };

            var error = (poruka) => {
                that.$toast.error(poruka.body);
            };

            var promise = KorisnikResource().get(
                this.$route.params);

            promise.then(success, error);
        },
        ucitajProjekteZaKorisnikUlogu(ulogaId) {
            var that = this;

            var success = response => {
                that.ulogaProjekti = response.body;
            };

            var error = (poruka) => {
                that.$toast.error(poruka);
            };

            var promise = ProjekatResource().vratiProjekteZaKorisnikUlogu({
                korisnickoIme: this.korisnikModel.korisnickoIme,
                ulogaId: ulogaId
            });

            promise.then(success, error);
        },
        
        ucitajKategorijeKorisnika() {

            //this.kategorije = [];
            //this.korisnikModel.kategorije = null;
            var success = (response) => {
                var that=this;
                that.kategorijeKorisnika=response.body;

            };
            var error = () => {
                console.log("nije uspjelo");

                //that.$toast.error('Nisu učitani podaci.');
            };
            var promise = KorisnikResource().vratiKategorijeKorisnika({
                korisnickoIme: this.$route.params.korisnickoIme
            });
            promise.then(success, error);
        },
    },
    resolve: {
        /* model() {
             return KorisnikResource().get(
                 this.$route.params);
         }*/
    }
};
</script>
