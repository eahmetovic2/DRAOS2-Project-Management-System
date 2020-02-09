using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Core.Constants
{
    /// <summary>
    /// Akcija za log
    /// </summary>
    public enum LogAkcija
    {
        /// <summary>
        /// Default akcija koja ne treba da bude korištena
        /// </summary>
        Nijedna = 0,

      
        /// Kategorija Pristup_sistemu

        /// <summary>
        /// Kada se korisnik pristupi sistemu sa korisničkim podacima
        /// Kategorija Pristup_sistemu
        /// </summary>
        Prijava_na_sistem = 1,
        /// <summary>
        /// Kada se korisnik odjavi sa sistema
        /// Kategorija Pristup_sistemu
        /// </summary>
        Odjava_sa_sistema = 2,
        /// <summary>
        /// Neuspješna prijava na sistem
        /// Kategorija Pristup_sistemu
        /// </summary>
        Neuspjela_prijava = 3,



        ///Kategorija Izvjestaji
        ///
        /// <summary>
        /// Kada se generiše izvještaj
        /// Kategorija Izvjestaji
        /// </summary>
        izvjestaj_generisan = 56,

        ///Kategorija Korisnik
       
        /// <summary>
        /// Kada se doda korisnik
        /// Kategorija Korisnik
        /// </summary>
        korisnik_dodaj = 57,
        /// <summary>
        /// Kada se izmijeni korisnik
        /// Kategorija Korisnik
        /// </summary>
        korisnik_izmijeni = 58,

        ///Kategorija Sifarnik
   
        /// <summary>
        /// Kada se doda novi red u neki od sifarnika
        /// Kategorija Sifarnik
        /// </summary>
        sifarnik_dodaj_red = 59,
        /// <summary>
        /// Kada se izmijeni neki od sifarnika
        /// Kategorija Sifarnik
        /// </summary>
        sifarnik_izmijeni_red = 60,

        zahtjev_izmijeni=61
      
    }
}
