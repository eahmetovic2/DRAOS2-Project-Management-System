<template>
<div id="loading">
    <v-app>
        <v-progress-circular indeterminate v-bind:size="120"></v-progress-circular>
    </v-app>
</div>
</template>

<script>
export default {
    name: 'Loading'
};
</script>
