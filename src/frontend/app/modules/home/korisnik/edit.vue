<template>
<page>
    <material-card title="Izmijeni korisnika" color="primary">

        <v-card-text>
            <v-form v-model="valid" ref="form">
                <v-text-field :rules="rules.punoIme" :counter="128" label="Ime i prezime" v-model="korisnikModel.punoIme" required class="required"></v-text-field>

                <v-text-field :rules="rules.email" :counter="256" label="E-mail" v-model="korisnikModel.email" type="email" required class="required"></v-text-field>

                <v-text-field :rules="rules.lozinka" :counter="128" label="Lozinka" v-model="model.lozinka" type="password"></v-text-field>

                <v-flex>
                    <v-select required class="required" @input="ucitajProjekteZaKorisnikUlogu" :items="korisnikModel.uloge" label="Uloga" item-text="vrstaUloge.uloga" item-value="vrstaUloge.id" v-model="odabranaUloga" bottom></v-select>
                </v-flex>

                <v-combobox v-if="!izabranaUlogaSupport && !izabranaUlogaAdministrator" eager v-model="ulogaProjekti" :items="projekti.items" label="Projekti" chips clearable multiple item-text="naziv" item-value="id">
                    <template v-slot:selection="data">
                        <v-chip active :selected="data.selected" close @input="removeProjekat(data.item)">
                            <strong>{{ data.item.naziv }}</strong>&nbsp;
                        </v-chip>
                    </template>
                </v-combobox>

                <v-form v-if="izabranaUlogaSupport">
                    <v-flex xs4 sm4 md4>
                        <v-select required class="required" :items="projekti.items" @input="ucitajDijeloveProjekta" item-text="naziv" item-value="id" label="Projekat" v-model="korisnikModel.projekat" bottom></v-select>
                    </v-flex>

                    <v-flex v-if="dijeloviProjekta.length!=0" xs4 sm4 md4 :style="{display: dijeloviProjekta.length>1 ? 'visible' : 'none'}">
                        <v-select required class="required" @input="ucitajKategorije" :items="dijeloviProjekta" item-text="naziv" item-value="id" label="Podprojekat" v-model="korisnikModel.dioProjekta" bottom></v-select>
                    </v-flex>

                    <v-combobox v-if="ucitaneKategorijeKorisnika" eager v-model="korisnikModel.kategorije" :items="kategorije"  label="Kategorije" chips clearable multiple item-text="naziv" item-value="id">
                        <template v-slot:selection="data">
                            <v-chip active :selected="data.selected" close @input="removeKategorija(data.item)">
                                <strong>{{ data.item.detaljanNaziv }}</strong>&nbsp;
                            </v-chip>
                        </template>
                    </v-combobox>
                </v-form>

                <br>

                <div class="text-xl-right my-3">
                    <odustani-btn @odustani="Odustani" />
                    <v-btn color="primary" @click="onSubmit">Snimi</v-btn>
                </div>
            </v-form>

        </v-card-text>
        <!-- <v-btn @click="otvoriDialogDodavanjeKorisnikaNaProjekat()">
            <v-icon class="mr-2">add_circle</v-icon>
            DODAVANJE NA PROJEKTE
        </v-btn>
         <dodavanje-korisnika-projekat v-if="dialogDodajKorisnikaNaProjekat" :ul="ul" :korisnicko-ime="korisnikModel.korisnickoIme"  @dialogDodajKorisnikaNaProjekat="zatvoriDialogDodajKorisnikaNaProjekat"></dodavanje-korisnika-projekat>
-->
    </material-card>
    <Upit v-if="activeUpozorenje" @odgovor="Odgovor"></Upit>
    <help-tip-dialog v-if="activeHelpTip" :title="VratiTitlePoId('korisnik-edit')" :content="VratiContentPoId('korisnik-edit')" @zatvori="ZatvoriHelpDialog"> </help-tip-dialog>
</page>
</template>

<script>
import {
    KorisnikResource,
    ProjekatResource
} from "api/resources";

import Identity from "auth/identity";
import Upit from "../components/upit";
import HelpTipDialogMixin from "helpers/help-tip-dialog-mixin";
import DodavanjeKorisnikaProjekat from '../projekat/korisnik-dodavanje.vue';

