<style>
.v-spinner {
    padding-top: 50px;
}
</style>

<template>
<div class="page-content">
    <h2 class="glavni-naslov" v-if="haveTitle">{{ title }}</h2>

    <slot v-else></slot>
</div>
</template>

<script>
export default {
    name: 'Page',

    props: {
        resolved: {
            default: true
        },
        title: {
            default: null
        }
    },

    methods: {
        navigate(location) {
            this.$router.push(location);
        }
    },

    computed: {
        haveTitle() {
            return this.title && this.title.length;
        },

        haveHeader() {
            return this.haveTitle;
        },

        isResolving() {
            return !this.resolved;
        }
    }
};
</script>
