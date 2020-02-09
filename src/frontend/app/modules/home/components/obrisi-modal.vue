<template>
<v-layout row justify-center style="position: relative;">
    <v-dialog v-model="aktivan" persistent :max-width="width">
        <v-card>
            <v-card-title class="headline">{{header}}</v-card-title>
            <v-card-text class="mb-3">
                {{body}}</v-card-text>
            <v-card-actions class="text-xs-center">
                <v-spacer></v-spacer>
                <v-btn color="tertiary" @click="$emit('close', {obrisi: false, id: item})">{{odustani}}</v-btn>
                <v-btn primary @click="$emit('close', {obrisi: true, id: item})">{{obrisi}}</v-btn>
                <v-spacer></v-spacer>
            </v-card-actions>
        </v-card>
    </v-dialog>
</v-layout>
</template>

<script>
export default {
    name: "ObrisiModal",
    props: {
        aktivan: {
            type: Boolean
        },
        header: {
            type: String
        },
        body: {
            type: String
        },
        obrisi: {
            type: String,
            default: "Obriši"
        },
        odustani: {
            type: String,
            default: "Odustani"
        },
        width: {
            type: String,
            default: "290"
        },
        item: {
            type: Number
        }
    }
};
</script>

<style>
.modal-mask {
    position: fixed;
    z-index: 9998;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.5);
    display: table;
    transition: opacity 0.3s ease;
}

.modal-container {
    width: 300px;
    margin: 50px auto;
    padding: 20px 30px;
    background-color: #fff;
    transition: all 0.3s ease;
}

.modal-enter {
    opacity: 0;
}

.modal-leave-active {
    opacity: 0;
}

.modal-enter .modal-container,
.modal-leave-active .modal-container {
    -webkit-transform: scale(1.1);
    transform: scale(1.1);
}
</style>
