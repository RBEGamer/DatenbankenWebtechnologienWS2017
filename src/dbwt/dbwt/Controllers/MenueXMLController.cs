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

        public Menue ParseMenueFromXML(){


            Menue neuesMenue = new Menue();

            Dictionary<string, Produkt> Produkte = new Dictionary<string, Produkt>();

            /*Auslesen des XML und Speichern in einer Liste mit Menü Objekten*/

            try
            {  /*TODO PFAD FIXEN   */




                var document = XDocument.Load(@"HIER PFAD ZUR XML");
                XElement root = document.Root; // Wurzelelement
                var menu = from e in root.Descendants("Menu") where (int)(Convert.ToDateTime(e.Attribute("Tag").Value).DayOfWeek) == weekday select e;
                var kw = (from e in root.Descendants("Menu") where (int)(Convert.ToDateTime(e.Attribute("Tag").Value).DayOfWeek) == weekday select e.Attribute("Kalenderwoche")).ToList()[0].Value.ToString();
                var motto = (from e in menu.Elements() select e).ToList()[0].Value.ToString();
                var produkte = (from e in menu.Descendants("Produkte").Descendants("Produkt") select e).ToList();


                neuesMenue.Motto = motto.ToString();
                neuesMenue.


                for (int i = 0; i < produkte.Count(); i++)
                {

                    Convert.ToString(produkte[i].Attribute("ProduktID").Value);


                    MySqlConnection con1 = new MySqlConnection(DB_ACCESS.Instance.get_conn_string()); // lässt sich per using(){} noch besser handhabe

                    con1.Open();
                    MySqlCommand cmd;
                    cmd = con1.CreateCommand();
                    cmd.CommandText = "SELECT * FROM `Produkte` WHERE `Loginname`='" + username + "' LIMIT 1";
                    MySqlDataReader r = cmd.ExecuteReader();



                    /*Datenbankabfrage hier */

                }



                
                


                MySqlConnection con1 = new MySqlConnection(DB_ACCESS.Instance.get_conn_string()); // lässt sich per using(){} noch besser handhabe

                    con1.Open();
                    MySqlCommand cmd;
                    cmd = con1.CreateCommand();
                    cmd.CommandText = "SELECT * FROM `FE-Nutzer` WHERE `Loginname`='" + username + "' LIMIT 1";
                    MySqlDataReader r = cmd.ExecuteReader();










                 



            }
            catch (System.Xml.XmlException e)
            {
                Console.WriteLine(e.Message);
            }




        }


        public IActionResult Index()
        {



  







            return View();
        }
    }
}


