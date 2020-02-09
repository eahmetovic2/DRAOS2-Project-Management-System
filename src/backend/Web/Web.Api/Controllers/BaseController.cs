using Web.Api.Common.Attributes;
using Web.Api.Common.Result;
using Web.Services.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    /// <summary>
    /// Bazni kontroler za aplikaciju
    /// </summary>
    [Authorize]
    [ValidateModel]
    public class BaseController : Controller
    {
        /// <summary>
        /// Konvertuj ServiceResult u IActionResult
        /// </summary>
        /// <typeparam name="T">Tip rezultata</typeparam>
        /// <param name="result">Rezultat koji se konvertuje</param>
        /// <returns>Odgovarajuci IActionResult za resultat koji je dat</returns>
        public IActionResult Convert<T>(ServiceResult<T> result)
        {
            var visitor = new ActionResultVisitor<T>();
            return result.Visit(visitor);
        }
    }
}
