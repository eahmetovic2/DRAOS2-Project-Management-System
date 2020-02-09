using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Web.Entities.Models.Base
{
    /// <summary>
    /// Postavke sistema
    /// </summary>
    public class Postavke : EntityAutoriziran
    {
        /// <summary>
        /// Konstruktor sa defaultnim vrijednostima postavki
        /// </summary>
        public Postavke()
        {
            NaslovSistema = "Web";
            TrajanjeSesije = 5;
            UrlKarte = "http://{s}.tile.osm.org/{z}/{x}/{y}.png";
            AutorskaPravaKarte = @"&copy; <a href=""http://osm.org/copyright"">OpenStreetMap</a> contributors";
           
        }

        /// <summary>
        /// Id postavki
        /// </summary>
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Naslov sistema (koristi se u frontendu)
        /// </summary>
        [Required]
        [StringLength(64)]
        public String NaslovSistema { get; set; }

        /// <summary>
        /// Trajanje sesije korisnika u danima
        /// </summary>
        [Range(1, 10)]
        public int TrajanjeSesije { get; set; }

        /// <summary>
        /// Url koji se koristi kao provajder karti
        /// </summary>
        [Required]
        [StringLength(128)]
        public String UrlKarte { get; set; }

        /// <summary>
        /// Autorska prava provajdera karte
        /// </summary>
        [Required]
        [StringLength(256)]
        public String AutorskaPravaKarte { get; set; }

     
    }
}
