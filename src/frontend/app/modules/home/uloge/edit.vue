<template>
<page >        
    <template v-if="resolved">
        <material-card :title="'Izmjena uloge ' + naziv" color="primary" text="Osnovne informacije">
            <v-card-text>
                <v-form v-model="validOsnovno" ref="formosnovno">
                    <v-text-field :rules="rules.obavezno100" :counter="100" label="Naziv" v-model="resolved.uloga.naziv" required class="required"></v-text-field>
                    <v-text-field :rules="rules.obavezno100" :counter="100" label="Šifra" v-model="resolved.uloga.sifra" required class="required"></v-text-field>
                    <v-select :rules="rules.obavezno" required class="required" :items="resolved.frontendModuli" item-text="naziv" item-value="id" v-model="resolved.uloga.frontendModulId" label="Korisnički interfejs" bottom></v-select>
                    <v-select :rules="rules.obavezno" :multiple="true" required class="required" :items="resolved.uloge.items" item-text="naziv" item-value="id" v-model="resolved.uloga.dozvoljeneUlogeZaUpravljanje" label="Dozvoljene uloge za dodavanje" bottom></v-select>
                    <v-layout class="mt-3">
                        <v-spacer />
                        <odustani-btn />
                        <v-btn color="primary" @click="snimiOsnovno">Snimi</v-btn>
                    </v-layout>
                </v-form>
            </v-card-text>
        </material-card>
        
        <material-card class="pt-3" title="Potrebne dodatne informacije" color="primary">
            <v-card-text>
                <v-form>
                    <v-list subheader two-line>
                        <v-list-tile v-for="dodatno in resolved.korisnikDodatneInformacijeUcenik" :key="dodatno.id">
                            <v-list-tile-action>
                                <v-checkbox :input-value="potrebnoDodatno(dodatno)" @change="updatePotrebnoDodatno($event, dodatno)"></v-checkbox>
                            </v-list-tile-action>

                            <v-list-tile-content>
                                <v-list-tile-title>{{ dodatno.naziv }}</v-list-tile-title>
                                <v-list-tile-sub-title>{{ dodatno.opis }}</v-list-tile-sub-title>
                            </v-list-tile-content>
                        </v-list-tile>
                    </v-list>
                    <v-layout class="mt-3">
                        <v-spacer />
                        <odustani-btn />
                        <v-btn color="primary" @click="snimiDodatno">Spasi</v-btn>
                    </v-layout>
                </v-form>
            </v-card-text>
        </material-card>
        <material-card color="primary" class="pt-3">
            <v-flex slot="header">
                <v-layout row justify-space-between>
                    <v-flex xs8>
                        <span class="card-naslov">Dozvoljene akcije </span>
                    </v-flex>
                    <v-flex xs4 class="text-xs-right toolbar-autocomplete">
                            <v-autocomplete class="ml-3 " hide-details item-text="naziv" item-value="id" :items="resolved.grupe.items" v-model="odabranaGrupa" return-object label="Grupa"></v-autocomplete>
                        </v-flex>
                </v-layout>
            </v-flex>
            <v-form v-model="valid" ref="form">
                <v-card-text>
                    <template v-if="odabranaGrupa != null">
                        <v-layout row wrap>
                            <template v-if="odabranaGrupa.pravoObjekti.items.length">
                                <v-flex class="elevation-1 mb-3" xs12 v-for="objekt in odabranaGrupa.pravoObjekti.items" :key="objekt.id">
                                    <v-list subheader two-line>
                                        <v-subheader>{{ objekt.naziv }}</v-subheader>
                                        <template v-if="objekt.pravoAkcije.items.length">
                                            <v-list-tile v-for="akcija in objekt.pravoAkcije.items" :key="akcija.id">
                                                <v-list-tile-action>
                                                    <v-checkbox :input-value="getDozvoljena(akcija)" @change="updateDozvoljeno($event, akcija)"></v-checkbox>
                                                </v-list-tile-action>

                                                <v-list-tile-content>
                                                    <v-list-tile-title>{{ akcija.naziv }}</v-list-tile-title>
                                                    <v-list-tile-sub-title>{{ akcija.opis }}</v-list-tile-sub-title>
                                                </v-list-tile-content>
                                            </v-list-tile>
                                        </template>
                                        <template v-else>
                                            <div class="ma-4">Objekt nema akcija</div>
                                        </template>

                                    </v-list>
                                </v-flex>
                            </template>
                            <template v-else>
                                <div class="ma-4">Grupa nema objekata</div>
                            </template>

                        </v-layout>
                        <v-layout class="mt-3">
                            <v-spacer />
                            <odustani-btn />
                            <v-btn color="primary" @click="snimiDozvoljeno">Spasi</v-btn>
                        </v-layout>
                    </template>
                </v-card-text>
            </v-form>
        </material-card>
    </template>
</page>
</template>

<script>
import {
    UlogaResource
} from "api/resources";

import {
    UlogaTipoviDodatneInformacijeResource
} from "api/resources";

import {
    SifarnikResource
} from "api/resources";

