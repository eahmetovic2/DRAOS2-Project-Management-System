<style>
#iframeHolder {
    height: 800px;
}

.lista-izvjestaja.application .theme--light.v-subheader, .theme--light .v-subheader {
    font-weight: bold;
    color: black;
    background-color: whitesmoke;
}
</style>

<template>
<page :resolved="resolved">
    <v-card v-if="resolved">
        <v-card-title primary-title class="grey lighten-4">
            Izvještaji
            <v-btn class="glavni-naslov" @click="() => this.activeHelpTip = true" flat icon color="secondary">
                <v-icon>help</v-icon>
            </v-btn>
        </v-card-title>
        <v-card-text>
            <v-form v-model="valid" ref="form">
                <div class="lista-izvjestaja">
                    <v-autocomplete :items="resolved.izvjestaji" return-object v-model="izvjestaj" label="Odaberite izvještaj" item-text="naziv" item-value="id" bottom required class="required"></v-autocomplete>
                </div>
                <div class="text-xl-right my-3">
                    <v-btn @click="ocisti">Očisti</v-btn>
                    <v-btn secondary @click="onSubmit">Prikaži</v-btn>
                </div>
            </v-form>
        </v-card-text>
    </v-card>
    <br />
    <v-card>
        <v-card-text id="iframeHolder">
            <o-iframe :url="izvjestajUrl" />
        </v-card-text>
    </v-card>
    <help-tip-dialog v-if="activeHelpTip" :title="VratiTitlePoId('izvjestaj-list')" :content="VratiContentPoId('izvjestaj-list')" @zatvori="ZatvoriHelpDialog"> </help-tip-dialog>
</page>
</template>

<script>
import {
    SifarnikResource
} from "api/resources";
import {
    TokenResource
} from "api/resources";
import Identity from "auth/identity";
import oiframe from "modules/home/components/o-iframe";
import HelpTipDialogMixin from 'helpers/help-tip-dialog-mixin';
export default {
    name: "ListaIzvjestaja",
    mixins: [HelpTipDialogMixin],
    data() {
        return {
            activeHelpTip: false,
            resolved: null,
            izvjestaj: null,
            izvjestajUrl: null,
            identity: Identity
        };
    },
    created() {
        this.$watch(() => this.resolved, this.ucitajIzvjestaje);
    },
    methods: {
        ocisti() {
            this.izvjestajId = null;
        },
        ucitajIzvjestaje() {
            if (!this.resolved) return;
            var ukupnoIzvjestaji = this.resolved.izvjestaji;
            this.resolved.izvjestaji = [];

            var lista = ukupnoIzvjestaji.filter(item => item.grupa == 1);
            if (lista.length > 0) {
                this.resolved.izvjestaji.push({
                    header: "Liste"
                });
                this.resolved.izvjestaji = this.resolved.izvjestaji.concat(lista);
            }

            var statisticiPrikaz = ukupnoIzvjestaji.filter(item => item.grupa == 2);
            if (statisticiPrikaz.length > 0) {
                this.resolved.izvjestaji.push({
                    divider: true
                });
                this.resolved.izvjestaji.push({
                    header: "Statistički prikaz"
                });
                this.resolved.izvjestaji = this.resolved.izvjestaji.concat(
                    statisticiPrikaz
                );
            }
            var grafickiPrikaz = ukupnoIzvjestaji.filter(item => item.grupa == 3);
            if (grafickiPrikaz.length > 0) {
                this.resolved.izvjestaji.push({
                    divider: true
                });
                this.resolved.izvjestaji.push({
                    header: "Grafički prikaz"
                });
                this.resolved.izvjestaji = this.resolved.izvjestaji.concat(
                    grafickiPrikaz
                );
            }
            var onlineUpis = ukupnoIzvjestaji.filter(item => item.grupa == 5);
            if (onlineUpis.length > 0) {
                this.resolved.izvjestaji.push({
                    divider: true
                });
                this.resolved.izvjestaji.push({
                    header: "Online upis"
                });
                this.resolved.izvjestaji = this.resolved.izvjestaji.concat(onlineUpis);
            }
            var ostali = ukupnoIzvjestaji.filter(item => item.grupa == 4);
            if (ostali.length > 0) {
                this.resolved.izvjestaji.push({
                    divider: true
                });
                this.resolved.izvjestaji.push({
                    header: "Ostali"
                });
                this.resolved.izvjestaji = this.resolved.izvjestaji.concat(ostali);
            }
        },
        onSubmit() {
            var that = this;
            if (!this.izvjestaj) return;

            var success = function (model) {
                var url = that.izvjestaj.data + "&ttxid=" + model.body.id;
                that.izvjestajUrl = url;
            };

            TokenResource()
                .temp({
                    korisnickoIme: Identity.korisnickoIme()
                }, {
                    korisnickoIme: Identity.korisnickoIme()
                })
                .then(success);
        }
    },

    components: {
        oiframe
    },

    resolve: {
        izvjestaji: function () {
            return SifarnikResource().get({
                sifarnik: "Izvjestaj"
            });
        }
    }
};
</script>
