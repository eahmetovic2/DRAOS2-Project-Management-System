﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web.Entities.Models.Base;

namespace Web.Entities.Models.Zahtjev
{
    public class Dokument : EntityAutoriziran
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Naziv dokumenta, koji se moze koristiti za prikaz
        /// </summary>
        public string Naziv { get; set; }

        /// <summary>
        /// Putanja u upload folderu
        /// </summary>
        public string Putanja { get; set; }

     
    }
}
