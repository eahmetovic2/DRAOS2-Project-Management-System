<style scoped>
/*:root {
    --sirina-kolone: {{ sirinaKolone }};
}*/

.buttons {
    margin-top: 35px;
}

.ghost {
    opacity: 0.5;
    background: #c8ebfb;
}

.status-to-do {
    color: #da8d00;
}

.status-in-progress {
    color: blue;
}

.status-done {
    color: green;
}

.kolona {
    /*width: var(--sirina-kolone); */

    flex: 1;
}

a {
    display: block;
}
</style>
<template>
<page>
    <material-card color="primary">

        <v-flex v-if="childOmogucenHeader" slot="header">
            <v-layout row justify-space-between>
                <v-flex>
                    <span class="card-naslov">Zadaci </span>
                </v-flex>
                
        <v-flex v-if="childOmogucenoDodavanjeIFiltriranje" class="text-xs-right toolbar-btn">
            <v-btn flat @click="otvoriDialogDodajZahtjev()">
                <v-icon class="mr-2">add_circle</v-icon>
                DODAJ
            </v-btn>
            <v-menu :close-on-content-click="false" content-class="dropdown-menu" v-model="filterMenu" bottom left offset-y transition="slide-y-transition">
                <v-btn flat slot="activator" class="toolbar-btn" @click="otvoriFiltriranje">
                    <v-icon class="mr-2">filter_list</v-icon>Filtriraj
                </v-btn>
                <v-card>
                    <v-card-title>
                        <div class="filter-title">
                            Filtriraj po
                        </div>
                    </v-card-title>
                    <v-form class="filter" @submit.prevent="filtriraj()">
                        <v-card-text class="pt-0">
                            <v-text-field label="Naziv" hide-details v-model="inputs.naziv"></v-text-field>
                        </v-card-text>
                        <v-card-text class="pt-0">
                            <v-text-field label="Opis" hide-details v-model="inputs.opis"></v-text-field>
                        </v-card-text>
                        <v-card-text class="pt-0">
                            <v-select :items="projekti" @input="ucitajKonfiguracijuProjekta" item-text="naziv" label="Projekat" item-value="id" hide-details v-model="inputs.projekatId" bottom></v-select>
                        </v-card-text>
                        <v-card-text class="pt-0">
                            <v-select :items="dijeloviProjekta" @input="ucitajKategorijeZahtjeva" item-text="naziv" label="Dio projekta" item-value="id" hide-details v-model="inputs.dioProjektaId" bottom></v-select>
                        </v-card-text>
                        <v-card-text class="pt-0">
                            <v-select clearable :items="zahtjevKategorije" item-text="naziv" label="Kategorija zahtjeva" item-value="id" hide-details v-model="inputs.kategorijaId" bottom></v-select>
                            <strong>*Potrebno odabrati dio projekta za pretragu po kategoriji</strong>
                        </v-card-text>
                        <v-card-text class="pt-0">
                            <v-select clearable :items="zahtjevTipovi" item-text="naziv" label="Tip zahtjeva" item-value="id" hide-details v-model="inputs.tipId" bottom></v-select>
                        </v-card-text>
                        <v-card-text class="pt-0">
                            <v-select clearable :items="zahtjevPrioriteti" item-text="naziv" label="Prioritet zahtjeva" item-value="id" hide-details v-model="inputs.prioritetId" bottom></v-select>
                        </v-card-text>
                        <v-card-text class="pt-0">
                            <v-select clearable :items="zahtjevStatusi" item-text="naziv" label="Status zahtjeva" item-value="id" hide-details v-model="inputs.statusId" bottom></v-select>
                        </v-card-text>
                        <v-card-text class="pt-0">
                            <v-text-field label="Korisnik" hide-details v-model="inputs.createdBy"></v-text-field>
                        </v-card-text>

                        <v-card-text class="pt-0">
                           <!-- <v-checkbox label="Potrebno uraditi" v-model="inputs.toDo" />
                            <v-checkbox label="Radi se" v-model="inputs.inProgress" />
                            <v-checkbox label="Završen" v-model="inputs.done" /> -->
                            <input type="checkbox" id="todo" v-model="inputs.toDo">
                            <label for="todo">Potrebno uraditi</label>
                            <input type="checkbox" id="inprogress" v-model="inputs.inProgress">
                            <label for="inprogress">Radi se</label>
                            <input type="checkbox" id="done" v-model="inputs.done">
                            <label for="done">Završen</label>
                        </v-card-text>

                        <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn flat color="primary" @click="ponistiFilter()">Poništi</v-btn>
                            <v-btn flat color="primary" type="submit">Filtriraj</v-btn>
                        </v-card-actions>
                    </v-form>
                </v-card>
            </v-menu>
        </v-flex>

            </v-layout>
        </v-flex>

        <v-card-text>
            <v-data-table :hide-actions="childSakrijPrikazPaginacije" v-bind:headers="childHeadersiZaPrikaz" v-bind:items="model.items" v-bind:pagination.sync="pagination" :total-items="totalItems" no-data-text="Nema zadataka za prikaz." :loading="loading" :rows-per-page-items="rowsPerPageItems" :columns="columns" :rows="rows">
                <template slot="items" scope="props">

                    <tr @click="otvoriZahtjev(props.item.id)">

                        <td v-if="showColumn('Naziv')">{{ props.item.naziv }}</td>
                        <td v-if="showColumn('Opis')" style="max-width: 250px">{{ props.item.opis }}</td>
                        <td v-if="showColumn('Tip')">{{ props.item.zahtjevTip }}</td>
                        <td v-if="showColumn('Kategorija')">{{ props.item.zahtjevKategorija }}</td>
                        <td v-if="showColumn('Prioritet')">{{ props.item.zahtjevPrioritet }}</td>
                        <td v-if="showColumn('Status')" >
                           <v-flex v-if="props.item.zahtjevStatus === 'Potrebno uraditi'" class="status-to-do"> {{ props.item.zahtjevStatus}}</v-flex>
                           <v-flex v-else-if="props.item.zahtjevStatus === 'Radi se'" class="status-in-progress"> {{ props.item.zahtjevStatus }}</v-flex>
                           <v-flex v-else-if="props.item.zahtjevStatus === 'Završen'" class="status-done"> {{ props.item.zahtjevStatus }}</v-flex>
                         </td>
                        <td v-if="showColumn('Datum kreiranja')">{{ props.item.datumKreiranja.substring(8,10) + '.' + props.item.datumKreiranja.substring(5,7)+ '.'+props.item.datumKreiranja.substring(0,4) + '. ' + props.item.datumKreiranja.substring(11,19)}} </td>
                        <td v-if="showColumn('Kreirao')">{{ props.item.createdBy }}</td>
                        <td v-if="showColumn('Projekat')">{{ props.item.projekat }}</td>
                        <td v-if="showColumn('Podprojekat')">{{props.item.dioProjekta}}</td>

                        <td v-if="showColumn('')" class="text-xs-right">
                            <v-menu open-on-hover offset-y>
                                <v-icon slot="activator">settings</v-icon>
                                <v-list>
                                    <v-list-tile :to="{ name: 'home.zahtjev.pregled', params: {zahtjevId: props.item.id }}">
                                        <v-list-tile-title>Pregled zahtjeva</v-list-tile-title>
                                    </v-list-tile>
                                    <!-- <v-list-tile :to="{ name: 'home.projekat.edit', params: {projekatId: props.item.id }}">
                                    <v-list-tile-title>Izmijeni projekat</v-list-tile-title>
                                </v-list-tile> -->
                                </v-list>
                            </v-menu>
                        </td>
                    </tr>

                </template>
                <template slot="pageText" scope="{ pageStart, pageStop }">
                    Od {{ pageStart }} do {{ pageStop }}
                </template>
            </v-data-table>
        </v-card-text>
        <!-- <help-tip-dialog v-if="activeHelpTip" :title="VratiTitlePoId('korisnik-list')" :content="VratiContentPoId('korisnik-list')" @zatvori="ZatvoriHelpDialog"> </help-tip-dialog>  -->
        <pregled-zahtjev v-if="dialogPregledZahtjeva" :zahtjev-id="pregledZahtjevaId" @azuriraniZahtjev="azuriranjeZahtjeva"></pregled-zahtjev>

        <dodavanje-zahtjeva v-if="dialogDodajZahtjev" @dialogDodajZahtjev="zatvoriDialogDodajZahtjev"></dodavanje-zahtjeva>
    </material-card>