export default {
    name: "EditUloga",
    data() {
        return {
            odabranaGrupa: null,
            naziv: this.$route.query.naziv,
            ulogaId: this.$route.params.ulogaId,
            resolved: null,
            validOsnovno: true,
            dodatneInformacije: [{
                odabrano: false,
                naziv: "SkolaId",
                opis: "Ovo se odnosi na uloge kao npr. Uprava škole"
            }, {
                odabrano: false,
                naziv: "OsobaId",
                opis: "Ovo se odnosi na uloge kao npr. Nastavnik"
            }, {
                odabrano: false,
                naziv: "UcenikId",
                opis: "Ovo se odnosi na uloge kao npr. Učenik"
            }],
            rules: {
                obavezno: [v => !!v || "Polje je obavezno"],
                obavezno100: [
                    v => !!v || "Polje je obavezno",
                    v =>
                    (!!v && v.length <= 100) || "Polje mora biti manje od 100 karaktera"
                ]
            }
        };
    },
    methods: {
        snimiOsnovno() {
            var that = this;

            this.$refs.formosnovno.validate();
            this.focusInvalidInput(this.$refs.formosnovno);

            if (!this.validOsnovno) return;

            var success = () => {
                this.proslijedi = true;
                that.$toast.success("Uspješno snimljene osnovne informacije");
            };
            var error = () => {
                that.$toast.error(
                    "Izmjena uloge nije uspjela. Provjerite unesene podatke."
                );
            };
            console.log(that.resolved.uloga, that.$route.params.ulogaId)
            var promise = UlogaResource().update({
                ulogaId: that.$route.params.ulogaId
            }, that.resolved.uloga);

            promise.then(success, error);
        },
        snimiDozvoljeno() {
            var model = this.resolved.dozvoljeneAkcije.items.map(a => a.pravoAkcijaId);
            var that = this;

            function success() {
                that.$toast.success("Uspješno snimljene dozvoljene akcije");
            }

            function error() {
                that.$toast.error("Dogodila se greška");
            }

            UlogaResource().snimiDozvoljeneAkcije({
                ulogaId: this.ulogaId
            }, {
                ulogaId: this.ulogaId,
                akcije: model
            }).then(success, error);
        },

        snimiDodatno() {
            var model = this.resolved.dodatneInformacije.items.map(a => a.korisnikTipDodatneInformacijeId);
            var that = this;

            function success() {
                that.$toast.success("Uspješno snimljene potrebne dodatne informacije");
            }

            function error() {
                that.$toast.error("Dogodila se greška");
            }

            UlogaTipoviDodatneInformacijeResource().snimiZaUlogu({
                ulogaId: this.ulogaId
            }, {
                ulogaId: this.ulogaId,
                dodatneInformacije: model
            }).then(success, error);
        },


        getDozvoljena(akcija) {
            return this.resolved.dozvoljeneAkcije.items.some(a => a.pravoAkcijaId === akcija.id);
        },

        potrebnoDodatno(dodatno) {
            return this.resolved.dodatneInformacije.items.some(a => a.korisnikTipDodatneInformacijeId === dodatno.id);
        },

        updateDozvoljeno(vrijednost, akcija) {
            if (!vrijednost) {
                this.resolved.dozvoljeneAkcije.items = this.resolved.dozvoljeneAkcije.items.filter(a => {
                    return a.pravoAkcijaId !== akcija.id;
                });
            } else if (!this.resolved.dozvoljeneAkcije.items.some(a => a.pravoAkcijaId === akcija.id)) {
                this.resolved.dozvoljeneAkcije.items.push({
                    pravoAkcijaId: akcija.id,
                    ulogaId: this.ulogaId
                });
            }
        },

        updatePotrebnoDodatno(vrijednost, dodatno) {
            if (!vrijednost) {
                this.resolved.dodatneInformacije.items = this.resolved.dodatneInformacije.items.filter(a => {
                    return a.korisnikTipDodatneInformacijeId !== dodatno.id;
                });
            } else if (!this.resolved.dodatneInformacije.items.some(a => a.korisnikTipDodatneInformacijeId === dodatno.id)) {
                this.resolved.dodatneInformacije.items.push({
                    korisnikTipDodatneInformacijeId: dodatno.id,
                    ulogaId: this.ulogaId
                });
            }
        }
    },
    resolve: {
        uloga() {
            return UlogaResource().get({
                ulogaId: this.ulogaId
            });
        },
        uloge() {
            return UlogaResource().get();
        },
        grupe() {
            return UlogaResource().grupePrava({
                ulogaId: this.ulogaId
            });
        },
        dozvoljeneAkcije() {
            return UlogaResource().dozvoljeneAkcije({
                ulogaId: this.ulogaId
            });
        },
        dodatneInformacije() {
            return UlogaTipoviDodatneInformacijeResource().sveZaUlogu({
                ulogaId: this.ulogaId
            });
        },
        korisnikDodatneInformacijeUcenik() {
            return SifarnikResource().get({
                sifarnik: 'KorisnikTipDodatneInformacije'
            });
        },
        frontendModuli() {
            return SifarnikResource().get({
                sifarnik: 'FrontendModul'
            });
        }
    }
};
</script>
