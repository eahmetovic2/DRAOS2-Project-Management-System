var FormMixin = {
    methods: {
        focusInvalidInput(forma) {
            const firstInvalid = forma.inputs.sort(compare).find(input => input.errorBucket && input.errorBucket.length > 0);
            
            function compare(a, b) {
                // Sortiraj po poziciji na stranici
                const topa = a.$el.getBoundingClientRect().top;
                const topb = b.$el.getBoundingClientRect().top;
                if (topa < topb) {
                    return -1;
                }
                if (topa > topb) {
                    return 1;
                }

                // Ako su u istom redu sortiraj po horizontalnoj poziciji
                const lefta = a.$el.getBoundingClientRect().left;
                const leftb = b.$el.getBoundingClientRect().left;
                if (lefta < leftb) {
                    return -1;
                }
                if (lefta > leftb) {
                    return 1;
                }

                return 0;
            }
            
            function scroll() {
                window.scrollBy(0, -150);
            }

            if (firstInvalid) {
                this.$nextTick(firstInvalid.focusInput);
                this.$nextTick(firstInvalid.focus);
                this.$nextTick(scroll);
            }
            
            
        },
        focusInvalidInputTimeout(forma, timeout) {
            setTimeout(() => {
                this.focusInvalidInput(forma);
            }, timeout);
        }
    }
};

export default FormMixin;
