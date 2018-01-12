using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dbwt.Models
{
    public class Menue
    {
        public string Tag { get; set; }

        public string KW { get; set; }

        public string Motto { get; set; }

        public Dictionary<string,Produkt> Produkte { get; set; }

        public Bild highlight { get; set; }

    }
}
