using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Web.Api.Auth.Requirements
{
    /// <summary>
    /// Provjera koja provjerava da li je korisnik vlasnik resursa
    /// </summary>
    public class UserIsOwnerChecker : IChecker
    {
        /// <summary>
        /// Default ime parametra koje nosi ime korisnika
        /// </summary>
        private const String defaultParameterName = "korisnickoIme";

        /// <summary>
        /// Ime parametra koje nosi ime korisnika
        /// </summary>
        public String ParameterName { get; private set; }

        /// <summary>
        /// Konstruktor provjere
        /// </summary>
        /// <param name="parameterName">Ime parametra koje nosi ime korisnika</param>
        public UserIsOwnerChecker(String parameterName = defaultParameterName)
        {
            this.ParameterName = parameterName;
        }

        /// <summary>
        /// Provjerava da li je korisnik vlasnik resursa
        /// </summary>
        public CheckResult Check(AuthorizationHandlerContext context, IChecker self)
        {
            // dobavi mvc kontekst
            var mvcContext = context.Resource as AuthorizationFilterContext;
            if (mvcContext == null)
                return CheckResult.Ignore;

            // dobavi ime prametra iz rute
            if (!mvcContext.RouteData.Values.ContainsKey(ParameterName))
                return CheckResult.Ignore;

            var routeValue = mvcContext.RouteData.Values[ParameterName].ToString();

            // dobavi ime trenutnog korisnika
            var userValue = context.User.FindFirst(ClaimTypes.Name);
            if (userValue == null)
                return CheckResult.Ignore;

            // da li je ime trenutnog korisnika i ime korisnika u resursu isto
            if (routeValue == userValue.Value)
                return CheckResult.Success;
            return CheckResult.Ignore;
        }
    }
}
