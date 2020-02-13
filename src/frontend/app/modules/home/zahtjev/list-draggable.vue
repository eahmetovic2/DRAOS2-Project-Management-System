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

.kolona {
    /*width: var(--sirina-kolone); */

    flex: 1;
}

.zahtjev-statusi {
    box-shadow: 0px 3px 5px -1px rgba(0, 0, 0, 0.12), 0px 5px 8px 0px rgba(0, 0, 0, 0.12), 0px 1px 14px 0px rgba(0, 0, 0, 0.12) !important;
    margin-right: 5px;

}

/* .boxstatusi{
    color: green;  
} */
.bojastatus{
    color: #da8d00;  
    
}

.bojastatus h6{
font-weight: bold !important;
}

.bojastatus1 h6{
font-weight: bold !important;
}

.bojastatus2 h6{
font-weight: bold !important;
}

.bojastatus1{
    color: blue;  
}
.bojastatus2{
    color: green;  
}

.task {
    background: #e6e5e5;
    font-size: 16px;
    color: black;
}
.task:hover {
    cursor: pointer;
}
.prethodni-broj-dana {
    max-width: 35%;
    float: right;
}

.draggable-item {
    font-size: 78%;
}

.draggable-area {
    height: 100%;
}
</style>
<template>
<page>

    <material-card color="primary">
        <v-flex slot="header">
            <v-layout row justify-space-between>

                <v-flex>
                    <span class="card-naslov">Zadaci</span>
                </v-flex>

                <v-flex class="text-xs-right toolbar-btn">
                    <v-btn flat @click="otvoriDialogDodajZahtjev()">
                        <v-icon class="mr-2">add_circle</v-icon>
                        DODAJ
                    </v-btn>
                </v-flex>
            </v-layout>
        </v-flex>

        <!-- <help-tip-dialog v-if="activeHelpTip" :title="VratiTitlePoId('korisnik-list')" :content="VratiContentPoId('korisnik-list')" @zatvori="ZatvoriHelpDialog"> </help-tip-dialog>  -->
        <dodavanje-zahtjeva v-if="dialogDodajZahtjev" @dialogDodajZahtjev="zatvoriDialogDodajZahtjev"></dodavanje-zahtjeva>

        <v-layout row>
            <v-flex xs6 md4 lg3>
                <v-card-text style="max-width:75%">
                    <v-select :items="projekti" @input="ucitajStatuseZahtjeva" item-text="naziv" label="Projekat" item-value="id" hide-details v-model="inputs.projekatId" bottom></v-select>
                </v-card-text>
            </v-flex>
            <v-flex xs6 md8 lg9 class="text-xs-right">
                <v-card-text class="text-xs-right prethodni-broj-dana">
                    <v-autocomplete placeholder="Svi zahtjevi" clearable filled="true" :items="prethodniDani" @input="ucitajIRasporediZahtjeve" item-text="naziv" item-value="brojDana" label="Zahtjevi za prethodni broj dana" hide-details v-model="inputs.prethodniBrojDana" bottom></v-autocomplete>
                    <!-- "period prikaza zahtjeva" -->
                </v-card-text>
            </v-flex>
        </v-layout>
        <v-container fill-height fluid grid-list-md mt-0 class='about'>

            <v-layout column fill-height >
                <v-layout row fill-height>
                    <v-flex class="kolona zahtjev-statusi" :elevation="6" v-for="(status,index) in zahtjevStatusi">
                       <v-flex v-if="status.oznaka =='0'" class="text-xs-center bojastatus"> <h6 >{{status.naziv}}</h6></v-flex>
                       <v-flex v-else-if="status.oznaka =='1'" class="text-xs-center bojastatus1"> <h6 >{{status.naziv}}</h6></v-flex>
                       <v-flex v-else-if="status.oznaka =='2'" class="text-xs-center bojastatus2"> <h6 >{{status.naziv}}</h6></v-flex>
                        <v-divider></v-divider>

                        <draggable v-if="ucitaniZahtjevi" class="draggable-area" ghost-class="ghost" :move="checkMove" @change="azurirajStatusZahtjeva($event,index)" v-model='list[index]' :id="index" :options='{group: "zahtjevi"}' animation=200 :disabled="onemogucenoAzuriranje">
                            <v-flex class="draggable-item" @click="otvoriZahtjev(element.id)" v-for="(element,index) in list[index]">
                                <v-card dark class="task">
                                    <v-card-text>
                                        <!-- <i @click="element.fixed = !element.fixed" aria-hidden="true"></i> -->
                                        {{ element.naziv }}
                                    </v-card-text>
                                </v-card>
                            </v-flex>
                        </draggable>

                    </v-flex>

                </v-layout>
            </v-layout>
        </v-container>

    </material-card>

    <pregled-zahtjev v-if="dialogPregledZahtjeva" :zahtjev-id="pregledZahtjevaId" @azuriraniZahtjev="azuriranjeZahtjeva"></pregled-zahtjev>

