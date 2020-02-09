<style lang="scss" scoped>
.kartica {
    padding: 10px 20px;
    border-radius: 4px;

    .btn-add {
        border-radius: 4px;
        font-size: 14px;
        line-height: 18px;
        font-weight: 550;
        text-transform: unset;
        height: 35px;
    }

    .btn-odustani {
        color: #286efa;
        font-size: 14px;
        line-height: 18px;
        font-weight: 550;
        background-color: transparent !important;
        box-shadow: unset;
        text-transform: unset;
    }
}

.naslov {
    font-size: 15px !important;
    line-height: 19px !important;
    color: #286EFA !important;
    text-align: left !important;
    padding-left: 24px;
    padding-top: 16px;
}
</style><style lang="scss">
.datum-text {
    //font-size: 14px;
    //padding: 5px;

    //color: #888e9e;
    background-color: #f4f7fa;
    border: 1px solid #dfe2e8;
    border-radius: 4px;
    //max-height: 50px; 
    //max-width: 160px;

    padding-top: 0px !important;
    margin-top: 0px !important;

    .v-text-field__slot {
        background-color: none;
        border: none;

        //bottom: 2px !important;
        input {
            //text-align: center;
        }
    }

    .v-input__slot {
        margin-bottom: 0px !important;
    }

    .v-text-field__details {
        display: none;
    }

    img {
        padding-top: 6px;
        padding-right: 5px;
    }
}

.v-date-picker-table {
    .v-btn--active {
        background-color: #696a6d !important;
    }
}

.date-picker .v-picker__body {
    padding-bottom: 60px;
}

.v-menu__content {
    min-width: 290px !important;
}

.v-time-picker-title__time .v-picker__title__btn,
.v-time-picker-title__time span {
    font-size: 40px;
}

.v-picker__title {
    padding: 0 16px;
}
</style>

<template>
<v-dialog :persistent="trajan" v-model="dialog" :width="width">
    <div slot="activator">
        <slot name="activator"></slot>
    </div>

    <v-card>
        <v-card-title style="padding:0px;">
            <h3 class="naslov">Izmjena radnog vremena</h3>
        </v-card-title>
        <v-divider></v-divider>
        <v-card-text>
            <v-form v-model="valid" ref="form">
                <slot name="kartica"></slot>
                <v-layout row wrap>

                    <v-flex xs6 md6 lg6>
                        <div style="margin-top: 15px;">Vrijeme od</div>
                        <div style="display:inline-block">
                            <v-autocomplete :rules="rules.obaveznoVrijeme" @change="dodajMinute" flat solo style="width:70px; display:inline-block;" small append-icon="unfold_more" v-model="model.satOd" :items="sati" item-text="naziv" item-value="id" persistent-hint></v-autocomplete>
                            <v-autocomplete :rules="rules.obaveznoVrijeme" @change="dodajMinute" flat solo style="width:70px; display:inline-block;" small append-icon="unfold_more" v-model="model.minutaOd" :items="minute" item-text="naziv" item-value="id" persistent-hint></v-autocomplete>
                        </div>
                    </v-flex>
                    <v-flex xs6 md6 lg6>
                        <div style="float: right;">
                            <div style="margin-top: 15px;">Vrijeme do</div>
                            <div style="display:inline-block">
                                <v-autocomplete :rules="rules.obaveznoVrijeme" flat solo style="width:70px; display:inline-block;" small append-icon="unfold_more" v-model="model.satDo" :items="sati" item-text="naziv" item-value="id" persistent-hint></v-autocomplete>
                                <v-autocomplete :rules="rules.obaveznoVrijeme" flat solo style="width:70px; display:inline-block;" small append-icon="unfold_more" v-model="model.minutaDo" :items="minute" item-text="naziv" item-value="id" persistent-hint></v-autocomplete>
                            </div>
                        </div>
                    </v-flex>
                </v-layout>
            </v-form>
        </v-card-text>
        <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn class="odustani" flat @click.stop="odgovor(false)">{{odustaniTekst}}</v-btn>
            <v-btn class="btn-snimi" flat @click.stop="odgovor(true)">
                <v-icon color="white">add</v-icon>{{potvrdiTekst}}
            </v-btn>
        </v-card-actions>
    </v-card>