</page>
</template>

<script>
import HelpTipDialogMixin from 'helpers/help-tip-dialog-mixin';
import {
    ProjekatResource,
    ZahtjevResource
} from 'api/resources';
import Identity from 'auth/identity';
import PaginationMixin from 'helpers/pagination-mixin';
import DodavanjeZahtjeva from '../zahtjev/dodavanje.vue';
import PregledZahtjev from '../zahtjev/pregled.vue';

export default {
    name: 'ListaZahtjevaProjekat',
    mixins: [PaginationMixin, HelpTipDialogMixin],
    components: {
        DodavanjeZahtjeva,
        PregledZahtjev
    },
    props: ['listaZahtjeva', 'omogucenoDodavanjeIFiltriranje', 'naslov', 'headersiZaPrikaz', 'omogucenHeader', 'sakrijPrikazPaginacije'],

    data() {

        return {
            duzinaNazivaOpisa: 30,
            list: [],
            childSakrijPrikazPaginacije: this.sakrijPrikazPaginacije,
            childOmogucenoDodavanjeIFiltriranje: this.omogucenoDodavanjeIFiltriranje,
            childOmogucenHeader: this.omogucenHeader,
            childHeadersiZaPrikaz: [],
            prikazKategorije: false,
            prikazStatusa: false,
            prikazCreatedBy: false,
            prikazPodprojekat: false,
            childNaslov: this.naslov,
            activeHelpTip: false,
            model: {
                items: []
            },
            totalItems: 0,
            filterMenu: false,
            filtrirano: false,
            loading: false,
            projekti: [],
            dijeloviProjekta: [],
            zahtjevKategorije: [],
            zahtjevTipovi: [],
            zahtjevPrioriteti: [],
            zahtjevStatusi: [],

            headers: [{
                    text: 'Naziv',
                    align: 'left',
                    sortable: false,
                    value: 'naziv',
                    prikaz: true
                }, {
                    text: 'Opis',
                    align: 'left',
                    sortable: false,
                    value: 'opis',
                    prikaz: true

                }, {
                    text: 'Tip',
                    align: 'left',
                    sortable: false,
                    value: 'tip',
                    prikaz: true

                }, {
                    text: 'Kategorija',
                    align: 'left',
                    sortable: false,
                    value: 'kategorija',
                    prikaz: true

                },
                {
                    text: 'Prioritet',
                    align: 'left',
                    sortable: false,
                    value: 'prioritet',
                    prikaz: true

                },
                {
                    text: 'Status',
                    align: 'left',
                    sortable: false,
                    value: 'status',
                    prikaz: true
                },

                {
                    text: 'Datum kreiranja',
                    align: 'left',
                    sortable: false,
                    value: 'datumKreiranja',
                    prikaz: true
                },
                {
                    text: 'Kreirao',
                    align: 'left',
                    sortable: false,
                    value: 'createdBy',
                    prikaz: true

                },
                {
                    text: 'Projekat',
                    align: 'left',
                    sortable: false,
                    value: 'projekat',
                    prikaz: true
                },
                {
                    text: 'Podprojekat',
                    align: 'left',
                    sortable: false,
                    value: 'dioProjekta',
                    prikaz: true

                },
                {
                    text: '',
                    align: 'left',
                    sortable: false,
                    value: '',
                    prikaz: true
                }
            ],
            inputs: {
                naziv: null,
                opis: null,
                dioProjektaId: null,
                statusId: null,
                prioritetId: null,
                tipId: null,
                kategorijaId: null,
                korisnik: null,
                page: 1,
                count: 10,
                projekatId: null,
                prethodniBrojDana: null,
                sve: false,
                toDo: true,
                inProgress: true,
                done: true
            },
            projekatInputs: {
                page: 1,
                count: 10,
                sve: true
            },
            identity: Identity,
            dialogDodajZahtjev: false,
            dialogPregledZahtjeva: false,

        };
    },

    created() {
         this.pagination.rowsPerPage = 15
        // console.log(this.pregledZahtjevaId)
    
         if (this.naslov == null)
             this.childNaslov = "Zahtjevi";

         if (this.omogucenoDodavanjeIFiltriranje == null)
            this.childOmogucenoDodavanjeIFiltriranje = true;

        if (this.omogucenHeader == null)
            this.childOmogucenHeader = true;

         if (this.childSakrijPrikazPaginacije == null)
            this.childSakrijPrikazPaginacije = false;

         if (this.headersiZaPrikaz == null) {
             this.childHeadersiZaPrikaz = this.headers;
         } else {
             this.odabirHeadersaZaPrikaz();
         }

        this.inputs.projekatId = null;
    },

    mounted() {

         if (this.inputs.page) {
             this.pagination.page = this.inputs.page;
         }
         if (this.inputs.count) {
             this.pagination.count = this.inputs.count;
    }
    },

    methods: {

        otvoriFiltriranje() {
            this.ucitajProjekte();
            //this.ucitajDijeloveProjekta();
        },
        filtriraj() {
           /* if (this.inputs.naziv) {
                this.filtrirano = true;
            } else {
                this.filtrirano = false;
            }*/
            this.filterMenu = false;
            this.resetujPaginaciju();
        },
        ponistiFilter() {
            this.inputs.naziv = "";
            this.inputs.opis = "";
            this.inputs.dioProjektaId = null;
            this.inputs.statusId = null;
            this.inputs.prioritetId = null;
            this.inputs.tipId = null;
            this.inputs.kategorijaId = null;
            this.inputs.createdBy = "";
            this.inputs.projekatId = null;

            this.inputs.toDo = true;
            this.inputs.inProgress = true;
            this.inputs.done = false;


            this.dijeloviProjekta = [];
            this.zahtjevKategorije = [];
            this.zahtjevTipovi = [];
            this.zahtjevPrioriteti = [];
            this.zahtjevStatusi = [];

            this.filterMenu = false;
            this.filtrirano = false;
            this.resetujPaginaciju();
        },
        ucitajKonfiguracijuProjekta(id) {

            //da ne ostaju kategorije od ranije izabranog projekta, jer se kategorije ucitavaju tek kad se izabere dio projekta
            this.zahtjevKategorije = [];

            this.ucitajZahtjevTipove(id);
            this.ucitajPrioriteteZahtjeva(id);
            this.ucitajStatuseZahtjeva(id);
            this.ucitajDijeloveProjekta(id);
        },
        ucitajProjekte() {

            var that = this;
            that.loading = true;
            var success = (response) => {
                that.projekatInputs.sve = false;
                that.projekti = response.body.items;

            };
            var error = () => {
                this.$toast.error('Nisu ucitani projekti.');
            };
            that.projekatInputs.sve = true;
            var promise = ProjekatResource().vratiSveProjekte(that.projekatInputs);
            promise.
            then(success, error);
        },

        updateData() {
            var that = this;
            
          

            if (that.listaZahtjeva == null  ||  !that.listaZahtjeva) {
                this.osvjeziQuery(this.inputs);

                var success = (model) => {
                    console.log(this.inputs, "test")
                    that.model = model.body;
                    that.totalItems = model.body.total;
                    that.ograniciDuzinuNazivaIOpisa();
                    that.loading = false;
                    if (that.inputs.projekatId)
                        that.ucitajStatuseZahtjeva(that.inputs.projekatId);
                };

                var error = () => {
                    that.$toast.error("Filtriranje nije uspješno");
                };

                var promise = ZahtjevResource().vratiSveZahtjeve(this.inputs);

                promise.
                then(success, error);
            } 
            
            else {
                that.model.items = that.listaZahtjeva;

                that.ograniciDuzinuNazivaIOpisa();

            }
        },
        otvoriDialogDodajZahtjev() {
            this.dialogDodajZahtjev = true;
            var projekatId = this.$route.params.projekatId;
            this.$emit("projekat", projekatId);

        },
        zatvoriDialogDodajZahtjev(dialogDodajZahtjev) {
            this.dialogDodajZahtjev = dialogDodajZahtjev;
        },
        ucitajZahtjevTipove(id) {
            var success = (response) => {

                var that = this;
                that.zahtjevTipovi = response.body;

            };
            var error = () => {
                console.log("nije uspjelo");
            };

            var promise = ProjekatResource().vratiTipoveZahtjevaZaProjekat({
                projekatId: id
            });

            promise.then(success, error);
        },

        ucitajPrioriteteZahtjeva(id) {
            var success = (response) => {

                var that = this;
                that.zahtjevPrioriteti = response.body;

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

        ucitajStatuseZahtjeva(id) {
            console.log(id,"status")
            var success = (response) => {
                
                var that = this;
                that.zahtjevStatusi = response.body;
                //that.rasporediZahtjevePoStatusu();

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

        ucitajDijeloveProjekta(id) {

            var success = (response) => {

                var that = this;
                that.dijeloviProjekta = response.body;

            };
            var error = () => {
                console.log("nije uspjelo");
            };

            var promise = ProjekatResource().vratiDijeloveProjekta({
                projekatId: id
            });

            promise.then(success, error);
        },

        ucitajKategorijeZahtjeva(id) {
            var success = (response) => {

                var that = this;
                that.zahtjevKategorije = response.body;

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
        otvoriZahtjev(id) {
            this.pregledZahtjevaId = id;
            this.dialogPregledZahtjeva = true;
            /*  this.$router.push({
                  name: 'home.zahtjev.pregled',
                  params: {
                      zahtjevId: id
                  }
              })*/

        },
        odabirHeadersaZaPrikaz() {
            //headersiZaPrikaz, primjer "11100101101"
            //1 predstavlja da se headers prikazuje
            //0 da se ne prikazuje
            var niz = this.headersiZaPrikaz.split("");

            for (var i = 0; i < niz.length; i++) {

                if (niz[i] == 0) {
                    this.headers[i].prikaz = false;
                }

            }

            var n = [];
            for (var j = 0; j < this.headers.length; j++) {
                if (this.headers[j].prikaz == true)
                    n.push(this.headers[j]);
            }
            this.childHeadersiZaPrikaz = n;
        },
        showColumn(col) {
            return this.headers.find(h => h.text === col).prikaz
        },
        ograniciDuzinuNazivaIOpisa() {
            var modelItemLength = this.model.items.length;
            for (var i = 0; i < modelItemLength; i++) {
                if (this.model.items[i].naziv.length > this.duzinaNazivaOpisa) {
                    this.model.items[i].naziv = this.model.items[i].naziv.slice(0, this.duzinaNazivaOpisa - 3) + "...";
                }
            }
            for (var j = 0; j < modelItemLength; j++) {
                if (this.model.items[j].opis.length > this.duzinaNazivaOpisa) {
                    this.model.items[j].opis = this.model.items[j].opis.slice(0, this.duzinaNazivaOpisa - 3) + "...";
                }
            }
        },
        azuriranjeZahtjeva(zahtjev, azuriran) {

            this.dialogPregledZahtjeva = false;

            if (azuriran) {
                this.updateData();
            }

        }/*,
        resetujPaginaciju() {
            console.log("reset paginacije");
            this.inputs.page = 1;
            this.pagination.page = 1;
            this.updateData();       
        }*/

    }
}
</script>
