﻿using Autofac;
using Web.Entities;
using Web.Entities.Models.Sifarnik;
using Web.Models.Mapping;
using Web.Services.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Web.Core.Database;
using Microsoft.EntityFrameworkCore;
using Web.Services.Security;

[assembly: InternalsVisibleTo("Web.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
[assembly: InternalsVisibleTo("Web.Konzola")]

namespace Web.Services.Implementation
{
    /// <summary>
    /// Bazna implementacija servisa za sve ostale service
    /// </summary>
    public class Service : IService, ISecurityHandler
    {
        /// <summary>
        /// Lifetime scope IoC kontenjera
        /// </summary>
        public ILifetimeScope Scope { get; set; }

        /// <summary>
        /// Konstruktor koji prima lifetime scope IoC kontejnera
        /// </summary>
        /// <param name="scope">Lifetime scope IoC kontejnera</param>
        public Service(ILifetimeScope scope)
        {
            this.Scope = scope;
        }

        /// <summary>
        /// Vratu prazan resultat koji je ok
        /// </summary>
        /// <returns>Prazan resultat koji je ok</returns>
        public OkServiceResult Ok()
        {
            return new OkServiceResult();
        }

        /// <summary>
        /// Vrati rezultat koji je ok i ima vrijednost
        /// </summary>
        /// <typeparam name="T">Tip rezultata</typeparam>
        /// <param name="value">Vrijednost rezultata</param>
        /// <returns>Rezultat koji je ok i ima vrijednost</returns>
        public OkServiceResult<T> Ok<T>(T value)
        {
            return new OkServiceResult<T>(value);
        }

        /// <summary>
        /// Vrati rezultat koji predstavlja gresku kada entitet nije pronaden
        /// </summary>
        /// <returns>Rezultat koji predstavlja gresku kada entitet nije pronaden</returns>
        public NotFoundServiceResult NotFound()
        {
            return new NotFoundServiceResult();
        }

        /// <summary>
        /// Vrati rezultat koji predstavlja neodredenu gresku
        /// </summary>
        /// <param name="message">Opis greske</param>
        /// <returns>Rezultat koji predstavlja neodredenu gresku</returns>
        public ErrorServiceResult Error(String message, bool prefix = true)
        {
            return new ErrorServiceResult(message, prefix);
        }

        /// <summary>
        /// Vrati rezultat koji predstavlja neodredenu gresku, sa parametrima za formatiranje opisa
        /// </summary>
        /// <param name="message">Opis greske</param>
        /// <param name="args">Parametri za formatiranje opisa</param>
        /// <returns>Rezultat koji predstavlja neodredenu gresku</returns>
        public ErrorServiceResult Error(String message, params String[] args)
        {
            message = String.Format(message, args);
            return new ErrorServiceResult(message);
        }

        /// <summary>
        /// Vrati rezultat koji predstavlja gresku validacije
        /// </summary>
        /// <param name="message">Opis greske</param>
        /// <returns>Rezultat koji predstavlja gresku validacije</returns>
        public ValidationErrorServiceResult ValidationError(String message)
        {
            return new ValidationErrorServiceResult(message);
        }

        /// <summary>
        /// Vrati rezultat koji predstavlja gresku validacije, sa parametrima za formatiranje opisa
        /// </summary>
        /// <param name="message">Opis greske</param>
        /// <param name="args">Parametri za formatiranje opisa</param>
        /// <returns>Rezultat koji predstavlja gresku validacije</returns>
        public ValidationErrorServiceResult ValidationError(String message, params String[] args)
        {
            message = String.Format(message, args);
            return new ValidationErrorServiceResult(message);
        }

        /// <summary>
        /// Vrati rezultat koji predstavlja gresku kada neki povezani entitet nije pronaden
        /// </summary>
        /// <param name="name">Ime povezanog entiteta</param>
        /// <returns>Rezultat koji predstavlja gresku kada neki povezani entitet nije pronaden</returns>
        public MissingEntityServiceResult MissingEntity(String name)
        {
            return new MissingEntityServiceResult(name);
        }

        /// <summary>
        /// Vrati rezultat koji predstavlja gresku kada entitet koji se kreira vec postoji
        /// </summary>
        /// <param name="propertyName">Ime kolone u kojoj postoji konflikt</param>
        /// <returns>Rezultat koji predstavlja gresku kada entitet koji se kreira vec postoji</returns>
        public ExistingEntityServiceResult ExistingEntity(String propertyName)
        {
            return new ExistingEntityServiceResult(propertyName);
        }

        /// <summary>
        /// Ovo je metoda koju je potrebno pozivati
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <param name="securityLevel"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Secure<TEntity>(IQueryable<TEntity> query, SecurityLevel securityLevel) where TEntity : class
        {
            return Secure(query, securityLevel, Scope);
        }

        /// <summary>
        /// Ovo je namijenjeno da se direktno poziva samo iz sifarnikservice
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <param name="securityLevel"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> Secure<TEntity>(IQueryable<TEntity> query, SecurityLevel securityLevel, ILifetimeScope scope) where TEntity : class
        {
            if (typeof(TEntity).GetTypeInfo().BaseType == (typeof(Entities.Models.Sifarnik.Sifarnik)))
            {
                var securityFilter = scope.Resolve<ISecurityFilter<Entities.Models.Sifarnik.Sifarnik>>();

                return securityFilter.Secure(query.Cast<Entities.Models.Sifarnik.Sifarnik>(), securityLevel).Cast<TEntity>();
            }
            else
            {
                //Ako ima eksplicitna konfiguracija onda je koristi
                var securityFilter = scope.Resolve<ISecurityFilter<TEntity>>();
                if (securityFilter != null)
                    return securityFilter.Secure(query, securityLevel);

            }

            throw new Exception("Ne postoji implementacija security filtera");
        }

        /// <summary>
        ///Ovo je metoda koju je potrebno pozivati za prevode entiteta
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>      
        /// <returns></returns>
        public IQueryable<TEntity> Prevedi<TEntity>(IQueryable<TEntity> query, int? jezik) where TEntity : class
        {

            return Prevedi(query, Scope, jezik);
        }

        public static ICollection<TEntity> Prevedi<TEntity>(ICollection<TEntity> query, ILifetimeScope scope, int? jezik) where TEntity : class
        {

            var prevod = Prevedi(query.AsQueryable(), scope, jezik);
            return prevod.ToList();
        }

        public String PrevediTekst(String key, int? jezik)
        {
            XDocument doc = new XDocument();
            if (jezik == 0)
            {
                doc = XDocument.Load("Prevod/bs.xml");

            }
            if (jezik == 1)
            {
                doc = XDocument.Load("Prevod/hr.xml");
            }
            if (jezik == 2)
            {
                doc = XDocument.Load("Prevod/sr.xml");
            }

            var definitions = doc.Descendants("Definition")
                     .Select(x => new {
                         Execution = x.Attribute("Execution").Value,
                         Name = x.Attribute("Name").Value
                     });

            var nn = definitions.Where(x => x.Execution == key).FirstOrDefault();
            if (nn != null)
            {
                return nn.Name;
            }
            return "";

        }

        /// <summary>
        ///Ovo je metoda koju je potrebno pozivati za prevode entiteta
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>      
        /// <returns></returns>
        public static IQueryable<TEntity> Prevedi<TEntity>(IQueryable<TEntity> query, ILifetimeScope scope, int? jezik) where TEntity : class
        {
            var authService = scope.Resolve<IAuthService>();
            var context = scope.Resolve<Context>();
            if (jezik == null)
            {
                var korisnik = authService.TrenutniKorisnik();
                if (korisnik.Jezik.HasValue)
                {
                    jezik = korisnik.Jezik;
                }
            }
            if (query.Count() > 0 && jezik.HasValue)
            {
                var firstEntity = query.FirstOrDefault();
                var entityType = context.Model.FindEntityType(firstEntity.ToString());
                var entityName = entityType.SqlServer().TableName;
                //dobavi prevode
                var prevod = context.Prevodi
                    .Where(x => x.Tabela == entityName
                    && x.Jezik == jezik)
                    .Select(x => new {
                        redId = x.RedId,
                        polje = x.Polje,
                        prevodTekst = x.PrevodTekst
                    }).AsQueryable();


                var prevodQuery = from q in query
                                  join p in prevod on Convert.ToInt32(q.GetType().GetProperty("Id").GetValue(q)) equals p.redId
                                  select new
                                  {
                                      Q = q,
                                      P = p
                                  }
                               ;


                prevodQuery.Where(q => q.P != null).ToList()
                                     .ForEach(q => q.Q.GetType().GetProperties()
                                        .Where(s => s.Name == q.P.polje).ToList()
                                           .ForEach(item => item.SetValue(q.Q, q.P.prevodTekst)));



                var prevodQueryFin = prevodQuery.Select(x => x.Q);
                var prevodQueryFinList = prevodQueryFin.ToList();
                var queryBezPrevoda = query.Where(q => !prevodQueryFinList.Contains(q));
                query = queryBezPrevoda.Union(prevodQueryFin);

            }


            return query;

        }

        public int SaveChanges(Context context)
        {
            var korisnik = Scope.Resolve<IAuthService>().TrenutniKorisnik();
            return context.SaveChanges(korisnik?.KorisnickoIme);
        }

        public bool ImaPravo(string akcija)
        {
            var korisnik = Scope.Resolve<IAuthService>().TrenutniKorisnik();
            var context = Scope.Resolve<Context>();

            var imaAkciju = context.PravoAkcijaUloge
                                .Any(a => a.PravoAkcija.Sifra == akcija
                                                && a.UlogaId == korisnik.TrenutnaUlogaId);
            //var imaAkciju = true;
            return imaAkciju;
        }
    }
}