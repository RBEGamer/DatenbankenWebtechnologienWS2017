using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using MySql.Data.MySqlClient;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dbwt.Controllers
{
    public class LoginController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            HttpContextNew.set_con(HttpContext);

            if (String.IsNullOrEmpty(HttpContext.Session.Get<String>("user")))
            {
                ViewData["state"] = 0;
            }
            else
            {
                ViewData["state"] = 1;
                ViewData["user"] = HttpContext.Session.Get<String>("user");
            }

            
            MySqlConnection con1 = new MySqlConnection(DB_ACCESS.Instance.get_conn_string()); // lässt sich per using(){} noch besser handhaben
            try
            {
                con1.Open();

               MySqlCommand cmd = con1.CreateCommand();
                if (ViewData["user"] == null) { return View(); }
                cmd.CommandText = "SELECT * FROM `FE-Nutzer` LEFT JOIN `Mitarbeiter` ON `Mitarbeiter`.`FK_Fe-Nutzer` = `FE-Nutzer`.`Nr` LEFT JOIN `Student` ON `Student`.`FK_Fe-Nutzer` = `FE-Nutzer`.`Nr` WHERE `Loginname`='" + HttpContext.Session.Get<String>("user") + "' LIMIT 1";
               MySqlDataReader r = cmd.ExecuteReader();
                bool is_admin = false;
                while (r.Read())
                {
                    ViewData["Benutzerrolle"] = r["Benutzerrolle"];
                    if ((String)r["Benutzerrolle"] == "Student")
                    {
                        ViewData["Studiengang"] = r["Studiengang"];
                        ViewData["Matrikelnummer"] = r["Matrikelnummer"];
                    }
                    else if ((String)r["Benutzerrolle"] == "Mitarbeiter")
                    {
                        ViewData["MA-Nummer"] = r["MA-Nummer"];
                        ViewData["Büro"] = r["Büro"];
                    }


                    HttpContext.Session.Set<bool>("admin", (bool)r["admin"]);
                    HttpContext.Session.Set<bool>("Aktiv", (bool)r["Aktiv"]);
                    is_admin = (bool)r["admin"];
                    ViewData["admin"] = (bool)r["admin"];




                    if (!(bool)r["Aktiv"])
                    {
                        ViewData["state"] = 4;
                    }

                    ViewData["lastlogin"] = r["LetzterLogin"];

                }
                con1.Close();
                con1.Open();
                //UPDATE LAST LOGIN
                cmd.CommandText = "UPDATE `FE-Nutzer` SET `LetzterLogin`=CURRENT_TIMESTAMP WHERE `Loginname`='" + HttpContext.Session.Get<String>("user") + "'";
                r = cmd.ExecuteReader();

                con1.Close();
                con1.Open();
                //LOAD ADMIN TABLE
                if (is_admin)
                {

                 //   return RedirectToAction("Index", "AdminPanel", new { bigpicture = 1 });
                }





                con1.Close();



            }
            catch (Exception e)
            {
            }


            return View();
        }

    

        [HttpPost]
        public ActionResult Index(String username, String password, String action, String userid)
        {

            if(action != null && action == "verify" && HttpContext.Session.Get<bool>("admin"))
            {
                //MySqlConnection con = new MySqlConnection(DB_ACCESS.Instance.get_conn_string());
                //con.Open();
                //MySqlCommand cmd;
                //cmd = con.CreateCommand();
                //cmd.CommandText = "UPDATE `FE-Nutzer` SET `verified`='1' WHERE `Nr` = '"+ userid + "'";
                //MySqlDataReader r = cmd.ExecuteReader();
                //while(r.Read()){

                //}



                    //con.Close();
            }

            if (action != null && action == "disbale" && HttpContext.Session.Get<bool>("admin"))
            {
                //MySqlConnection con = new MySqlConnection(DB_ACCESS.Instance.get_conn_string());
                //con.Open();
                //MySqlCommand cmd;
                //cmd = con.CreateCommand();
                //cmd.CommandText = "UPDATE `FE-Nutzer` SET `verified`='0' WHERE `Nr` = '" + userid + "'";
                //MySqlDataReader r = cmd.ExecuteReader();
                //while (r.Read())
                //{

                //}



                //con.Close();
            }


            HttpContextNew.set_con(HttpContext);

            ViewData["action"] = action;
            ViewData["username"] = username;
            ViewData["password"] = password;
            ViewData["method"] = "post";

            ViewData["method"] = "post";
            
            if (!String.IsNullOrEmpty(HttpContext.Session.Get<String>("user")) && action != null && action == "logout")
            {
                HttpContext.Session.Set<String>("user", null);
                HttpContext.Session.Set<String>("role", "Gast");
                ViewData["state"] = 0;
            }

                if (String.IsNullOrEmpty(HttpContext.Session.Get<String>("user")))
            {

                if (username != null && password != null && action != null && action == "login")
                {
                    string hash_com = PasswordSecurity.PasswordStorage.CreateHash(password);
                    string hash_db = "";
                    string role = "";
                    MySqlConnection con1 = new MySqlConnection(DB_ACCESS.Instance.get_conn_string()); // lässt sich per using(){} noch besser handhaben
                    try
                    {
                        con1.Open();
                        MySqlCommand cmd;
                        cmd = con1.CreateCommand();
                        cmd.CommandText = "SELECT * FROM `FE-Nutzer` WHERE `Loginname`='"+username+"' LIMIT 1";
                        MySqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            hash_db += (String)r["Algorythmus"];
                            hash_db += ":";
                            hash_db += "64000";
                            hash_db += ":";
                            hash_db += ((uint)r["Strech"]).ToString();
                            hash_db += ":";
                            hash_db += (String)r["Salt"];
                            hash_db += ":";
                            hash_db += (String)r["Hash"];                
                            role = (String)r["Benutzerrolle"];
                        }
                        con1.Close();
                        if (PasswordSecurity.PasswordStorage.VerifyPassword(password, hash_db))
                        {
                            ViewData["state"] = 1;
                            ViewData["user"] = username;
                            HttpContext.Session.Set<String>("user", username);
                            HttpContext.Session.Set<String>("role", role);

                            HttpContext.Session.Set<bool>("admin", false);
                            HttpContext.Session.Set<bool>("Aktiv",false);
                            //-------------------------------------------------------------------------------------------------------------------------------------------------

                            bool is_admin = false;

                            try
                            {
                                con1.Open();
                                
                                cmd = con1.CreateCommand();
                                if (ViewData["user"] == null) { return View(); }
                                cmd.CommandText = "SELECT * FROM `FE-Nutzer` LEFT JOIN `Mitarbeiter` ON `Mitarbeiter`.`FK_Fe-Nutzer` = `FE-Nutzer`.`Nr` LEFT JOIN `Student` ON `Student`.`FK_Fe-Nutzer` = `FE-Nutzer`.`Nr` WHERE `Loginname`='" + username + "' LIMIT 1";
                                 r = cmd.ExecuteReader();
                                while (r.Read())
                                {
                                    ViewData["Benutzerrolle"] = r["Benutzerrolle"];
                                    if ((String)r["Benutzerrolle"] == "Student")
                                    {
                                        ViewData["Studiengang"] = r["Studiengang"];
                                        ViewData["Matrikelnummer"] = r["Matrikelnummer"];
                                    }
                                    else if ((String)r["Benutzerrolle"] == "Mitarbeiter")
                                    {
                                        ViewData["MA-Nummer"] = r["MA-Nummer"];
                                        ViewData["Büro"] = r["Büro"];
                                    }


                                    HttpContext.Session.Set<bool>("admin", (bool)r["admin"]);
                                    HttpContext.Session.Set<bool>("Aktiv", (bool)r["Aktiv"]);
                                    is_admin = (bool)r["admin"];
                                    ViewData["admin"] = (bool)r["admin"];





                                    if (!(bool)r["Aktiv"])
                                    {
                                        ViewData["state"] = 4;
                                    }

                                    ViewData["lastlogin"] = r["LetzterLogin"];
                           
                                }
                                con1.Close();
                                con1.Open();
                                //UPDATE LAST LOGIN
                                cmd.CommandText = "UPDATE `FE-Nutzer` SET `LetzterLogin`=CURRENT_TIMESTAMP WHERE `Loginname`='"+username+"'";
                                r = cmd.ExecuteReader();

                                con1.Close();
                                con1.Open();
                                //LOAD ADMIN TABLE
                                ViewData["table"] = "";
                                con1.Close();
                                if (is_admin)
                                { 
                                    return RedirectToAction("Index", "AdminPanel", new { bigpicture = 1 });
                                }





                              



                            }
                            catch (Exception e)
                            {
                            }
      

    //------------------------------------------------------------------------------------------------------------------------------------------------------
}
                        else
                        {
                            ViewData["username"] = username;
                            ViewData["state"] = 3;
                        }

                    }
                    catch (Exception e)
                    {
                        ViewData["state"] = 2;
                    }                  
            }
                else
                {
                    ViewData["state"] = 0;
                }
            }
            else
            {
                ViewData["state"] = 1;
                ViewData["user"] = HttpContext.Session.Get<String>("user");
            }
            
            return View();
        }


    }
}
