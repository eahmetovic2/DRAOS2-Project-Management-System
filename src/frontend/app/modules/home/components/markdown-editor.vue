<template>
<textarea></textarea>
</template>

<style>
.CodeMirror.cm-s-paper.CodeMirror-wrap {
    height: 200px;
    overflow-y: auto;
}

.editor-toolbar .fa {
    margin: 10px;
}
</style>

<script>
import SimpleMde from 'simplemde';

export default {
    name: 'MarkdownEditor',

    props: {
        value: String
    },

    ready() {
        this.initialize();
        this.syncValue();
    },

    mounted() {
        this.initialize();
    },

    methods: {
        initialize() {
            var config = {
                element: this.$el,
                initialValue: this.value,
                spellChecker: false,
                status: false
            };

            this.simpleMde = new SimpleMde(config);
            this.bindingEvents();
        },

        bindingEvents() {
            this.simpleMde.codemirror.on('change', () => {
                this.$emit('input', this.simpleMde.value());
            });
        },

        syncValue() {
            this.simpleMde.codemirror.on('change', () => {
                this.value = this.simpleMde.value();
            });
        }
    },

    destroyed() {
        this.simpleMde = null;
    },

    watch: {
        value(val) {
            if (val === this.simpleMde.value())
                return;
            this.simpleMde.value(val);
        }
    },
};
</script>
