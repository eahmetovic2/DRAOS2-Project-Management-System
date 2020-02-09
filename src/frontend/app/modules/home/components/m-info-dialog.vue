<template>
<v-layout>

    <v-btn @click.native.stop="dialog = true" flat>Normal</v-btn>
    <v-dialog v-model="dialog">
        <v-card>
            <v-card-title class="headline">{{ title }}</v-card-title>
            <v-card-text>{{ content }}</v-card-text>
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn class="green--text darken-1" flat="flat" @click.native="dialog = false">{{ okDugme }}</v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</v-layout>
</template>

<script>
export default {
    name: 'MInfoDialog',
    props: {
        title: {
            default: null
        },
        content: {
            default: null
        },
        okDugme: {
            default: "Uredu"
        }
    },
    data() {
        return {
            dialog: false
        };
    }
};
</script>
