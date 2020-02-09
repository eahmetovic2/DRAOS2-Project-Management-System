using Autofac;
using Web.Api.Config;
using Web.Models.Response.ApplicationConfiguration;
using Web.Models.Response.Upload;
using Web.Services;
using Microsoft.Extensions.Options;

namespace Web.Api.Common.Services
{
    public class ApplicationConfigurationService : IApplicationConfigurationService
    {
        IOptions<ReportServiceSettings> reportSettings;
        IOptions<UploadSettings> uploadSettings;    
        ILifetimeScope scope;

        public ApplicationConfigurationService(ILifetimeScope scope, IOptions<ReportServiceSettings> reportSettings, IOptions<UploadSettings> uploadSettings
                                                )
        {
            this.scope = scope;
            this.reportSettings = reportSettings;
            this.uploadSettings = uploadSettings;
         
        }

        public ReportServiceSettingsModel GetReportServiceSettings()
        {
            return new ReportServiceSettingsModel()
            {
                Url = reportSettings.Value.Url,
                PreviewPageUrl = reportSettings.Value.PreviewPageUrl
            };
        }

        public UploadSettingsModel GetUploadSettings()
        {
            return new UploadSettingsModel
            {
                PutanjaFoldera = uploadSettings.Value.PutanjaFoldera,
                DozvoljeneEkstenzije = FormatirajEkstenzije(uploadSettings.Value.DozvoljeneEkstenzije),
                MaksimalnaVelicinaMB = uploadSettings.Value.MaksimalnaVelicinaMB
            };
        }

       

        public string[] FormatirajEkstenzije(string ekstenzije)
        {
            var formatirane = uploadSettings.Value.DozvoljeneEkstenzije.Split(',');

            for (int i = 0; i < formatirane.Length; i++)
            {
                formatirane[i] = "." + formatirane[i];
            }

            return formatirane;
        }

     
    }
}
