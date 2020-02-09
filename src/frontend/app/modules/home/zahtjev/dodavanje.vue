<template>
<page>

    <template>
        <v-layout row justify-center>
            <v-dialog v-model="dialogDodajZahtjev" :projekat="ucitajPostavkeZahtjevaProjekta" persistent max-width="600px">

                <v-card>
                    <v-card-title>
                        <span class="headline">Dodavanje novog zahtjeva</span>
                    </v-card-title>
                    <v-card-text>
                        <v-form v-model="valid" ref="form">

                            <v-container grid-list-md>
                                <v-layout wrap>
                                    <v-flex xs4 sm4 md4>
                                        <v-select required class="required" :items="projekti.items" @input="ucitajKonfiguracijuProjekta" item-text="naziv" item-value="id" label="Projekat" v-model="zahtjevModel.projekat" bottom></v-select>
                                    </v-flex>
                                    <br>
                                    <v-flex xs12 sm12 md12>
                                        <v-text-field label="Naslov" :counter="128" :rules="rules.nazivZahtjeva" required v-model="zahtjevModel.naziv"></v-text-field>
                                    </v-flex>
                                    <v-flex xs12 sm12 md12>
                                        <v-text-field multi-line label="Opis" :rules="rules.opisZahtjeva" required v-model="zahtjevModel.opis"></v-text-field>
                                    </v-flex>

                                    <v-flex v-if="dijeloviProjekta.length!=0" xs12 sm6 md6 :style="{display: dijeloviProjekta.length>1 ? 'visible' : 'none'}">
                                        <v-select required class="required" @input="ucitajKategorijeZahtjeva" :items="dijeloviProjekta" item-text="naziv" item-value="id" label="Podprojekat" v-model="zahtjevModel.dioProjekta" bottom></v-select>
                                    </v-flex>
                                    <v-flex xs12 sm6 md6>
                                        <v-select required class="required" :items="kategorijeZahtjeva" item-text="naziv" item-value="id" label="Kategorija zahtjeva" v-model="zahtjevModel.kategorija" bottom></v-select>
                                    </v-flex>

                                    <!-- <v-btn style="width: 100%" color="tertiary" v-if="!prikazDodatnihOpcija" @click="prikaziDodatneOpcije">Prikaži dodatne opcije</v-btn>
                                    <v-btn style="width: 100%" color="tertiary" v-else @click="sakrijDodatneOpcije">Sakrij dodatne opcije</v-btn>

                                    <v-layout wrap v-show="prikazDodatnihOpcija">

                                    </v-layout> -->
                                </v-layout>

                                <v-expansion-panel expand :value="[false]">
                                    <v-expansion-panel-content>
                                        <div slot="header">Prikaz dodatnih opcija</div>
                                        <v-container grid-list-md>
                                            <v-layout wrap>
                                                <v-flex xs12 sm6 md6>
                                                    <v-select required class="required" :items="tipoviZahtjeva" item-text="naziv" item-value="id" label="Tip zahtjeva" v-model="zahtjevModel.tip" bottom></v-select>
                                                </v-flex>
                                                <v-flex xs12 sm6 md6>
                                                    <v-select required class="required" :items="prioritetiZahtjeva" item-text="naziv" item-value="id" label="Prioritet zahtjeva" v-model="zahtjevModel.prioritet" bottom></v-select>
                                                </v-flex>

                                                <v-flex xs12 sm12 md12>
                                                    <vue-dropzone ref="dropzone" id="upload-file-zahtjev" :options="dropzoneOptions" @vdropzone-removed-file="removeAllFiles" @vdropzone-success="(file, response) => snimiObjekt(response)" @vdropzone-sending="(file, xhr, formData) => addParam(formData, zahtjevModel, vrstaDokumenta)">
                                                        <div class="dropzone-custom-content">
                                                            <div class="text-xs-center">
                                                                <i aria-hidden="true" class="material-icons icon">file_upload</i>
                                                                <p> Upload </p>
                                                            </div>
                                                        </div>
                                                    </vue-dropzone>
                                                </v-flex>
                                            </v-layout>
                                        </v-container>
                                    </v-expansion-panel-content>
                                </v-expansion-panel>

                            </v-container>
                        </v-form>

                    </v-card-text>

                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn color="blue darken-1" flat @click="zatvoriDialog()">Odustani</v-btn>
                        <v-btn color="blue darken-1" flat @click="dodajNoviZahtjev()">Dodaj</v-btn>
                    </v-card-actions>

                </v-card>

            </v-dialog>
        </v-layout>

    </template>

</page>
</template>

<script>
import {
    ProjekatResource,
    ZahtjevResource
} from 'api/resources';
import Identity from "auth/identity";
import Upit from "../components/upit";
import HelpTipDialogMixin from "helpers/help-tip-dialog-mixin";
import UploadMixin from "helpers/upload-mixin";

