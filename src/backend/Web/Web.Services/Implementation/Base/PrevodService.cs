using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Core.Constants;
using Web.Core.Extensions;
using Web.Entities;
using Web.Entities.Models.Sifarnik;
using Web.Models.Response.Base.Prevod;
using Web.Services.Definition.Base;
using Web.Services.Result;

namespace Web.Services.Implementation.Base
{
    public class PrevodService : Service, IPrevodService
    {

        public Context context { get; set; }
        public IAuthService authService { get; set; }


        public PrevodService(ILifetimeScope scope, Context context, IAuthService authService) : base(scope)
        {

            this.context = context;
            this.authService = authService;

        }


        public static object GetPropValue(object src, string propName)
        {

            if (src.GetType().GetProperty(propName) != null)
            {
                if (src.GetType().GetProperty(propName).GetValue(src, null) != null)
                    return
                       src.GetType().GetProperty(propName).GetValue(src, null);
                else return "";
            }

            else return "";
        }

        public ServiceResult<List<PrevodModel>> VratiListuPrevoda(String tabela, int id)
        {
            return GetEntitetPrevod<dynamic>(tabela, id);
        }


        public TEntity GetEntitet<TEntity>(String tabela, int id) where TEntity : class
        {
            //override naziva za sifarnike
            foreach (ESifarnik sifarnik in (ESifarnik[])Enum.GetValues(typeof(ESifarnik)))
            {
                if (sifarnik.ToString() == tabela)
                {
                    tabela = GetDescription.GetEnumDescription(sifarnik);
                }
            }
            //dobavi entitet           
            var enumerable = (IEnumerable<TEntity>)(typeof(Context).GetProperty(tabela).GetValue(context, null));

            //dobavi polja entita tipa string  
            var entity = enumerable.Where(x => Convert.ToInt32(x.GetType().GetProperty("Id").GetValue(x)) == id).FirstOrDefault();
            return entity;
        }

        public ServiceResult<List<PrevodModel>> GetEntitetPrevod<TEntity>(String tabela, int id) where TEntity : class
        {
            //override naziva za sifarnike
            foreach (ESifarnik sifarnik in (ESifarnik[])Enum.GetValues(typeof(ESifarnik)))
            {
                if (sifarnik.ToString() == tabela)
                {
                    tabela = GetDescription.GetEnumDescription(sifarnik);
                }
            }
            //dobavi entitet           
            var enumerable = (IEnumerable<TEntity>)(typeof(Context).GetProperty(tabela).GetValue(context, null));

            //dobavi polja entita tipa string  
            var entity = enumerable.Where(x => Convert.ToInt32(x.GetType().GetProperty("Id").GetValue(x)) == id).FirstOrDefault();

            var polja = entity.GetType().GetProperties()
                .Where(x =>
                x.PropertyType.FullName == "System.String"
                && x.Name != "CreatedBy"
                && x.Name != "ModifiedBy"
                && x.Name != "Sifra")
                .Select(x => x.Name);

            //model
            List<PrevodModel> model = new List<PrevodModel>();
            //ako ima  polja u tabeli tipa string
            if (polja.Count() > 0)
            {
                foreach (Jezici jezik in (Jezici[])Enum.GetValues(typeof(Jezici)))
                {
                    model.Add(new PrevodModel
                    {
                        Jezik = (int)jezik,
                        JezikNaziv = GetDescription.GetEnumDescription(jezik),
                        Polja = polja.Select(x => new PoljePrevodModel
                        {
                            Polje = x,
                            Prevod = DajPrevod(tabela, (int)jezik, id, x, GetPropValue(entity, x).ToString()),
                            Vrijednost = GetPropValue(entity, x).ToString()
                        }).ToList()
                    });
                }
                return Ok(model);
            }
            else
            {
                return NotFound();
            }
        }

        public String DajPrevod(String tabela, int? jezik, int id, String polje, String tekst)
        {
            if (jezik == null)
            {
                var korisnik = authService.TrenutniKorisnik();
                if (korisnik.Jezik.HasValue)
                {
                    jezik = korisnik.Jezik;
                }
            }

            var prevod = context.Prevodi.Where(x =>
                                          x.Jezik == jezik
                                          && x.Tabela == tabela
                                          && x.Polje == polje
                                          && x.RedId == id

                                        ).FirstOrDefault();
            if (prevod != null)
            {
                if (!String.IsNullOrEmpty(prevod.PrevodTekst))
                {
                    tekst = prevod.PrevodTekst;
                }

            }
            return tekst;

        }

        public ServiceResult<List<PrevodModel>> KreirajPrevod<TEntity>(String tabela, int id, List<PrevodModel> model) where TEntity : class
        {

            var korisnik = authService.TrenutniKorisnik();
            //override naziva za sifarnike
            foreach (ESifarnik sifarnik in (ESifarnik[])Enum.GetValues(typeof(ESifarnik)))
            {
                if (sifarnik.ToString() == tabela)
                {
                    tabela = GetDescription.GetEnumDescription(sifarnik);
                }
            }

            ///dobavi red koji se mijenja
            var entity = GetEntitet<TEntity>(tabela, id);
            var datumIzmjene = entity.GetType().GetProperty("DatumIzmjene");
            datumIzmjene.SetValue(entity, DateTime.Now);
            var izmijenio = entity.GetType().GetProperty("ModifiedBy");
            izmijenio.SetValue(entity, korisnik.KorisnickoIme);

            //doraditi da se radi update postojećih prevoda
            var stavkeZaBrisanje = context.Prevodi.Where(x => x.RedId == id && x.Tabela == tabela).ToList();
            context.Prevodi.RemoveRange(stavkeZaBrisanje);
            SaveChanges(context);
            foreach (var item in model)
            {
                foreach (var prevod in item.Polja)
                {
                    context.Prevodi.Add(new Prevod
                    {
                        Jezik = item.Jezik,
                        Polje = prevod.Polje,
                        RedId = id,
                        PrevodTekst = prevod.Prevod,
                        Tabela = tabela.ToString()

                    });
                }

            }
            SaveChanges(context);
            return VratiListuPrevoda(tabela, id);




        }

        public ServiceResult<List<PrevodModel>> KreirajPrevod(String tabela, int id, List<PrevodModel> model)
        {

            KreirajPrevod<dynamic>(tabela, id, model);
            return VratiListuPrevoda(tabela, id);




        }
    }
}
