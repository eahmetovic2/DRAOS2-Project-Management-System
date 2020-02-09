using Autofac;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Entities;
using Web.Entities.Models.Zahtjev;
using Web.Models.Mapping.Mappers.Base.ZahtjevMap;
using Web.Models.Request.Zahtjev.ZahtjevKomentar;
using Web.Models.Response.Zahtjev.ZahtjevKomentar;
using Web.Services.Definition.Zahtjev;
using Web.Services.Result;

namespace Web.Services.Implementation.Base
{
    public class ZahtjevKomentarService : Service, IZahtjevKomentarService
    {
        private Context context;


        private IAuthService authService;

        /// <summary>
        /// 
        /// </summary>


        private IApplicationConfigurationService applicationConfigurationService;


        /// <summary>
        /// Konstruktor servisa
        /// </summary>
        public ZahtjevKomentarService(ILifetimeScope scope, Context context, IAuthService authService, IApplicationConfigurationService applicationConfigurationService)
            : base(scope)
        {
            this.context = context;
            this.authService = authService;
            this.applicationConfigurationService = applicationConfigurationService;

        }

        public ServiceResult<Nothing> KreirajKomentarZaZahtjev(int zahtjevId, KreirajKomentarRequestModel model)
        {
            try
            {
                ZahtjevKomentar zahtjevKomentar = new ZahtjevKomentar();
            zahtjevKomentar.Komentar = model.Komentar;
            //zahtjevKomentar.KorisnikId = model.KorisnikId;

            Zahtjev zahtjev = context.Zahtjevi.Include(z=>z.Komentari)
                .FirstOrDefault(x => x.Id == zahtjevId);

            if (zahtjev == null)
                return NotFound();

            
                zahtjev.Komentari.Add(zahtjevKomentar);
                SaveChanges(context);


                if (model.DokumentId != null)
                {
                    PrilogKomentar prilogKomentar = new PrilogKomentar();
                    prilogKomentar.DokumentId = (int)model.DokumentId;
                    prilogKomentar.ZahtjevKomentarId = zahtjevKomentar.Id;

                    context.PriloziKomentar.Add(prilogKomentar);

                    SaveChanges(context);
                }
            }
            catch (Exception e)
            {
                throw;
            }



            return Ok();
        }

        public ServiceResult<ZahtjevKomentariListModel> VratiSveKomentareZahtjeva(int zahtjevId, ListaKomentaraZahtjevaRequestModel model)
        {
            var zahtjevKomentari = context.ZahtjevKomentari.Where(z => z.ZahtjevId == zahtjevId).ToZahtjevKomentarListModelItem().ToList();
            foreach(var z in zahtjevKomentari)
            {
                var prilozi=context.PriloziKomentar.Include(d => d.Dokument).Where(p => p.ZahtjevKomentarId == z.Id).Select(d => d.Dokument).ToList();
                z.PriloziKomentara=prilozi;
            }


            var total = zahtjevKomentari.Count();

            //ovo trenutno vraca sve komentare zahtjeva 
            var result = new ZahtjevKomentariListModel
            {
                Items = zahtjevKomentari,
                Page = model.Page,
                Total = total
            };
            return Ok(result);

        }

        /*public ServiceResult<ZahtjevListModel> VratiSveZahtjeve(ListaZahtjevaRequestModel model)
        {
            var securityLevel = new SecurityLevel();

            var query = context.Zahtjevi
                               .AsQueryable();

            query = Secure(query, securityLevel);

            if (!String.IsNullOrWhiteSpace(model.Naziv))
            {
                var lowerNaziv = model.Naziv.ToLower();
                query = query.Where(s => s.Naziv.ToLower().Contains(lowerNaziv));
            }
            // uradi filtriranje po opisu zahtjeva
            if (!String.IsNullOrWhiteSpace(model.Opis))
            {
                var lowerOpis = model.Naziv.ToLower();
                query = query.Where(s => s.Opis.ToLower().Contains(lowerOpis));
            }

            var zahtjevi = query.ToZahtjevListModelItem()
                .ToList();

            if (zahtjevi == null)
                return NotFound();

            var total = zahtjevi.Count();

            zahtjevi = zahtjevi.Skip(model.Page * model.Count - model.Count)
                    .Take(model.Count).ToList();


            var result = new ZahtjevListModel
            {
                Items = zahtjevi,
                Page = model.Page,
                Total = total
            };

            return Ok(result);
        }*/

    }
}
