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

        //public ActionResult Index()
        //{

        //    ViewData["Title"] = "Administration";
        //    MySqlConnection con1 = new MySqlConnection(DB_ACCESS.Instance.get_conn_string()); // lässt sich per using(){} noch besser handhaben

        //    List<FeNutzer> newRegistered = new List<FeNutzer>();
        //    List<FeNutzer> lastLogins = new List<FeNutzer>();

        //    if (!String.IsNullOrEmpty(HttpContext.Session.Get<String>("role")) && (HttpContext.Session.Get<String>("role") == "Student" || HttpContext.Session.Get<String>("role") == "Gast" || HttpContext.Session.Get<String>("role") == "Mitarbeiter"))
        //    {
        //        ViewData["role"] = HttpContext.Session.Get<String>("role");
        //    }
        //    else
        //    {
        //        ViewData["role"] = "Gast";
        //    }



        //    try
        //    {
        //        con1.Open();
        //        MySqlCommand cmd;
        //        cmd = con1.CreateCommand();



        //        String cmdb = "SELECT * FROM `FENUTZER` WHERE `verified`==0 order by Anlegedatum asc";


        //        cmd.CommandText = cmdb;
        //        MySqlDataReader r = cmd.ExecuteReader();

        //        while (r.Read())
        //        {
        //            FeNutzer nutzer = new FeNutzer();

        //            nutzer.CreatedOnDate = (DateTime)r["Anlegedatum"];
        //            nutzer.isAdmin = (bool)r["admin"];
        //            nutzer.isVerified = (bool)r["verified"];
        //            nutzer.LastLogin = (DateTime)r["LetzterLogin"];
        //            nutzer.NameWithEmail = (string)r["Login-Name"] + (String)r["E-Mail"];
        //            nutzer.Rolle = (string)r["Benutzerrolle"];
        //            nutzer.FeUserID = (int)r["Nr"];

        //            newRegistered.Add(nutzer);

        //        }



        //        cmdb = "SELECT * FROM `FENUTZER` WHERE `verified`==1 order by lastLogin asc limit 20";


        //        cmd.CommandText = cmdb;
        //        MySqlDataReader result2 = cmd.ExecuteReader();

        //        while (r.Read())
        //        {
        //            FeNutzer nutzer = new FeNutzer();

        //            nutzer.CreatedOnDate = (DateTime)r["Anlegedatum"];
        //            nutzer.isAdmin = (bool)r["admin"];
        //            nutzer.isVerified = (bool)r["verified"];
        //            nutzer.LastLogin = (DateTime)r["LetzterLogin"];
        //            nutzer.NameWithEmail = (string)r["Loginname"] + (String)r["E-Mail"];
        //            nutzer.Rolle = (string)r["Benutzerrolle"];
        //            nutzer.FeUserID = (int)r["Nr"];
        //            lastLogins.Add(nutzer);


        //        }

        //        con1.Close();




        //    }


        //    catch (Exception e)
        //    {

        //    }

        //    return PartialView();



        //}


        //public IActionResult Index()
        //{
        //    UserToLINQ db = new UserToLINQ();

        //    //LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = true;


        //    //var query = from nutzer in db.FeNutzer where nutzer.LetzterLogin != null select nutzer;
        //    //var query = from nutzer in db.FeNutzer select nutzer;

        //    //  var query = db.getLINQ_TABLE().FeNutzer.OrderByDescending(nutzer => nutzer.LetzterLogin).Select(nutzer => nutzer).Where(nutzer => nutzer.Aktiv == true); ;
        //    //  var query1 = db.FeNutzer.OrderByDescending(nutzer => nutzer.Anlegedatum).Select(nutzer => nutzer).Where(nutzer => nutzer.Aktiv == false);

        //    var not_active_user = from e in db.getLINQ_TABLE().Descendants("FE-Nutzer") where e.Attribute("Aktiv").Value == "1" select e;

        //    //  List<DataModels.FeNutzer> list_anmeldung = query.ToList<DataModels.FeNutzer>();
        //    //  List<DataModels.FeNutzer> list_registrierung = query1.ToList<DataModels.FeNutzer>();


        //    //Models.ListenContainer container = new Models.ListenContainer();
        //     //   container.list_anmeldung = list_anmeldung;
        //      //  container.list_registrierung = list_registrierung;

        //        return View();

        //    }

        //}

        [HttpGet]
        public IActionResult Index()
    {
        UserToLINQ db = new UserToLINQ();


            var query_letzte_anmeldung = from el in db.getLINQ_TABLE().Root.Descendants() where  bool.Parse(el.Attribute("Aktiv").Value.ToString()) == true select el;
            var query_registrierungen = from el in db.getLINQ_TABLE().Root.Descendants() where bool.Parse(el.Attribute("Aktiv").Value.ToString()) == false select el;


            IEnumerable<user_table> result_reg = from c in db.getLINQ_TABLE().Root.Descendants()
                                           where bool.Parse(c.Attribute("Aktiv").Value.ToString()) == false 
                                           select new user_table()
                                                  {
                                                      ID = (string)c.Attribute("Nr"),
                                                      Email = (string)c.Attribute("Email"),
                                                      time = (string)c.Attribute("Anlegedatum"),
                                                        username = (string)c.Attribute("Loginname")
                                           };

            IEnumerable<user_table> result_login = from c in db.getLINQ_TABLE().Root.Descendants()
                                                 where bool.Parse(c.Attribute("Aktiv").Value.ToString()) == true
                                                 select new user_table()
                                                 {
                                                     ID = (string)c.Attribute("Nr"),
                                                     Email = (string)c.Attribute("Email"),
                                                     time = (string)c.Attribute("Anlegedatum"),
                                                     username = (string)c.Attribute("Loginname")
                                                 };

            List<user_table> list_reg = result_reg.ToList<user_table>();
            List<user_table> list_login = result_login.ToList<user_table>();










            if (!HttpContext.Session.Get<bool>("admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["admin"] = HttpContext.Session.Get<bool>("admin");



            return View(new user_table_info(list_reg, list_login));
        }

        [HttpPost]
        public IActionResult Index(string id, string action)
        {


            //TODO ENABLE STUFF
            if(!String.IsNullOrEmpty(id) && !String.IsNullOrEmpty(action))
            {
                if (action != null && action == "enable" && HttpContext.Session.Get<bool>("admin"))
                {
                    MySqlConnection con = new MySqlConnection(DB_ACCESS.Instance.get_conn_string());
                    con.Open();
                    MySqlCommand cmd;
                    cmd = con.CreateCommand();
                    cmd.CommandText = "UPDATE `FE-Nutzer` SET `Aktiv`='1' WHERE `Nr` = '" + id + "'";
                    MySqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {

                    }



                    con.Close();
                }

                if (action != null && action == "disbale" && HttpContext.Session.Get<bool>("admin"))
                {
                    MySqlConnection con = new MySqlConnection(DB_ACCESS.Instance.get_conn_string());
                    con.Open();
                    MySqlCommand cmd;
                    cmd = con.CreateCommand();
                    cmd.CommandText = "UPDATE `FE-Nutzer` SET `Aktiv`='0' WHERE `Nr` = '" + id + "'";
                    MySqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {

                    }



                    con.Close();
                }


            }






            UserToLINQ db = new UserToLINQ();


            var query_letzte_anmeldung = from el in db.getLINQ_TABLE().Root.Descendants() where bool.Parse(el.Attribute("Aktiv").Value.ToString()) == true select el;
            var query_registrierungen = from el in db.getLINQ_TABLE().Root.Descendants() where bool.Parse(el.Attribute("Aktiv").Value.ToString()) == false select el;


            IEnumerable<user_table> result_reg = from c in db.getLINQ_TABLE().Root.Descendants()
                                                 where bool.Parse(c.Attribute("Aktiv").Value.ToString()) == false
                                                 select new user_table()
                                                 {
                                                     ID = (string)c.Attribute("Nr"),
                                                     Email = (string)c.Attribute("Email"),
                                                     time = (string)c.Attribute("Anlegedatum"),
                                                     username = (string)c.Attribute("Loginname")
                                                 };

            IEnumerable<user_table> result_login = from c in db.getLINQ_TABLE().Root.Descendants()
                                                   where bool.Parse(c.Attribute("Aktiv").Value.ToString()) == true
                                                   select new user_table()
                                                   {
                                                       ID = (string)c.Attribute("Nr"),
                                                       Email = (string)c.Attribute("Email"),
                                                       time = (string)c.Attribute("Anlegedatum"),
                                                       username = (string)c.Attribute("Loginname")
                                                   };

            List<user_table> list_reg = result_reg.ToList<user_table>();
            List<user_table> list_login = result_login.ToList<user_table>();



            if (!HttpContext.Session.Get<bool>("admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["admin"] = HttpContext.Session.Get<bool>("admin");


            return View(new user_table_info(list_reg, list_login));
        }




        public ActionResult _RegTableReg()
        {
           
            return PartialView();
        }

        public ActionResult _RegTableLogin()
        {

            return PartialView();
        }

    }
}
