<style>
.widget-container {
    align-items: stretch;
}

.widget-container .v-card.blok-card {
    height: 100% !important;
}

.widget-item {
    flex-grow: 1;
}

.karta-skola {
    z-index: 0;
}

.online-korisnici-mapa {
    height: 400px;
    align-items: stretch;
}

.online-korisnici-mapa .v-card {
    height: 100% !important;
    margin-right: 16px;
}

.online-korisnici-mapa .v-card .v-card__text {
    padding-top: 40px;
}

.map-widget {
    min-height: 340px !important;
}

@media (max-width: 992px) {
    .widget-item {
        flex-basis: 25%;
    }

    .online-korisnici {
        margin-bottom: 16px;
    }

    .online-korisnici-mapa .v-card {
        margin-right: 0;
    }
}

@media (max-width: 640px) {
    .widget-item {
        flex-basis: 100%;
    }
}

.card-padding {
    padding: 10px;
}

.tabovi {
    padding-top: 8px;
}
</style>

<template>
<div v-if="ulogaPorvjera">

    <v-layout wrap>
        <v-flex v-if="imaPravo('osnovno_dashboard_broj_zahtjeva_potrebno_uraditi')" class="card-padding" sm6 xs12 md6 lg3>
            <material-stats-card color="orange" icon="assignment" v-model="model.brojZahtjevaPotrebnoUraditi" title="Broj zadataka za uraditi" />
        </v-flex>
        <v-flex v-if="imaPravo('osnovno_dashboard_broj_zahtjeva_u_progresu')" class="card-padding" sm6 xs12 md6 lg3>
            <material-stats-card color="info" icon="dynamic_feed" title="Broj zadataka u izradi" v-model="model.brojZahtjevaUProgresu" />
        </v-flex>
        <v-flex v-if="imaPravo('osnovno_dashboard_broj_zahtjeva_ukupno')" class="card-padding" sm6 xs12 md6 lg3>
            <material-stats-card color="green" icon="filter_none" title="Ukupan broj zadataka" v-model="model.brojZahtjevaUkupno" />
        </v-flex>
        <v-flex v-if="imaPravo('osnovno_dashboard_broj_zahtjeva_nisu_rijeseni')" class="card-padding" sm6 xs12 md6 lg3>
            <material-stats-card color="orange" icon="filter_none" title="Broj zadataka koji nisu riješeni" v-model="model.brojZahtjevaKojiNisuRijeseni" />
        </v-flex>
        <v-flex class="card-padding" sm6 xs12 md6 lg3>
            <material-stats-card color="green" icon="check_circle" title="Broj završenih zadataka" v-model="model.brojZavrsenihZahtjeva"/>
        </v-flex>
        <v-flex v-if="imaPravo('osnovno_dashboard_broj_projekata')" class="card-padding" sm6 xs12 md6 lg3>
            <material-stats-card color="primary" icon="view_comfy" title="Broj projekata" v-model="model.brojProjekata" />
        </v-flex>
        <v-flex v-if="imaPravo('osnovno_dashboard_broj_komentara_korisnika_ukupno')" class="card-padding" sm6 xs12 md6 lg3>
            <material-stats-card color="green" icon="view_comfy" title="Ukupan broj komentara" v-model="model.brojKomentaraKorisnika"  />
        </v-flex>

        <!-- <v-layout align-center row fill-height>
            <v-icon size="80">add_circle</v-icon>
        </v-layout> -->

        <v-flex v-if="imaPravo('osnovno_dashboard_zavrseni_zahtjevi_prosla_sedmica')" class="card-padding" md12 sm12 lg4>
            <material-chart-card v-if="ucitanBrojZavrsenihZahtjevaPoDanimaProslaSedmica" :data="zavrseniZahtjeviProslaSedmicaChart.data" :options="zavrseniZahtjeviProslaSedmicaChart.options" color="green" type="Line">

                <h4 class="title font-weight-light">Završeni zadaci</h4>
                <p class="category d-inline-flex font-weight-light">
                    <v-icon color="green" small>
                        mdi-arrow-up
                    </v-icon>
                    <!--<span class="green--text">55%</span>&nbsp; -->
                    Ukupan broj završenih zadataka prošle sedmice po danima
                </p>

                <!--  <template slot="actions">
                    <v-icon class="mr-2" small>
                        mdi-clock-outline
                    </v-icon>
                    <span class="caption grey--text font-weight-light">updated 4 minutes ago</span> 
                </template>-->
            </material-chart-card>
        </v-flex>
        <v-flex v-if="imaPravo('osnovno_dashboard_broj_zahtjeva_po_projektu')" class="card-padding" md12 sm12 lg4>
            <material-chart-card v-if="ucitanBrojZahtjevaPoProjektu" :data="zahtjeviPoProjektimaChart.data" :options="zahtjeviPoProjektimaChart.options" :responsive-options="zahtjeviPoProjektimaChart.responsiveOptions" color="orange" type="Bar">
                <h4 class="title font-weight-light">Zadaci po projektima</h4>
                <p class="category d-inline-flex font-weight-light">Ukupan broj svih zadataka po projektima</p>
            </material-chart-card>
        </v-flex>
        <v-flex v-if="imaPravo('osnovno_dashboard_prijavljeni_zahtjevi_prosle_sedmice')" class="card-padding" md12 sm12 lg4>
            <material-chart-card v-if="ucitanBrojKreiranihZahtjevaPoDanimaProslaSedmica" :data="kreiraniZahtjeviProslaSedmicaChart.data" :options="kreiraniZahtjeviProslaSedmicaChart.options" color="info" type="Line">
                <h3 class="title font-weight-light">Prijavljeni zadaci</h3>
                <p class="category d-inline-flex font-weight-light">Ukupan broj prijavljenih zadataka prošle sedmice po danima</p>

            </material-chart-card>
        </v-flex>

        <v-flex v-if="imaPravo('osnovno_dashboard_broj_zahtjeva_po_mjesecima') " class="card-padding" md12 sm12 lg4>
            <material-chart-card v-if="ucitanBrojZahtjevaPoMjesecima" :data="zahtjeviPoMjesecimaChart.data" :options="zahtjeviPoMjesecimaChart.options" color="info" type="Line">
                <h3 class="title font-weight-light"> Zadaci</h3>
                <p class="category d-inline-flex font-weight-light">Ukupan broj zadataka po mjesecima</p>

            </material-chart-card>
        </v-flex>
        <v-flex v-if="imaPravo('osnovno_dashboard_broj_zahtjeva_po_danima_aktivna_godina')" class="card-padding" md12 sm12 lg4>
            <material-chart-card v-if="ucitanBrojZahtjevaPoDanimaAktivnaGodina" :data="zahtjeviPoDanimaAktivnaGodinaChart.data" :options="zahtjeviPoDanimaAktivnaGodinaChart.options" color="info" type="Line">
                <h3 class="title font-weight-light"> Zadaci</h3>
                <p class="category d-inline-flex font-weight-light">Ukupan broj kreiranih zadataka po danima, aktivna godina</p>

            </material-chart-card>
        </v-flex>

        <v-flex v-if="imaPravo('osnovno_dashboard_broj_rijesenih_zahtjeva_po_sedmicama')" class="card-padding" md12 sm12 lg4>
            <material-chart-card v-if="ucitanBrojZahtjevaPoSedmicama" :data="zahtjeviPoSedmicamaChart.data" :options="zahtjeviPoSedmicamaChart.options" color="info" type="Line">
                <h3 class="title font-weight-light"> Zadaci</h3>
                <p class="category d-inline-flex font-weight-light">Ukupan broj zadataka po prethodnim sedmicama</p>

            </material-chart-card>
        </v-flex>

        <v-flex v-if="imaPravo('osnovno_dashboard_zahtjevi_najduze_u_potrebno_uraditi')" class="card-padding" md12 lg6>
            <lista-zahtjeva-projekat v-if="ucitaniZahtjeviNajduzeUPotrebnoUraditi" :lista-zahtjeva="zahtjeviNajduzeUPotrebnoUraditi" :omoguceno-dodavanje-i-filtriranje="false" :naslov="nazivZahtjeviNajduzeUPotrebnoUraditi" headersi-za-prikaz="10101010000" :sakrij-prikaz-paginacije="true"></lista-zahtjeva-projekat>
        </v-flex>

        <v-flex v-if="imaPravo('osnovno_dashboard_zadnji_dodati_zahtjevi_potrebno_uraditi')" class="card-padding" md12 lg6>
            <lista-zahtjeva-projekat v-if="ucitaniZahtjeviZadnjiDodatiKojeJePotrebnoUraditi" :lista-zahtjeva="zahtjeviZadnjiDodatiKojeJePotrebnoUraditi" :omoguceno-dodavanje-i-filtriranje="false" :naslov="nazivZahtjeviZadnjiDodatiKojeJePotrebnoUraditi" headersi-za-prikaz="10101010000" :sakrij-prikaz-paginacije="true"></lista-zahtjeva-projekat>
        </v-flex>

        <v-flex v-if="imaPravo('osnovno_dashboard_zahtjevi_zadnji_dodati_nisu_rijeseni')" class="card-padding" md12 lg6>
            <lista-zahtjeva-projekat v-if="ucitaniZahtjeviZadnjiDodatiNisuRijeseni" :lista-zahtjeva="zahtjeviZadnjiDodatiNisuRijeseni" :omoguceno-dodavanje-i-filtriranje="false" :naslov="'Zahtjevi zadnji dodati koji nisu riješeni'" headersi-za-prikaz="10101010000" :sakrij-prikaz-paginacije="true"></lista-zahtjeva-projekat>
        </v-flex>

        <v-flex v-if="imaPravo('osnovno_dashboard_zahtjevi_rijeseni')" class="card-padding" md12 lg6>
            <lista-zahtjeva-projekat v-if="ucitaniZahtjeviZadnjiRijeseni" :lista-zahtjeva="zahtjeviZadnjiRijeseni" :omoguceno-dodavanje-i-filtriranje="false" :naslov="'Zahtjevi zadnji koji su riješeni'" headersi-za-prikaz="10101010000" :sakrij-prikaz-paginacije="true"></lista-zahtjeva-projekat>
        </v-flex>

    </v-layout>
