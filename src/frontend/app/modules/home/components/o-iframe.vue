<template>
<iframe id="theiframe" frameborder="0" allowfullscreen scrolling="yes" ref="theiframe"></iframe>
</template>

<script>
export default {
    name: 'Oiframe',
    props: {
        url: {
            default: null
        },
        ratio: {
            default: false
        }
    },

    mounted() {
        this.setUrl();

    },
    created() {
        var that = this;

        setInterval(function () {
            that.fixSize();
        }, 200);
        // that.$refs.theiframe.addEventListener("DOMFrameContentLoaded", function(event) {
        //     console.log("DOM fully loaded and parsed");
        //   });
        //   window.addEventListener('DOMFrameContentLoaded',   that.fixSize());

    },

    watch: {
        url() {
            this.setUrl();

        },
    },
    methods: {

        setUrl() {

            var vif = this.$refs.theiframe;
            if (this.url && this.url != '#')
                vif.src = this.url;
            this.calculateSize();
        },
        fixSize() {
            var vif = document.getElementById('theiframe');
            if (vif) {
                var table = vif.contentWindow.document.getElementById('ReportViewerRSFReports_fixedTable');
                if (table) {
                    table.style.width = '100%';
                }

            }
        },
        calculateSize() {

            var vif = this.$refs.theiframe;
            var maxWidth = vif.parentElement.getBoundingClientRect().width;
            var maxHeight = vif.parentElement.getBoundingClientRect().height;
            var height = 0;
            if (ratio) {
                var ratio = vif.height / vif.width;
                height = maxWidth * ratio;
            } else {
                height = maxHeight - 35;
            }
            vif.style.height = height + 'px';
            vif.style.width = '100%';
            //  setTimeout(  this.fixSize(),2000 );   

        }
    }
};
</script>
