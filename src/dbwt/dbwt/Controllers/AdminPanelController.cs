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

       // public ActionResult FeNutzer()
        //{
          //  var model = repository.GetThingByParameter(parameter1);
          //  var partialViewModel = new PartialViewModel(model);
          //  return PartialView(partialViewModel);
        //}

        public ActionResult Index()
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



            try
            {
                con1.Open();
                MySqlCommand cmd;
                cmd = con1.CreateCommand();



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
                    nutzer.NameWithEmail = (string)r["Login-Name"] + (String)r["E-Mail"];
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

            return PartialView();



        }
    }
}
