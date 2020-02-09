<template>
<router-view>
</router-view>
</template>

<script>
import PostavkeSistema from './postavke-sistema';

export default {
    name: 'Postavke',

    routes: [{
        path: 'postavke-sistema',
        name: 'home.postavke.postavke-sistema',
        component: PostavkeSistema
    }]
};
</script>
