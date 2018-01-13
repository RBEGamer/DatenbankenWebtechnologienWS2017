using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dbwt.Models
{


    public struct Preis
    {
        public Preis(float a, float b, float c)
        {
            Studentenbetrag = a;
            Mitarbeiterbetrag = b;
            Gastbeitrag = c;
        }
        public float Studentenbetrag;
        public float Mitarbeiterbetrag;
        public float Gastbeitrag;
    }
    public class Produkt
    {
        public int pid { get; set; }
        public bool vegan { get; set; }

        public bool vegetarisch { get; set; }

        public string Beschreibung { get; set; }

        List<Bild> Bilder;

        public string Typ { get; set; }
        public string Titel { get; set; }

        public Preis preis { get; set; }
    }
}
