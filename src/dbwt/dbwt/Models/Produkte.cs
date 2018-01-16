using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace dbwt
{

    public class MenuList
    {
        public List<MenuProdukt> Produkte = null;
        public string Motto { get; set; }

        public int Highlight { get; set; }

        public string Image { get; set; }
        public class MenuProdukt
        {
            public int ID { get; set; }
            public string Name { get; set; }

            public bool Vegan { get; set; }
            public bool Vegetarisch { get; set; }

            public String Beschreibung { get; set; }

            public String Typ { get; set; }

            public Preis preis;


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

        }


        


        public MenuList()
        {
            Produkte = new List<MenuProdukt>();
        }

        public void addProdukt(int id, string name)
        {
            MenuProdukt item = new MenuProdukt();
            item.ID = id;
            item.Typ = name;


            Produkte.Add(item);
        }

    }
}