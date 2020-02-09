using Autofac;
using Web.Core.Constants;
using Web.Core.Database;
using Web.Core.Extensions;
using Web.Services.Definition.Sifarnik;
using Web.Services.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static Web.Services.Implementation.SifarnikService;
using Web.Models.Response.Sifarnik;

namespace Web.Services.Implementation.Sifarnik
{
    /// <summary>
    /// Servis koji vraca sifarnike za dropdown liste
    /// </summary>
    public class GetSifarniciService : Service, IGetSifarniciService
    {
        public GetSifarniciService(ILifetimeScope scope) : base(scope) { }

        public static string GetMemberName<T, TValue>(Expression<Func<T, TValue>> memberAccess)
        {
            return ((MemberExpression)memberAccess.Body).Member.Name;
        }

        public Dictionary<ESifarnik, Func<InputParameters, SifarnikList>> GetSifarnici()
        {
            return new
            Dictionary<ESifarnik, Func<InputParameters, SifarnikList>>() {
                {                    
                    ESifarnik.Pol,
                    new Func<InputParameters, SifarnikList>
                    (
                        (inputParameters) => {
                            var list = Secure (Prevedi (inputParameters.Context.Polovi, inputParameters.Scope, null),
                                new SecurityLevel { Read = true },
                                inputParameters.Scope);

                            if (inputParameters.SamoDatum) {
                                var max = list.Max (a => a.DatumIzmjene);
                                return new SifarnikList () {
                                    DatumResponsa = max.HasValue ? max.Value : new DateTime ()
                                };
                            }

                            if (inputParameters.DatumIzmjene.HasValue) {
                                list = list.Where (a => a.DatumIzmjene.HasValue && a.DatumIzmjene >= inputParameters.DatumIzmjene);
                            }
                            var entityName = list.Select (x => x.ToString ()).FirstOrDefault ();

                            var tabela = inputParameters.Context.Model.FindEntityType (entityName).SqlServer ().TableName;

                            return list.OrderBy (a => a.Poredak).Select (a => new SifarnikModel () {
                                Id = a.Id,
                                    Naziv = a.Naziv,
                                    Sifra = a.Sifra,
                                    Poredak = a.Poredak,
                                    DatumIzmjene = a.DatumIzmjene

                            }).ToSifarnikList ();

                        }
                    )
                }, {
                    ESifarnik.FrontendModul,
                    new Func<InputParameters, SifarnikList>
                    (
                        (inputParameters) => {
                            if (inputParameters.SamoDatum) {
                                var max = inputParameters.Context.FrontendModuli.Max (a => a.DatumIzmjene);
                                return new SifarnikList () {
                                    DatumResponsa = max.HasValue ? max.Value : new DateTime ()
                                };
                            }

                            var list = Prevedi (inputParameters.Context.FrontendModuli, inputParameters.Scope, null);

                            if (inputParameters.DatumIzmjene.HasValue) {
                                list = list.Where (a => a.DatumIzmjene.HasValue && a.DatumIzmjene >= inputParameters.DatumIzmjene);
                            }

                            return list.Select (a => new SifarnikModel () {
                                Id = a.Id,
                                    Naziv = a.Naziv,
                                    Sifra = a.Sifra
                            }).ToSifarnikList ();
                        }
                    )
                },  {
                    ESifarnik.KorisnikTipDodatneInformacije,
                    new Func<InputParameters, SifarnikList>
                    (
                        (inputParameters) => {
                            if (inputParameters.SamoDatum) {
                                var max = inputParameters.Context.KorisnikTipoviDodatneInformacije.Max (a => a.DatumIzmjene);
                                return new SifarnikList () {
                                    DatumResponsa = max.HasValue ? max.Value : new DateTime ()
                                };
                            }

                            var list = Prevedi (inputParameters.Context.KorisnikTipoviDodatneInformacije, inputParameters.Scope, null);

                            if (inputParameters.DatumIzmjene.HasValue) {
                                list = list.Where (a => a.DatumIzmjene.HasValue && a.DatumIzmjene >= inputParameters.DatumIzmjene);
                            }

                            return list.OrderBy (a => a.Poredak).Select (a => new SifarnikKorisnikDodatnoModel () {
                                Id = a.Id,
                                    Naziv = a.Naziv,
                                    Opis = a.Opis,
                                    Poredak = a.Poredak

                            }).ToSifarnikList ();
                        }
                    )
                },
            };
        }
    }
}