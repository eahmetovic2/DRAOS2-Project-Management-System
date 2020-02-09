<template>
<page :resolved="resolved">
    <material-card title="Dodaj korisnika" color="primary" v-if="resolved">
        <v-card-text>
            <v-form v-model="valid" ref="form">
                <v-text-field :rules="rules.korisnickoIme" autocomplete="new-username" :counter="64" label="Korisničko ime" v-model="model.korisnickoIme" required class="required"></v-text-field>
                <v-text-field :rules="rules.punoIme" :counter="128" label="Ime i prezime" v-model="model.punoIme" required class="required"></v-text-field>
                <v-text-field :rules="rules.email" :counter="256" label="E-mail" v-model="model.email" type="email" required class="required"></v-text-field>
                <v-text-field :rules="rules.lozinka" autocomplete="new-password" :counter="128" label="Lozinka" v-model="model.lozinka" type="password" required class="required"></v-text-field>

                <!-- <v-combobox :rules="rules.uloga"  v-model="model.uloge" :items="resolved.dozvoljeneUloge.items" label="Uloge" chips clearable multiple item-text="uloga" item-value="id">
                    <template v-slot:selection="data">
                        <v-chip :selected="data.selected" close @input="remove(data.item)">
                            <strong>{{ data.item.uloga }}</strong>&nbsp;
                        </v-chip>
                    </template>
                </v-combobox> -->
                <v-select required :rules="rules.uloga" class="required" :items="resolved.dozvoljeneUloge.items" @input="izabranaUloga" item-text="uloga" item-value="id" label="Uloga" v-model="model.uloge" bottom></v-select>

                <v-combobox v-if="!izabranaUlogaSupport && !izabranaUlogaAdministrator" v-model="model.projekti" :items="resolved.projekti.items" label="Projekti" chips clearable multiple item-text="naziv" item-value="id">
                    <template v-slot:selection="data">
                        <v-chip :selected="data.selected" close @input="removeProjekat(data.item)">
                            <strong>{{ data.item.naziv }}</strong>&nbsp;
                        </v-chip>
                    </template>
                </v-combobox>

                <v-form v-if="izabranaUlogaSupport" >
                    <v-flex xs4 sm4 md4>
                        <v-select required class="required" :items="resolved.projekti.items" @input="ucitajDijeloveProjekta" item-text="naziv" item-value="id" label="Projekat" v-model="zahtjevModel.projekat" bottom></v-select>
                    </v-flex>

                    <v-flex v-if="dijeloviProjekta.length!=0" xs4 sm4 md4 :style="{display: dijeloviProjekta.length>1 ? 'visible' : 'none'}">
                        <v-select required class="required" @input="ucitajKategorijeZahtjeva" :items="dijeloviProjekta" item-text="naziv" item-value="id" label="Podprojekat" v-model="zahtjevModel.dioProjekta" bottom></v-select>
                    </v-flex>

                    <v-combobox v-model="model.kategorije" :items="kategorijeZahtjeva" label="Kategorije" chips clearable multiple item-text="naziv" item-value="id">
                        <template v-slot:selection="data">
                            <v-chip :selected="data.selected" close @input="removeKategorija(data.item)">
                                <strong>{{ data.item.naziv }}</strong>&nbsp;
                            </v-chip>
                        </template>
                    </v-combobox>
                </v-form>

                <div class="text-xl-right my-3">
                    <odustani-btn @odustani="Odustani" />
                    <v-btn color="primary" @click="onSubmit">Dodaj</v-btn>
                </div>
            </v-form>

        </v-card-text>
    </material-card>
    <Upit v-if="activeUpozorenje" @odgovor="Odgovor"></Upit>
    <help-tip-dialog v-if="activeHelpTip" :title="VratiTitlePoId('korisnik-dodavanje')" :content="VratiContentPoId('korisnik-dodavanje')" @zatvori="ZatvoriHelpDialog"> </help-tip-dialog>
</page>
</template>

