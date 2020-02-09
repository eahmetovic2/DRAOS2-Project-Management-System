using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Core.Auth.Implementation
{
    /// <summary>
    /// BCrypt implementacija hash provajder interfejsa
    /// </summary>
    public class BcryptHashProvider : IHashProvider
    {
        public byte[] HashPassword(String password)
        {
            // kreiramo hash lozinke i vratimo niz bajta
            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            return Encoding.UTF8.GetBytes(hash);
        }

        public bool ValidatePassword(String password, byte[] hash)
        {
            if (password == null || hash == null)
                return false;

            try
            {
                // pretvorimo bajte u string i provjerimo da li se poklapaju sa lozinkom
                var hashString = Encoding.UTF8.GetString(hash);
                return BCrypt.Net.BCrypt.Verify(password, hashString);
            }
            catch
            {
                // u sljucaju greske javljamo da se ne poklapaju
                return false;
            }
        }
    }
}
