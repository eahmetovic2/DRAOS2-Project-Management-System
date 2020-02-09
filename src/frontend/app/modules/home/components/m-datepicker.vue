<template>
<v-layout>
    <v-flex>
        <v-menu :disabled="disabled" offset-x :close-on-content-click="false" v-model="menu" ref="menu" full-width>
            <v-text-field append-icon="event" :mask="mask" :label="label" :rules="rules" slot="activator" :header-date-format="headerDate" :class="{inputClass: true, 'required': required}" v-model="dateTimeValueInput" @input="updateDatepicker()" type="text" :disabled="disabled" :required="required"></v-text-field>
             <v-date-picker width="290" no-title ref="datepicker" v-on:dblclick.native="closeMenu" class="datepicker-menu" locale="sr-Latn-BA" actions :allowed-dates="allowedDates" :first-day-of-week="1" v-model="dateTimeValuePicker">
                <v-spacer />
                <v-btn flat color="primary" @click.native="menu = false">Odustani</v-btn>
                <v-btn flat color="primary" @click.native="closeMenu">Odaberi</v-btn>
            </v-date-picker>
        </v-menu>
    </v-flex>
</v-layout>
</template>

<script>
import moment from 'moment';
export default {
    name: 'MDatePicker',
    props: {
        value: {
            default: null
        },
        mask: {
            default: '##. ##. ####.',
        },
        dateFormat: {
            default: 'DDMMYYYY'
        },
        inputClass: {
            type: String
        },
        required: {
            type: Boolean,
            default: false
        },
        label: {
            type: String
        },
        disabled: {
            type: Boolean,
            default: false
        },
        allowedDates: {
            default: function () {
                return () => true;
            }
        }
    },
    data() {
        return {
            menu: false,
            dateTimeValueInput: '',
            dateTimeValuePicker: null,
            rules: []
        };
    },
    computed: {
        dateTimeValue() {
            return this.value;
        }
    },
    created() {
        if (this.required) {
            this.rules.push(v => !!v || "Polje je obavezno");
        }
        this.rules.push((v) => !v || moment.utc(v, this.dateFormat, true).isValid() || 'Datum nije ispravan');
        this.rules.push((v) => !v || !moment.utc(v, this.dateFormat, true).isValid() || this.allowedDates(moment.utc(v, this.dateFormat, true).toDate()) || 'Datum nije dozvoljen');
        
        this.update(this.value);
    },


  watch: {
    value(newValue, oldValue) {
      if (newValue !== oldValue) {
        var datum = moment.utc(newValue);
        if (newValue != null && this.ispravanDatum(datum)) {
          this.dateTimeValueInput = moment
            .utc(newValue)
            .format(this.dateFormat);
          this.dateTimeValuePicker = moment.utc(newValue).format("YYYY-MM-DD");
        } else {
          this.dateTimeValueInput = "";
          // Ovdje se stavi new Date da bi se po defaultu stavio odabran trenutni datum
          this.dateTimeValuePicker = moment.utc().format();
        }
      }
    }
  },

      methods: {
        // Ova metoda prima string datuma npr. 14.11.1993
        update(value) {
        var datum = moment.utc(value);
            if (this.ispravanDatum(datum)) {
                this.dateTimeValuePicker = moment.utc(value).format("YYYY-MM-DD");
                this.dateTimeValueInput = moment.utc(value).format(this.dateFormat);
                this.emit();
            }
        },
        closeMenu() {
            this.$refs.menu.save(this.dateTimeValuePicker);

            var datum = moment.utc(this.dateTimeValuePicker);
            if (!this.ispravanDatum(datum)) {
                this.dateTimeValuePicker = '';
            }
            this.emit();
        },

        ispravanDatum(datum) {
            return datum.isValid() && this.allowedDates(datum.toDate());
        },

        updateDatepicker() {
            var datum = moment.utc(this.dateTimeValueInput, this.dateFormat, true);
            if (this.ispravanDatum(datum)) {
                this.dateTimeValuePicker = datum.format("YYYY-MM-DD");
            } else {
                this.dateTimeValuePicker = '';
            }
            this.emit();
        },

        emit() {
            // Ovo pusha promjene u parent
            var datum = moment.utc(this.dateTimeValuePicker);

            if (this.ispravanDatum(datum)) {
                this.$emit('input', moment.utc(this.dateTimeValuePicker).toDate());
            } else {
                this.$emit('input', null);
            }

        },

        getStringDate() {
            return this.dateTimeValueInput;
        }
    }

};
</script>
