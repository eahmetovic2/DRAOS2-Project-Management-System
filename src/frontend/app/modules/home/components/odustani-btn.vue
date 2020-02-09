<template>
<v-btn color="tertiary" @click="odustani()">
    Odustani</v-btn>
</template>

<script>
export default {
    name: 'OdustaniBtn',
    methods: {
        odustani() {
            this.$emit('odustani', true);
            this.$router.go(-1);
        }
    }
};
</script>
