using Web.UserAgent;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Common.Extensions
{
    /// <summary>
    /// Ekstenzije za HttpRequest objekat
    /// </summary>
    public static class RequestExtensions
    {
        /// <summary>
        /// Vrati ip adresu korisnika
        /// </summary>
        /// <param name="request">Http request objekat</param>
        /// <returns>Ip adresu klijenta koji je pokrenuo request</returns>
        public static String GetIpAddress(this HttpRequest request)
        {
            if (request.Headers.ContainsKey("X-Forwarded-For"))
                return request.Headers["X-Forwarded-For"].ToString();
            else if (request.HttpContext.Connection.RemoteIpAddress != null)
                return request.HttpContext.Connection.RemoteIpAddress.ToString();
            return null;
        }

        /// <summary>
        /// Vrati ime klijenta korisnika
        /// </summary>
        /// <param name="request">Http request objekat</param>
        /// <param name="userAgentParser">Parser user agent stringa</param>
        /// <returns>Ime klijenta koji je pokrenuo request</returns>
        public static String ParseUserAgent(this HttpRequest request, IUserAgentParser userAgentParser)
        {
            if (request.Headers.ContainsKey("User-Agent"))
            {
                var userAgent = request.Headers["User-Agent"].ToString();
                var info = userAgentParser.Parse(userAgent);

                return info.GetInfo();
            }

            return null;
        }
    }
}
