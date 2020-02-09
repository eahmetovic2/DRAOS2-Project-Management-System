import debounce from 'lodash.debounce';

var PaginationMixin = {
    mounted() {
        this.updateData = debounce(this.updateData, 300, {leading: true});
        if (!this.disableUpdate) {
            this.updateData();
        } 
        this.$watch('pagination', (novi, stari) => {
            if (novi.page !== stari.page || novi.rowsPerPage !== stari.rowsPerPage) {
                this.inputs.page = novi.page;
                this.inputs.count = novi.rowsPerPage;

                if (!this.disableUpdate) {
                    this.updateData();
                }                
            }
        }, { deep: true });
    },
    data() {
        return {
            pagination: {
                page: 1,
                rowsPerPage: 30,
            },
            rowsPerPageItems: [10, 20,30, 50]
        };
    },
    methods: {
        resetujPaginaciju() {
            this.inputs.page = 1;
            this.pagination.page = 1;
            this.updateData();       
        }
    }
};

export default PaginationMixin;
