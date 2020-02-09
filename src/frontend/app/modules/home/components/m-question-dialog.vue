<template>
<v-layout row justify-center style="position: relative;">
    <v-dialog v-model="aktivan" persistent :max-width="width">
        <v-card class="upis-dialog">
            <v-card-title class="headline">{{header}}</v-card-title>
            <v-card-text>{{body}}</v-card-text>
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="primary" @click="$emit('close', {potvrdi: true})">{{okDugme}}</v-btn>
                <v-btn flat="flat" @click="$emit('close', {potvrdi: false})">{{odustani}}</v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</v-layout>
</template>

<script>
export default {
    name: "MQuestionDialog",
    props: {
        aktivan: {
            type: Boolean
        },
        header: {
            default: null
        },
        body: {
            default: null
        },
        okDugme: {
            default: "Uredu"
        },
        odustani: {
            default: "Odustani"
        },
        width: {
            type: String,
            default: "500"
        }
    },
    data() {
        return {
            dialog: false
        };
    }
};
</script>