export default {
    name: "EditKorisnika",

    components: {
        Upit,
        DodavanjeKorisnikaProjekat
    },
    mixins: [HelpTipDialogMixin],
    data() {
        return {
            activeHelpTip: false,
            proslijedi: false,
            activeUpozorenje: false,
            next: {},
            ul: [],
            resolved: null,
            identity: Identity,
            dialogDodajKorisnikaNaProjekat: false,
            izabranaUlogaSupport: false,
            izabranaUlogaAdministrator: false,


            valid: false,
            model: {
                lozinka: ""
            },

            inputs: {
                sve: true
            },
            odabranaUloga: {},
            projekti: [],
            ulogaProjekti: [],
            korisnikModel: {},
            kategorije: [],
            dijeloviProjekta: [],
            ucitaneKategorijeKorisnika: false,

            rules: {
                punoIme: [
                    v => !!v || "Morate da unesete ime i prezime",
                    v =>
                    (v && v.length <= 128) ||
                    "Ime i prezime mora biti manje od 128 karaktera"
                ],
                email: [
                    v => !!v || "Morate da unesete e-mail",
                    v =>
                    (v && v.length <= 256) || "Email mora biti kraći od 256 karaktera",
                    v =>
                    /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(v) ||
                    "E-mail mora biti ispravan"
                ],
                lozinka: [
                    v =>
                    !v || v.length <= 128 || "Lozinka mora biti manja od 128 karaktera",
                    v =>
                    !v || /^.{6,}$/.test(v) || "Lozinka mora imati najmanje 6 karaktera"
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
    created() {
        this.ucitajModel();

    },
    methods: {

        ucitajProjekteZaKorisnikUlogu(ulogaId) {
            var that = this;

            var success = response => {
                that.ulogaProjekti = response.body;
                that.ucitajProjekte();

            };

            var error = (poruka) => {
                that.$toast.error(poruka.body);
            };

            var promise = ProjekatResource().vratiProjekteZaKorisnikUlogu({
                korisnickoIme: this.korisnikModel.korisnickoIme,
                ulogaId: ulogaId
            });

            promise.then(success, error);
        },
        posaljiUloge() {
            var uloge = this.korisnikModel.uloge;
            this.uloge = uloge;
        },
        zatvoriDialogDodajKorisnikaNaProjekat(dialogDodajKorisnikaNaProjekat) {
            this.dialogDodajKorisnikaNaProjekat = dialogDodajKorisnikaNaProjekat;
        },
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
                        name: "home.korisnik.pregled",
                        params: {
                            korisnickoIme: that.korisnikModel.korisnickoIme
                        }
                    });
                };
                var error = poruka => {
                    that.$toast.error(poruka.body);
                };
                let modelSaLozinkom = this.korisnikModel;
                modelSaLozinkom.projekti = this.ulogaProjekti;
                modelSaLozinkom.lozinka = this.model.lozinka;
                //modelSaLozinkom.ucenici = that.dodaniUcenici;

                var promise = KorisnikResource().update(
                    that.$route.params,
                    modelSaLozinkom
                );
                promise.then(success, error);
            }
        },

        colorClass(color) {
            return color + " lighten-2";
        },
        ucitajProjekte() {
            var that = this;

            var success = response => {
                that.projekti = response.body;
            };

            var error = (poruka) => {
                that.$toast.error(poruka.body);
            };

            var promise = ProjekatResource().vratiSveProjekte(that.inputs);

            promise.then(success, error);
        },
        ucitajModel() {
            var that = this;

            var success = response => {
                that.korisnikModel = response.body;
                that.odabranaUloga = that.korisnikModel.uloge[0];

                if (that.odabranaUloga.vrstaUloge.uloga == 'Support') {
                    that.izabranaUlogaSupport = true;
                    that.izabranaUlogaAdministrator=false;
                }
                else if (that.odabranaUloga.vrstaUloge.uloga == 'Administrator') {
                    that.izabranaUlogaSupport = false;
                    that.izabranaUlogaAdministrator=true;
                }
                that.ucitajProjekteZaKorisnikUlogu(that.odabranaUloga.vrstaUloge.id);
                that.ucitajKategorijeKorisnika();
            };

            var error = (poruka) => {
                that.$toast.error(poruka.body);
            };

            var promise = KorisnikResource()
                .get(this.$route.params);

            promise.then(success, error);
        },

        ucitajDijeloveProjekta(id) {
            this.dijeloviProjekta = [];
            var success = (response) => {

                var that = this;
                that.dijeloviProjekta = response.body;

                for (var i = 0; i < that.dijeloviProjekta.length; i++) {
                    {
                        that.korisnikModel.dioProjekta = that.dijeloviProjekta[i].id;
                        break;
                    }
                }
                that.ucitajKategorije(that.korisnikModel.dioProjekta);
            };
            var error = () => {
                console.log("nije uspjelo");

                //that.$toast.error('Nisu učitani podaci.');
            };

            var promise = ProjekatResource().vratiDijeloveProjekta({
                projekatId: id
            });

            promise.then(success, error);
        },
        ucitajKategorije(id) {
            this.kategorije = [];
            //this.korisnikModel.kategorije = null;
            var that = this;

            var success = (response) => {
                that.kategorije = response.body;
                console.log("kategorije", that.kategorije);

            };
            var error = () => {
                console.log("nije uspjelo");

                //that.$toast.error('Nisu učitani podaci.');
            };

            var promise = ProjekatResource().vratiZahtjevKategorijeZaDioProjekta({
                dioProjektaId: id
            });

            promise.then(success, error);
        },
        ucitajKategorijeKorisnika() {

            //this.kategorije = [];
            //this.korisnikModel.kategorije = null;
            var success = (response) => {
                var that = this;
                //that.korisnikModel.kategorije = response.body;
                that.kategorije = response.body;
                that.korisnikModel.kategorije = that.kategorije;
                that.ucitaneKategorijeKorisnika = true;

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

        removeProjekat(item) {
            this.ulogaProjekti.splice(this.ulogaProjekti.indexOf(item), 1);
            this.ulogaProjekti = [...this.ulogaProjekti];
        },
        removeKategorija(item) {
            this.korisnikModel.kategorije.splice(this.korisnikModel.kategorije.indexOf(item), 1);
            this.korisnikModel.kategorije = [...this.korisnikModel.kategorije];
        },
    }

};
</script>
