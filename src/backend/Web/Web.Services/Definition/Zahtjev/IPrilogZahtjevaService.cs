using Emis.Web.Models.Request;
using Emis.Web.Models.Response;
using System.Threading.Tasks;
using Web.Entities.Models.Zahtjev;
using Web.Models.Response.Common;
using Web.Models.Response.Upload;
using Web.Services.Result;

namespace Web.Services.Definition.Base
{

    public interface IPrilogZahtjevaService
    {
        //ServiceResult<FileResponse> Download(int id);
        Task<FileResponse> Download(int id);

        //Task<IActionResult> Download(int id);
        //Task<ServiceResult<FileResult>> Download(int id);
        //Task<int> Post(Microsoft.AspNetCore.Http.IFormFile file);


    }
}
