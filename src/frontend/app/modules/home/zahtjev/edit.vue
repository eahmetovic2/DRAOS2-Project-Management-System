<template>
<page>
    <template v-if="ucitanZahtjev">
        <material-card title="Izmijeni zahtjev" color="primary">

            <v-card-text>
                <v-form v-model="valid" ref="form">
                    <v-text-field :counter="128" label="Naziv" :rules="rules.nazivZahtjeva" v-model="zahtjevModel.naziv" required class="required"></v-text-field>
                    <v-textarea multi-line label="Opis" :rules="rules.opisZahtjeva" v-model="zahtjevModel.opis" required class="required"></v-textarea>


                    <v-flex xs12 sm4 md4>
                        <span>Početak izrade: </span>
                        <m-datepicker v-model="zahtjevModel.pocetakIzrade"></m-datepicker>
                    </v-flex>


                    <v-flex xs12 sm4 md4>
                        <span>Zahtjev kreirao: </span>
                        <span>{{ zahtjevModel.createdBy }}</span>
                    </v-flex>

                    <v-flex xs12 sm4 md4>
                        <span>Datum kreiranja: </span>
                        <span> {{ ' '+zahtjevModel.datumKreiranja.substring(8,10) + '. ' + zahtjevModel.datumKreiranja.substring(5,7)+ '. '+zahtjevModel.datumKreiranja.substring(0,4) + '. '+zahtjevModel.datumKreiranja.substring(11,19) }}</span>
                    </v-flex>

                    <v-flex xs12 sm4 md4>
                        <span>Datum izmjene: </span>
                        <span> {{ ' '+zahtjevModel.datumIzmjene.substring(8,10) + '. ' + zahtjevModel.datumIzmjene.substring(5,7)+ '. '+zahtjevModel.datumIzmjene.substring(0,4) + '. '+zahtjevModel.datumIzmjene.substring(11,19) }}</span>
                    </v-flex>
                    <span></span>

                    <v-flex xs12 sm4 md4>
                        <v-select required class="required" @input="ucitajKategorijeZahtjeva" :items="dijeloviProjekta" item-text="naziv" item-value="id" label="Dio projekta" v-model="zahtjevModel.dioProjekta" bottom></v-select>
                    </v-flex>
                    <v-flex xs12 sm4 md4>
                        <v-select required class="required" @input="ucitajSupportKorisnike" :items="kategorijeZahtjeva" item-text="naziv" item-value="id" label="Kategorija zahtjeva" v-model="zahtjevModel.kategorija" bottom></v-select>
                    </v-flex>

                    <span></span>

                    <v-flex xs12 sm4 md4>
                        <v-select required class="required" :items="tipoviZahtjeva" item-text="naziv" item-value="id" label="Tip zahtjeva" v-model="zahtjevModel.tip" bottom></v-select>
                    </v-flex>

                    <v-flex xs12 sm4 md4>
                        <v-select required class="required" :items="statusiZahtjeva" item-text="naziv" item-value="id" label="Status zahtjeva" v-model="zahtjevModel.status" bottom></v-select>
                    </v-flex>

                    <v-flex xs12 sm4 md4>
                        <v-select required class="required" :items="prioritetiZahtjeva" item-text="naziv" item-value="id" label="Prioritet zahtjeva" v-model="zahtjevModel.prioritet" bottom></v-select>
                    </v-flex>

                    <v-flex v-if="imaPravo('zahtjev_zahtjev_edit_dodijeljeni_korisnik')" xs12 sm4 md4>
                        <v-select required class="required" clearable :items="supportKorisnici" item-text="korisnickoIme" item-value="korisnickoIme" label="Dodijeljeni korisnik" v-model="zahtjevModel.dodijeljeniKorisnikIme" bottom></v-select>
                    </v-flex>

                    <div class="text-xl-right my-3">
                        <v-btn color="tertiary" @click="odustani">Odustani </v-btn>
                        <v-btn color="primary" @click="onSubmit">Snimi</v-btn>
                    </div>
                </v-form>
            </v-card-text>
        </material-card>
    </template>
    <help-tip-dialog v-if="activeHelpTip" :title="helpTipDialogTitle" :content="helpTipDialogContent" @zatvori="ZatvoriHelpDialog"> </help-tip-dialog>
</page>
</template>

