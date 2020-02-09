using Autofac;
using Web.Entities;
using Web.Entities.Models.Base;
using Web.Models.Request.Postavke;
using Web.Models.Response.Postavke;
using Web.Services.Result;
using System.Linq;
using Web.Models.Mapping.Mappers.PostavkeMap;

namespace Web.Services.Implementation
{
    /// <summary>
    /// Implementacija servisa koji radi sa postavkama
    /// </summary>
    public class PostavkeService : Service, IPostavkeService
    {
        /// <summary>
        /// Kesirana vrijednost postavki
        /// </summary>
        private static ServiceResult<PostavkeModel> cachedResult;

        /// <summary>
        /// Entity framework db kontekst 
        /// </summary>
        private Context context;

 

        /// <summary>
        /// Konstruktor servisa
        /// </summary>
        public PostavkeService(ILifetimeScope scope, Context context)
            : base(scope)
        {
            this.context = context;

        }

        public ServiceResult<PostavkeModel> VratiPostavke()
        {
            // ako imamo kesiran rezultat, vrati to 
            if (cachedResult != null)
                return cachedResult;

            // dobavi postavke ako postoje, ako ne, kreiraj default vrijednost
            var postavke = context.Postavke
                .OrderBy(p => p.Id)
                .FirstOrDefault();

            if (postavke == null)
                postavke = new Postavke();

            // uradi mapiranje, spasi rezultat ako je ok
            var result = postavke.ToPostavkeModel();

            return Ok(result);
        }

        public ServiceResult<PostavkeModel> AzurirajPostavke(AzurirajPostavkeRequestModel model)
        {
            // dobavi postavke
            var postavke = context.Postavke
                .OrderBy(p => p.Id)
                .FirstOrDefault();

            // ako ne postoje, napravi nove
            if (postavke == null)
            {
                postavke = new Postavke();
                context.Add(postavke);
            }

            // postavi vrijednosti
            postavke.NaslovSistema = model.NaslovSistema;
            postavke.TrajanjeSesije = model.TrajanjeSesije;
            postavke.UrlKarte = model.UrlKarte;
            postavke.AutorskaPravaKarte = model.AutorskaPravaKarte;
            
            SaveChanges(context);

            // ocisti kes, vrati kreirane/azuirane postavke
            cachedResult = null;
            return VratiPostavke();
        }

        public Postavke DajSistemskePostavke() {
            var postavke = context.Postavke
                .OrderBy(p => p.Id)
                .FirstOrDefault();

            // ako ne postoje, napravi nove
            if (postavke == null)
            {
                postavke = new Postavke();
                context.Add(postavke);
            }

            return postavke;
        }
    }
}
