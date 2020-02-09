using Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Entities;
using Web.Models.Response.Common;
using Web.Models.Response.Upload;
using Web.Services.Definition.Base;
using Web.Services.Result;


namespace Web.Services.Implementation.Base
{
    public class PrilogZahtjevaService:IPrilogZahtjevaService
    {
        private Context context;


        private IAuthService authService;



        private IApplicationConfigurationService applicationConfigurationService;


        public PrilogZahtjevaService(ILifetimeScope scope, Context context, IAuthService authService, IApplicationConfigurationService applicationConfigurationService)
        {
            this.context = context;
            this.authService = authService;
            this.applicationConfigurationService = applicationConfigurationService;

        }

        //async Task<ServiceResult<FileResponse>> Download(int id)
            async Task<FileResponse> IPrilogZahtjevaService.Download(int id)
        {
            FileResponse fileResponse = null;

            var prilogZahtjeva = context.Dokumenti.Where(d => d.Id == id).FirstOrDefault();

            /*fileResponse = new FileResponse()
            {
                Naziv = prilogZahtjeva.Naziv,
                Bytes = package.GetAsByteArray(),
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };

            return Ok(fileResponse);*/

            /* if (prilogZahtjeva == null)
                 return Error("filename not present");*/
            var folderPath = applicationConfigurationService.GetUploadSettings().PutanjaFoldera;

            var path = Path.Combine(folderPath, prilogZahtjeva.Putanja);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            fileResponse = new FileResponse()
            {
                Naziv = prilogZahtjeva.Naziv,
                //Bytes = package.GetAsByteArray(),
                //ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };

            return (fileResponse);

           // return File(memory, GetContentType(path), Path.GetFileName(path));
        }

    }
}
