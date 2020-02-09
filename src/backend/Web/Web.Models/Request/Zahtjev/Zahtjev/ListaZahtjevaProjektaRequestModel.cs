using System;
using System.Collections.Generic;
using System.Text;
using Web.Models.Request.Common;

namespace Web.Models.Request.Zahtjev.Zahtjev
{
    public class ListaZahtjevaProjektaRequestModel:PagedRequestModel
    {
        public String Naziv { get; set; }
        public String Opis { get; set; }
        public int? DioProjektaId { get; set; }
        public int? KategorijaId { get; set; }
        public int? TipId { get; set; }
        public int? PrioritetId { get; set; }
        public int? StatusId { get; set; }
        public string CreatedBy { get; set; }


    }
}
