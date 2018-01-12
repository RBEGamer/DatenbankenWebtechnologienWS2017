using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dbwt.Models
{
    public class Produkt
    {
        public bool vegan { get; set; }

        public bool vegetarisch { get; set; }

        public string Beschreibung { get; set; }

        List<Bild> Bilder;


    }
}
