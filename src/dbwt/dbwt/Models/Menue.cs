using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dbwt.Models
{
    public class Menue
    {
        public DateTime Tag { get; set; }

        public int KW { get; set; }

        public string Motto { get; set; }

        public Dictionary<string,Produkt> Produkte { get; set; }

        public Bild highlight { get; set; }

    }
}
