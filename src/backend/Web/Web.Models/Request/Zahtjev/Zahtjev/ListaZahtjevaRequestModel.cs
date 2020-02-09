using System;
using System.Collections.Generic;
using Web.Models.Request.Common;

namespace Emis.Web.Models.Request
{
    public class ListaZahtjevaRequestModel : PagedRequestModel
    {
        public String Naziv { get; set; }
        public String Opis { get; set; }
        public int? ProjekatId { get; set; }
        public int? DioProjektaId { get; set; }
        public int? KategorijaId { get; set; }
        public int? TipId { get; set; }
        public int? PrioritetId { get; set; }
        public int? StatusId { get; set; }
        public string CreatedBy { get; set; }
        public int? PrethodniBrojDana { get; set; }
        public bool ToDo { get; set; }
        public bool InProgress { get; set; }
        public bool Done { get; set; }

    }
}