<script>
import {
    KorisnikResource,
    PravoUpravljanjaKorisnikomResource,
    ProjekatResource
} from "api/resources";
import Identity from "auth/identity";
import Upit from "../components/upit";
import HelpTipDialogMixin from "helpers/help-tip-dialog-mixin";
export default {
    name: "DodavanjeKorisnika",

    components: {
        Upit
    },
    mixins: [HelpTipDialogMixin],
    data() {
        return {
            chips: ['Programming', 'Playing video games', 'Watching movies', 'Sleeping'],
            items: ['Streaming', 'Eating'],
            activeHelpTip: false,
            proslijedi: false,
            activeUpozorenje: false,
            next: {},
            resolved: null,
            valid: false,
            identity: Identity,
            rules: {
                uloga: [v => (v && v.length != 0) || "Uloga je obavezna"],
                obavezno: [v => !!v || "Polje je obavezno"],
                korisnickoIme: [
                    v => !!v || "Morate da unesete korisničko ime",
                    v =>
                    (v && v.length <= 64) ||
                    "Korisničko ime mora biti manje od 64 karaktera"
                ],
                punoIme: [
                    v => !!v || "Morate da unesete ime i prezime",
                    v =>
                    (v && v.length <= 128) ||
                    "Ime i prezime mora biti manje od 128 karaktera"
                ],
                lozinka: [
                    v => !!v || "Morate da unesete lozinku",
                    v =>
                    (v && v.length <= 128) ||
                    "Lozinka mora biti manja od 128 karaktera",
                    v => /^.{6,}$/.test(v) || "Lozinka mora imati najmanje 6 karaktera"
                ],
                email: [
                    v => !!v || "Morate da unesete e-mail",
                    v =>
                    (v && v.length <= 256) || "Email mora biti kraći od 256 karaktera",
                    v =>
                    /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(v) ||
                    "E-mail mora biti ispravan"
                ]
            },
            model: {
                korisnickoIme: null,
                email: null,
                punoIme: null,
                lozinka: null,
                uloge: [],
                projekti: [],
                kategorije: []
            },
            projektiKorisnika: [],
            dijeloviProjekta: [],
            kategorijeZahtjeva: [],
            zahtjevModel: {},
            izabranaUlogaSupport: false,
            izabranaUlogaAdministrator: false,

            inputs: {
                sve: true
            },
            porukeGreška: []

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
        remove(item) {
            this.model.uloge.splice(this.model.uloge.indexOf(item), 1);
            this.model.uloge = [...this.model.uloge];
        },

        removeProjekat(item) {
            this.model.projekti.splice(this.model.projekti.indexOf(item), 1);
            this.model.projekti = [...this.model.projekti];
        },
        removeKategorija(item) {
            this.model.kategorije.splice(this.model.kategorije.indexOf(item), 1);
            this.model.kategorije = [...this.model.kategorije];
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
        onSubmit() {
            this.valid = this.$refs.form.validate();
            this.focusInvalidInput(this.$refs.form);
            var that = this;
            if (this.valid) {
                var success = model => {
                    that.$toast.success('Uspješno dodan korisnik.');

                    this.proslijedi = true;
                    that.$router.push({
                        name: "home.korisnik.pregled",
                        params: {
                            korisnickoIme: model.body.korisnickoIme
                        }
                    });
                };
                var error = poruka => {
                    /* if (poruka.body.Email != undefined)
                         this.porukeGreška.push(poruka.body.Email[0]);
                     console.log(this.porukeGreška[0]);*/
                    that.$toast.error(poruka.body);
                };
                var ulogeArray = [];
                ulogeArray.push({
                    vrstaUlogeId: this.model.uloge
                });

                var model = {
                    korisnickoIme: this.model.korisnickoIme,
                    email: this.model.email,
                    lozinka: this.model.lozinka,
                    punoIme: this.model.punoIme,
                    uloge: ulogeArray,
                    projekti: this.model.projekti.map((p) => {
                        return {
                            ProjekatId: p.id
                        };
                    }),
                    kategorije: this.model.kategorije.map((k) => {
                        return {
                            zahtjevKategorijaId: k.id
                        };
                    })
                };

                var promise = KorisnikResource().save(that.$route.params, model);
                promise.then(success, error);
            }

        },
        regexUsername() {
            var regexp = /^\w+-?\w+(?!-)$/;
            return regexp.test(this.model);
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

        izabranaUloga() {
            this.izabranaUlogaSupport = false;
            this.izabranaUlogaAdministrator = false;
            for (var i = 0; i < this.resolved.dozvoljeneUloge.items.length; i++) {

                if (this.model.uloge == this.resolved.dozvoljeneUloge.items[i].id &&
                    this.resolved.dozvoljeneUloge.items[i].uloga == "Support") {
                    this.izabranaUlogaSupport = true;
                    this.izabranaUlogaAdministrator = false;
                    break;
                }
                if (this.model.uloge == this.resolved.dozvoljeneUloge.items[i].id &&
                    this.resolved.dozvoljeneUloge.items[i].uloga == "Administrator") {
                    this.izabranaUlogaAdministrator = true;
                    this.izabranaUlogaSupport = false;
                    break;
                }
            }
        }
    },
    resolve: {
        dozvoljeneUloge() {
            return PravoUpravljanjaKorisnikomResource().get({
                ulogaId: this.identity.trenutnaUlogaId()
            });
        },
        projekti() {
            return ProjekatResource().vratiSveProjekte(this.inputs);

        }
    }
};
</script>