export default {
    name: "DodavanjeZahtjeva",

    components: {
        Upit
    },
    mixins: [HelpTipDialogMixin, UploadMixin],
    props: {
        poruka: {
            type: String,
            default: 'Dodaj fajl u prilog zahtjeva'
        }
        /*,

                vrstaDokumenta: {
                    type: String,
                    required: true
                }*/
    },
    data() {
        return {
            acceptedFiles: {
                type: String,
                default: ".jpg,.png,.jpeg"
            },

            /*addParam(formData, item, vrstaDokumenta) {
                if (this.uploads == null) {
                    this.uploads = [];
                }
                console.log("form data",formData, "item",item, "vrsta dokumenta", vrstaDokumenta);
                var upload = {
                    item: item,
                    id: Math.floor((Math.random() * 100000) + 1)
                };
                this.uploads.push(upload);
                console.log("uploads",this.uploads);
            },*/
            dropzoneOptionsOsnovno: {
                maxFiles: 5,
                queueLimit: 5,
                maxFileSize: 30,
                acceptedFiles: this.acceptedFiles,
                addRemoveLinks: true,
                url: "test",
                dictDefaultMessage: '<i aria-hidden="true" class="material-icons icon">file_upload</i><br />' + this.poruka
            },
            activeHelpTip: false,
            proslijedi: false,
            activeUpozorenje: false,
            next: {},
            resolved: null,
            valid: false,
            identity: Identity,
            projekatId: null,
            prikazDijelovaProjekta: true,
            prikazDodatnihOpcija: false,

            zahtjevModel: {
                naziv: null,
                opis: null,
                kategorija: null,
                tip: null,
                projekat: null,
                dokument: null
            },
            dijeloviProjekta: [],
            tipoviZahtjeva: [],
            prioritetiZahtjeva: [],
            projekti: [],
            dialogDodajZahtjev: true,
            inputs: {
                sve: true
            },
            rules: {
                nazivZahtjeva: [
                    v => !!v || "Morate unijeti naziv zahtjeva",
                    v =>
                    (v && v.length <= 128) ||
                    "Naziv ne može biti veći od 128 karaktera"
                ],
                opisZahtjeva: [
                    v => !!v || "Morate unijeti opis zahtjeva"
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
        this.ucitajSveProjekte();
    },
    methods: {

        snimiObjekt(model) {
            this.zahtjevModel.dokument = model.result;

            this.result = model.result;
            this.uspjesanUploadSlike = true;
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

        colorClass(color) {
            return color + " lighten-2";
        },
        ucitajKonfiguracijuProjekta(id) {
            //this.dijeloviProjekta=[];
            this.ucitajDijeloveProjekta(id);
            this.ucitajTipoveZahtjeva(id);
            this.ucitajPrioriteteZahtjeva(id);

        },
        ucitajTipoveZahtjeva(id) {
            var success = (response) => {

                var that = this;
                that.tipoviZahtjeva = response.body;
                that.odrediDefaultniTip();

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
                that.odrediDefaultniPrioritet();

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

        ucitajSveProjekte() {
            var success = (response) => {

                var that = this;
                that.projekti = response.body;

                if (that.projekti.length != 0)
                    that.zahtjevModel.projekat = that.projekti.items[0].id;

                that.ucitajKonfiguracijuProjekta(that.zahtjevModel.projekat);
            };
            var error = () => {
                console.log("nije uspjelo");

                //that.$toast.error('Nisu učitani podaci.');
            };

            var promise = ProjekatResource().vratiSveProjekte(this.inputs);
            promise.then(success, error);

        },
        zatvoriDialog() {
            this.dialogDodajZahtjev = false;
            this.$emit("dialogDodajZahtjev", this.dialogDodajZahtjev);

        },
        ucitajDijeloveProjekta(id) {
            this.dijeloviProjekta = [];
            var success = (response) => {

                var that = this;
                that.dijeloviProjekta = response.body;

                for (var i = 0; i < that.dijeloviProjekta.length; i++) {
                    {
                        that.zahtjevModel.dioProjekta = that.dijeloviProjekta[i].id;
                        break;
                    }
                }
                that.ucitajKategorijeZahtjeva(that.zahtjevModel.dioProjekta);
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
        ucitajKategorijeZahtjeva(id) {
            this.kategorijeZahtjeva = [];
            this.zahtjevModel.kategorija = null;
            var success = (response) => {

                var that = this;
                that.kategorijeZahtjeva = response.body;
                for (var i = 0; i < that.kategorijeZahtjeva.length; i++) {
                    {
                        that.zahtjevModel.kategorija = that.kategorijeZahtjeva[i].id;
                        break;
                    }
                }
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

        dodajNoviZahtjev() {

            this.valid = this.$refs.form.validate();
            this.focusInvalidInput(this.$refs.form);

            if (!this.valid) return;
            else {

                var that = this;
                var success = model => {
                    this.proslijedi = true;
                    that.$router.go({
                        name: "home.zahtjev.pregled",
                        params: {
                            zahtjevId: model.body.id
                        }
                    });
                    that.$toast.success("Uspješno kreiran novi zahtjev.");

                };
                var error = poruka => {
                    that.$toast.error(poruka.body);
                };

                var zahtjev = {};

                zahtjev.projekatId = that.zahtjevModel.projekat;
                zahtjev.naziv = that.zahtjevModel.naziv;
                zahtjev.opis = that.zahtjevModel.opis;
                zahtjev.zahtjevKategorijaId = that.zahtjevModel.kategorija;
                zahtjev.zahtjevTipId = that.zahtjevModel.tip;
                zahtjev.zahtjevPrioritetId = that.zahtjevModel.prioritet;
                zahtjev.dioProjektaId = that.zahtjevModel.dioProjekta;
                zahtjev.dokumentId = that.zahtjevModel.dokument;

                var promise = ZahtjevResource().dodajNoviZahtjev({
                    projekatId: zahtjev.projekatId
                }, zahtjev);
                promise.then(success, error);
            }
        },
        ucitajPostavkeZahtjevaProjekta(id) {
            this.projekatId = id;

        },
        removeAllFiles() {
            this.$refs.dropzone.removeAllFiles();
            this.zahtjevModel.dokument = null;
        },
        prikaziDodatneOpcije() {
            this.prikazDodatnihOpcija = true;
        },
        sakrijDodatneOpcije() {
            this.prikazDodatnihOpcija = false;
        }
    }
};
</script>
