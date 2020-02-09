<style>
.dropzone-custom-content {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    text-align: center;
}

.dropzone-custom-title {
    margin-top: 0;
    color: #00b782;
}

.subtitle {
    color: #314b5f;
}

.dropzone-custom-content {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    text-align: center;
}

.dropzone-custom-title {
    margin-top: 0;
    color: #00b782;
}

.subtitle {
    color: #314b5f;
}

.text-boldiran {
    font-weight: 800;
}

.text-datum {
    font-size: small;
}

.dropzone .dz-message {
    margin: 0px !important;
    padding: 0px;
}

.vue-dropzone .dropzone .dz-clickable {
    padding: 0px !important;
}

#upload-file-komentar {
    background-color: #f5f5f5;
    font-family: 'Arial', sans-serif;
    letter-spacing: 0.2px;
    color: #777;
    transition: background-color .2s linear;
    height: 200px;
    padding: 5px;
}

#upload-file-komentar .dz-preview {
    width: 60px;
     /*display: inline-block */
}

.dz-preview .dz-image {
    width: 60px;
    height: 60px;
    margin-left: 0px;
    margin-bottom: 0px;
    display: none;
}

#upload-file-komentar .dz-preview .dz-image>div {
    width: inherit;
    height: inherit;
    border-radius: 50%;
    background-size: contain;
}

#upload-file-komentar .dz-preview .dz-image>img {
    width: 100%;
}

#upload-file-komentar .dz-preview .dz-details {
    color: rgb(39, 37, 37);
    transition: opacity .2s linear;
    text-align: center;
}

.dz-success-mark,
.dz-error-mark{display: none}
/*.dz-remove {
    /*display: none
}*/
</style>
<template>
<page>

    <material-card color="primary">
        <br>
        <v-form v-model="valid" ref="form" class="upis-komentar-form">

            <v-textarea :counter="2000" label="Komentar" v-model="model.komentar" :rules="komentarLengthRules" required class="required" outline></v-textarea>
            <v-layout pb-3 row justify-space-around>
                <v-flex>
                    <vue-dropzone ref="dropzone" id="upload-file-komentar" :options="dropzoneOptions" @vdropzone-removed-file="removeAllFiles" @vdropzone-success="(file, response) => snimiObjekt(response)" @vdropzone-sending="(file, xhr, formData) => addParam(formData, model, vrstaDokumenta)">
                        <div class="dropzone-custom-content">
                            <h3 class="dropzone-custom-title">Drag and drop to upload content!</h3>

                            <div class="text-xs-center">
                            <i aria-hidden="true" class="material-icons icon">file_upload</i>
                            <p> Upload </p>
                        </div>
                        </div>

                    </vue-dropzone>
                </v-flex>
            </v-layout>

            <v-flex xs12 sm12 md12 lg12 text-xs-right>
                <v-btn type="button" @click="onSubmit">Snimi</v-btn>
            </v-flex>

        </v-form>

        <h4>Komentari: </h4>

        <v-data-table no-data-text="Nema komentara" v-bind:items="listaKomentara" :hide-actions="true">
            <template slot="items" scope="props">
                <tr>
                    <!-- <v-icon class="upis-support-icon">person_outline</v-icon> -->
                    <div>
                        <span class="text-boldiran">{{props.item.createdBy}}</span>

                        <span class="text-datum">{{props.item.datumKreiranja.substring(8,10) + '.' + props.item.datumKreiranja.substring(5,7)+ '.'+props.item.datumKreiranja.substring(0,4) + '. ' + props.item.datumKreiranja.substring(11,19)}}</span>

                        <br>

                        <span>
                            {{ props.item.komentar }}
                        </span>

                    </div>
                    <br>
                </tr>

                <tr v-if="props.item.priloziKomentara.length!=0">
                    <td>Prilozi komentara:<a @click="preuzmiDokument(props.item.priloziKomentara[0].id)"> {{props.item.priloziKomentara[0].naziv}}
                        </a></td>
                </tr>
            </template>
        </v-data-table>
        <br>
    </material-card>
</page>
</template>

<script>
import {
    KorisnikResource,
    ZahtjevResource
} from 'api/resources';
import Identity from 'auth/identity';
import config from "config";
import UploadMixin from "helpers/upload-mixin";
import HelpTipDialogMixin from "helpers/help-tip-dialog-mixin";
import Upit from "../../components/upit";
import vueDropzone from 'vue2-dropzone'

export default {
    name: 'ListaKomentaraZahtjev',
    components: {
        Upit
    },
    mixins: [HelpTipDialogMixin, UploadMixin],
    props: {
        poruka: {
            type: String,
            default: 'Dodaj fajl u prilog komentara'
        },
        zahtjevId: Number
        /*,

                vrstaDokumenta: {
                    type: String,
                    required: true
                }*/
    },

    data() {
        return {

            acceptedFiles: {
                type: String,
                default: ".pdf" //.jpg,.png,.jpeg,.pdf,.txt"
            },
            dropzoneOptionsOsnovno: {
                maxFiles: 1,
                queueLimit: 1,
                maxFileSize: 30,
                acceptedFiles: ".pdf,.txt,.jpg,.png,.jpeg,.doc,.docx,.xls,xlsx,.PDF,.TXT,.JPG,.PNG,.JPEG,.DOC,.DOCX,.XLS,.XLSX",
                addRemoveLinks: true,
                thumbnailWidth: 300,
                dictDefaultMessage: '<i aria-hidden="true" class="material-icons icon">file_upload</i><br />' + this.poruka
            },
            onFocus: false,
            valid: false,
            listaKomentara: [],
            model: {
                komentar: null,
                dokumentId: null
            },
            komentarLengthRules: [
                (v) => !!v || 'Ne možete poslati prazan komentar',
                (v) => v && v.length <= 2000 || 'Komentar ne može biti veći od 200 karaktera',
            ]

        };
    },

    created() {},

    mounted() {
        this.ucitajKomentareZahtjeva();
    },

    methods: {
        snimiObjekt(model) {
            //console.log(model);
            //console.log("dokumentId", model.result);
            //this.model.dokumentId = model.result;

            this.result = model.result;
            this.uspjesanUploadSlike = true;
        },
        onSubmit() {
            var that = this;
            this.$refs.form.validate();
            this.focusInvalidInput(this.$refs.form);
            if (this.valid) {
                var success = model => {
                    that.$toast.success(
                        "Uspješno dodano."
                    );
                    this.$refs.dropzone.removeAllFiles();
                    that.removeAllFiles();

                    that.$refs.form.reset();

                    that.ucitajKomentareZahtjeva();
                };
                var error = () => {
                    this.$toast.error('Nije uspjelo slanje komentara.');
                };
                var promise = ZahtjevResource().dodajKomentar({
                    zahtjevId: this.zahtjevId
                }, this.model);
                promise.then(success, error);
            }
        },
        ucitajKomentareZahtjeva() {
            var success = (response) => {

                var that = this;
                that.listaKomentara = response.body.items;
                //that.ucit = true;

            };
            var error = () => {
                console.log("nije uspjelo");

                //that.$toast.error('Nisu učitani podaci.');
            };

            var promise = ZahtjevResource().vratiKomentareZahtjeva({
                zahtjevId: this.zahtjevId
            });

            promise.then(success, error);
        },
        removeAllFiles() {
            this.model.dokumentId = null;
        }
    }

};
</script>
