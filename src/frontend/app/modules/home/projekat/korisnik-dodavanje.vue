<template>
<page v-if="ucitaniProjekti">
    <v-dialog v-model="dialogDodajKorisnikaNaProjekat" max-width="600px">
        <v-card-text>
            <v-form ref="form">

                <!--   <v-combobox v-model="model.uloge" :items="model.ul.items" label="Uloge" chips clearable multiple item-text="uloga" item-value="id">
                    <template v-slot:selection="data">
                        <v-chip :selected="data.selected" close @input="remove(data.item)">
                            <strong>{{ data.item.uloga }}</strong>&nbsp;
                        </v-chip>
                    </template>
                </v-combobox>  

                <v-combobox v-model="model.projekti" :items="projekti.items" label="Projekti" chips clearable multiple item-text="naziv" item-value="id">
                    <template v-slot:selection="data">
                        <v-chip :selected="data.selected" close @input="removeProjekat(data.item)">
                            <strong>{{ data.item.naziv }}</strong>&nbsp;
                        </v-chip>
                    </template>
                </v-combobox>-->

                <v-flex>
                    <v-select required class="required" @input="ucitajProjekteZaKorisnikUlogu" :items="korisnikModel.uloge" item-text="vrstaUloge.uloga" item-value="vrstaUloge.id" v-model="odabranaUloga" bottom></v-select>
                </v-flex>

                <v-combobox v-model="ulogaProjekti" :items="projekti.items" label="Projekti za odabranu ulogu" chips clearable multiple item-text="naziv" item-value="id">
                    <template v-slot:selection="data">
                        <v-chip :selected="data.selected" close @input="removeProjekat(data.item)">
                            <strong>{{ data.item.naziv }}</strong>&nbsp;
                        </v-chip>
                    </template>
                </v-combobox>

                <div class="text-xl-right my-3">
                    <odustani-btn @odustani="Odustani" />
                    <v-btn color="primary" @click="onSubmit">Sačuvaj</v-btn>
                </div>
            </v-form>

        </v-card-text>
    </v-dialog>
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
    name: "DodavanjeKorisnikaProjekat",

    components: {
        Upit
    },
    props: ['ul', 'korisnickoIme'],
    mixins: [HelpTipDialogMixin],
    created() {
        console.log(this.korisnickoIme);
        this.odabranaUloga = this.ul[0].vrstaUloge.id;
        this.korisnikModel.uloge = this.ul;
        console.log(this.odabranaUloga);
        this.ucitajProjekteZaKorisnikUlogu(this.odabranaUloga);
        this.ucitajSveProjekte();
        console.log("tessssssst");
        console.log(this.ul);

    },
    data() {
        return {
            activeHelpTip: false,
            proslijedi: false,
            activeUpozorenje: false,
            next: {},
            resolved: null,
            valid: false,
            identity: Identity,
            boje: ["indigo", "blue", "cyan", "teal", "blue-grey", "green"],

            model: {
                korisnickoIme: null,
                email: null,
                punoIme: null,
                lozinka: null,
                projekti: []
            },
            korisnikModel: {},
            odabranaUloga: {},
            ucitaniProjekti: false,
            projektiKorisnika: [],
            ulogaProjekti: [],

            inputs: {
                sve: true
            },
            dialogDodajKorisnikaNaProjekat: true,
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
        ucitajSveProjekte() {
            var success = (response) => {

                var that = this;
                that.projekti = response.body;
                this.ucitaniProjekti = true;
                console.log(that.projekti);
            };
            var error = () => {
                console.log("nije uspjelo");

                //that.$toast.error('Nisu učitani podaci.');
            };

            var promise = ProjekatResource().vratiSveProjekte(this.inputs);
            promise.then(success, error);

        },

        ucitajProjekteZaKorisnikUlogu(ulogaId) {
            var that = this;

            var success = response => {
                that.ulogaProjekti = response.body;
                console.log("uloga projekti");
                console.log(that.ulogaProjekti);
                that.ucitajProjekte();

            };

            var error = (poruka) => {
                that.$toast.error(poruka);
            };

            var promise = ProjekatResource().vratiProjekteZaKorisnikUlogu({
                korisnickoIme: this.korisnickoIme,
                ulogaId: ulogaId
            });

            promise.then(success, error);
        },

        ucitajProjekte() {
            var that = this;

            var success = response => {
                that.projekti = response.body;
            };

            var error = (poruka) => {
                that.$toast.error(poruka);
            };

            var promise = ProjekatResource().vratiSveProjekte(that.inputs);

            promise.then(success, error);
        },
        remove(item) {
            this.model.uloge.splice(this.model.uloge.indexOf(item), 1);
            this.model.uloge = [...this.model.uloge];
        },

        removeProjekat(item) {
            this.model.projekti.splice(this.model.projekti.indexOf(item), 1);
            this.model.projekti = [...this.model.projekti];
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

        ucitajUloge(ul) {
            console.log("tessssst");
            console.log(this.ul);
            this.model.uloge = ul;
            console.log(this.model.uloge);

            //this.korisnikId = id;

            /* this.uloge = [{
                 text: "Administrator",
                 value: 1
             }];*/
        },
        onSubmit() {

            /*this.valid = this.$refs.form.validate();
            this.focusInvalidInput(this.$refs.form);*/

            console.log(this.odabranaUloga);
            console.log(this.ulogaProjekti);
            console.log(this.korisnickoIme);
            
            var model = {
                ulogaId: this.odabranaUloga,
                projekti: []
            };

            for (let index = 0; index < this.ulogaProjekti.length; index++) {
                model.projekti.push(this.ulogaProjekti[index].id);
            }
            console.log(model);
            var that = this;
            var success = model => {
                this.proslijedi = true;
                that.$router.push({
                    name: "home.korisnik.edit",
                    params: {
                        korisnickoIme: model.body.korisnickoIme
                    }
                });
            };
            var error = poruka => {
                if (poruka.body.Email != undefined)
                    this.porukeGreška.push(poruka.body.Email[0]);
                console.log(this.porukeGreška[0]);
                that.$toast.error(poruka.body);
            };

            var promise = KorisnikResource().azurirajProjekteKorisnika({
                korisnickoIme: that.korisnickoIme
            }, model);
            promise.then(success, error);

        },
    },
    resolve: {
        /* dozvoljeneUloge() {
             return PravoUpravljanjaKorisnikomResource().get({
                 ulogaId: this.identity.trenutnaUlogaId()
             });
         },*/
        /* projekti() {
             return ProjekatResource().vratiSveProjekte(this.inputs);

         },*/
        model() {

            return KorisnikResource().get(
                this.$route.params);
        }
    }
};
</script>