</v-dialog>
</template>

<script>
import moment from "moment";
export default {
    name: "TimePickerModal",
    props: {

        pocetnoVrijeme: {
            default: null
        },
        krajnjeVrijeme: {
            default: null
        },
        value: {
            default: null
        },
        mask: {
            default: "##. ##. ####."
        },
        dateFormat: {
            default: "DDMMYYYY"
        },
        inputClass: {
            type: String
        },
        allowedDates: {
            default: function () {
                return () => true;
            }
        },
        potvrdiTekst: {
            type: String,
            default: "Potvrdi"
        },
        odustaniTekst: {
            type: String,
            default: "Poništi"
        },
        trajan: {
            type: Boolean,
            default: true
        },
        width: {
            type: Number,
            default: 430
        }
    },
    data() {
        return {
            date: new Date().toISOString().substr(0, 10),
            menu: false,
            dateTimeValueInput: "",
            dateTimeValuePicker: null,
            rules: [],
            valid: true,
            dialog: false,
            sati: [],
            minute: [],
            radnoVrijemeOd: null,
            radnoVrijemeDo: null,
            model: {
                datumVrijemeOdrzavanja: null,
                satOd: null,
                minutaOd: null,
                satDo: null,
                minutaDo: null,
            },
        };
    },
    computed: {
        dateTimeValue() {
            return this.value;
        }
    },
    created() {
        this.sati = this.dajSate();
        this.minute = this.dajMinute();
        this.model.satOd = this.pocetnoVrijeme.slice(0, 2);
        this.model.minutaOd = this.pocetnoVrijeme.slice(3, 5);

        this.model.satDo = this.krajnjeVrijeme.slice(0, 2);
        this.model.minutaDo = this.krajnjeVrijeme.slice(3, 5);
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
        dodajMinute() {
            if (this.model.satOd != null && this.model.minutaOd != null) {
                /*var sat = (parseInt(this.model.minutaOd) + 45) / 60 >= 1;
                this.model.satDo = sat ? ((parseInt(this.model.satOd) + 1) % 24).toString() : this.model.satOd; 
                var vrijeme = ((parseInt(this.model.minutaOd) + 45) % 60).toString();
                this.model.minutaDo = vrijeme.length == 1 ? '0' + vrijeme : vrijeme;*/

            }
        },
        closeMenu() {
            this.$refs.menu.save(this.dateTimeValuePicker);

            var datum = moment.utc(this.dateTimeValuePicker);
            if (!this.ispravanDatum(datum)) {
                this.dateTimeValuePicker = "";
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
                this.dateTimeValuePicker = "";
            }
            this.emit();
        },

        emit() {
            // Ovo pusha promjene u parent
            var datum = moment.utc(this.dateTimeValuePicker);

            if (this.ispravanDatum(datum)) {
                this.$emit("input", moment.utc(this.dateTimeValuePicker).toDate());
            } else {
                this.$emit("input", null);
            }
        },

        getStringDate() {
            return this.dateTimeValueInput;
        },
        odgovor(val) {
            if (val == false) {
                this.dialog = false;
                return;
            }
            this.$refs.form.validate();
            this.focusInvalidInput(this.$refs.form);
            if (!this.valid)
                return;
            this.dialog = false;
            var vrijemeOd = (this.model.satOd.length == 1 ? '0' + this.model.satOd : this.model.satOd) + ":" + this.model.minutaOd;
            var vrijemeDo = (this.model.satDo.length == 1 ? '0' + this.model.satDo : this.model.satDo) + ":" + this.model.minutaDo;

            this.$emit("potvrda", vrijemeOd, vrijemeDo);
        },
        dajSate() {
            var sati = [];
            for (let index = 0; index < 24; index++) {
                if (index < 10) {
                    sati.push('0' + index.toString());
                } else {
                    sati.push(index.toString());
                }
            }
            return sati;
        },
        dajMinute() {
            var minute = [];
            for (let index = 0; index < 60; index += 5) {
                let naziv = (index == 0 || index == 5) ? '0' + index.toString() : index.toString();
                minute.push(naziv);
            }
            return minute;
        }
    }
};
</script>
