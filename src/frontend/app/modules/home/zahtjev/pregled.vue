<style>
.opis-zahtjeva-text {
    white-space: pre-wrap;
}

.text-centriran {
    text-align: center;
}

.status-to-do {
    color: orange;
}

.status-in-progress {
    color: blue;
}

.status-done {
    color: green;
}
</style>
<template>
<page>
    <template v-if="ucitanZahtjev">
        <v-dialog v-if="ucitanZahtjev" v-model="ucitanZahtjev" @input="zatvoriDialogPregledZahtjev" max-width="1000px">

            <material-card color="primary">
                <v-flex slot="header">
                    <v-layout row justify-space-between>
                        <v-flex>
                            <span class="card-naslov">Pregled zahtjeva </span>
                        </v-flex>
                        <v-flex class="text-xs-right toolbar-btn">
                            <v-menu open-on-hover offset-y class="toolbar-menu" bottom left content-class="dropdown-menu" transition="slide-y-transition">
                                <v-icon slot="activator">more_vert</v-icon>
                                <v-list>
                                    <v-list-tile @click="otvoriEditZahtjeva">
                                        <v-list-tile-title>Izmjena zahtjeva</v-list-tile-title>
                                    </v-list-tile>
                                    <v-list-tile @click="otvoriHistorijuStatusaZahtjeva">
                                        <v-list-tile-title>Pregled promjena statusa zahtjeva</v-list-tile-title>
                                    </v-list-tile>
                                    <v-list-tile @click="otvoriDialogBrisanjeZahtjeva">
                                        <v-list-tile-title>Brisanje zahtjeva</v-list-tile-title>
                                    </v-list-tile>
                                </v-list>
                            </v-menu>
                        </v-flex>
                    </v-layout>
                </v-flex>
                <v-card-text>

                    <p class="text-boldiran text-centriran">
                        {{ zahtjevModel.naziv }}
                    </p>
                    <v-divider></v-divider>
                    <!-- <v-textarea  auto-grow label="Opis" max-rows="6" v-model="zahtjevModel.opis"></v-textarea> -->
                    <br>
                    <pre class="opis-zahtjeva-text">{{ zahtjevModel.opis }}</pre>
                    <br>

                    <v-divider></v-divider>
                    <br>
                    <table class="v-table">
                        <tbody>
                            <tr>
                                <td>Status: </td>
                                <td v-if="zahtjevModel.zahtjevStatus.oznaka=='0'" class="status-to-do">{{ zahtjevModel.zahtjevStatus.naziv }}</td>
                                <td v-if="zahtjevModel.zahtjevStatus.oznaka=='1'" class="status-in-progress">{{ zahtjevModel.zahtjevStatus.naziv }}</td>
                                <td v-if="zahtjevModel.zahtjevStatus.oznaka=='2'" class="status-done">{{ zahtjevModel.zahtjevStatus.naziv }}</td>

                            </tr>
                            <tr>
                                <td>Dio projekta: </td>
                                <td>{{ zahtjevModel.dioProjekta}}</td>
                            </tr>
                            <tr>
                                <td>Kategorija: </td>
                                <td>{{ zahtjevModel.zahtjevKategorija}}</td>
                            </tr>

                            <tr>
                                <td>Zahtjev kreirao: </td>
                                <td>{{ zahtjevModel.createdBy }}</td>
                            </tr>

                            <tr>
                                <td>Datum kreiranja: </td>
                                <td>{{ zahtjevModel.datumKreiranja.substring(8,10) + '. ' + zahtjevModel.datumKreiranja.substring(5,7)+ '. '+zahtjevModel.datumKreiranja.substring(0,4) + '. '+zahtjevModel.datumKreiranja.substring(11,19)}}</td>
                            </tr>

                            <tr>
                                <td>Datum izmjene: </td>
                                <td>{{ zahtjevModel.datumIzmjene.substring(8,10) + '. ' + zahtjevModel.datumIzmjene.substring(5,7)+ '. '+zahtjevModel.datumIzmjene.substring(0,4) + '. ' +zahtjevModel.datumIzmjene.substring(11,19) }}</td>
                            </tr>

                            <tr>
                                <td>Tip: </td>
                                <td>{{ zahtjevModel.zahtjevTip }}</td>
                            </tr>

                            <!--   <tr>
                            <td>Datum nastanka</td>
                            <td>{{ resolved.model.datumKreiranja.substring(8,10) + '. ' + resolved.model.datumKreiranja.substring(5,7)+ '. '+resolved.model.datumKreiranja.substring(0,4) + '.' }}</td>
                        </tr> -->

                            <tr>
                                <td>Prioritet: </td>
                                <td>{{ zahtjevModel.zahtjevPrioritet }}</td>
                            </tr>

                            <tr>
                                <td> Potrošeno vrijeme </td>
                                <td>
                                    <v-chip>{{zahtjevModel.potrosenoVrijeme}}</v-chip>
                                </td>
                            </tr>

                            <tr>
                                <td> Dodijeljeni korisnik </td>
                                <td>
                                    <v-chip>{{zahtjevModel.dodijeljeniKorisnikIme}}</v-chip>
                                </td>
                            </tr>

                            <tr v-if="zahtjevModel.dokumenti.length!=0">
                                <td> Prilozi zahtjeva: </td>
                                <td><a @click="preuzmiDokument(zahtjevModel.dokumenti[0].id)"> {{zahtjevModel.dokumenti[0].naziv}}
                                    </a></td>
                            </tr>

                        </tbody>
                    </table>
                </v-card-text>

                <lista-komentara-zahtjev :zahtjev-id="childZahtjevId"></lista-komentara-zahtjev>

                <v-dialog v-if="dialogEditZahtjeva" v-model="dialogEditZahtjeva" max-width="1000px" @input="zatvoriDialogPregledZahtjev">
                    <edit-zahtjev :zahtjev-id="childZahtjevId" @dialogEditZahtjeva="zatvoriDialogEditZahtjev"></edit-zahtjev>
                </v-dialog>

                <izmjena-zahtjeva-historija v-if="dialogHistorijaStatusaZahtjeva" :zahtjevId="zahtjevId" @close="zatvoriDialogHistorijaStatusaZahtjeva"></izmjena-zahtjeva-historija>

                <!--  <v-dialog v-model="dialogBrisanjeZahtjeva">
                    <v-card>
                        <v-card-title class="headline">{{ title }}</v-card-title>
                        <v-card-text>{{ content }}</v-card-text>
                        <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn class="green--text darken-1" flat="flat" @click.native="brisanjeZahtjeva">OK</v-btn>
                        </v-card-actions>
                    </v-card>
                </v-dialog> -->

                <obrisi-modal :aktivan="dialogBrisanjeZahtjeva" :item="zahtjevId" header="Brisanje zahtjeva" body="Jeste li sigurni da želite obrisati zahtjev?" @close="closeModal($event)" width="350"></obrisi-modal>
            </material-card>
        </v-dialog>
    </template>
    <help-tip-dialog v-if="activeHelpTip" :title="helpTipDialogTitle" :content="helpTipDialogContent" @zatvori="ZatvoriHelpDialog"> </help-tip-dialog>
