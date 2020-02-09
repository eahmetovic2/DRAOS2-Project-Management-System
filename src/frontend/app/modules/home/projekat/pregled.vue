<template>
<page>

    <template v-if="imaPravo('projekat_projekat_pregled')">
        <material-card class="card-tabs" color="primary">
            <v-flex slot="header">
                <v-tabs v-model="tabs" color="transparent" slider-color="white">
                    <span class="subheading font-weight-light mr-3" style="align-self: center">Projekat</span>
                    <v-tab class="mr-3">
                        <v-icon class="mr-2">subject</v-icon>
                        Pregled
                    </v-tab>
                    <v-tab class="mr-3">
                        <v-icon class="mr-2">people</v-icon>
                        Korisnici
                    </v-tab>
                    <!-- <v-tab class="mr-3">
                        <v-icon class="mr-2">description</v-icon>
                        Zahtjevi
                    </v-tab> -->
                    <v-tab>
                        <v-icon class="mr-2">settings_applications</v-icon>
                        Konfiguracija
                    </v-tab>
                </v-tabs>
            </v-flex>

            <v-tabs-items v-model="tabs" class="tabovi">

                <v-tab-item>

                    <template v-if="ucitanProjekat && editProjekta==false">

                        <v-flex class="text-xs-right toolbar-btn">
                            <v-btn @click="omoguciEditovanjeProjekta">
                                <v-icon class="mr-2">edit</v-icon>
                                Edit
                            </v-btn>
                        </v-flex>

                        <v-flex slot="header">
                            <v-layout row justify-space-between>
                                <v-flex>
                                    <span class="card-naslov">Pregled projekta </span>
                                </v-flex>
                                <!-- <v-flex class="text-xs-right toolbar-btn">
                        <v-menu open-on-hover offset-y>
                            <v-icon slot="activator">more_vert</v-icon>
                            <v-list>
                                <v-list-tile :to="{ name: 'home.projekat.edit', params: {projekatId: resolved.model.id }}">
                                    <v-list-tile-title>Izmijeni projekat</v-list-tile-title>
                                </v-list-tile>  

                            </v-list>
                        </v-menu>
                    </v-flex>  -->
                            </v-layout>
                        </v-flex>
                        <v-card-text>

                            <table class="v-table">
                                <tbody>
                                    <tr>
                                        <td>Naziv projekta: </td>
                                        <td>{{ projekatModel.naziv }}</td>

                                        <!-- <v-flex shrink>
                                            <v-layout row>
                                                <v-btn class="v-btn--simple" color="success" icon>
                                                    <v-icon color="primary">edit</v-icon>
                                                </v-btn>
                                                <v-btn class="v-btn--simple" color="danger" icon>
                                                    <v-icon color="error">close</v-icon>
                                                </v-btn>
                                            </v-layout>
                                        </v-flex>-->
                                    </tr>

                                    <tr>
                                        <td>Opis: </td>
                                        <td>{{ projekatModel.opis }}</td>
                                    </tr>

                                    <!-- <tr>
                            <td>Datum nastanka</td>
                            <td>{{ resolved.model.datumKreiranja.substring(8,10) + '. ' + resolved.model.datumKreiranja.substring(5,7)+ '. '+resolved.model.datumKreiranja.substring(0,4) + '.' }}</td>
                        </tr>-->

                                </tbody>
                            </table>
                        </v-card-text>

                    </template>
                    <edit-projekta v-if="editProjekta" v-on:onemogucenEdit="onemoguciEditProjekta"></edit-projekta>

                </v-tab-item>
                <v-tab-item>
                    <lista-korisnika :projekat-id="$route.params.projekatId" :omogucen-header="false"></lista-korisnika>

                </v-tab-item>
                <!-- <v-tab-item>
                    <lista-zahtjeva-projekat :omoguceno-dodavanje-i-filtriranje="false" :omogucen-header="false">
                    </lista-zahtjeva-projekat>
                </v-tab-item> -->

                <v-tab-item>
                    <v-select required class="required" :items="opcijeKonfiguracija" item-text="naziv" item-value="id" label="Opcije" v-model="odabranaOpcijaKonfiguracije" bottom></v-select>

                    <edit-konfiguracije-projekta v-if="odabranaOpcijaKonfiguracije==0">
                    </edit-konfiguracije-projekta>
                    <edit-zahtjev-prioriteta v-if="odabranaOpcijaKonfiguracije==1">
                    </edit-zahtjev-prioriteta>
                    <edit-zahtjev-statusa v-if="odabranaOpcijaKonfiguracije==2">
                    </edit-zahtjev-statusa>
                    <edit-zahtjev-tipova v-if="odabranaOpcijaKonfiguracije==3">
                    </edit-zahtjev-tipova>
                    <list-dio-projekta v-if="odabranaOpcijaKonfiguracije==4"></list-dio-projekta>
                </v-tab-item>

            </v-tabs-items>
        </material-card>
    </template>

    <help-tip-dialog v-if="activeHelpTip" :title="helpTipDialogTitle" :content="helpTipDialogContent" @zatvori="ZatvoriHelpDialog"> </help-tip-dialog>
