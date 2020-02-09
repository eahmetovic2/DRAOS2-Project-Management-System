<style lang="scss" scoped>
.v-badge__badge {
    max-height: 2px !important;
    font-size: 2px
}
</style>

<template>
<div style="max-height:30px; ">
    <v-badge overlap color="error">
        <span v-if="broj > 0" slot="badge">{{broj}}</span>
        <v-avatar size="30px">
            <v-icon dark color="grey">notifications</v-icon>
        </v-avatar>
    </v-badge>
</div>
</template>

<script>
export default {
    props: {
        broj: {
            type: Number
        }
    }
};
</script>