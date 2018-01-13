using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dbwt.Models;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace dbwt.Controllers
{
    public class MenueXMLController : Controller
    {

        public List<Produkt> ParseMenueFromXML(){


            Menue neuesMenue = new Menue();

            Dictionary<string, Produkt> Produkte = new Dictionary<string, Produkt>();
            List<Produkt> ret = new List<Produkt>();
            /*Auslesen des XML und Speichern in einer Liste mit Menü Objekten*/

            try
            {  /*TODO PFAD FIXEN   */

                String username = "";
               username =  HttpContext.Session.Get<String>("user");

//                  var document = XDocument.Load("https://pastebin.com/raw/G7QV5Sz6");
  //              XElement root = document.Root; // Wurzelelement
               // var menu = from e in root.Descendants("Menu") select e; //where (int)(Convert.ToDateTime(e.Attribute("Tag").Value).DayOfWeek) == (int)DateTime.Today.DayOfWeek select e;
               // var kw = (from e in root.Descendants("Menu") where (int)(Convert.ToDateTime(e.Attribute("Tag").Value).DayOfWeek) == (int)DateTime.Today.DayOfWeek select e.Attribute("Kalenderwoche")).ToList()[0].Value.ToString();
               // var motto = (from e in menu.Elements() select e).ToList()[0].Value.ToString();
               // var produkte = (from e in menu.Descendants("Produkte").Descendants("Produkt") select e).ToList();


            //    neuesMenue.Motto = motto.ToString();
             //   neuesMenue.KW = int.Parse(kw);
                //neuesMenue.Produkte = produkte;


                Produkt p = new Produkt();
                p.Beschreibung = "Toll";
                p.vegan = false;
                p.vegetarisch = false;
                p.Typ = "Tellergericht";
                p.pid = 1;
                Produkte.Add(p.pid.ToString(), p);

                p.Beschreibung = "Toll";
                p.vegan = false;
                p.Typ = "Tellergericht";
                p.vegetarisch = false;
                p.pid = 2;
                Produkte.Add(p.pid.ToString(), p);

                p.Beschreibung = "Toll";
                p.vegan = false;
                p.Typ = "Tellergericht";
                p.vegetarisch = false;
                p.pid = 3;
                Produkte.Add(p.pid.ToString(), p);


                MySqlConnection con1 = new MySqlConnection(DB_ACCESS.Instance.get_conn_string()); // lässt sich per using(){} noch besser handhabe
                con1.Open();
                MySqlCommand cmd;



        



                foreach (KeyValuePair<string, Produkt> entry in Produkte)
                {
                    cmd = con1.CreateCommand();
                    cmd.CommandText = "SELECT `Gastbetrag`,`Studentenbetrag`,`Mitarbeiterbetrag`, `Produkt`.`Id` AS `Id`, `Bild`.`DetailsBildPfad`, `Beschreibung`, `Produkt`.`Titel` FROM `Produkt` LEFT JOIN `Preis` ON `Preis`.`Id` = `Produkt`.`FK_Preis` LEFT JOIN `Bild` ON `Bild`.`Id` = `Produkt`.`FK_Bild` WHERE `Produkt`.`Id`='" + entry.Value.pid + "' LIMIT 1";
                    MySqlDataReader r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                        Produkt tmp = entry.Value;
                        tmp.Titel = (String)r["Titel"];       
                        tmp.preis = new Preis(r["Studentenbetrag"].ToString(), r["Gastbetrag"].ToString(), r["Mitarbeiterbetrag"].ToString());
                        tmp.Beschreibung = r["Beschreibung"].ToString();
                        tmp.Bilder = new List<Bild>();
                        tmp.Bilder.Add(new Bild((String)r["DetailsBildPfad"]));
                        ret.Add(tmp);
                        break;
                    }
                    r.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



            return ret;
        }


        public IActionResult Index()
        {



            List<Produkt> m = ParseMenueFromXML();


            
            ViewData["img_path"] = "default.png";
            String tbl_string = "<table width='100%'><tr><th>TYP</th><th>Mahltzeit</th><th>Preis</th></tr>";



            for (int i = 0; i < m.Count(); i++)
            {

                String preis = "-- bitte erfragen --";


                if (!String.IsNullOrEmpty(HttpContext.Session.Get<String>("role")) && HttpContext.Session.Get<String>("role") == "Student")
                {
                    preis = m[i].preis.Studentenbetrag.ToString() + "€";
                }
                
                else if (!String.IsNullOrEmpty(HttpContext.Session.Get<String>("role")) && HttpContext.Session.Get<String>("role") == "Mitarbeiterbetrag")
                {
                    preis = m[i].preis.Mitarbeiterbetrag.ToString() + "€";
                }
                else
                {
                    preis = m[i].preis.Gastbeitrag.ToString() + "€";
                }


                    tbl_string += "<tr><td>" + m[i].Typ.ToString() + "</td><td><p><b>" + m[i].Titel.ToString() + "</b></p><br><p>" + m[i].Beschreibung.ToString() + "</p></td><td>" + preis + "</td></tr>";

            }



            tbl_string += "</table>";
            ViewData["table"] = tbl_string;




            return View();
        }
    }
}


