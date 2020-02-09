using System.Linq;

namespace Web.Core.Database
{
    /// <summary>
    /// Implemented security policy on a query level
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISecurityFilter<T>
    {
        IQueryable<T> Secure(IQueryable<T> query, SecurityLevel securityLevel);
    }
}
