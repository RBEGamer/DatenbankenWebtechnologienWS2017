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
                                                        username = (string)c.Attribute("Loginname"),
                                                        role = (string)c.Attribute("Benutzerrolle")
                                           };

            IEnumerable<user_table> result_login = from c in db.getLINQ_TABLE().Root.Descendants()
                                                 where bool.Parse(c.Attribute("Aktiv").Value.ToString()) == true
                                                 select new user_table()
                                                 {
                                                     ID = (string)c.Attribute("Nr"),
                                                     Email = (string)c.Attribute("Email"),
                                                     time = (string)c.Attribute("Anlegedatum"),
                                                     username = (string)c.Attribute("Loginname"),
                                                       role = (string)c.Attribute("Benutzerrolle")
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
                    try
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
                    catch (Exception ex)
                    {

                    }




                   
                }

                if (action != null && action == "disbale" && HttpContext.Session.Get<bool>("admin"))
                {
                    try
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
                    catch (Exception ex)
                    {

                    }
                 
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
                                                     username = (string)c.Attribute("Loginname"),
                                                    role = (string)c.Attribute("Benutzerrolle")
                                                 };

            IEnumerable<user_table> result_login = from c in db.getLINQ_TABLE().Root.Descendants()
                                                   where bool.Parse(c.Attribute("Aktiv").Value.ToString()) == true
                                                   select new user_table()
                                                   {
                                                       ID = (string)c.Attribute("Nr"),
                                                       Email = (string)c.Attribute("Email"),
                                                       time = (string)c.Attribute("Anlegedatum"),
                                                       username = (string)c.Attribute("Loginname"),
                                                        role = (string)c.Attribute("Benutzerrolle")
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