</div>
</template>

<script>
import Identity from "auth/identity";
import Chartist from 'chartist';
import ListaZahtjevaProjekat from "./zahtjev/list.vue";
import ListaProjekata from "./projekat/list.vue";

import {
    DashboardResource,
    ProjekatResource
} from "api/resources";
import Blok from "./widgets/blok";

export default {
    name: "Dashboard",
    components: {
        ListaZahtjevaProjekat,
        ListaProjekata
    },

    data() {
        return {
            stiglo: false,
            ucitanBrojKreiranihZahtjevaPoDanimaProslaSedmica: false,
            ucitanBrojZavrsenihZahtjevaPoDanimaProslaSedmica: false,
            ucitanBrojZahtjevaPoProjektu: false,
            ucitaniZahtjeviNajduzeUPotrebnoUraditi: false,
            ucitaniZahtjeviZadnjiDodatiKojeJePotrebnoUraditi: false,
            ucitaniSviProjektiKorisnika: false,
            ucitanBrojZahtjevaPoSedmicama: false,
            ucitanBrojZahtjevaPoMjesecima: false,
            ucitanBrojZahtjevaPoDanimaAktivnaGodina: false,
            ucitaniZahtjeviZadnjiDodatiNisuRijeseni: false,
            ucitaniZahtjeviZadnjiRijeseni: false,
            identity: Identity,
            trenutnaUlogaId: {},

            model: {},
            zavrseniZahtjeviProslaSedmicaChart: {
                data: {
                    labels: ['Pon', 'Uto', 'Sri', 'Čet', 'Pet', 'Sub', 'Ned'],
                    datasets: [{
                        fill: false,
                        label: 'Dnevni broj zadataka',
                        borderColor: 'rgba(255, 255, 255, 0.8)',
                        backgroundColor: 'white',
                        lineTension: 0,
                        data: []
                    }],

                }
            },
            kreiraniZahtjeviProslaSedmicaChart: {
                data: {
                    labels: ['Pon', 'Uto', 'Sri', 'Čet', 'Pet', 'Sub', 'Ned'],
                    datasets: [{
                        fill: false,
                        label: 'Dnevni broj zadataka',
                        borderColor: 'rgba(255, 255, 255, 0.8)',
                        backgroundColor: 'white',
                        lineTension: 0,
                        data: []
                    }],

                }
            },
            zahtjeviPoProjektimaChart: {

                data: {
                    labels: [],
                    datasets: [{
                        fill: false,
                        label: 'Broj zadataka',
                        borderColor: 'rgba(255, 255, 255, 0.8)',
                        backgroundColor: 'white',
                        lineTension: 0,
                        data: []
                    }]
                },
                options: {
                    scales: {
                        yAxes: [{
                            ticks: {
                                beginAtZero: true
                            }
                        }]
                    }
                }
            },

            dataCompletedTasksChart: {
                data: {
                    labels: ['12am', '3pm', '6pm', '9pm', '12pm', '3am', '6am', '9am'],
                    datasets: [{
                        fill: false,
                        label: 'Completed Tasks',
                        borderColor: 'rgba(255, 255, 255, 0.8)',
                        backgroundColor: 'white',
                        lineTension: 0,
                        data: [230, 750, 450, 300, 280, 240, 200, 190]
                    }],
                }
            },
            emailsSubscriptionChart: {
                data: {
                    labels: ['Ja', 'Fe', 'Ma', 'Ap', 'Mai', 'Ju', 'Jul', 'Au', 'Se', 'Oc', 'No', 'De'],
                    datasets: [{
                        label: 'Email Subscription',
                        borderColor: 'rgba(255, 255, 255, 0.9)',
                        backgroundColor: 'rgba(255, 255, 255, 0.9)',
                        data: [542, 443, 320, 780, 553, 453, 326, 434, 568, 610, 756, 895]
                    }],
                },
            },

            zahtjeviPoMjesecimaChart: {
                data: {
                    labels: ['Jan', 'Feb', 'Mar', 'Apr', 'Maj', 'Jun', 'Jul', 'Aug', 'Sep', 'Okt', 'Nov', 'Dec'],
                    datasets: [{
                        fill: false,
                        label: 'Zadaci po mjesecima',
                        borderColor: 'rgba(255, 255, 255, 0.8)',
                        backgroundColor: 'white',
                        lineTension: 0,
                        data: []
                    }],

                },
            },

            zahtjeviPoDanimaAktivnaGodinaChart: {
                data: {
                    labels: ['Pon', 'Uto', 'Sri', 'Čet', 'Pet', "Sub", "Ned"],
                    datasets: [{
                        fill: false,
                        label: 'Zadaci po danima',
                        borderColor: 'rgba(255, 255, 255, 0.8)',
                        backgroundColor: 'white',
                        lineTension: 0,
                        data: []
                    }],

                },
            },

            zahtjeviPoSedmicamaChart: {
                data: {
                    labels: ['5', '4', '3', '2', '1'],
                    datasets: [{
                        fill: false,
                        label: 'Zadaci po sedmicama',
                        borderColor: 'rgba(255, 255, 255, 0.8)',
                        backgroundColor: 'white',
                        lineTension: 0,
                        data: []
                    }],

                },
            },
            zahtjeviNajduzeUPotrebnoUraditi: [],
            zahtjeviZadnjiDodatiKojeJePotrebnoUraditi: [],
            nazivZahtjeviNajduzeUPotrebnoUraditi: "Zadaci najduže u statusu: potrebno uraditi",
            nazivZahtjeviZadnjiDodatiKojeJePotrebnoUraditi: "Zadnji dodati zadaci koje potrebno uraditi",
            zahtjeviZadnjiDodatiNisuRijeseni: [],
            zahtjeviZadnjiRijeseni: [],

            tabs: 0,
            list: {
                0: false,
                1: false,
                2: false
            },
            lozinka: "",

        };
    },

    created() {

            this.ulogaPorvjera();

        this.ucitajStatistikuZaDashboard();

        if (Identity.imaPravo('osnovno_dashboard_zavrseni_zahtjevi_prosla_sedmica'))
            this.ucitajBrojZavrsenihZahtjevaPoDanimaProslaSedmica();

        if (Identity.imaPravo('osnovno_dashboard_prijavljeni_zahtjevi_prosle_sedmice'))
            this.ucitajBrojKreiranihZahtjevaPoDanimaProslaSedmica();

        if (Identity.imaPravo('osnovno_dashboard_broj_zahtjeva_po_projektu'))
            this.ucitajBrojZahtjevaPoProjektu();

        if (Identity.imaPravo('osnovno_dashboard_zahtjevi_najduze_u_potrebno_uraditi'))
            this.ucitajZahtjeviNajduzeUPotrebnoUraditi();

        if (Identity.imaPravo('osnovno_dashboard_zadnji_dodati_zahtjevi_potrebno_uraditi'))
            this.ucitajZahtjeviZadnjiDodatiKojeJePotrebnoUraditi();

        if (Identity.imaPravo('osnovno_dashboard_broj_zahtjeva_po_mjesecima'))
            this.ucitajBrojZahtjevaPoMjesecima();

        if (Identity.imaPravo('osnovno_dashboard_broj_zahtjeva_po_danima_aktivna_godina'))
            this.ucitajBrojZahtjevaPoDanimaAktivnaGodina();

        if (Identity.imaPravo('osnovno_dashboard_zahtjevi_zadnji_dodati_nisu_rijeseni'))
            this.ucitajZahtjeviZadnjiDodatiNisuRijeseni();

        if (Identity.imaPravo('osnovno_dashboard_zahtjevi_rijeseni'))
            this.ucitajZahtjeviZadnjiRijeseni();

        if (Identity.imaPravo('osnovno_dashboard_broj_rijesenih_zahtjeva_po_sedmicama'))
            this.ucitajBrojZahtjevaPoSedmicama();

        window.addEventListener("resize", this.handleResize);
    },
    methods: {

        ucitajStatistikuZaDashboard() {
            if (this.$route.name == "home.dashboard") {
                var that = this;

                var success = model => {
                    that.stiglo = true;
                    that.model = model.body;
                    that.model.brojZahtjevaPotrebnoUraditi = that.model.brojZahtjevaPotrebnoUraditi.toString();
                    that.model.brojZahtjevaUProgresu = that.model.brojZahtjevaUProgresu.toString();
                    that.model.brojZavrsenihZahtjeva = that.model.brojZavrsenihZahtjeva.toString();
                    that.model.brojProjekata = that.model.brojProjekata.toString();

                    that.model.brojZahtjevaUkupno = that.model.brojZahtjevaUkupno.toString();
                    that.model.brojZahtjevaKojiNisuRijeseni = that.model.brojZahtjevaKojiNisuRijeseni.toString();
                    that.model.brojKomentaraKorisnika = that.model.brojKomentaraKorisnika.toString();

                    // that.zavrseniZahtjeviProslaSedmicaChart.data.datasets[0].data = model.body.brojZavrsenihZahtjevaPoDanimaProslaSedmica;

                };
                DashboardResource()
                    .statistika()
                    .then(success);
            }
        },

        ulogaPorvjera(){
            var that = this;
            
            console.log(that.identity.uloga());
            if(that.identity.uloga() == 'Task user')
                 {
            that.$router.push({name:'home.zahtjev.list'});
            
             }
        },

        ucitajBrojZavrsenihZahtjevaPoDanimaProslaSedmica() {
            var that = this;

            var success = model => {
                that.ucitanBrojZavrsenihZahtjevaPoDanimaProslaSedmica = true;

                that.zavrseniZahtjeviProslaSedmicaChart.data.datasets[0].data = model.body;

            };

            var error = () => {
                console.log("nije uspjelo");
                //that.$toast.error('Nisu učitani podaci.');
            };

            var promise = DashboardResource().zavrseniZahtjeviPoDanimaProslaSedmica();
            promise.then(success, error);
        },

        ucitajBrojKreiranihZahtjevaPoDanimaProslaSedmica() {
            var that = this;

            var success = model => {
                that.ucitanBrojKreiranihZahtjevaPoDanimaProslaSedmica = true;
                that.kreiraniZahtjeviProslaSedmicaChart.data.datasets[0].data = model.body;

            };

            DashboardResource().kreiraniZahtjeviPoDanimaProslaSedmica().then(success);
        },
        ucitajBrojZahtjevaPoProjektu() {
            var that = this;

            var success = model => {
                that.ucitanBrojZahtjevaPoProjektu = true;

                model.body.forEach(element => {
                    if (element.naziv.length > 17) {
                        that.zahtjeviPoProjektimaChart.data.labels.push(element.naziv.slice(0, 14) + "...");

                    } else {
                        that.zahtjeviPoProjektimaChart.data.labels.push(element.naziv);
                    }
                });

                model.body.forEach(element => {
                    that.zahtjeviPoProjektimaChart.data.datasets[0].data.push(element.ukupanBrojZahtjeva);
                });
            };

            DashboardResource().brojZahtjevaPoProjektu().then(success);
        },
        ucitajZahtjeviNajduzeUPotrebnoUraditi() {
            var that = this;

            var success = model => {
                that.ucitaniZahtjeviNajduzeUPotrebnoUraditi = true;
                that.zahtjeviNajduzeUPotrebnoUraditi = model.body;

            };

            DashboardResource().zahtjeviNajduzeUPotrebnoUraditi().then(success);
        },
        ucitajZahtjeviZadnjiDodatiKojeJePotrebnoUraditi() {
            var that = this;

            var success = model => {
                that.ucitaniZahtjeviZadnjiDodatiKojeJePotrebnoUraditi = true;
                that.zahtjeviZadnjiDodatiKojeJePotrebnoUraditi = model.body;

            };

            DashboardResource().zahtjeviZadnjiDodatiKojeJePotrebnoUraditi().then(success);
        },
        ucitajSveProjekteKorisnika() {
            var that = this;

            var success = model => {
                that.ucitaniSviProjektiKorisnika = true;

            };
            ProjekatResource().vratiSveProjekte().then(success);

            // DashboardResource().sviProjektiKorisnika().then(success);
        },
        ucitajBrojZahtjevaPoDanimaAktivnaGodina() {
            var that = this;

            var success = model => {
                that.ucitanBrojZahtjevaPoDanimaAktivnaGodina = true;
                that.zahtjeviPoDanimaAktivnaGodinaChart.data.datasets[0].data = model.body;

            };
            DashboardResource().zahtjeviPoDanimaAktivnaGodina().then(success);
        },

        ucitajBrojZahtjevaPoMjesecima() {
            var that = this;

            var success = model => {
                that.ucitanBrojZahtjevaPoMjesecima = true;
                that.zahtjeviPoMjesecimaChart.data.datasets[0].data = model.body;

            };
            DashboardResource().zahtjeviPoMjesecima().then(success);
        },

        ucitajBrojZahtjevaPoSedmicama() {
            var that = this;

            var success = model => {
                that.ucitanBrojZahtjevaPoSedmicama = true;
                that.zahtjeviPoSedmicamaChart.data.datasets[0].data = model.body;

            };
            DashboardResource().zahtjeviPoSedmicama().then(success);
        },

        ucitajZahtjeviZadnjiDodatiNisuRijeseni() {
            var that = this;

            var success = model => {
                that.ucitaniZahtjeviZadnjiDodatiNisuRijeseni = true;
                that.zahtjeviZadnjiDodatiNisuRijeseni = model.body;

            };
            DashboardResource().zahtjeviZadnjiDodatiNisuRijeseni().then(success);
        },

        ucitajZahtjeviZadnjiRijeseni() {
            var that = this;

            var success = model => {
                that.ucitaniZahtjeviZadnjiRijeseni = true;
                that.zahtjeviZadnjiRijeseni = model.body;

            };
            DashboardResource().zahtjeviZadnjiDodatiRijeseni().then(success);
        },
        /* updateData() {
             if (this.$route.name == "home.dashboard") {
                 var that = this;

                 var success = model => {
                     that.model = model.body;

                 };
                 DashboardResource()
                     .statistika()
                     .then(success);
             }
         },*/

        handleResize() {
            if (this.$refs.onlinechart != null) {
                this.$refs.onlinechart.drawChart();
            }
        }
    }
};
</script>