<script>
import {
    KorisnikResource,
    ZahtjevResource,
    ProjekatResource
} from 'api/resources';
import Identity from 'auth/identity';
import HelpTipDialogMixin from 'helpers/help-tip-dialog-mixin';
import MDatepicker from '../components/m-datepicker';
export default {
    name: 'EditZahtjev',
    mixins: [HelpTipDialogMixin],
    props: ['zahtjevId'],

    data() {
        return {
            childZahtjevId: Number(this.zahtjevId),
            activeHelpTip: false,
            resolved: null,
            identity: Identity,
            zahtjevModel: null,
            ucitanZahtjev: false,
            tipoviZahtjeva: [],
            prioritetiZahtjeva: [],
            statusiZahtjeva: [],
            kategorijeZahtjeva: [],
            supportKorisnici: [],

            rules: {
                nazivZahtjeva: [
                    v => !!v || "Polje naziv ne može biti prazno",
                    v =>
                    (v && v.length <= 128) ||
                    "Naziv ne može biti veći od 128 karaktera"
                ],
                opisZahtjeva: [
                    v => !!v || "Polje opis ne može biti prazno"
                ]
            }
        };
    },

    created() {
        this.ucitajZahtjev();
    },
    methods: {

        ucitajZahtjev() {
            var that = this;

            if (that.$route.params.zahtjevId != null)
                that.childZahtjevId = Number(that.$route.params.zahtjevId);

            that.loading = true;
            var success = (response) => {
                that.zahtjevModel = response.body;
                that.ucitanZahtjev = true;
                that.ucitajTipoveZahtjeva(that.zahtjevModel.projekatId);
                that.ucitajStatuseZahtjeva(that.zahtjevModel.projekatId);
                that.ucitajPrioriteteZahtjeva(that.zahtjevModel.projekatId);
                that.ucitajDijeloveProjekta(that.zahtjevModel.projekatId);
            };
            var error = (poruka) => {
                this.$toast.error(poruka.body);
            };

            var promise = ZahtjevResource().vratiZahtjev({
                zahtjevId: that.childZahtjevId
            });
            promise.
            then(success, error);
        },
        ucitajTipoveZahtjeva(id) {
            var success = (response) => {

                var that = this;
                that.tipoviZahtjeva = response.body;

                for (var i = 0; i < that.tipoviZahtjeva.length; i++) {

                    if (that.tipoviZahtjeva[i].naziv == that.zahtjevModel.zahtjevTip) {
                        that.zahtjevModel.tip = that.tipoviZahtjeva[i].id;
                        break;
                    }
                }

            };
            var error = () => {
                console.log("nije uspjelo");

                //that.$toast.error('Nisu učitani podaci.');
            };

            var promise = ProjekatResource().vratiTipoveZahtjevaZaProjekat({
                projekatId: id
            });

            promise.then(success, error);
        },

        odrediDefaultniTip() {

            for (var i = 0; i < this.tipoviZahtjeva.length; i++) {

                if (this.tipoviZahtjeva[i].default == true) {
                    this.zahtjevModel.tip = this.tipoviZahtjeva[i].id;
                    break;
                }
            }
        },
        ucitajPrioriteteZahtjeva(id) {
            var success = (response) => {

                var that = this;
                that.prioritetiZahtjeva = response.body;

                for (var i = 0; i < that.prioritetiZahtjeva.length; i++) {

                    if (that.prioritetiZahtjeva[i].naziv == that.zahtjevModel.zahtjevPrioritet) {
                        that.zahtjevModel.prioritet = that.prioritetiZahtjeva[i].id;
                        break;
                    }
                }
            };
            var error = () => {
                console.log("nije uspjelo");

                //that.$toast.error('Nisu učitani podaci.');
            };

            var promise = ProjekatResource().vratiZahtjevPrioriteteZaProjekat({
                projekatId: id
            });

            promise.then(success, error);
        },
        odrediDefaultniPrioritet() {

            for (var i = 0; i < this.prioritetiZahtjeva.length; i++) {

                if (this.prioritetiZahtjeva[i].default == true) {
                    this.zahtjevModel.prioritet = this.prioritetiZahtjeva[i].id;

                    break;
                }
            }
        },

        ucitajStatuseZahtjeva(id) {
            var success = (response) => {

                var that = this;
                that.statusiZahtjeva = response.body;
                for (var i = 0; i < that.statusiZahtjeva.length; i++) {
                    if (that.statusiZahtjeva[i].naziv == that.zahtjevModel.zahtjevStatus.naziv) {
                        that.zahtjevModel.status = that.statusiZahtjeva[i].id;
                        break;
                    }
                }
            };
            var error = () => {
                console.log("nije uspjelo");

                //that.$toast.error('Nisu učitani podaci.');
            };

            var promise = ProjekatResource().vratiStatuseZahtjevaZaProjekat({
                projekatId: id
            });

            promise.then(success, error);
        },

        odrediDefaultniStatus() {

            for (var i = 0; i < this.statusiZahtjeva.length; i++) {

                if (this.statusiZahtjeva[i].default == true) {
                    this.zahtjevModel.status = this.statusiZahtjeva[i].id;
                    break;
                }
            }
        },
        ucitajDijeloveProjekta(id) {

            var success = (response) => {

                var that = this;
                that.dijeloviProjekta = response.body;

                for (var i = 0; i < that.dijeloviProjekta.length; i++) {

                    if (that.dijeloviProjekta[i].naziv == that.zahtjevModel.dioProjekta) {
                        that.zahtjevModel.dioProjekta = that.dijeloviProjekta[i].id;
                        break;
                    }
                }

                var model = that.zahtjevModel;
                that.ucitajPostojecuKategorijuZahtjeva(model);
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
        ucitajPostojecuKategorijuZahtjeva(model) {

            var success = (response) => {

                var that = this;
                that.kategorijeZahtjeva = response.body;

                for (var j = 0; j < that.kategorijeZahtjeva.length; j++) {

                    if (that.kategorijeZahtjeva[j].naziv == model.zahtjevKategorija) {
                        that.zahtjevModel.kategorija = that.kategorijeZahtjeva[j].id;

                        break;
                    }
                }
                //that.zahtjevModel.dodijeljeniKorisnikIme=null;
                that.ucitajSupportKorisnikeZaZahtjevKategoriju(that.zahtjevModel.kategorija);

            };
            var error = () => {
                console.log("nije uspjelo");

                //that.$toast.error('Nisu učitani podaci.');
            };
            var promise = ProjekatResource().vratiZahtjevKategorijeZaDioProjekta({
                dioProjektaId: model.dioProjekta
            });

            promise.then(success, error);
        },
        ucitajKategorijeZahtjeva(id) {
            var success = (response) => {

                var that = this;
                that.kategorijeZahtjeva = response.body;
                for (var i = 0; i < that.kategorijeZahtjeva.length; i++) {
                    {
                        that.zahtjevModel.kategorija = that.kategorijeZahtjeva[i].id;
                        break;
                    }
                }
                that.zahtjevModel.dodijeljeniKorisnikIme = null;
                that.ucitajSupportKorisnikeZaZahtjevKategoriju(that.zahtjevModel.kategorija);

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
        odustani() { 
            //this.$emit('odustani', true);
            //this.$emit("dialogEditZahtjeva", false);
            this.$emit("dialogEditZahtjeva", false);

        },
        onSubmit() {

            this.$refs.form.validate();
            this.focusInvalidInput(this.$refs.form);

            if (this.valid) {
                var that = this;

                var success = model => {
                    this.$toast.success(
                        "Uspješno ažuriran zahtjev."
                    );
                    /*that.$router.push({
                        name: 'home.zahtjev.pregled',
                        params: {
                            zahtjevId: model.body.id
                        }
                    });*/

                    this.$emit("dialogEditZahtjeva", true);

                };
                var error = poruka => {
                    that.$toast.error(poruka.body);
                };

                var zahtjev = {};
                zahtjev.naziv = that.zahtjevModel.naziv;
                zahtjev.opis = that.zahtjevModel.opis;
                zahtjev.zahtjevStatusId = that.zahtjevModel.status;
                zahtjev.zahtjevKategorijaId = that.zahtjevModel.kategorija;
                zahtjev.zahtjevTipId = that.zahtjevModel.tip;
                zahtjev.zahtjevPrioritetId = that.zahtjevModel.prioritet;
                zahtjev.pocetakIzrade = that.zahtjevModel.pocetakIzrade;

                zahtjev.dodijeljeniKorisnikIme = that.zahtjevModel.dodijeljeniKorisnikIme;
                var promise = ZahtjevResource().azurirajZahtjev({
                    zahtjevId: that.childZahtjevId
                }, zahtjev);

                promise.then(success, error);
            }
        },
        ucitajSupportKorisnikeZaZahtjevKategoriju(id) {
            var that = this;
            //that.supportKorisnici=null;
            //that.zahtjevModel.dodijeljeniKorisnikIme=null;
            var success = (response) => {
                that.supportKorisnici = response.body;
            };
            var error = (poruka) => {
                this.$toast.error(poruka.body);
            };

            var promise = KorisnikResource().vratiSupportKorisnikeZaZahtjevKategoriju({
                zahtjevKategorijaId: id
            });
            promise.
            then(success, error);
        },
        ucitajSupportKorisnike(id) {
            this.zahtjevModel.dodijeljeniKorisnikIme = null;
            this.ucitajSupportKorisnikeZaZahtjevKategoriju(id);
        }
    },
    resolve: {},
    components: {
        MDatepicker
    }
};
</script>
