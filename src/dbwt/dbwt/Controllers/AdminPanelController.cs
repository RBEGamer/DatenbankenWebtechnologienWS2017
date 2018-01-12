using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dbwt.Models;
using MySql.Data.MySqlClient;

namespace dbwt.Controllers
{
    public class AdminPanelController : Controller
    {
        public IActionResult FeNutzerData(String id)
        {



            ViewData["Title"] = "Administration";
            MySqlConnection con1 = new MySqlConnection(DB_ACCESS.Instance.get_conn_string()); // lässt sich per using(){} noch besser handhaben

            List<FeNutzer> newRegistered = new List<FeNutzer>();
            List<FeNutzer> lastLogins = new List<FeNutzer>();

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

                String cmdb = "SELECT * FROM `FENUTZER` WHERE `verified`==0 order by Anlegedatum asc";


                cmd.CommandText = cmdb;
                MySqlDataReader r = cmd.ExecuteReader();
              
                while (r.Read())
                {
                    FeNutzer nutzer = new FeNutzer();
                    
                    nutzer.CreatedOnDate = (DateTime)r["Anlegedatum"];
                    nutzer.isAdmin = (bool)r["admin"];
                    nutzer.isVerified = (bool)r["verified"];
                    nutzer.LastLogin = (DateTime)r["LetzterLogin"];
                    nutzer.NameWithEmail = (string)r["Login-Name"]+ (String)r["E-Mail"];
                    nutzer.Rolle = (string)r["Benutzerrolle"];
                    nutzer.FeUserID = (int)r["Nr"];

                    newRegistered.Add(nutzer);
                    
                }

                

                cmdb = "SELECT * FROM `FENUTZER` WHERE `verified`==1 order by lastLogin asc limit 20";


                cmd.CommandText = cmdb;
                MySqlDataReader result2 = cmd.ExecuteReader();
                
                while (r.Read())
                {
                    FeNutzer nutzer = new FeNutzer();

                    nutzer.CreatedOnDate = (DateTime)r["Anlegedatum"];
                    nutzer.isAdmin = (bool)r["admin"];
                    nutzer.isVerified = (bool)r["verified"];
                    nutzer.LastLogin = (DateTime)r["LetzterLogin"];
                    nutzer.NameWithEmail = (string)r["Loginname"] + (String)r["E-Mail"];
                    nutzer.Rolle = (string)r["Benutzerrolle"];
                    nutzer.FeUserID = (int)r["Nr"];
                    lastLogins.Add(nutzer);


                }

                con1.Close();




            }


            catch (Exception e)
            {
                
            }

            return View();
        }
    }
}