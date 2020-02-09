using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Models.Response.Upload
{
    public class UploadSettingsModel
    {
        public string PutanjaFoldera { get; set; }
        public string[] DozvoljeneEkstenzije { get; set; }
        public int MaksimalnaVelicinaMB { get; set; }
    }
}
