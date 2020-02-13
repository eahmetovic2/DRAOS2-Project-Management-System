<template>
<page>
    <material-card color="primary">
        <v-flex slot="header">
            <v-layout row justify-space-between>
                <v-flex>
                    <span class="card-naslov">Tok projekta </span>
                </v-flex>
            </v-layout>
        </v-flex>

        <v-card-text>
            
    <GSTC :config="config" @state="onState" />
            
        </v-card-text>
    </material-card>
</page>
</template>

<script>
import {
    ZahtjevResource
} from 'api/resources';
import GSTC from "../components/GSTC";
let subs = [];
export default {
    name: 'TokProjekta',
    components: {
        GSTC
    },
    props: ['omogucenoDodavanjeIFiltriranje'],

    data() {
        return {
            model: [],
            config: {
                height: 500,
                list: {
                    rows: {
                        "1": {
                        id: "1",
                        label: "Row 1"
                        },
                        "2": {
                        id: "2",
                        label: "Row 2"
                        },
                        "3": {
                        id: "3",
                        label: "Row 3"
                        },
                        "4": {
                        id: "4",
                        label: "Row 4"
                        }
                    },
                    columns: {
                        data: {
                        label: {
                            id: "label",
                            data: "label",
                            width: 200,
                            header: {
                            content: "Zadatak"
                            }
                        }
                        }
                    }
                },
                chart: {
                    items: {
                        "a1": {
              id: "a1",
              rowId: "1",
              label: "Item 1",
              time: {
                start: new Date().getTime(),
                end: new Date().getTime() + 24 * 60 * 60 * 1000
              }
            },
            "2": {
              id: "2",
              rowId: "2",
              label: "Item 2",
              time: {
                start: new Date().getTime() + 4 * 24 * 60 * 60 * 1000,
                end: new Date().getTime() + 5 * 24 * 60 * 60 * 1000
              }
            },
            "3": {
              id: "3",
              rowId: "2",
              label: "Item 3",
              time: {
                start: new Date().getTime() + 6 * 24 * 60 * 60 * 1000,
                end: new Date().getTime() + 7 * 24 * 60 * 60 * 1000
              }
            },
            "4": {
              id: "4",
              rowId: "3",
              label: "Item 4",
              time: {
                start: new Date().getTime() + 10 * 24 * 60 * 60 * 1000,
                end: new Date().getTime() + 12 * 24 * 60 * 60 * 1000
              }
            },
                    }
                }
            }
        };
    },

    created() {
        this.vratiTokProjekta();
        const state = GSTC.api.stateFromConfig(this.config);
        console.log("STATE", state)
    },


    methods: {
        vratiTokProjekta() {
            var that = this;
            var success = (model) => {
                that.model = model.body.items;
                var tasks = that.model.map(function(v){
                    return {
                        label: v.naziv ,
                        id: v.id                  
                    };
                });
                var zadaci = {};
                for(var i= 0; i<tasks.length; i++) {   
                    var br = i+1;                 
                    zadaci["" + tasks[i].id + ""] = tasks[i];
                }
                that.config.list.rows = zadaci;

                var objekti = that.model.map(function(v){
                    return {
                        id: "a" + v.id + "",
                        rowId: "" + v.id + "",
                        label: v.naziv,
                        time: {
                            start: v.pocetakIzrade,
                            end: v.pocetakIzrade
                        } 
                    }}
                );
                var gant = {};
                for(i= 0; i<objekti.length; i++) {   
                    br = i+1;                 
                    gant[objekti[i].id] = objekti[i];
                }
                that.config.chart.items = gant;
                console.log("GANT", gant)

            };

            var error = () => {
                that.$toast.error("Filtriranje nije uspješno");
            };

            var inputs = {
                projekatId: this.$route.params.projekatId,
            };
            var promise = ZahtjevResource().vratiSveZahtjeveProjekta(inputs);

            promise.then(success, error);
        },
        onState(state) {
            console.log("ON STATE", state)
            this.state = state;
            subs.push(
                state.subscribe("config.chart.items.1", item => {
                console.log("item 1 changed", item);
                })
            );
            subs.push(
                state.subscribe("config.list.rows.1", row => {
                console.log("row 1 changed", row);
                })
            );
        }
    },
    mounted() {
        setTimeout(() => {
            const item1 = this.config.chart.items["a1"];
            item1.label = "label changed dynamically";
            item1.time.end += 2 * 24 * 60 * 60 * 1000;
        }, 2000);
    },
    beforeDestroy() {
        subs.forEach(unsub => unsub());
    }

};
</script>
