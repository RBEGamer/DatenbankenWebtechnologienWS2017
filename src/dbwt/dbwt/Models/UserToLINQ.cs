using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;


namespace dbwt.Models
{
    public class UserToLINQ
    {

        XDocument d;


        public XDocument getLINQ_TABLE()
        {
            return d;
        }

            public UserToLINQ()
            {





         


            MySqlConnection con1 = new MySqlConnection(DB_ACCESS.Instance.get_conn_string()); // lässt sich per using(){} noch besser handhabe
            con1.Open();
            MySqlCommand cmd;

            List<FeNutzer> n = new List<FeNutzer>();
                //GET IMAGE
                try
                {
                    cmd = con1.CreateCommand();
                    cmd.CommandText = "SELECT * FROM `FE-Nutzer` WHERE 1";
                    MySqlDataReader r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                    FeNutzer nutzer = new FeNutzer();
                    nutzer.Nr = int.Parse(r["Nr"].ToString());
                    nutzer.Aktiv = bool.Parse(r["Aktiv"].ToString());
                    nutzer.Vorname = r["Vorname"].ToString();
                    nutzer.Nachname = r["Nachname"].ToString();
                    nutzer.Loginname = r["Loginname"].ToString();
                    nutzer.Email = r["Email"].ToString();
                    nutzer.Hash = r["Hash"].ToString();
                    nutzer.Salt = r["Salt"].ToString();
                    nutzer.Algorythmus = r["Algorythmus"].ToString();
                    nutzer.Strech = int.Parse(r["Strech"].ToString());
                    nutzer.LetzterLogin = r["LetzterLogin"].ToString();
                    nutzer.Anlegedatum = r["Anlegedatum"].ToString();
                    nutzer.Benutzerrolle = r["Benutzerrolle"].ToString();
                    nutzer.verified = bool.Parse(r["verified"].ToString());
                    nutzer.admin = bool.Parse(r["admin"].ToString());
                    n.Add(nutzer);
                }
                }
                catch (Exception)
                {

                }




            List<XElement> elements = new List<XElement>();

            for (int i = 0; i < n.Count; i++)
            {

                XElement tmp = new XElement("Nutzer");
                tmp.Add(new XAttribute("Nr", n[i].Nr.ToString()));
                tmp.Add(new XAttribute("Aktiv", n[i].Aktiv));
                tmp.Add(new XAttribute("Vorname", n[i].Vorname.ToString()));
                tmp.Add(new XAttribute("Nachname", n[i].Nachname.ToString()));
                tmp.Add(new XAttribute("Email", n[i].Email.ToString()));
                tmp.Add(new XAttribute("Loginname", n[i].Loginname.ToString()));
                tmp.Add(new XAttribute("Hash", n[i].Hash.ToString()));
                tmp.Add(new XAttribute("Salt", n[i].Salt.ToString()));
                tmp.Add(new XAttribute("Algorythmus", n[i].Algorythmus.ToString()));
                tmp.Add(new XAttribute("Strech", n[i].Strech.ToString()));
                tmp.Add(new XAttribute("LetzterLogin", n[i].LetzterLogin.ToString()));
                tmp.Add(new XAttribute("Anlegedatum", n[i].Anlegedatum.ToString()));
                tmp.Add(new XAttribute("Benutzerrolle", n[i].Benutzerrolle.ToString()));
                tmp.Add(new XAttribute("verified", n[i].verified.ToString()));
                tmp.Add(new XAttribute("admin", n[i].admin.ToString()));
                elements.Add(tmp);
            }

             d = new XDocument(new XElement("FE-Nutzer", elements.ToArray()));
           
       

        }

            //public MenuList getMenuList(int weekday = -1)
            //{
            //    MenuList result = new MenuList();

            //    if (weekday == -1)
            //    {
            //        weekday = (int)DateTime.Now.DayOfWeek;
            //    }

            //    if (open())
            //    {
            //        System.Diagnostics.Debug.WriteLine("weekday:" + weekday);

            //        try
            //        {
            //            var menu = from e in root.Descendants("Menu") where (int)(Convert.ToDateTime(e.Attribute("Tag").Value).DayOfWeek) == weekday select e;
            //            var kw = (from e in root.Descendants("Menu") where (int)(Convert.ToDateTime(e.Attribute("Tag").Value).DayOfWeek) == weekday select e.Attribute("Kalenderwoche")).ToList()[0].Value.ToString();
            //            var high = (from e in root.Descendants("Menu") where (int)(Convert.ToDateTime(e.Attribute("Tag").Value).DayOfWeek) == weekday select e.Attribute("Highlight")).ToList();
            //            var motto = (from e in menu.Elements() select e).ToList()[0].Value.ToString();
            //            var produkte = (from e in menu.Descendants("Produkte").Descendants("Produkt") select e).ToList();

            //            result.Motto = motto + " (KW " + kw + ")";

            //            int highligt_id = -1;
            //            bool hl_day = false;
            //            for (int i = 0; i < menu.Attributes().ToList().Count; i++)
            //            {
            //                if (menu.Attributes().ToList()[i].Name.ToString() == "Tag" && (int)DateTime.Parse(menu.Attributes().ToList()[i].Value).DayOfWeek == weekday)
            //                {
            //                    //highligt_id = menu.Attributes().ToList()[0].Value;
            //                    hl_day = true;
            //                    continue;
            //                }
            //                if (menu.Attributes().ToList()[i].Name.ToString() == "Highlight")
            //                {
            //                    highligt_id = int.Parse(menu.Attributes().ToList()[i].Value);
            //                    continue;
            //                }

            //            }

            //            if (!hl_day)
            //            {
            //                highligt_id = -1;
            //            }

            //            result.Highlight = highligt_id;

            //            for (int i = 0; i < produkte.Count(); i++)
            //            {
            //                result.addProdukt(
            //                        Convert.ToInt32(produkte[i].Attribute("ProduktID").Value),
            //                        produkte[i].Attribute("Typ").Value.ToString()
            //                                                );
            //            }
            //        }
            //        catch
            //        {
            //            System.Diagnostics.Debug.WriteLine("Fehler in XML Verarbeitung");
            //        }

            //    }

            //    return result;

            //}
        }
    
}
