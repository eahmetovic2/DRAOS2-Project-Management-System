<template>
<div>
    <v-app>
        <v-dialog v-model="dialog" persistent :max-width="width">
            <v-card>
                <v-card-title>
                    <div class="headline">Upozorenje</div>
                </v-card-title>
                <v-card-text>{{poruka}} </v-card-text>
                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn :class=buttonClass :outline=outline flat="flat" @click.native="odgovori(false)">{{ne}}</v-btn>
                    <v-btn :class=buttonClass :outline=outline flat="flat" @click.native="odgovori(true)">{{da}}</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </v-app>
</div>
</template>

<script>
export default {
    name: "Upit",
    props: {
        poruka: {
            default: "Jeste li sigurni da želite napustiti trenutnu formu?"
        },
        da: {
            default: "Da"
        },
        ne: {
            default: "Ne"
        },
        buttonClass: {
            default: "secondary--text darken-1"
        },
        width: {
            default: 290
        },
        outline: {
            default: false
        }
    },
    data() {
        return {
            dialog: {}
        };
    },

    methods: {
        odgovori(result) {
            this.dialog = false;
            this.$emit("odgovor", result);
        }
    }
};
</script>
