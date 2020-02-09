using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Models.Response.Upload
{
    public class FileResult
    {
        public byte[] Bytes { get; set; }
        public string MimeType { get; set; }
        public string Naziv { get; set; }
    }
}
