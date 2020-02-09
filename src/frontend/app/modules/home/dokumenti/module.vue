<template>
<router-view>
</router-view>
</template>

<script>
import VideoUputstvo from './video-uputstvo';

export default {
    name: 'VideoUputstvo',

    routes: [{
        path: 'dokumentacija',
        name: 'home.uputstvo.video',
        component: VideoUputstvo
    }]
};
</script>
