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

            List<Menue> menues = new List<Menue>();


            /*Auslesen des XML und Speichern in einer Liste mit Menü Objekten*/

            try
            {  /*TODO PFAD FIXEN   */

                
                

                var document = XDocument.Load(@"C:\DBWT\XMLFiles\mondial-europe.xml");
                XElement root = document.Root; // Wurzelelement
                IEnumerable<XElement> menueListings = document.Elements();

                var prods = root
               .Descendants("Produkte")
               .SelectMany(x => x.Descendants("Produkt"));



                //          Erzeugt eine List<XElement> mit allen <City> Elementen aus desc

                var prodIds = prods.Elements("city").Value.ToList();


                MySqlConnection con1 = new MySqlConnection(DB_ACCESS.Instance.get_conn_string()); // lässt sich per using(){} noch besser handhabe

                    con1.Open();
                    MySqlCommand cmd;
                    cmd = con1.CreateCommand();
                    cmd.CommandText = "SELECT * FROM `FE-Nutzer` WHERE `Loginname`='" + username + "' LIMIT 1";
                    MySqlDataReader r = cmd.ExecuteReader();










                    foreach ( var listings in menueListings ) {

                    Menue eintrag = new Menue();
                    eintrag.KW = listings.Element("Kalenderwoche").Value ;
                    eintrag.Motto = listings.Element("Motto").Value;
                    eintrag.Tag = listings.Element("Tag").Value;
  
                    var prods = xel.Descendants("Produkte");
                   

                   
                    eintrag.Produkte = prods.ToDictionary(
                        m => m.Attribute("ProduktID").Value,
                        m => m.Element("Produkt").Value
                        );




                    menues.Add(listings)
                }



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


