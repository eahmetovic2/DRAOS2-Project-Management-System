<template>
<v-card class="blok-card">
    <v-card-title primary-title>
        <v-layout row class="text-xs-center">
            <v-flex>
                {{naziv}}
            </v-flex>
        </v-layout>
    </v-card-title>
    <v-card-text>
        <p class="card-text text-xs-center">{{vrijednost}}</p>
    </v-card-text>

</v-card>
</template>

<script>
export default {
    name: 'blok',

    props: {
        naziv: {
            type: String,
            default: ''
        },
        vrijednost: {
            default: ''
        },
        klasa: {
            type: String,
            default: ''
        },
    },
};
</script>

<style>
.blok-card .v-card-title {
    font-size: 16px;
}

.blok-card .card-text {
    font-size: 28px;
}
</style>
