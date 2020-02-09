using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Entities.Models.Base
{
    public class EntityAutoriziran : EntityDated
    {
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
