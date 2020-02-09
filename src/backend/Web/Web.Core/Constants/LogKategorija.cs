using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Core.Constants
{
    /// <summary>
    /// Kategorija za log
    /// </summary>
    public enum LogKategorija
    {
        /// <summary>
        /// Default kategorija koja ne treba da bude korištena
        /// </summary>
        Nijedna = 0,
        /// <summary>
        /// Koristi se za prećenje prijava i odjava na sistem
        /// </summary>
        Pristup_sistemu = 1,     
      

        /// <summary>
        /// Koristi se za praćenje dešavanja po  korisnicima
        /// </summary>
        korisnik = 16,
        /// <summary>
        /// Koristi se za praćenje dešavanja po  izvjestajima
        /// </summary>
        izvjestaji = 17,
   
        /// <summary>
        /// Koristi se za praćenje dešavanja po  izvjestajima
        /// </summary>
        dokumenti = 19,
        /// <summary>
        /// Koristi se za praćenje dešavanja po  sifarnicima koje niko ne mijenja osim sa rolom Administrator
        /// </summary>
        sifarnici = 20,

        zahtjevi=21
     
    }
}
