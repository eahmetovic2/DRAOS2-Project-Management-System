using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Entities.Models.Base
{
    public class EntityEntryLog
    {
        public EntityState State { get; set; }
        public EntityEntry Entry { get; set; }
    }
}
