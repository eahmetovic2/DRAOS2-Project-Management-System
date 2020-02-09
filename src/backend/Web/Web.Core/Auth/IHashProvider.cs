using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Core.Auth
{
    /// <summary>
    /// Interfejs koji predstavlja objekat koji racuna i provjerava hasheve lozinki
    /// </summary>
    public interface IHashProvider
    {
        /// <summary>
        /// Izracunaj hash lozinke
        /// </summary>
        /// <param name="password">Lozinka za hashiranje</param>
        /// <returns>Hash lozinke kao niz bajta</returns>
        byte[] HashPassword(String password);

        /// <summary>
        /// Provjerava da li lozinka odgovara hash-u
        /// </summary>
        /// <param name="password">Lozinka koja se provjerava</param>
        /// <param name="hash">Hash koji se provjerava</param>
        /// <returns>Da li se data lozinka i hash podudaraju</returns>
        bool ValidatePassword(String password, byte[] hash);
    }
}
