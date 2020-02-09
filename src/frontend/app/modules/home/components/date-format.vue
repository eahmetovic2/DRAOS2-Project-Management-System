<style>
.date {
    white-space: nowrap;
}
</style>

<template>
<span class="date">{{vrijednost | moment(format)}}</span>
</template>

<script>
export default {
    name: 'DateFormat',

    props: {
        vrijednost: {
            default: null
        },
        format: {
            default: 'DD. MM. YYYY.'
        }
    },

};
</script>