</page>
</template>

<script>
import HelpTipDialogMixin from 'helpers/help-tip-dialog-mixin';
import {
    ProjekatResource,
    ZahtjevResource
} from 'api/resources';
import Identity from 'auth/identity';
import DodavanjeZahtjeva from '../zahtjev/dodavanje.vue';
import PregledZahtjev from '../zahtjev/pregled.vue';
import listVue from './komentar/list.vue';

export default {
    name: 'ListaZahtjevaDraggable',
    mixins: [HelpTipDialogMixin],
    components: {
        DodavanjeZahtjeva,
        PregledZahtjev
    },
    props: ['listaZahtjeva', 'omogucenoDodavanjeIFiltriranje', 'naslov', 'headersiZaPrikaz', 'omogucenHeader', 'sakrijPrikazPaginacije'],

    data() {

        return {
            onemogucenoAzuriranje: false,
            duzinaNazivaOpisa: 45,
            list: [],
            activeHelpTip: false,
            model: {
                items: []
            },
            totalItems: 0,
            loading: false,
            projekti: [],
            zahtjevStatusi: [],
            dialogPregledZahtjeva: false,
            pregledZahtjevaId: null,
            ucitaniZahtjevi: false,
            boja1: 'red',
            boja2: 'blue',
            boja3: 'greeen',

            inputs: {
                naziv: null,
                opis: null,
                statusId: null,
                projekatId: null,
                prethodniBrojDana: 7,
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
            prethodniDani: [],
            red: null,
            kolona: null
        };
    },

    created() {

        this.inputs.sve = true;

        if (!Identity.imaPravo("zahtjev_zahtjev_edit_status"))
            this.onemogucenoAzuriranje = true;

        this.ucitajProjekte();
        this.inicijalizacijaNizaPrethodnihDana();
    },

    mounted() {

    },

    methods: {
        checkMove() {
            if (Identity.imaPravo("zahtjev_zahtjev_edit_status"))
                return true;
            return false;
        },
        azurirajStatusZahtjeva(card, index) {

            if (card.added != null) {
                //console.log("Pomjeren status u drugi tab: ", card.added.element.id, " to ", index);
                //console.log("Novi zahtjev status: id ", this.zahtjevStatusi[index].id, "index: ", index, " i naziv: ", this.zahtjevStatusi[index].naziv);

                var that = this;
                var success = (response) => {
                    this.$toast.success('Uspješno ažuriranje statusa');

                };
                var error = (poruka) => {
                    this.$toast.error(poruka.body);
                    return false;
                };
                var promise = ZahtjevResource().azurirajStatusZahtjeva({
                    zahtjevId: card.added.element.id
                }, {
                    zahtjevStatusId: this.zahtjevStatusi[index].id
                });
                promise.
                then(success, error);
            }

        },
        ucitajProjekte() {

            var that = this;
            that.loading = true;
            var success = (response) => {
                that.projekatInputs.sve = false;
                that.projekti = response.body.items;
                if (that.projekti.length != 0) {
                    that.inputs.projekatId = that.projekti[0].id;
                    that.ucitajStatuseZahtjeva(that.inputs.projekatId);
                }

            };
            var error = () => {
                this.$toast.error('Nisu ucitani projekti.');
            };
            that.projekatInputs.sve = true;
            var promise = ProjekatResource().vratiSveProjekte(that.projekatInputs);
            promise.
            then(success, error);
        },
        ucitajIRasporediZahtjeve() {
            var that = this;
            that.ucitaniZahtjevi = false;

            //this.osvjeziQuery(this.inputs);
            var success = (model) => {
                that.model = model.body;
                that.totalItems = model.body.total;

                that.ograniciDuzinuNaziva();
                that.rasporediZahtjevePoStatusu();
                that.ucitaniZahtjevi = true;

                that.loading = false;

                /*if (that.inputs.projekatId != null)
                    that.ucitajStatuseZahtjeva(that.inputs.projekatId);*/
            };

            var error = () => {
                that.$toast.error("Filtriranje nije uspješno");
            };

            var promise = ZahtjevResource().vratiSveZahtjeve(this.inputs);

            promise.
            then(success, error);

        },
        otvoriDialogDodajZahtjev() {
            this.dialogDodajZahtjev = true;
            var projekatId = this.$route.params.projekatId;
            this.$emit("projekat", projekatId);

        },
        zatvoriDialogDodajZahtjev(dialogDodajZahtjev) {
            this.dialogDodajZahtjev = dialogDodajZahtjev;
        },

        ucitajStatuseZahtjeva(id) {
            var success = (response) => {

                var that = this;
                that.zahtjevStatusi = response.body;
                that.ucitajIRasporediZahtjeve();

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
        otvoriZahtjev(id) {
            this.$route.params.zahtjevId = id;
            this.pregledZahtjevaId = id;
            this.dialogPregledZahtjeva = true;
        },
        rasporediZahtjevePoStatusu() {
            var zahtjevStatusiLength = this.zahtjevStatusi.length;
            for (var a = 0; a < zahtjevStatusiLength; a++) {
                this.list[a] = [];
            }

            var modelItemsLength = this.model.items.length;
            for (var j = 0; j < zahtjevStatusiLength; j++) {

                for (var i = 0; i < modelItemsLength; i++) {

                    if (this.model.items[i].zahtjevStatus == this.zahtjevStatusi[j].naziv) {
                        this.list[j].push(this.model.items[i]);

                    }
                }

            }
        },
        ograniciDuzinuNaziva() {
            var modelItemsLength=this.model.items.length;
            for (var i = 0; i < modelItemsLength; i++) {
                if (this.model.items[i].naziv.length > this.duzinaNazivaOpisa) {
                    this.model.items[i].naziv = this.model.items[i].naziv.slice(0, this.duzinaNazivaOpisa - 3) + "...";
                }
            }

        },
        ograniciDuzinuOpisa() {
            var modelItemsLength=this.model.items.length;
            for (var j = 0; j < modelItemsLength; j++) {
                if (this.model.items[j].opis.length > this.duzinaNazivaOpisa) {
                    this.model.items[j].opis = this.model.items[j].opis.slice(0, this.duzinaNazivaOpisa - 3) + "...";
                }
            }
        },
        inicijalizacijaNizaPrethodnihDana() {
            for (var i = 1; i <= 365; i++)
                this.prethodniDani.push(i);

        },
        azuriranjeZahtjeva(zahtjev, azuriran) {
            this.dialogPregledZahtjeva = false;
            if (azuriran) {

                var red = null;

                for (var i = 0; i < this.zahtjevStatusi.length; i++) {
                    var foundIndex = this.list[i].findIndex(x => x.id == this.pregledZahtjevaId);
                    if (foundIndex != -1) {
                        red = i;
                        break;
                    }
                }

                if (zahtjev != null) {

                    this.list[red][foundIndex] = zahtjev;
                    if (this.zahtjevStatusi[red].naziv != zahtjev.zahtjevStatus) {
                        this.list[red].splice(foundIndex, 1);
                    }
                    var indexZahtjevStatus = this.zahtjevStatusi.findIndex(x => x.naziv == zahtjev.zahtjevStatus.naziv);

                    if (indexZahtjevStatus != -1) {

                        if (zahtjev.naziv.length > this.duzinaNazivaOpisa) {
                            zahtjev.naziv = zahtjev.naziv.slice(0, this.duzinaNazivaOpisa - 3) + "...";
                        }
                        this.list[indexZahtjevStatus].push(zahtjev);
                    }

                } else {
                    this.list[red].splice(foundIndex, 1);
                }
            }
        }

    }
}
</script>
