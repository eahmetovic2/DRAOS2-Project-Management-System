import Identity from "auth/identity";
import config from "config";
import { TokenResource } from "api/resources";

var UploadMixin = {
	data(){
		return {
			identity: Identity,
			uploadUrl: config.SERVICE_ROOT + '/upload',
			dropzoneOptionsOsnovno: {
				maxFiles: 1,
				queueLimit: 1,				
				maxFileSize: 30,
				acceptedFiles: ".pdf",
				dictDefaultMessage: '<i aria-hidden="true" class="material-icons icon">file_upload</i>'
			}
		};    
	},
	computed: {
		isAdministrator() {
			return this.identity.isAdministrator();
		},
		dropzoneOptions() {
			return Object.assign(this.dropzoneOptionsOsnovno, {
				clickable: true,
				headers: this.headers,
				url: this.uploadUrl,				
				success: this.uspjesanUpload,
				error: this.neuspjesanUpload,
				init: function() {
					this.on('addedfile', function() {
						if (this.files.length > 1) {
						this.removeFile(this.files[0]);
						}
					});
				}
			});
		},
		headers() {
			if (this.identity != null) {
				return {
					'X-AUTH-TOKEN': this.identity.id()
				};
			}
			return null;       
			},
	},
	methods: {
		uspjesanUpload(file, response) {
			var uploadId = response.options.find(a => a.key === 'uploadId').value[0];
			var item = this.uploads.find(a => a.id == uploadId).item;
			var dokumentId = response.result;
			
			item.dokumentId = dokumentId;

			this.$toast.success("Dokument uspješno poslan");
		},
		neuspjesanUpload() {
      this.$toast.error("Greška prilikom slanja dokumenta");
		},
		preuzmiDokument(dokumentId) {
		var that = this;

		function success(model){
			var dokumentUrl = that.uploadUrl + "/" + dokumentId + "?ttxid=" + model.body.id;
			window.open(dokumentUrl, "_blank", "");
		}

		TokenResource()
		.temp(
			{
			korisnickoIme: Identity.korisnickoIme()
			},
			{
			korisnickoIme: Identity.korisnickoIme()
			}
		)
		.then(success);
    },
    

    // Posto na stranici imamo vise upload komponenti potrebno je slati id da se zna na koju komponentu se odnosi
    // kada se dobije callback i za to nam sluzi uploads folder
    addParam(formData, item) {
			if (this.uploads == null) {
				this.uploads = [];
			}

			var upload = {
				item: item,
				id: Math.floor((Math.random() * 100000) + 1)
			};
			this.uploads.push(upload);

			formData.append("uploadId", upload.id);
	},
	remove(file)
	{
		console.log("remove",file);
		//this.removeFile(file);
		file.previewElement.remove();
	
	},
  }
};

export default UploadMixin;
