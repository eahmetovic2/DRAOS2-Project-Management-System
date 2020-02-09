using Web.Models.Response.ApplicationConfiguration;
using Web.Models.Response.Upload;

namespace Web.Services
{
    public interface IApplicationConfigurationService : IService
    {
        ReportServiceSettingsModel GetReportServiceSettings();
        UploadSettingsModel GetUploadSettings();
	
    }
}
