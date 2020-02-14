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
        GSTC,
    },
    props: ['omogucenoDodavanjeIFiltriranje'],

    data() {
        return {
            model: [],
            config: {
                height: 500,
                list: {
                    rows: {
                        /* "1": {
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
                        } */
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
                        /* "1": {
              id: "1",
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
            }, */
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
                    //zadaci["" + tasks[i].id + ""] = tasks[i];
                    this.$set(that.config.list.rows, tasks[i].id, tasks[i])    ;
                }
                console.log("that.config.list.rows", that.config.list.rows)
                //that.config.list.rows = zadaci;

                var objekti = that.model.map(function(v){
                    var boja = 'green';
                    if(v.zahtjevStatus == 'Potrebno uraditi') {
                        boja = 'red';
                    }
                    else if(v.zahtjevStatus == 'Radi se') {
                        boja = 'orange';
                    }
                     
                    return {
                        id: "" + v.id + "",
                        rowId: "" + v.id + "",
                        label: v.zahtjevStatus,
                        time: {
                            start: new Date(v.pocetakIzrade).getTime(),
                            end: new Date(v.pocetakIzrade).getTime() + 24 * 60 * 60 * 1000
                        } ,
                        style: {background:boja}
                    }}
                );
                for(i= 0; i<objekti.length-1; i++) {   
                    br = i+1;             
                    objekti[i].time.end = objekti[i+1].time.start;
                }
                for(i= 0; i<objekti.length; i++) {   
                    br = i+1;             
                    this.$set(that.config.chart.items, objekti[i].id, objekti[i])    ;
                    //gant[objekti[i].id] = objekti[i];
                }
                //that.config.chart.items = gant;
                console.log("GANT", that.config.chart.items)
                console.log("objekti", objekti)

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
    beforeDestroy() {
        subs.forEach(unsub => unsub());
    }

};
</script>
