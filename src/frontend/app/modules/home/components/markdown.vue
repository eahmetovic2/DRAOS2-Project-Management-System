<template>
<div v-html="renderedMarkdown">
</div>
</template>

<script>
import marked from 'marked';

export default {
    name: 'Markdown',

    props: {
        value: String
    },

    computed: {
        renderedMarkdown() {
            return marked(this.value);
        }
    }
};
</script>
