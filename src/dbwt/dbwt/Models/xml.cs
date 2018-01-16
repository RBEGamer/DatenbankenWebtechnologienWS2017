using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Xml.Linq;

namespace dbwt
{
    public class XMLFactory
    {
        string XMLFILE = ("./Speiseplan.xml");
        private XElement root;

        private bool open()
        {
            bool result = true;

            try
            {
                var document = XDocument.Load(@XMLFILE);
                root = document.Root;

                System.Diagnostics.Debug.WriteLine("ok");
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("xml error");
                result = false;
            }

            return result;
        }

        public XMLFactory()
        {
            getMenuList();
        }

        public MenuList getMenuList(int weekday = -1)
        {
            MenuList result = new MenuList();

            if (weekday == -1)
            {
                weekday = (int)DateTime.Now.DayOfWeek;
            }

            if (open())
            {
                System.Diagnostics.Debug.WriteLine("weekday:" + weekday);

                try
                {
                    var menu = from e in root.Descendants("Menu") where (int)(Convert.ToDateTime(e.Attribute("Tag").Value).DayOfWeek) == weekday select e;
                    var kw = (from e in root.Descendants("Menu") where (int)(Convert.ToDateTime(e.Attribute("Tag").Value).DayOfWeek) == weekday select e.Attribute("Kalenderwoche")).ToList()[0].Value.ToString();
                    var motto = (from e in menu.Elements() select e).ToList()[0].Value.ToString();
                    var produkte = (from e in menu.Descendants("Produkte").Descendants("Produkt") select e).ToList();

                    //Liste mit Ergebnissen füllen
                    result.Motto = motto + " (KW " + kw + ")";

                    for (int i = 0; i < produkte.Count(); i++)
                    {
                        result.addProdukt(
                                Convert.ToInt32(produkte[i].Attribute("ProduktID").Value),
                                produkte[i].Attribute("Typ").Value.ToString()
                            );
                    }
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("Fehler in XML Verarbeitung");
                }

            }

            return result;

        }
    }
}