</page>
</template>

<script>
import {
    ProjekatResource
} from 'api/resources';
import Identity from 'auth/identity';
import HelpTipDialogMixin from 'helpers/help-tip-dialog-mixin';
import ListDioProjekta from "./dio-projekta/list.vue";
//import PregledKonfiguracijeProjekta from "./konfiguracija-projekta/pregled.vue";
import EditKonfiguracijeProjekta from "./konfiguracija-projekta/edit.vue";
import EditProjekta from "./edit.vue";
//import PregledZahtjevStatusa from "./zahtjev-status/pregled.vue";
//import PregledZahtjevTipova from "./zahtjev-tip/pregled.vue";
import EditZahtjevPrioriteta from "./zahtjev-prioritet/edit.vue";
import EditZahtjevStatusa from "./zahtjev-status/edit.vue";
import EditZahtjevTipova from "./zahtjev-tip/edit.vue";
//import ListaKorisnikaProjekta from "./list-korisnici.vue";
import ListaZahtjevaProjekat from "../zahtjev/list.vue";
import ListaKorisnika from "../korisnik/list.vue";

export default {
    name: 'PregledProjekat',
    components: {
        EditProjekta,
        ListDioProjekta,
        //PregledKonfiguracijeProjekta,
        EditKonfiguracijeProjekta,
        EditZahtjevPrioriteta,
        //PregledZahtjevStatusa,
        EditZahtjevStatusa,
        //PregledZahtjevTipova,
        EditZahtjevTipova,
        //ListaKorisnikaProjekta,
        ListaZahtjevaProjekat,
        ListaKorisnika

    },
    mixins: [HelpTipDialogMixin],
    data() {
        return {
            nazivProjekta: null,
            opisProjekta: null,
            ucitanProjekat: false,
            activeHelpTip: false,
            helpTipDialogTitle: "Pregled projekta",
            helpTipDialogContent: `Pregled podataka o projektu.`,
            resolved: null,
            identity: Identity,
            tabs: 0,
            editProjekta: false,
            korisniciProjekta:[],
            ucitaniKorisniciProjekta: false,

            opcijeKonfiguracija: [{
                    id: 0,
                    naziv: "Konfiguracija projekta"
                },
                {
                    id: 1,
                    naziv: "Prioriteti zahtjeva"
                },
                {
                    id: 2,
                    naziv: "Statusi zahtjeva"
                },
                {
                    id: 3,
                    naziv: "Tipovi zahtjeva"
                },
                {
                    id: 4,
                    naziv: "Dijelovi projekta"
                }
            ],
            odabranaOpcijaKonfiguracije: 0,

        };
    },
    mounted() {
        this.vratiProjekat();
        //this.vratiKorisnikeProjekta();
    },
    created() {},

    methods: {
        omoguciEditovanjeProjekta() {
            this.editProjekta = true;
        },
        onemoguciEditProjekta() {
            this.editProjekta = false;
            this.vratiProjekat();
        },
        vratiProjekat() {
            var that = this;

            that.ucitanProjekat = false;

            var success = (response) => {

                that.projekatModel = response.body;
                that.ucitanProjekat = true;

            };
            var error = (poruka) => {
                console.log("nije uspjelo");

                that.$toast.error(poruka.body);
            };

            var promise = ProjekatResource().vratiProjekat({
                projekatId: this.$route.params.projekatId
            });

            promise.then(success, error);
        },
        /*vratiKorisnikeProjekta() {
            var that = this;
            this.osvjeziQuery(this.inputs);

            this.loading = true;
            var success = (model) => {
                that.korisniciProjekta = model.body;
                that.ucitaniKorisniciProjekta=true;
            };

            var error = () => {
                that.$toast.error("Filtriranje nije uspješno");
            };
            var promise = ProjekatResource().vratiKorisnikeProjekta({
                projekatId: this.$route.params.projekatId
            });

            promise.then(success, error);
        }*/
    }
};
</script>