</page>
</template>

<script>
import {
    KorisnikResource,
    ZahtjevResource,
    NotifikacijeResource
} from 'api/resources';
import Identity from 'auth/identity';
import HelpTipDialogMixin from 'helpers/help-tip-dialog-mixin';
import UploadMixin from "helpers/upload-mixin";
import ListaKomentaraZahtjev from './komentar/list.vue';
import EditZahtjev from './edit.vue';
import IzmjenaZahtjevaHistorija from './status-historija/izmjena-zahtjeva-historija';
import ObrisiModal from '../components/obrisi-modal'

export default {
    name: 'PregledZahtjev',
    components: {
        ListaKomentaraZahtjev,
        EditZahtjev,
        IzmjenaZahtjevaHistorija,
        ObrisiModal
    },
    props: ['zahtjevId'],
    mixins: [HelpTipDialogMixin, UploadMixin],
    data() {
        return {
            childZahtjevId: Number(this.zahtjevId),
            activeHelpTip: false,
            resolved: null,
            identity: Identity,
            zahtjevModel: null,
            ucitanZahtjev: false,
            prilog: null,
            dialogEditZahtjeva: false,
            azuriraniZahtjev: null,
            azuriran: false,
            dialogHistorijaStatusaZahtjeva: false,
            dialogBrisanjeZahtjeva: false
        };
    },
    watch: {
        $route(to, from) {
            this.ucitajZahtjev(to.params.zahtjevId);
        }

    },
    created() {
        this.ucitajZahtjev();
        //this.vratiIzmjeneStatusaZahtjeva(this.zahtjevId);
        //this.ucitajPrilogZahtjeva();
    },
    methods: {

        ucitajZahtjev() {
             
            var that = this;
            that.ucitanZahtjev = false;
            //   if((that.$route.params.zahtjevId == null) && (that.childZahtjevId == null))
            //      return
           if(isNaN(that.$route.params.zahtjevId)) 
           return;
            if (that.$route.params.zahtjevId != null)
                that.childZahtjevId = Number(that.$route.params.zahtjevId);

                

            
            // if(that.childZahtjevId ==undefined  ||  that.childZahtjevId==null)
            // console.log('lllllllll')
            // return;

            var success = (response) => {
                that.zahtjevModel = response.body;

                if (that.zahtjevModel == null) {
                    that.$emit("azuriraniZahtjev", that.azuriraniZahtjev);
                }
                that.prilagodiPrikazPotrosenogVremena();

                if (that.zahtjevModel.dodijeljeniKorisnikIme == null)
                    that.zahtjevModel.dodijeljeniKorisnikIme = '-';

                that.ucitanZahtjev = true;

                that.otvoriKorisnikoveNotifikacijeZaZahtjev();

            };
            var error = (poruka) => {
                this.$emit("azuriraniZahtjev", null, true);

                this.$toast.error(poruka.body);

            };

            var promise = ZahtjevResource().vratiZahtjev({
                zahtjevId: that.childZahtjevId
            });
            promise.
            then(success, error);

        },
        otvoriEditZahtjeva() {
            /*this.$router.push({
                       name: "home.zahtjev.edit",
                       params: {
                           zahtjevId: this.zahtjevModel.id
                       }
                   });*/
            this.dialogEditZahtjeva = true;
        },
        prilagodiPrikazPotrosenogVremena() {

            var vrijeme = '';
            var brojCifaraDani = 0;

            for (var i = 0; i < this.zahtjevModel.potrosenoVrijeme.length; i++) {
                if (this.zahtjevModel.potrosenoVrijeme[i] == ':') {
                    brojCifaraDani = i;
                    break;
                }
            }
            if (!(this.zahtjevModel.potrosenoVrijeme.substr(0, 2) == '00')) {
                vrijeme = vrijeme + this.zahtjevModel.potrosenoVrijeme.substr(0, brojCifaraDani) + 'd ';
            }
            if (!(this.zahtjevModel.potrosenoVrijeme.substr(brojCifaraDani + 1, 2) == '00')) {
                vrijeme = vrijeme + this.zahtjevModel.potrosenoVrijeme.substr(brojCifaraDani + 1, 2) + 'h ';
            }
            if (!(this.zahtjevModel.potrosenoVrijeme.substr(brojCifaraDani + 4, 2) == '00')) {
                vrijeme = vrijeme + (this.zahtjevModel.potrosenoVrijeme.substr(brojCifaraDani + 4, 2) + 'm ');
            }
            if (!(this.zahtjevModel.potrosenoVrijeme.substr(brojCifaraDani + 7, 2) == '00')) {
                vrijeme = vrijeme + (this.zahtjevModel.potrosenoVrijeme.substr(brojCifaraDani + 7, 2) + 's');
            }
            if (vrijeme == '')
                vrijeme = "-";

            this.zahtjevModel.potrosenoVrijeme = vrijeme;

        },
        zatvoriDialogEditZahtjev(azuriran) {
            if (azuriran) {
                this.azuriran = azuriran;
                this.ucitajZahtjev();
            }
            this.dialogEditZahtjeva = false;

        },
        zatvoriDialogPregledZahtjev() {
            this.azuriraniZahtjev = this.zahtjevModel;
            this.$emit("azuriraniZahtjev", this.azuriraniZahtjev, this.azuriran);
        },
        vratiIzmjeneStatusaZahtjeva(id) {
            var that = this;

            var success = (response) => {
                for (var i = 0; i < response.body.length; i++) {
                    var datum = response.body[i].datumKreiranja;
                    var prilagodjeniDatum = datum.substring(8, 10) + '.' + datum.substring(5, 7) + '.' + datum.substring(0, 4) + '. ' + datum.substring(11, 19);
                }

            };
            var error = (poruka) => {
                this.$toast.error(poruka.body);
            };
            var promise = ZahtjevResource().vratiIzmjeneStatusaZahtjeva({
                zahtjevId: id
            });

            promise.
            then(success, error);
        },
        otvoriHistorijuStatusaZahtjeva() {
            this.dialogHistorijaStatusaZahtjeva = true;
        },
        otvoriDialogBrisanjeZahtjeva() {
            this.dialogBrisanjeZahtjeva = true;
        },

        closeModal(event) {
            if (event.obrisi) {
                this.brisanjeZahtjeva();
            }
            this.dialogBrisanjeZahtjeva = false;
        },
        brisanjeZahtjeva() {
            var that = this;
            var success = (response) => {
                that.$toast.success("Zahtjev uspješno izbrisan");
                that.$emit("azuriraniZahtjev", null, true);

            };
            var error = (poruka) => {
                this.$toast.error(poruka.body);
            };
            var promise = ZahtjevResource().brisanjeZahtjeva({
                zahtjevId: that.zahtjevModel.id
            }, {});

            promise.
            then(success, error);
        },
        zatvoriDialogHistorijaStatusaZahtjeva() {
            this.dialogHistorijaStatusaZahtjeva = false;
        },
        otvoriKorisnikoveNotifikacijeZaZahtjev() {

            var that = this;
            var success = (response) => {};
            var error = () => {};
            var promise = NotifikacijeResource().otvoriKorisnikoveNotifikacijeZaZahtjev({
                zahtjevId: that.childZahtjevId
            }, {});

            promise.
            then(success, error);
        }
    },
    resolve: {}
};
</script>
