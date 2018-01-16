using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dbwt.Models
{


    public struct Preis
    {
        public Preis(String a, String b, String c)
        {
            Studentenbetrag = float.Parse(a);
            Mitarbeiterbetrag = float.Parse(b);
            Gastbeitrag = float.Parse(c);
        }
        public float Studentenbetrag;
        public float Mitarbeiterbetrag;
        public float Gastbeitrag;
    }
    public class Produkt
    {
        public int Id { get; set; }
        public bool vegan { get; set; }

        public bool vegetarisch { get; set; }

        public string Beschreibung { get; set; }

        public List<Bild> Bilder;

        public string Typ { get; set; }
        public string Titel { get; set; }

        public Preis preis { get; set; }
    }
}
