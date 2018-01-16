using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Xml.Linq;

namespace dbwt
{
    public class XMLFactory
    {
        string XMLF = ("./Speiseplan.xml");
        private XElement root;

        private bool open()
        {
            bool result = true;

            try
            {
                var document = XDocument.Load(@XMLF);
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
                    var high = (from e in root.Descendants("Menu") where (int)(Convert.ToDateTime(e.Attribute("Tag").Value).DayOfWeek) == weekday select e.Attribute("Highlight")).ToList() ;
                    var motto = (from e in menu.Elements() select e).ToList()[0].Value.ToString();
                    var produkte = (from e in menu.Descendants("Produkte").Descendants("Produkt") select e).ToList();

                    result.Motto = motto + " (KW " + kw + ")";

                     int highligt_id = -1;
                    bool hl_day = false;
                    for (int i = 0; i < menu.Attributes().ToList().Count; i++)
                    {
                        if (menu.Attributes().ToList()[i].Name.ToString() == "Tag" && (int)DateTime.Parse(menu.Attributes().ToList()[i].Value).DayOfWeek == weekday)
                        {
                            //highligt_id = menu.Attributes().ToList()[0].Value;
                            hl_day = true;
                            continue;
                        }
                        if (menu.Attributes().ToList()[i].Name.ToString() == "Highlight")
                        {
                           highligt_id= int.Parse(menu.Attributes().ToList()[i].Value);
                            continue;
                        }

                    }

                    if (!hl_day)
                    {
                        highligt_id = -1;
                    }

                    result.Highlight = highligt_id;

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