using Emis.Web.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Models.Response.Base.Dashboard;

namespace Web.Models.Response.Dashboard
{
    public class DashboardModel
    {
        public int BrojZahtjevaPotrebnoUraditi { get; set; }

        public int BrojZahtjevaUProgresu { get; set; }

        public int BrojZavrsenihZahtjeva { get; set; }

        public int BrojProjekata { get; set; }

        public int BrojZahtjevaUkupno { get; set; }

        public int BrojZahtjevaKojiNisuRijeseni { get; set; }

        public int BrojKomentaraKorisnika { get; set; }

    }
}
