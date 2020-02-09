using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Core.Constants
{
    /// <summary>
    /// Uloga korisnika u sistemu
    /// </summary>
    public enum Uloga
    {
        /// <summary>
        /// Uloga nije odredena, ne bi trebalo da se desi
        /// </summary>
        Neodredeno = 0,

        /// <summary>
        /// Korisnik je administrator sistema
        /// </summary>
        Administrator = 1,

        Moderator = 2,

        Support = 3,

        TaskUser = 4
    }
}
