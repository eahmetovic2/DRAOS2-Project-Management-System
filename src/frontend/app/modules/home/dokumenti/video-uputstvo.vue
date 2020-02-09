<template>
<div>
    <video ref="videoRef" src="" id="video-container" width="100%" controls></video>
</div>
</template>

<script>
export default {
    mounted() {
        var pocetakNaziva = this.$route.fullPath.indexOf("=");
        var naziv = this.$route.fullPath.substring(pocetakNaziva + 1);
        var video = document.getElementById("video-container");
        video.src = naziv;
        video.play();
    }
};
</script>
