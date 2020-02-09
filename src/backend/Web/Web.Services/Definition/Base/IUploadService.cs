using Web.Models.Response.Upload;
using Web.Services.Result;
using System.IO;
using System.Threading.Tasks;

namespace Web.Services
{
    public interface IUploadService : IService
    {
        Task<ServiceResult<FileResult>> Get(int id);
        Task<int> Post(Microsoft.AspNetCore.Http.IFormFile file);
    }
}
