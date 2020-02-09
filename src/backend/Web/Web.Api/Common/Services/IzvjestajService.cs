using Autofac;
using Autofac.Core;
using Web.Api.Config;
using Web.Services;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Web.Api.Common.Services
{
    public class IzvjestajService : IIzvjestajService
    {
        IOptions<ReportServiceSettings> reportSettings;

        /// <summary>
        /// Konstruktor servisa
        /// </summary>
        public IzvjestajService(ILifetimeScope scope, IOptions<ReportServiceSettings> reportSettings)
        {
            this.reportSettings = reportSettings;
        }

        public async Task<byte[]> Pdf(IzvjestajPostavke postavke)
        {
            try
            {
                using (var client = new HttpClient())
                {                   
                    var stringContent = new StringContent(JsonConvert.SerializeObject(postavke), Encoding.UTF8, "application/json");
                    var url = string.Format("{0}/Izvjestaj/pdf", reportSettings.Value.Url);
                    
                    var response = await client.PostAsync(url, stringContent);

                    byte[] bytes = await response.Content.ReadAsByteArrayAsync();

                    return bytes;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
