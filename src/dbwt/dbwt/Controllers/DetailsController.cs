using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dbwt.Models;
using MySql.Data.MySqlClient;


namespace dbwt.Controllers
{
    public class DetailsController : Controller
    {
       
       //role
        //preis
        //name
        //beschreibung
        //zutaten
        public IActionResult Index()
        {
            HttpContextNew.set_con(HttpContext);
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet]
        public ActionResult Index(String id)
        {
   
            ViewData["Title"] = "Details";
            MySqlConnection con1 = new MySqlConnection(DB_ACCESS.Instance.get_conn_string()); // lässt sich per using(){} noch besser handhaben

            

            if (!String.IsNullOrEmpty(HttpContext.Session.Get<String>("role")) && (HttpContext.Session.Get<String>("role") == "Student" || HttpContext.Session.Get<String>("role") == "Gast" || HttpContext.Session.Get<String>("role") == "Mitarbeiter"))
            {
                ViewData["role"] = HttpContext.Session.Get<String>("role");
            }
            else
            {
                ViewData["role"] = "Gast";
            }
            if (id != null)
            {
                ViewData["prodid"] = id.ToString();
            }
            else
            {
                //TODO SET HEADER
               // @Html.Raw("<script>alert('FEHLENDER PARAMETER')</script>");
            }


            try
            {
                con1.Open();
                MySqlCommand cmd;
                cmd = con1.CreateCommand();

                if (ViewData["prodid"] == null)
                {

                    ViewData["header_addition"] = "<meta http-equiv=\"refresh\" content=\"3; url=/\"/>";
                    return View();
                }

                String cmdb = "SELECT * FROM `Produkt` LEFT JOIN `Preis` ON `Preis`.`Id` = `Produkt`.`FK_Preis` LEFT JOIN `Bild` ON `Bild`.`Id` = `Produkt`.`FK_Bild` WHERE `Produkt`.`Id`='" + id.ToString() + "' LIMIT 1";


                cmd.CommandText = cmdb;
                MySqlDataReader r = cmd.ExecuteReader();
                bool done = false;
                while (r.Read())
                {
                    ViewData["Titel"] = (String)r["Titel"];
                    ViewData["Beschreibung"] = (String)r["Beschreibung"];



                    if (ViewData["role"] != null && (String)ViewData["role"] == "Student")
                    {
                        ViewData["preis"] = r["Studentenbetrag"];
                    }
                    else if (ViewData["role"] != null && (String)ViewData["role"] == "Mitarbeiter")
                    {
                        ViewData["preis"] = r["Mitarbeiterbetrag"];
                    }
                    else
                    {
                        ViewData["preis"] = r["Gastbetrag"];
                    }

                    ViewData["img_path"] = r["DetailsBildPfad"];
                    done = true;

                }


                con1.Close();

                if (!done)
                {

                    ViewData["header_addition"] = "<meta http-equiv=\"refresh\" content=\"3; url=/\"/>";
                }
            //< script >
            //    alert("Diese Produkt is leider nicht verfügbar");
            //        window.setTimeout('location.href="/"', 3000); ;
            //</ script >
            //            return;
            //    }

            }
            catch (Exception e)
            {
                ViewData["header_addition"] = "<meta http-equiv=\"refresh\" content=\"3; url=/\"/>";
            }
        

            return View();
        }
        
    }
}
